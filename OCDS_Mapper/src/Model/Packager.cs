using System;
using System.Collections.Generic;
using System.IO;
using log4net.Core;
using Newtonsoft.Json.Linq;
using OCDS_Mapper.src.Interfaces;

namespace OCDS_Mapper.src.Model
{
    /* Clase del componente de empaquetado de datos */

    public class Packager : IPackager
    {
        /* Atributos privados */

        /*  atributo _Log => Action<object, string, Level>
         *      Puntero a la función de logging
         */
        private readonly Action<object, string, Level> _Log;


        /*  atributo _filename => string
         *      Nombre del fichero donde se escribirán los datos
         */
        private readonly string _filename;


        /*  atributo _idOccurrences => IDictionary<string, int>
         *      Diccionario accedido por el Mapper para obtener ids únicos
         */
        private readonly IDictionary<string, int> _idOccurrences;


        /*  atributo _packaged => JObject
         *      Objeto de empaquetado final
         */
        private readonly JObject _packaged;

        /*  atributo _uri => string
         *      Uri que describe el paquete que será publicado
         */
        private readonly string _uri;

        /*  atributo _uriOccurrences => IDictionary<string, int>
         *      Diccionario que controla si hay documentos con mismas URIs
         */
        private static readonly IDictionary<string, int> _uriOccurrences = new Dictionary<string, int>();



        /* Constructor */

        // @param Log : Puntero a la función de logging
        // @param timestamp : timestamp del documento de licitaciones
        public Packager(Action<object, string, Level> Log, string timestamp)
        {
            _Log = Log;

            // Inicializa las estructuras de la instancia
            _packaged = new JObject();
            _idOccurrences = new Dictionary<string, int>();

            string id = GetDate(DateTime.Parse(timestamp));

            if (!_uriOccurrences.ContainsKey(id))
            {
                _uriOccurrences[id] = 1;
            }
            else
            {
                id = $"{id}_{_uriOccurrences[id]++}";
            }

            _filename = $"document_{id}.json";
            _uri = $"{Program.Configuration["Upload_URL"]}{_filename}";

            // Inserta los metadatos del paquete
            InsertMetadata();
        }



        /* Implementación de IPackager */


        /*  función GetIdentifier(string) => string
         *      Obtiene el número de ocurrencias del identificador hasta el momento
         *      con el objetivo de evitar colisiones de ids no únicos
         *  @param entryID : identificador de la entrada
         *  @return : número de ocurrencias del identificador
         */
        public string GetIdentifier(string entryID)
        {
            if (_idOccurrences.ContainsKey(entryID))
            {
                _idOccurrences[entryID]++;
            }
            else
            {
                _idOccurrences[entryID] = 1;
            }
            return _idOccurrences[entryID].ToString();
        }


        /*  función Package(JObject) => void
         *      Introduce una entrada al paquete
         *  @param entry : objeto JSON que introducir
         */
        public void Package(JObject entry)
        {
            JArray releases = (JArray) _packaged["releases"];
            releases.Add(entry);
        }


        /*  función Publish(string) => void
         *      TODO
         *  @param dirPath : path en el que escribir el documento
         */
        public void Publish(string dirPath)
        {
            string filePath = $"{dirPath}/{_filename}";
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(_packaged.ToString());
            }
            _Log(this, $"Document {filePath} mapped and written", Level.Info);
        }



        /* Funciones auxiliares */

        /*  función InsertMetadata() => void
         *      Agrega los metadatos al paquete de entregas
         */
        private void InsertMetadata()
        {
            _packaged.Add("extensions", new JArray(Program.Configuration["Lot_extension_URL"]));
            _packaged.Add("publisher", new JObject(new JProperty("name", "Ontology Engineering Group"), new JProperty("uri", "https://oeg.fi.upm.es/")));
            _packaged.Add("publishedDate", GetDate(DateTime.Now));
            _packaged.Add("uri", _uri);
            _packaged.Add("version", "1.1");
            _packaged.Add("releases", new JArray());
        }


        /*  función estática GetDate(DateTime) => string
         *      Devuelve la fecha provista en el formato OCDS
         *  @param dt : fecha a procesar
         *  @return : fecha provista, formato YYYY-MM-DDTHH:MM:SSZ
         */
        private static string GetDate(DateTime dt)
        {
            return $@"{dt.Year}-{
                    dt.Month.ToString().PadLeft(2, '0')
                }-{
                    dt.Day.ToString().PadLeft(2, '0')
                }T{
                    dt.Hour.ToString().PadLeft(2, '0')
                }:{
                    dt.Minute.ToString().PadLeft(2, '0')
                }:{
                    dt.Second.ToString().PadLeft(2, '0')
                }Z";
        }
    }
}
