﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using log4net.Core;
using OCDS_Mapper.src.Exceptions;
using OCDS_Mapper.src.Interfaces;

namespace OCDS_Mapper.src.Model
{
    /* Clase del componente de parseo */

    public class Parser : IParser
    {
        /* Propiedades */

        /*  propiedad DocumentRoot => XElement
         *      Representa el elemento XML raíz del documento atom
         */
        public XElement DocumentRoot { get; set; }

        /*  propiedad EntryRoot => XElement
         *      Representa el elemento XML raíz de la entrada siendo parseada:
         *      Elemento <cac-place-ext:ContractFolderStatus>
         */
        public XElement EntryRoot { get; set; }



        /* Atributos privados */

        /*  atributo _Log => Action<object, string, Level>
         *      Puntero a la función de logging
         */
        private readonly Action<object, string, Level> _Log;

        /*  atributo _codeLists => IDictionary<string, XElement>
         *      Colección de ficheros de códigos de acceso recurrente
         */
        private static IDictionary<string, XElement> _codeLists = new Dictionary<string, XElement>()
        {
            {
                "CPV",
                XElement.Load(ConfigurationManager.AppSettings["CPV_codelist"])
            },
            {
                "ContractingSTC",
                XElement.Load(ConfigurationManager.AppSettings["ContractingSTC_codelist"])
            }
        };



        /* Constructor */

        // @param Log : Puntero a la función de logging
        // @param filepath : ruta al fichero atom a parsear
        public Parser(Action<object, string, Level> Log, string filePath)
        {
            _Log = Log;

            // Carga el fichero a parsear y logea el tiempo gastado en ello
            DocumentRoot = XElement.Load(filePath);

            _Log.Invoke(this, $"XML document {filePath} loaded", Level.Info);
        }



        /* Implementación de IParser */

        /*  función GetNamespaces() => IDictionary<string, XNamespace>
         *      Representa mediante pares de nombres y espacios de nombres los namespaces del documento
         *      Recupera dicha información de los atributos del elemento feed en los ficheros atom
         *  @return : Diccionario con los pares de espacios de nombres
         *      @ej : { {"cac" : XNamespace("urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2")}, ... }
         */
        public IDictionary<string, XNamespace> GetNamespaces()
        {
            IDictionary<string, XNamespace> namespaces = new Dictionary<string, XNamespace>();

            // Itera sobre los atributos del elemento raíz (feed)
            foreach (var attr in DocumentRoot.Attributes())
            {
                // Construye un par a través del nombre del atributo y el namespace que representa su valor
                namespaces.Add(attr.Name.LocalName, XNamespace.Get(attr.Value));
                _Log.Invoke(this, $"Adding to namespace: {attr.Name.LocalName}", Level.Debug);
            }

            _Log.Invoke(this, $"Loaded namespace with {namespaces.Count} elements", Level.Debug);
            return namespaces;
        }


        /*  función GetEntrySet(IDictionary<string, XNamespace>) => IEnumerable<XElement>
         *      Devuelve el conjunto de entradas en el fichero atom
         *      Las entradas vienen definidas por el elemento entry
         *  @param namespaces : Diccionario de espacios de nombres
         *  @return : Conjunto de elementos XML
         *      @ej : [ XElement(<entry xmlns="..."> <id>...</id> ... </entry>), ... ]
         */
        public IEnumerable<XElement> GetEntrySet(IDictionary<string, XNamespace> namespaces)
        {
            // Realiza una consulta Linq para extraer el conjunto de entradas
            IEnumerable<XElement> entrySet =
                from entry in DocumentRoot.Elements(namespaces["xmlns"] + "entry")
                select entry;

            _Log.Invoke(this, $"Loaded entry set with {entrySet.Count()} elements", Level.Info);
            return entrySet;
        }


        /*  función SetEntryRootElement(XElement) => void
         *      Actualiza la propiedad del objeto EntryRoot
         *      Extrae el elemento XML correspondiente a "ContractFolderStatus"
         *  @throws InvalidParsedElementException : si el elemento no contiene "ContractFolderStatus"
         */
        public void SetEntryRootElement(XElement entry)
        {
            // Ejecuta una consulta Linq para extraer el elemento ContractFolderStatus
            IEnumerable<XElement> query =
                from node in entry.Elements()
                where node.Name.LocalName.Equals("ContractFolderStatus")
                select node;

            // Actualiza la propiedad
            if (query.Any())
            {
                EntryRoot = query.First();
            }
            else
            {
                throw new InvalidParsedElementException(entry.Elements().First().Value);
            }
        }


