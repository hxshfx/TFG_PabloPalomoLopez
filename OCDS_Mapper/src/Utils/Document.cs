using System.IO;

namespace OCDS_Mapper.src.Model
{
    /* Clase que representa un documento de licitaciones */
    public class Document
    {
        /* Propiedades */

        /*  propiedad Path => string
         *      Ruta del documento de licitaciones
         */
        public string Path { get; }


        /*  propiedad Stream => TextReader
         *      Stream de texto con el contenido del documento
         */
        public TextReader Stream { get; }



        /* Constructor */
        // @param path : ruta al documento
        public Document(string path)
        {
            Path = path;
            Stream = File.OpenText(path);
        }
    }
}
