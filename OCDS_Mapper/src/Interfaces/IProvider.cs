using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;

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

        /*  propiedad Files => BlockingCollection
         *      Colección thread-safe que permite una extracción
         *      ordenada de los documentos provistos
         */
        AsyncCollection<string> Files { get; set; }

        /* propiedad AllProvider => Thread
         *      Thread utilizado para la provisión en el caso de utilizar PROVIDER_ALL
         */
        Thread AllProvider { get; set; }


        /* Funciones */

        /*  función asíncrona TakeFile() => string
         *      Función bloqueante que devuelve un documento provisto cuando éste está disponible
         *  @return : path al documento provisto
         */
        Task<string> TakeFile();

        /*  función SetParser(IParser) => void
         *      (utilizada solo en modo PROVIDE_ALL)
         *      Actualiza la instancia del Parser utilizada por el Provider
         *      Desbloquea el thread en background que utiliza dicho componente
         *  @param parser : Instancia del Parser para cada documento
         */
        void SetParser(IParser parser);

        /*  función RemoveFile(string) => void
         *      Función que elimina un archivo provisto ya mapeado
         *  param filePath : path del documento a eliminar
         */
        void RemoveFile(string filePath);
    }
}