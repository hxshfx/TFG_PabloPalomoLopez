using System.Collections.Generic;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace OCDS_Mapper.src.Interfaces
{
    /* Interfaz del componente de mapeo */

    public interface IMapper
    {
        /* Propiedades */

        /*  propiedad _mappedEntry => JObject
         *      Representa el elemento JSON con la información mapeada
         */
        JObject MappedEntry { get; set; }


        /* Funciones */


        /*  función MapElement(IEnumerable<string>, XElement[]) => void
         *      Realiza el mapeo de un elemento
         *  @param pathMap : ruta del elemento cuando sea mapeado
         *  @param parsedElement : elemento(s) a mapear
         *      @ej : MapElement( [ "tender", "title" ], "ABCD" )
         *              => _mappedEntry = { "tender": { "title": "ABCD" } }
         *      @ej : MapElement( [ "tag" ], "PRE" )
         *              => _mappedEntry = { "tag": [ "planning" ] }
         */
        void MapElement(IEnumerable<string> pathMap, XElement[] parsedElement);

        /*  función Commit() => void
         *      Introduce los cambios al JSON que no se pueden introducir
         *      mediante los mapeos unitarios de elementos
         */
        void Commit();
    }
}
