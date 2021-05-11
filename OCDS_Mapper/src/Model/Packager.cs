using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using log4net.Core;
using Newtonsoft.Json.Linq;
using OCDS_Mapper.src.Exceptions;
using OCDS_Mapper.src.Interfaces;
using OCDS_Mapper.src.Utils;

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



        /* Constructor */

        // @param Log : Puntero a la función de logging
        // @param timestamp : timestamp del documento de licitaciones a partir del cual se construirá el id mediante hash
        public Packager(Action<object, string, Level> Log, string timestamp)
        {
            _Log = Log;

            // Inicializa las estructuras de la instancia
            _idOccurrences = new Dictionary<string,int>();
            _packaged = new JObject();

            //DateTime dt = DateTime.Parse(timestamp);

            // Forma la URI del objeto a publicar mediante el hash del timestamp TODO ?
            byte[] hash = SHA1.Create().ComputeHash(ASCIIEncoding.Default.GetBytes(timestamp));
            StringBuilder id = new StringBuilder();

            foreach(byte b in hash)
            {
                id.AppendFormat("{0:x2}", b);
            }

            _uri = $"{Program.Configuration["Upload_URL"]}/document_{id.ToString()}.json";

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


        /*  TODO
         *
         *  @param opcional filePath : path en el que escribir el documento
         *  @throws InvalidOperationCodeException : si el parámetro code no es PACKAGE_LOCAL o PACKAGE_REMOTE
         */
        public void Publish(EPackagerOperationCode code, string filePath = null)
        {
            if (code == EPackagerOperationCode.PACKAGE_LOCAL)
            {
                WriteToFile(filePath);
            }
            else if (code == EPackagerOperationCode.PACKAGE_REMOTE)
            {
                UploadData();
            }
            else
            {
                _Log(this, "This method only takes PACKAGE_LOCAL or PACKAGE_REMOTE codes", Level.Error);
                throw new InvalidOperationCodeException();
            }
        }


        
        /* Funciones auxiliares */

        /*  función InsertMetadata() => void
         *      Agrega los metadatos al paquete de entregas
         */
        private void InsertMetadata()
        {
            _packaged.Add("extensions", new JArray(Program.Configuration["Lot_extension_URL"]));
            _packaged.Add("publisher", new JObject(new JProperty("name", "Ontology Engineering Group"), new JProperty("uri", "https://oeg.fi.upm.es/")));
            _packaged.Add("publishedDate", GetDate());
            _packaged.Add("uri", _uri);
            _packaged.Add("version", "1.1");
            _packaged.Add("releases", new JArray());
        }


        /*  función UploadData() => void
         *      TODO
         */
        private void UploadData()
        {
            _Log(this, "TODO", Level.Info);
        }


        /*  función WriteToFile(string) => void
         *      Escribe el documento empaquetado de manera local
         *  @param opcional filePath : path en el que escribir el documento
         */
        private void WriteToFile(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(_packaged.ToString());
            }
            _Log(this, $"Document {filePath} mapped and written", Level.Info);
        }


        /*  función estática GetDate() => string
         *      Devuelve la fecha actual en el formato OCDS
         *  @return : fecha actual (formato YYYY-MM-DDTHH:MM:SSZ)
         */
        private static string GetDate()
        {
            DateTime now = DateTime.Now;
            return $@"{now.Year}-{
                    now.Month.ToString().PadLeft(2, '0')
                }-{
                    now.Day.ToString().PadLeft(2, '0')
                }T{
                    now.Hour.ToString().PadLeft(2, '0')
                }:{
                    now.Minute.ToString().PadLeft(2, '0')
                }:{
                    now.Second.ToString().PadLeft(2, '0')
                }Z";
        }
    }
}
