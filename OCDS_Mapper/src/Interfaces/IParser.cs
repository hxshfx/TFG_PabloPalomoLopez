using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OCDS_Mapper.src.Interfaces
{
    /* Interfaz del componente de parseo */
    
    public interface IParser
    {
        /* Propiedades */

        /*  propiedad DocumentRoot => XElement
         *      Representa el elemento XML raíz del documento atom
         */
        XElement DocumentRoot { get; set; }

        /*  propiedad EntryRoot => XElement
         *      Representa el elemento XML raíz de la entrada siendo parseada:
         *      @ej : XElement(<cac-place-ext:ContractFolderStatus>)
         */
        XElement EntryRoot { get; set; }


        /* Funciones */

        /*  función GetNamespaces() => IDictionary<string, XNamespace>
         *      Representa mediante pares de nombres y espacios de nombres los namespaces del documento
         *      Recupera dicha información de los atributos del elemento feed en los ficheros atom
         *  @return : Diccionario con los pares de espacios de nombres
         *      @ej : { {"cac" : XNamespace("urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2")}, ... }
         */
        IDictionary<string, XNamespace> GetNamespaces();

        /*  función GetEntrySet(IDictionary<string, XNamespace>) => IEnumerable<XElement>
         *      Devuelve el conjunto de entradas en el fichero atom
         *      Las entradas vienen definidas por el elemento entry
         *  @param namespaces : Diccionario de espacios de nombres
         *  @return : Conjunto de elementos XML
         *      @ej : [ XElement(<entry xmlns="..."> <id>...</id> ... </entry>), ... ]
         */
        IEnumerable<XElement> GetEntrySet(IDictionary<string, XNamespace> namespaces);

        /*  función SetEntryRootElement(XElement) => void
         *      Actualiza la propiedad del objeto EntryRoot
         *      Extrae el elemento XML correspondiente a "ContractFolderStatus"
         *  @throws InvalidParsedElementException : si el elemento no contiene "ContractFolderStatus"
         */
        void SetEntryRootElement(XElement entry);

        /*  función GetElements(IEnumerable<XName>) => XElement[]
         *      Devuelve el (los) elemento(s) XML descrito por la ruta pasada como parámetro
         *  @param pathToElement : lista enlazada con la ruta del elemento deseado:
         *      @ej : [ XName(XNamespace("cac") + "ProcurementProject"),
         *              XName(XNamespace("cbc") + "Name") ]
         *  @return : elemento(s) buscado(s), o null si no se puede encontrar
         *      @ej : XElement(<cbc:Name>"..."</cbc:Name>)
         */
        XElement[] GetElements(IEnumerable<XName> pathToElement);

        /*  función GetNextFile() => Uri
         *      Devuelve la URI correspondiente al siguiente fichero, descrito por el
         *      elemento "link" y el atributo "rel=next"
         *  @return : La URI que describe el fichero enlazado al siendo parseado en esta instancia
         */
        Uri GetNextFile();

        /*  función GetDocumentTimestamp(bool) => string
         *      Devuelve el timestamp del documento o de una entrada específica
         *  @param document : True si se quiere recuperar el timestamp del documento, False si se quiere el de la entrada
         *  @return : timestamp (o campo "updated") del documento de licitaciones o de la entrada
         */
        string GetDocumentTimestamp(bool document);
    }
}
