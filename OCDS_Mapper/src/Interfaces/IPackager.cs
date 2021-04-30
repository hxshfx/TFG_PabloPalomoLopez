using Newtonsoft.Json.Linq;
using OCDS_Mapper.src.Utils;

namespace OCDS_Mapper.src.Interfaces
{
    /* Interfaz del componente de empaquetado de datos  */

    public interface IPackager
    {
        /* Funciones */

        /*  función Package(JObject) => void
         *      Introduce una entrada al paquete
         *  @param entry : objeto JSON que introducir
         */
        void Package(JObject entry);

        /*  función GetIdentifier(string) => string
         *      Obtiene el número de ocurrencias del identificador hasta el momento
         *      con el objetivo de evitar colisiones de ids no únicos
         *  @param entryID : identificador de la entrada
         *  @return : número de ocurrencias del identificador
         */
        string GetIdentifier(string entryID);

        /*  TODO
         *
         *
         */
        void Publish(EPackagerOperationCode code, string filePath = null);
    }
}
