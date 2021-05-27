using System.Threading.Tasks;
using Nito.AsyncEx;
using OCDS_Mapper.src.Model;

namespace OCDS_Mapper.src.Interfaces
{
    /* Interfaz del componente de provisión de datos */

    public interface IProvider
    {
        /* Propiedades */

        /*  propiedad Parser => IParser
         *      Instancia del Parser para obtener los enlaces de los documentos
         */
        IParser Parser { get; set; }

        /*  propiedad Files => AsyncCollection<Document>
         *      Colección thread-safe que permite una extracción
         *      ordenada de los documentos provistos
         */
        AsyncCollection<Document> Files { get; set; }


        /* Funciones */

        /*  función asíncrona TakeFile() => Document
         *      Función bloqueante que devuelve un documento provisto cuando éste está disponible
         *  @return : stream de texto del documento provisto, o null si no quedan más documentos que proveer
         */
        Task<Document> TakeFile();

        /*  función SetParser(IParser) => bool
         *      (utilizada solo en modo PROVIDE_ALL y PROVIDE_DAILY)
         *      Desbloquea el thread en background que integra al Provider y al Parser
         *  @param parser : Instancia del Parser para cada documento
         *  @return : true si el documento del Parser es día anterior al actual, false e.o.c
         */
        bool SetParser(IParser parser);

        /*  función RemoveFile(string) => void
         *      Función que elimina un archivo provisto ya mapeado
         *  @param filePath : path del documento a eliminar
         */
        void RemoveFile(string filePath);
    }
}