        /*  función GetElements(IEnumerable<XName>) => XElement[]
         *      Devuelve el (los) elemento(s) XML descrito por la ruta pasada como parámetro
         *  @param pathToElement : lista enlazada con la ruta del elemento deseado:
         *      @ej : [ XName(XNamespace("cac") + "ProcurementProject"),
         *              XName(XNamespace("cbc") + "Name") ]
         *  @return : elemento buscado, o null si no se puede encontrar
         *      @ej : XElement(<cbc:Name>"..."</cbc:Name>)
         */
        public XElement[] GetElements(IEnumerable<XName> pathToElement)
        {
            // Parte de la raíz para navegar por el XML, iterando por el path pasado como parámetro
            XElement element = EntryRoot;
            foreach (XName name in pathToElement)
            {
                // Realiza una consulta Linq a través del elemento actual, buscando el siguiente
                IEnumerable<XElement> query =
                    from node in element.Elements(name)
                    select node;
                
                // Si no lo encuentra, devuelve null
                if (!query.Any())
                {
                    _Log(this, $"Elemento {pathToElement.Last().LocalName} no encontrado", Level.Debug);
                    return null;
                }
                // Si existe, actualiza el elemento de búsqueda
                else
                {
                    element = query.First();
                    if (query.Count() > 1)
                    {
                        return query.ToArray();
                    }
                }
            }
            
            // Una vez finalizada la iteración de búsquedas, devuelve el elemento final
            return new XElement[]{ element };
        }


        /*  función GetNextFile() => Uri
         *      Devuelve la URI correspondiente al siguiente fichero, descrito por el
         *      elemento "link" y el atributo "rel=next"
         *  @return : La URI que describe el fichero enlazado al siendo parseado en esta instancia
         */
        public Uri GetNextFile()
        {
            // Realiza una consulta Linq desde la raíz del documento para encontrar el enlace
            IEnumerable<XElement> query =
                from node in DocumentRoot.Elements()
                where node.Name.LocalName.Equals("link") && node.LastAttribute.Value.Equals("next")
                select node;
            
            // Si no lo encuentra, devuelve null
            if (!query.Any())
            {
                _Log.Invoke(this, "Link to next file couldn't be found", Level.Warn);
                return null;
            }
            // Si existe, devuelve la URI correspondiente al siguiente fichero
            else
            {
                _Log.Invoke(this, $"Linked next file: {query.First().FirstAttribute.Value}", Level.Debug);
                return new Uri(query.First().FirstAttribute.Value);
            }
        }


        /* Funciones estáticas */

        /*  función GetSpecificElement(IEnumerable<XName>) => XElement[]
         *      Devuelve el elemento XML desde el elemento provisto, y con el nombre pasado como parámetro
         *  @param element : elemento a partir del cual buscar
         *  @param toSearch : nombre local del elemento a buscar
         *  @return : elemento buscado, o null si no se puede encontrar
         *      @ej : XElement(<cbc:Name>"..."</cbc:Name>)
         */
        public static XElement GetSpecificElement(XElement element, string toSearch)
        {
            // Realiza una consulta Linq desde el elemento pasado como parámetro
            IEnumerable<XElement> query =
                from node in element.Elements()
                where node.Name.LocalName.Equals(toSearch)
                select node;
            
            // Si no lo encuentra, devuelve null
            if (!query.Any())
            {
                return null;
            }
            // Si existe, devuelve el elemento específico buscado
            else
            {
                return query.First();
            }
        }


        /*  función GetCodeValue() => string
         *      Carga el documento de códigos provisto como parámetro y devuelve el valor
         *      de la entrada cuyo código casa con el segundo parámetro
         *  @param name : nombre descriptor del fichero de códigos
         *  @param code : código buscado
         *  @return : valor del código buscado
         */
        public static string GetCodeValue(string name, string code)
        {
            XElement document = _codeLists[name];

            if (document == null)
            {
                return null;
            }

            IEnumerable<XElement> query =
                from node in document.Elements()
                where node.Name.LocalName.Equals("SimpleCodeList")
                select node;
            
            query =
                from node in query.First().Elements()
                where node.Elements().First().Value.Equals(code)
                select node;
                
            // Si no lo encuentra, devuelve null
            if (!query.Any())
            {
                return null;
            }
            // Si existe, devuelve el valor de la entrada
            else
            {
                query =
                    from node in query.First().Elements()
                    where node.FirstAttribute.Value.Equals("nombre")
                    select node;
                return query.First().Value;
            }
        }
    }
}
