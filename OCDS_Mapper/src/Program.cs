using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using log4net;
using log4net.Config;
using log4net.Core;
using Microsoft.Extensions.Configuration;
using OCDS_Mapper.src.Model;
using OCDS_Mapper.src.Interfaces;
using OCDS_Mapper.src.Utils;

namespace OCDS_Mapper
{
    /* Punto de entrada de la aplicación */

    public static class Program
    {
        /* Propiedades */

        /*  propiedad estática Configuration => IConfiguration
         *      Permite el acceso a los componentes del fichero de configuración
         */
        public static IConfiguration Configuration
        {
            get { return _config; }
            set { _config = value; }
        }



        /* Atributos privados */

        /*  atributo estático _config => IConfiguration
         *      Oculta localmente la carga del fichero de configuración
         */
        private static IConfiguration _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


        /*  atributo estático _logger => ILog
         *      Objeto de logging de la aplicación
         */        
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /*  atributo estático _packagerCode => EPackagerOperationCode
        *      Modo de operación del componente de empaquetado
        */ 
        private static EPackagerOperationCode _packagerCode;


        /*  atributo estático _providerCode => EProviderOperationCode
         *      Objeto de logging del componente de provisión
         */ 
        private static EProviderOperationCode _providerCode;


        /*  atributo estático _providerFilePath => string
         *      Ruta del documento de licitaciones si se quiere proveer de manera local
         */ 
        private static string _providerFilePath;


        /*  atributo estático _packagerFilePath => string
         *      Ruta donde se quiere escribir el documento mapeado de manera local
         */ 
        private static string _packagerFilePath;



        /* Punto de entrada asíncrono */

        public static async Task Main(string[] args)
        {
            // Inicializa el logger
            InitLogger();

            // Realiza la comprobación de argumentos
            if (!args.Any())
            {
                Log(null, "Please input the operation codes:", Level.Warn);
                DisplayHelp();
                Environment.Exit((int) EStatusCodes.DISPLAY_HELP);
            }

            if (args[0].Equals("-h") || args[0].Equals("--help"))
            {
                DisplayHelp();
                Environment.Exit((int) EStatusCodes.DISPLAY_HELP);
            }

            EStatusCodes statusCode;
            IEnumerator<string> argsEnumerator = args.AsEnumerable<string>().GetEnumerator();

            if ((statusCode = CheckProviderCode(argsEnumerator)) != 0)
            {
                Environment.Exit((int) statusCode);
            }

            if ((statusCode = CheckPackagerCode(argsEnumerator)) != 0)
            {
                Environment.Exit((int) statusCode);
            }

            // Lanza el método de cómputo del procesado de datos
            await Compute();
            Environment.Exit((int) EStatusCodes.OK);
        }



        /* Funciones auxiliares */

        /*  función estática asíncrona Compute(IProvider, EProviderOperationCode) => void
         *      Realiza el procesado de documentos CODICE al esquema OCDS
         *      Pipeline completo desde la provisión de documentos hasta el empaquetado de los documentos mapeados
         *      TODO
         */
        public async static Task Compute()
        {
            // Inicializa el pipeline con el componente de provisión de datos
            IProvider provider = new Provider(Log, _providerCode, _providerFilePath);

            // Recupera el primer documento a mapear
            Document document = await provider.TakeFile();

            // Itera a través de los documentos hasta que no quede ninguno pendiente
            for (int count = 1; document != null; count++)
            {
                // Lanza las instancias de parseo y empaquetado
                IParser parser = new Parser(Log, document);
                IPackager packager = new Packager(Log, parser.GetDocumentTimestamp());

                // En caso de estar en caso de proveer todos los documentos disponibles, actualiza el Parser en el Provider
                if (_providerCode == EProviderOperationCode.PROVIDE_ALL)
                {
                    provider.SetParser(parser);
                }

                // Obtiene los espacios de nombres y las reglas de mapeo
                IDictionary<string, XNamespace> namespaces = parser.GetNamespaces();
                IDictionary<IEnumerable<XName>, IEnumerable<string>> mappingRules = Mappings.GetMappingRules(namespaces);

                // Obtiene el conjunto de entradas del documento
                IEnumerable<XElement> entrySet = parser.GetEntrySet(namespaces);

                foreach (XElement entry in entrySet)
                {
                    // Por cada entrada, crea un nuevo objeto de mapeo y establece su elemento raíz
                    IMapper mapper = new Mapper(Log, packager);
                    parser.SetEntryRootElement(entry);

                    // Itera por cada regla de mapeo
                    foreach (KeyValuePair<IEnumerable<XName>, IEnumerable<string>> map in mappingRules)
                    {
                        // Obtiene los elementos a mapear a través del Parser
                        XElement[] elem = parser.GetElements(map.Key);
                        if (elem != null)
                        {
                            // Mapea los elementos
                            mapper.MapElement(map.Value, elem);
                        }
                    }

                    // Introduce los cambios finales necesarios
                    mapper.Commit();
                    packager.Package(mapper.MappedEntry);
                }

                if (_providerCode == EProviderOperationCode.PROVIDE_ALL && _packagerCode == EPackagerOperationCode.PACKAGE_LOCAL)
                {
                    packager.Publish(_packagerCode, _packagerFilePath.Insert(_packagerFilePath.IndexOf(".json"), count.ToString()));
                }
                else
                {
                    packager.Publish(_packagerCode, _packagerFilePath);
                }

                provider.RemoveFile(document.Path);
                
                document = await provider.TakeFile();
            }
        }

        
        /*  función estática InitLogger() => void
         *      Inicializa el servicio de logging de la aplicación
         */
        public static void InitLogger()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }


