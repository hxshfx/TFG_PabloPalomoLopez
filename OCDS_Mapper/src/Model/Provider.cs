using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;
using OCDS_Mapper.src.Exceptions;
using OCDS_Mapper.src.Interfaces;
using OCDS_Mapper.src.Utils;

namespace OCDS_Mapper.src.Model
{
    /* Clase del componente de provisión de datos */

    public class Provider : IProvider
    {
        /* Propiedades */

        /*  propiedad Parser => IParser
         *      Instancia del Parser para obtener los enlaces de los documentos
         */
        public IParser Parser { get; set; } 


        /*  propiedad Files => AsyncCollection<Document>
         *      Colección thread-safe que permite una extracción
         *      ordenada de los documentos provistos
         */
        public AsyncCollection<Document> Files { get; set; }



        /* Atributos */

        /*  atributo _Log => Action<object, string, ELogLevel>
         *      Puntero a la función de logging
         */
        private readonly Action<object, string, ELogLevel> _Log;


        /*  atributo _fileName => string
         *      Nombre del fichero siendo provisto, descargado o local
         */
        private string _fileName;


        /*  atributo _mre => ManualResetEvent
         *      Mecanismo de sincronización para la interacción con el Parser
         */
        private ManualResetEvent _mre;


        /*  atributo _webClient => WebClient
         *      Objeto utilizado para la descarga de documentos
         */
        private readonly WebClient _webClient;


        /*  constante _LATEST_PATH => string
         *      Path del documento publicado más reciente
         */
        private readonly static string _LATEST_PATH = Program.Configuration["LatestDocument_URL"];


        /*  constante _OUTPUT_PATH => sring
         *      Path de los documentos provistos en modo PROVIDE_LATEST y PROVIDE_SPECIFIC
         *      y path base de los documentos provistos en modo PROVIDE_ALL (document{1:N}.atom)
         */
        private readonly static string _OUTPUT_PATH = "./tmp/document.atom";


        
        
        /* Constructor */

        // @param Log : Puntero a la función de logging
        // @param code : descriptor del modo de operación
        // @param filePath : ruta al documento para proveer, local o remoto
        // @throws FileNotFoundException : si el parámetro filePath no se corresponde ni a un archivo local ni a una URI correcta
        // @throws InvalidOperationCodeException : si el parámetro code no es vaĺido
        public Provider(Action<object, string, ELogLevel> Log, EProviderOperationCode code, string filePath)
        {
            _Log = Log;

            // Inicializa la estructura de ficheros y crea el directirio temporal si no existe
            Files = new AsyncCollection<Document>();
            if (!Directory.Exists("./tmp"))
            {
                Directory.CreateDirectory("./tmp");
            }

            if (code == EProviderOperationCode.PROVIDE_SPECIFIC)
            {
                // Si el fichero provisto existe en local, lo añade a la estructura y termina
                if (File.Exists(filePath))
                {
                    _fileName = filePath;
                    Files.Add(new Document(filePath));

                    _Log(this, $"Retrieving local file {filePath}", ELogLevel.INFO);

                    Files.CompleteAdding();
                }
                // Si el fichero provisto no existe en local, se asevera si está formado como una URI correcta
                else if (Uri.TryCreate(filePath, UriKind.RelativeOrAbsolute, out Uri uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
                {
                    _fileName = filePath;

                    // Crea el cliente Web e inicia la descarga
                    _webClient = new WebClient();

                    _webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompleted);
                    _webClient.DownloadFileAsync(uri, _OUTPUT_PATH);

                    _Log(this, $"Downloading {_fileName} file", ELogLevel.INFO);
                }
                else
                {
                    _Log(this, $"Path {filePath} doesn't match with any local file or correct URI", ELogLevel.ERROR);
                    throw new FileNotFoundException();
                }
            }
            else
            {
                // Inicializa el WebClient 
                _webClient = new WebClient();
                _webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompleted);

                if (code == EProviderOperationCode.PROVIDE_ALL || code == EProviderOperationCode.PROVIDE_DAILY)
                {
                    // Inicializa el mecanismo de sincronización
                    _mre = new ManualResetEvent(false);
                
                    // Lanza en background la tarea que irá proveyendo documentos
                    Thread downloader = new Thread(BackgroundDownloader);
                    downloader.IsBackground = true;
                    downloader.Start();

                    _Log(this, "Provider background downloader thread launched", ELogLevel.INFO);
                }
                else if (code == EProviderOperationCode.PROVIDE_LATEST)
                {
                    // Descarga el último fichero, siempre descrito por esta ruta
                    _fileName = _LATEST_PATH;
                    _webClient.DownloadFileAsync(new Uri(_fileName), _OUTPUT_PATH);

                    _Log(this, $"Downloading {_fileName} file", ELogLevel.INFO);
                }
                else
                {
                    _Log(this, "Please provide a valid operational code", ELogLevel.ERROR);
                    throw new InvalidOperationCodeException();
                }
            }
        }



