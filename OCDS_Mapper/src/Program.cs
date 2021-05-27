using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Config;
using NLog.Targets;
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
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();


        /*  atributo estático _providerCode => EProviderOperationCode
         *      Objeto de logging del componente de provisión
         */ 
        private static EProviderOperationCode _providerCode;


        /*  atributo estático _providerFilePath => string
         *      Ruta del documento de licitaciones si se quiere proveer de manera local
         */ 
        private static string _providerFilePath;


        /*  atributo estático _packagerDirPath => string
         *      Ruta donde se quieren escribir los documentos procseados
         */ 
        private static string _packagerDirPath;



        /* Punto de entrada asíncrono */

        public static async Task<int> Main(string[] args)
        {
            // Inicializa el logger
            InitLogger();

            // Realiza la comprobación de argumentos
            if (!args.Any())
            {
                DisplayHelp();
                return (int) EStatusCodes.DISPLAY_HELP;
            }

            if (args[0].Equals("-h") || args[0].Equals("--help"))
            {
                DisplayHelp();
                return (int) EStatusCodes.DISPLAY_HELP;
            }

            // Comprueba si los argumentos son correctos
            EStatusCodes statusCode;
            IEnumerator<string> argsEnumerator = args.AsEnumerable<string>().GetEnumerator();

            if ((statusCode = CheckArguments(argsEnumerator)) != EStatusCodes.OK)
            {
                return (int) statusCode;
            }

            // Lanza el método de cómputo del procesado de datos
            try
            {
                statusCode = await Compute();
            }
            catch
            {
                statusCode = EStatusCodes.FAILED;
            }
            
            return (int) statusCode;
        }



        /* Funciones auxiliares */

        /*  función estática asíncrona Compute() => EStatusCodes
         *      Realiza el procesado de documentos CODICE al esquema OCDS
         *      Pipeline completo desde la provisión de documentos hasta el empaquetado de los documentos mapeados
         *  @return : código de salida del programa
         */
        public async static Task<EStatusCodes> Compute()
        {
            // Inicializa el pipeline con el componente de provisión de datos
            IProvider provider = new Provider(Log, _providerCode, _providerFilePath);

            // Recupera el primer documento a mapear
            Document document = await provider.TakeFile();

            // Itera a través de los documentos hasta que no quede ninguno pendiente
            while (document != null)
            {
                // Lanza las instancias de parseo y empaquetado
                IParser parser = new Parser(Log, document);
                IPackager packager = new Packager(Log, parser.GetDocumentTimestamp(true));

                // En caso de estar en caso de proveer todos los documentos disponibles, actualiza el Parser en el Provider
                if (_providerCode == EProviderOperationCode.PROVIDE_ALL)
                {
                    _ = provider.SetParser(parser);
                }
                else if (_providerCode == EProviderOperationCode.PROVIDE_DAILY)
                {
                    bool sameDay = provider.SetParser(parser);
                    if (!sameDay)
                    {
                        Log(null, "Today's documents processed", ELogLevel.INFO);
                        return EStatusCodes.OK;
                    }
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
                    mapper.Commit(parser.GetDocumentTimestamp(false));
                    packager.Package(mapper.MappedEntry);
                }

                // Publica los datos empaquetados
                packager.Publish(_packagerDirPath);

                // Salvo que se esté mapeando un documento concreto, borra el documento sobre
                // el que se ha realizado el procesado (.atom)
                if (_providerCode != EProviderOperationCode.PROVIDE_SPECIFIC)
                {
                    provider.RemoveFile(document.Path);
                }
                
                // Se bloquea hasta recibir un nuevo documento del Provider
                document = await provider.TakeFile();
            }

            return EStatusCodes.OK;
        }

        
        /*  función estática InitLogger() => void
         *      Inicializa el servicio de logging de la aplicación
         */
        public static void InitLogger()
        {
            LoggingConfiguration config = new NLog.Config.LoggingConfiguration();

            // Establece las salidas de logging a consola y archivo
            Target logconsole = new NLog.Targets.ConsoleTarget("logconsole");
            Target logfile = new NLog.Targets.FileTarget("logfile") { FileName = "nlog.log" };
                        
            // Añade las reglas de configuración con los umbrales de los niveles de seguridad
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
                        
            // Aplica la configuración
            NLog.LogManager.Configuration = config;
        }


        /*  función estática Log(object, string, Level) => void
         *      Retransmite un mensaje a través del logging de la aplicación
         *  @param component : componente llamando a la función, o null si es éste propio programa
         *  @param message : mensaje a retransmitir
         *  @param level : nivel de severidad del mensaje
         */
        public static void Log(object component, string message, ELogLevel level)
        {
            if (component == null)
            {
                message = $"Program - {message}";
            }
            else if (component is Packager)
            {
                message = $"Packager - {message}";
            }
            else if (component is Parser)
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
            switch (level)
            {
                case ELogLevel.DEBUG:
                    _logger.Debug(message);
                    break;
                case ELogLevel.INFO:
                    _logger.Info(message);
                    break;
                case ELogLevel.WARN:
                    _logger.Warn(message);
                    break;
                case ELogLevel.ERROR:
                    _logger.Error(message);
                    break;
                case ELogLevel.FATAL:
                    _logger.Fatal(message);
                    break;
                default:
                    Console.WriteLine("Directiva " + level.ToString() + " no reconocida");
                    break;
            }
        }


        /*  función estática DisplayHelp() => void
         *      Escribe el mensaje de ayuda de la aplicación
         */
        public static void DisplayHelp()
        {
            Log(null, Regex.Replace(@"
                        |Usage: <dir> OperationCode [file]
                        |
                        |<dir> : path where processed documents will be stored
                        |
                        |OperationCode values:
                        |--all : (will attempt to map every available file starting from latest),
                        |--latest : (will map latest available file),
                        |--specific : <file> (will map provided <file>, local or remote)",
                        @"[ \t]+\|", 
                    string.Empty),
                ELogLevel.INFO);
        }
    
    
    
        /* Funciones auxiliares */

        /*  función estática CheckArguments(string) => bool
         *      Comprueba si el primer argumento con el directorio de salido es correcto,
         *      y si el segundo argumento (modo de operación del Provider) es correcto.
         *      Comprueba en el modo --specific si se provee el tercer argumento necesario.
         *      Actualiza los campos _providerCode, _providerFilePath (solo --specific) y _packagerDirPath
         *      @return OK si es correcto, o el código de error en otro caso
         */
        private static EStatusCodes CheckArguments(IEnumerator<string> args)
        {
            args.MoveNext();

            if (!Directory.Exists(args.Current))
            {
                Log(null, "Please provide a valid directory to store processed documents", ELogLevel.WARN);
                return EStatusCodes.INVALID_DIR;
            }

            _packagerDirPath = args.Current;

            if (!args.MoveNext())
            {
                DisplayHelp();
                return EStatusCodes.DISPLAY_HELP;
            }

            if (args.Current.Equals("--all"))
            {
                _providerCode = EProviderOperationCode.PROVIDE_ALL;
            }
            else if (args.Current.Equals("--daily"))
            {
                _providerCode = EProviderOperationCode.PROVIDE_DAILY;
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
                    Log(null, "Please provide a file to be mapped", ELogLevel.WARN);
                    return EStatusCodes.PATH_UNPROVIDED;
                }

                _providerCode = EProviderOperationCode.PROVIDE_SPECIFIC;
                _providerFilePath = args.Current;
            }
            else
            {
                // En caso de no proveer un argumento correcto, muestra el mensaje de ayuda
                DisplayHelp();
                return EStatusCodes.WRONG_CODE;
            }

            return EStatusCodes.OK;
        }
    }
}