        /*  función estática Log(object, string, Level) => void
         *      Retransmite un mensaje a través del logging de la aplicación
         *  @param component : componente llamando a la función, o null si es éste propio programa
         *  @param message : mensaje a retransmitir
         *  @param level : nivel de severidad del mensaje
         */
        public static void Log(object component, string message, Level level)
        {
            if (component is Parser)
            {
                message = $"Parser - {message}";
            }
            else if (component is Provider)
            {
                message = $"Provider - {message}";
            }
            else if (component is Mapper)
            {
                message = $"Mapper - {message}";
            }
            switch (level.Name)
            {
                case "DEBUG":
                    _logger.Debug(message);
                    break;
                case "INFO":
                    _logger.Info(message);
                    break;
                case "WARN":
                    _logger.Warn(message);
                    break;
                case "ERROR":
                    _logger.Error(message);
                    break;
                case "FATAL":
                    _logger.Fatal(message);
                    break;
                default:
                    Console.WriteLine("Directiva " + level.Name + " no reconocida");
                    break;
            }
        }


        /*  función estática DisplayHelp() => void
         *      Escribe el mensaje de ayuda de la aplicación
         */
        public static void DisplayHelp()
        {
            Log(null, Regex.Replace(@"
                        |Usage: ProviderCode [file] PackagerCode [file]
                        |ProviderCode values:
                        |--all (will attempt to map every available file starting from latest),
                        |--latest (will map latest available file),
                        |--specific <file> (will map provided <file>, local or remote),
                        |PackagerCode values:
                        |--local <file> (will publish mapped data into <file> path),
                        |--remote (will publish data remotely)",
                        @"[ \t]+\|", 
                    string.Empty),
                Level.Info);
        }
    
    
    
        /* Funciones auxiliares */

        /*  función estática CheckProviderCode(string) => bool
         *      Comprueba si el primer argumento (modo de operación del Provider) es correcto
         *      @return 0 si es correcto, o el código de error en otro caso
         */
        private static EStatusCodes CheckProviderCode(IEnumerator<string> args)
        {
            args.MoveNext();

            if (args.Current.Equals("--all"))
            {
                _providerCode = EProviderOperationCode.PROVIDE_ALL;
            }
            else if (args.Current.Equals("--latest"))
            {
                _providerCode = EProviderOperationCode.PROVIDE_LATEST;
            }
            else if (args.Current.Equals("--specific"))
            {
                // En caso de querer mapear un documento específico, comprueba si se ha pasado como argumento
                if (!args.MoveNext())
                {
                    Log(null, "Please provide a file to be mapped", Level.Warn);
                    return EStatusCodes.PATH_UNPROVIDED;
                }

                _providerCode = EProviderOperationCode.PROVIDE_SPECIFIC;
                _providerFilePath = args.Current;
            }
            else
            {
                // En caso de no proveer un argumento correcto, muestra el mensaje de ayuda
                DisplayHelp();
                return EStatusCodes.WRONG_ARGUMENTS;
            }
            return EStatusCodes.OK;
        }


        /*  función estática CheckPackagerCode(string) => bool
         *      Comprueba si el segundo argumento (modo de operación del Packager) es correcto
         *      @return 0 si es correcto, o el código de error en otro caso
         */
        private static EStatusCodes CheckPackagerCode(IEnumerator<string> args)
        {
            if (args.MoveNext())
            {
                if (args.Current.Equals("--local"))
                {

                    // En caso de querer publicar a una ruta local, comprueba si se ha pasado como argumento
                    if (!args.MoveNext())
                    {
                        Log(null, "Please provide a path to the mapped data", Level.Warn);
                        return EStatusCodes.PATH_UNPROVIDED;
                    }

                    if (!args.Current.EndsWith(".json"))
                    {
                        Log(null, "Please provide a path with .json extension", Level.Warn);
                        return EStatusCodes.WRONG_ARGUMENTS;
                    }

                    _packagerCode = EPackagerOperationCode.PACKAGE_LOCAL;
                    _packagerFilePath = args.Current;
                }
                else if (args.Current.Equals("--remote"))
                {
                    _packagerCode = EPackagerOperationCode.PACKAGE_REMOTE;
                }
                else
                {
                    // En caso de no proveer un argumento correcto, muestra el mensaje de ayuda
                    DisplayHelp();
                    return EStatusCodes.WRONG_ARGUMENTS;
                }
                return EStatusCodes.OK;
            }
            else
            {
                // En caso de no proveer un argumento correcto, muestra el mensaje de ayuda
                DisplayHelp();
                return EStatusCodes.WRONG_ARGUMENTS;
            }
        }
    }
}
