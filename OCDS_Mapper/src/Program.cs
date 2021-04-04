using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using log4net;
using log4net.Config;
using log4net.Core;
using OCDS_Mapper.src.Model;
using OCDS_Mapper.src.Interfaces;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace OCDS_Mapper
{
    public static class Program
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main(string[] args)
        {
            InitLogger();

            if (!args.Any())
            {
                Log(null, "Please input a operation code, use -h for help", Level.Warn);
                return;
            }

            if (args[0].Equals("-h"))
            {
                DisplayHelp();
                return;
            }

            Provider provider;
            Provider.EProviderOperationCode code;

            if (args[0].Equals("--all"))
            {
                code = Provider.EProviderOperationCode.PROVIDE_ALL;
                provider = new Provider(Log, code);
            }
            else if (args[0].Equals("--latest"))
            {
                code = Provider.EProviderOperationCode.PROVIDE_LATEST;
                provider = new Provider(Log, code);
            }
            else if (args[0].Equals("--specific"))
            {
                if (args[1] == null)
                {
                    Log(null, "Please provide a file to be mapped", Level.Warn);
                    return;
                }
                code = Provider.EProviderOperationCode.PROVIDE_SPECIFIC;
                provider = new Provider(Log, code, args[1]);
            }
            else
            {
                DisplayHelp();
                return;
            }

            Compute(provider, code);
        }

        private static void Compute(IProvider provider, Provider.EProviderOperationCode code)
        {
            for (int count = 1; provider.HasNext(); count++)
            {
                IParser parser = new Parser(Log, provider.TakeFile());

                if (code == Provider.EProviderOperationCode.PROVIDE_ALL)
                {
                    provider.SetParser(parser);
                }

                IList<JObject> mappedEntries = new List<JObject>();

                IDictionary<string, XNamespace> namespaces = parser.GetNamespaces();
                IDictionary<IEnumerable<XName>, IEnumerable<string>> mappings = Mappings.GetMappings(namespaces);

                IEnumerable<XElement> entrySet = parser.GetEntrySet(namespaces);
                foreach (XElement entry in entrySet)
                {
                    IMapper mapper = new Mapper(Log);
                    parser.SetEntryRootElement(entry);

                    foreach (KeyValuePair<IEnumerable<XName>, IEnumerable<string>> map in mappings)
                    {
                        XElement[] elem = parser.GetElement(map.Key);
                        if (elem != null)
                        {
                            mapper.MapElement(map.Value, elem);
                        }
                    }
                    mappedEntries.Add(mapper.MappedEntry);
                }
                Packager.Package(mappedEntries, count);
                Log(null, "Document mapped and written", Level.Info);
            }
        }

        private static void DisplayHelp()
        {
            Log(null, "Usage: --all (will attempt to map every available file)", Level.Info);
            Log(null, "Usage: --latest (will map latest available file)", Level.Info);
            Log(null, "Usage: --specific <file> (will map provided file)", Level.Info);
        }

        public static void InitLogger()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

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
                    logger.Debug(message);
                    break;
                case "INFO":
                    logger.Info(message);
                    break;
                case "WARN":
                    logger.Warn(message);
                    break;
                case "ERROR":
                    logger.Error(message);
                    break;
                case "FATAL":
                    logger.Fatal(message);
                    break;
                default:
                    Console.WriteLine("Directiva " + level.Name + " no reconocida");
                    break;
            }
        }
    }
}