        /* Implementación de IProvider */

        /*  función asíncrona TakeFile() => Document
         *      Función bloqueante que devuelve un documento provisto cuando éste está disponible
         *  @return : stream de texto del documento provisto, o null si no quedan más documentos que proveer
         */
        public async Task<Document> TakeFile()
        {
            // Devuelve el documento o null si no quedan más
            try
            {
                return await Files.TakeAsync();
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }


        /*  función SetParser(IParser) => bool
         *      (utilizada solo en modo PROVIDE_ALL y PROVIDE_DAILY)
         *      Desbloquea el thread en background que integra al Provider y al Parser
         *  @param parser : Instancia del Parser para cada documento
         *  @return : true si el documento del Parser es día anterior al actual, false e.o.c
         */
        public bool SetParser(IParser parser)
        {
            // Actualiza el parser y desbloquea el thread
            Parser = parser;
            _mre.Set();
            _Log(this, "Provider background thread unlocking", ELogLevel.DEBUG);

            // Devuelve si el documento actual es del mismo día que el anterior al día actual
            // para que el programa principal sepa cuando parar en el modo de PROVIDE_DAILY
            DateTime documentDate = DateTime.Parse(Parser.GetDocumentTimestamp(true));
            return DateTime.Today.AddDays(-1).Day == documentDate.Day;
        }


        /*  función RemoveFile(string) => void
         *      Función que elimina un archivo provisto ya mapeado
         *  @param filePath : path del documento a eliminar
         */
        public void RemoveFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                _Log(this, $"Completed file {filePath} deleted", ELogLevel.INFO);
            }
            else
            {
                _Log(this, $"Tried to delete unexisting file {filePath}", ELogLevel.ERROR);
            }
        }


        
        /* Funciones auxiliares */

        /*  función DownloadCompleted(object, _) => void
         *      Función que captura la finalización de una descarga
         *      Añade el elemento a la estructura bloqueante
         *  @param sender : Instancia del WebClient (accesible también a través del atributo privado)
         */
        private void DownloadCompleted(object sender, AsyncCompletedEventArgs _)
        {
            // casos PROVIDE_LATEST y PROVIDE_SPECIFIC
            if (_webClient.QueryString.Count == 0)
            {
                // Añade el elemento y libera los recursos puesto que no habrá más descargas
                Files.Add(new Document(_OUTPUT_PATH));

                Files.CompleteAdding();
                _webClient.Dispose();
            }
            // caso PROVIDE_ALL
            else
            {
                // Añade el elemento cuyo path está descrito por el QueryString
                Files.Add(new Document(_webClient.QueryString["file"]));
                _webClient.QueryString.Remove("file");
            }
            _Log(this, $"Completed download of file {_fileName}", ELogLevel.INFO);
        }


        /*  función BackgroundDownloader() => void
         *      Función para el thread del código de operacion PROVIDE_ALL y PROVIDE_DAILY
         *      Va proveyendo documentos enlazados utilizando el componente de Parseo
         */
        private void BackgroundDownloader()
        {
            // Contador para los documentos provistos (forma ./tmp/document{1:N}.atom)
            int outputCount = 0;
            string outputFileName = _OUTPUT_PATH;
            outputFileName = outputFileName.Insert(outputFileName.Length - ".atom".Length, outputCount.ToString());

            // Inicializa el proceso a través del documento más reciente
            _fileName = _LATEST_PATH;
            Uri uri = new Uri(_fileName);

            do
            {
                // Actualiza los paths de los ficheros
                outputFileName = outputFileName.Replace(outputCount.ToString(), (++outputCount).ToString());
                _fileName = uri.ToString();

                // Lleva al objeto webClient la información del fichero de output y realiza la descarga
                _webClient.QueryString.Add("file", outputFileName);
                _webClient.DownloadFileAsync(uri, outputFileName);

                _Log(this, $"Downloading {_fileName} file", ELogLevel.INFO);

                // Se bloquea hasta recibir una nueva instancia de Parser (para obtener su link:next)
                _mre.WaitOne();

                // Una vez desbloqueado, obtiene la URI describiendo el nuevo documento a proveer y resetea el mecanismo de sincronización
                uri = Parser.GetNextFile();
                _mre.Reset();
            }
            while (uri != null);

            // Una vez finalizado el cómputo, libera los recursos
            Files.CompleteAdding();
            _mre = null;
            _webClient.Dispose();
        }
    }
}
