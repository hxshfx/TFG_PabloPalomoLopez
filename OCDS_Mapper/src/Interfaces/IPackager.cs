using Newtonsoft.Json.Linq;
using OCDS_Mapper.src.Utils;

namespace OCDS_Mapper.src.Interfaces
{
    /* Interfaz del componente de empaquetado de datos  */

    public interface IPackager
    {
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

        /*  TODO
         *
         *
         */
        void Publish(string dirPath);
    }
}
