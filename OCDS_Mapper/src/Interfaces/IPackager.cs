using Newtonsoft.Json.Linq;

namespace OCDS_Mapper.src.Interfaces
{
    /* Interfaz del componente de empaquetado de datos  */

    public interface IPackager
    {
        /* Propiedades */

        /*  propiedad Packaged => JObject
         *      Objeto de empaquetado final
         */
        JObject Packaged { get; set; }


        /* Funciones */

        /*  función GetIdentifier(string) => string
         *      Obtiene el número de ocurrencias del identificador hasta el momento
         *      con el objetivo de evitar colisiones de ids no únicos
         *  @param entryID : identificador de la entrada
         *  @return : número de ocurrencias del identificador
         */
        string GetIdentifier(string entryID);

        /*  función Package(JObject) => void
         *      Introduce una entrada al paquete
         *  @param entry : objeto JSON que introducir
         */
        void Package(JObject entry);

        /*  función Publish(string) => void
         *      Publica los datos escribiéndolos en el directorio de salida
         *  @param dirPath : path del directorio de salida
         */
        void Publish(string dirPath);
    }
}
