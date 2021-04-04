using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using log4net.Core;
using Newtonsoft.Json.Linq;
using OCDS_Mapper.src.Exceptions;
using OCDS_Mapper.src.Interfaces;

namespace OCDS_Mapper.src.Model
{
    /* Clase del componente de mapeo */

    public class Mapper : IMapper
    {
        /* Propiedades */

        /*  propiedad _mappedEntry => JObject
         *      Representa el elemento JSON con la información mapeada
         */
        public JObject MappedEntry { get; set; }



        /* Atributos privados */

        /*  atributo _Log => Action<object, string, Level>
         *      Puntero a la función de logging
         */
        private readonly Action<object, string, Level> _Log;



        /* Constructor */

        // @param Log : Puntero a la función de logging
        public Mapper(Action<object, string, Level> Log)
        {
            _Log = Log;

            // Inicializa el objeto JSON en el que se mapeará la información
            MappedEntry = new JObject();
            _Log.Invoke(this, "JSON initialized", Level.Debug);
        }



        /* Implementación de IMapper */


        /*  función MapElement(IEnumerable<string>, XElement[]) => void
         *      Realiza el mapeo de un elemento
         *  @param pathMap : ruta del elemento cuando sea mapeado
         *  @param parsedElement : elemento(s) a mapear
         *      @ej : MapElement( [ "tender", "title" ], "ABCD" )
         *              => _mappedEntry = { "tender": { "title": "ABCD" } }
         *      @ej : MapElement( [ "tag" ], "PRE" )
         *              => _mappedEntry = { "tag": [ "planning" ] }
         */
        public void MapElement(IEnumerable<string> pathMap, XElement[] parsedElement)
        {
            // Obtiene el JToken (clase genérica) con el elemento mapeado
            JToken toInsert;
            try 
            {
                toInsert = GetElementToken(pathMap, parsedElement);
                if (toInsert == null)
                {
                    return;
                }
            }
            catch (Exception e)
            {
                _Log.Invoke(this, e.Message, Level.Error);
                return;
            }
            // Itera a través del path al elemento con un enumerador
            using (IEnumerator<string> pathEnumerator = pathMap.GetEnumerator())
            {
                // Inicia la ruta desde la raíz del JSON
                JToken pathToken = MappedEntry.Root;

                // Declara un JContainer para realizar la operación de inserción
                JContainer jContainer;

                // Avanza el enumerador al primer elemento y lo carga en 'currentPath'
                pathEnumerator.MoveNext();
                string currentPath = pathEnumerator.Current;
                
                // Mientras haya elementos, sigue iterando
                // Procesa el elemento previo (currentPath) al descrito por el enumerador (pathEnumerator.Current)
                while(pathEnumerator.MoveNext())
                {
                    // Si el elemento previo de la ruta no existe, lo inicializa
                    if (pathToken[currentPath] == null)
                    {
                        jContainer = (JContainer) pathToken;
                        jContainer.Add(new JProperty(currentPath, new JObject()));
                        _Log.Invoke(this, $"{currentPath} wasn't found, it's created", Level.Debug);
                    }

                    // Mueve el token a la nueva ruta y mueve el 'currentPath' al siguiente elemento
                    pathToken = pathToken[currentPath];
                    currentPath = pathEnumerator.Current;
                }

                // Inserta en la posición del token el elemento mapeado
                jContainer = (JContainer) pathToken;

                JProperty jProperty = new JProperty(currentPath, toInsert);

                // Rama para elementos a sobreescribir
                if (jContainer[currentPath] != null)
                {
                    jContainer[currentPath].Parent.Remove();
                }
                jContainer.Add(jProperty);
                _Log.Invoke(this, $"Adding element in {currentPath}", Level.Debug);
            }
        }



        /* Funciones auxiliares */

        /*  función GetElementContainer(IEnumerable<string>, XElement[]) => JToken
         *      Devuelve el elemento mapeado y serializado a JSON
         *  @param pathMap : ruta del elemento cuando sea mapeado
         *  @param parsedElement : elemento(s) a mapear
         *  @throws EmptyMappingRuleException : si pathMap está vacío
         *  @throws WrongMappingException : si se encuentra un mapping inválido
         *  @throws InvalidPathLengthException : si pathMap tiene una longitud inválida
         *      @ej : GetElementToken( [ "tender", "title" ], "ABCD" )
         *              => JToken( { "tender": { "title": "ABCD" } } )
         *      @ej : GetElementToken( [ "tag" ], "PRE" )
         *              => JToken ( { "tag": [ "planning" ] } )
         */
        private static JToken GetElementToken(IEnumerable<string> pathMap, XElement[] parsedElement)
        {
            // Si se encuentra un path vacío, lanza una excepción
            if (!pathMap.Any())
            {
                throw new EmptyMappingRuleException();
            }
            else
            {
                // Por motivos de complejidad, se definen 3 niveles de funciones para cada nivel de profundidad de path
                if (pathMap.Count() == 1)
                {
                    return GetElementContainerDepth1(pathMap.GetEnumerator(), parsedElement);
                }
                else if (pathMap.Count() == 2)
                {
                    return GetElementContainerDepth2(pathMap.GetEnumerator(), parsedElement);
                }
                else if (pathMap.Count() == 3)
                {
                    return GetElementContainerDepth3(pathMap.GetEnumerator(), parsedElement);
                }
                else
                {
                    throw new InvalidPathLengthException();
                }
            }
        }


        /*  función GetElementContainerDepth1(IEnumerator<string>, XElement) => JToken
         *      Devuelve el elemento mapeado y serializado a JSON (profundidad 1)
         *  @param pathEnumerator : enumerador de la ruta del elemento a mapear
         *  @param parsedElement : elemento a mapear
         *  @throws WrongMappingException : si se encuentra un mapping inválido
         */
        private static JToken GetElementContainerDepth1(IEnumerator<string> pathEnumerator, XElement[] parsedElement)
        {
            // Establece el puntero a la función de mapeo
            Func<XElement[], JToken> mappingFunction = null;

            // Avanza el enumerador y dirige la función de mapeo
            pathEnumerator.MoveNext();
            string path = pathEnumerator.Current;
            switch (path)
            {
                case Mappings.MappingElement.Tag:
                    mappingFunction = MapTag;
                    break;
                case Mappings.MappingElement.OCID:
                    mappingFunction = MapOCID;
                    break;
                default:
                    throw new WrongMappingException(path);
            }

            // Devuelve el elemento de la función específica de mapeo
            return mappingFunction.Invoke(parsedElement);
        }


        /*  función GetElementContainerDepth2(IEnumerator<string>, XElement) => JToken
         *      Devuelve el elemento mapeado y serializado a JSON (profundidad 2)
         *  @param pathEnumerator : enumerador de la ruta del elemento a mapear
         *  @param parsedElement : elemento a mapear
         *  @throws WrongMappingException : si se encuentra un mapping inválido
         */
        private static JToken GetElementContainerDepth2(IEnumerator<string> pathEnumerator, XElement[] parsedElement)
        {
            // Establece el puntero a la función de mapeo
            Func<XElement[], JToken> mappingFunction = null;

            // Avanza el enumerador y dirige la función de mapeo
            pathEnumerator.MoveNext();
            string path = pathEnumerator.Current;
            switch (path)
            {
                case Mappings.MappingElement.Tender:
                    pathEnumerator.MoveNext();
                    path = pathEnumerator.Current;
                    switch (path)
                    {
                        case Mappings.MappingElement.Tenders.MainProcurementCategory:
                            mappingFunction = MapTenderMainProcurementCategory;
                            break;
                        case Mappings.MappingElement.Tenders.ProcurementMethod:
                            mappingFunction = MapTenderProcurementMethod;
                            break;
                        case Mappings.MappingElement.Tenders.ProcurementMethodDetails:
                            mappingFunction = MapTenderProcurementMethodDetails;
                            break;
                        case Mappings.MappingElement.Tenders.SubmissionMethod:
                            mappingFunction = MapTenderSubmissionMethod;
                            break;
                        case Mappings.MappingElement.Tenders.SubmissionMethodDetails:
                            mappingFunction = MapTenderSubmissionMethodDetails;
                            break;
                        case Mappings.MappingElement.Tenders.Title:
                            mappingFunction = MapTenderTitle;
                            break;
                        case Mappings.MappingElement.Tenders.Value:
                            mappingFunction = MapTenderValue;
                            break;
                        default:
                            throw new WrongMappingException(path);
                    }
                    break;
                default:
                    throw new WrongMappingException(path);
            }

            // Devuelve el elemento de la función específica de mapeo
            return mappingFunction.Invoke(parsedElement);
        }


        /*  función GetElementContainerDepth3(IEnumerator<string>, XElement) => JToken
         *      Devuelve el elemento mapeado y serializado a JSON (profundidad 3)
         *  @param pathEnumerator : enumerador de la ruta del elemento a mapear
         *  @param parsedElement : elemento a mapear
         *  @throws WrongMappingException : si se encuentra un mapping inválido
         */
        private static JToken GetElementContainerDepth3(IEnumerator<string> pathEnumerator, XElement[] parsedElement)
        {
            // Establece el puntero a la función de mapeo
            Func<XElement[], JToken> mappingFunction = null;

            // Avanza el enumerador y dirige la función de mapeo
            pathEnumerator.MoveNext();
            string path = pathEnumerator.Current;
            switch (path)
            {
                case Mappings.MappingElement.Tender:
                    pathEnumerator.MoveNext();
                    path = pathEnumerator.Current;
                    switch (path)
                    {
                        case Mappings.MappingElement.Tenders.TenderPeriod:
                            pathEnumerator.MoveNext();
                            path = pathEnumerator.Current;
                            switch (path)
                            {
                                case Mappings.MappingElement.Tenders.TenderPeriods.StartDate:
                                    mappingFunction = MapTenderTenderPeriodStartDate;
                                    break;
                                case Mappings.MappingElement.Tenders.TenderPeriods.EndDate:
                                    mappingFunction = MapTenderTenderPeriodEndDate;
                                    break;
                                case Mappings.MappingElement.Tenders.TenderPeriods.DurationInDays:
                                    mappingFunction = MapTenderTenderPeriodDurationInDays;
                                    break;
                                default:
                                    throw new WrongMappingException(path);
                            }
                            break;
                        default:
                            throw new WrongMappingException(path);
                    }
                    break;
                case Mappings.MappingElement.Planning:
                    pathEnumerator.MoveNext();
                    path = pathEnumerator.Current;
                    switch (path)
                    {
                        case Mappings.MappingElement.Plannings.Budget:
                            pathEnumerator.MoveNext();
                            path = pathEnumerator.Current;
                            switch (path)
                            {
                                case Mappings.MappingElement.Plannings.Budgets.Amount:
                                    mappingFunction = MapPlanningBudgetAmount;
                                    break;
                                default:
                                    throw new WrongMappingException(path);
                            }
                            break;
                        default:
                            throw new WrongMappingException(path);
                    }
                    break;
                default:
                    throw new WrongMappingException(path);
            }

            // Devuelve el elemento de la función específica de mapeo
            return mappingFunction.Invoke(parsedElement);
        }


        /* Funciones específicas de mapeo */

        // Mapeo del elemento tag
        // @throws WrongMappingException : si se encuentra un mapping inválido
        private static JToken MapTag(XElement[] toMap)
        {
            string mapped;
            XElement element = toMap[0];

            if (element.Value.Equals("PRE"))
            {
                mapped = "planning";
            }
            else if (element.Value.Equals("PUB") || element.Value.Equals("EV"))
            {
                mapped = "tender";
            }
            else if (element.Value.Equals("ADJ"))
            {
                mapped = "award";
            }
            else if (element.Value.Equals("RES"))
            {
                mapped = "contract";
            }
            else if (element.Value.Equals("ANUL"))
            {
                mapped = "awardCancellation";
            }
            else
            {
                throw new WrongMappingException("tag", element.Value);
            }

            return new JArray(mapped);
        }

        // Mapeo del elemento OCID
        private static JToken MapOCID(XElement[] toMap)
        {
            return new JValue($"ES-{toMap[0].Value}");
        }

        // Mapeo del elemento tender.mainProcurementCategory
        // @throws WrongMappingException : si se encuentra un código sin mapeo definido
        private static JToken MapTenderMainProcurementCategory(XElement[] toMap)
        {
            string mapped;
            XElement element = toMap[0];

            if (element.Value.Equals("1"))
            {
                mapped = "goods";
            }
            else if (element.Value.Equals("2") || element.Value.Equals("21") || element.Value.Equals("22"))
            {
                mapped = "services";
            }
            else if (element.Value.Equals("3") || element.Value.Equals("31") || element.Value.Equals("32"))
            {
                mapped = "works";
            }
            else
            {
                throw new WrongMappingException("tender.mainProcurementCategory", element.Value);
            }

            return new JValue(mapped);
        }

        // Mapeo del elemento tender.procurementMethod
        private static JToken MapTenderProcurementMethod(XElement[] toMap)
        {
            string mapped;
            XElement element = toMap[0];

            if (element.Value.Equals("1") || element.Value.Equals("5") || element.Value.Equals("6") || element.Value.Equals("8") ||
                element.Value.Equals("9") || element.Value.Equals("12") || element.Value.Equals("13"))
            {
                mapped = "open";
            }
            else if (element.Value.Equals("2") || element.Value.Equals("7"))
            {
                mapped = "selective";
            }
            else if (element.Value.Equals("3") || element.Value.Equals("4") || element.Value.Equals("10") ||
                     element.Value.Equals("11") || element.Value.Equals("100"))
            {
                mapped = "limited";
            }
            else
            {
                throw new WrongMappingException("tender.procurementMethod", element.Value);
            }

            return new JValue(mapped);
        }

        // Mapeo del elemento tender.procurementMethodDetails
        // @throws WrongMappingException : si se encuentra un código sin mapeo definido
        private static JToken MapTenderProcurementMethodDetails(XElement[] toMap)
        {
            XElement element = toMap[0];
            if (!element.Value.Equals("0"))
            {
                string mapped = Parser.GetCodeValue("https://contrataciondelestado.es/codice/cl/2.08/ContractingSystemTypeCode-2.08.gc", element.Value);
                if (mapped == null)
                {
                    throw new WrongMappingException("tender.procurementMethodDetails", element.Value);
                }
                return new JValue(mapped);
            }
            else
            {
                return null;
            }
        }

        // Mapeo del elemento tender.submissionMethod
        // @throws WrongMappingException : si se encuentra un código sin mapeo definido
        private static JToken MapTenderSubmissionMethod(XElement[] toMap)
        {
            string mapped;
            XElement element = toMap[0];

            if (element.Name.LocalName.Equals("AuctionConstraintIndicator"))
            {
                if (element.Value.Equals("true"))
                {
                    mapped = "electronicAuction";
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (element.Value.Equals("1"))
                {
                    mapped = "electronicSubmission";
                }
                else if (element.Value.Equals("2"))
                {
                    mapped = "written";
                }
                else if (element.Value.Equals("3"))
                {
                    return new JArray("electronicSubmission", "written");
                }
                else
                {
                    throw new WrongMappingException("tender.submissionMethod", element.Value);
                }
            }

            return new JArray(mapped);
        }

        // Mapeo del elemento tender.submissionMethodDetails
        private static JToken MapTenderSubmissionMethodDetails(XElement[] toMap)
        {
            string mapped = "Languages: ";
            for (int i = 0; i < toMap.Length; i++)
            {
                mapped = $"{mapped}{toMap[i].Value}, ";
                if (i + 1 == toMap.Length)
                {
                    mapped = mapped.Remove(mapped.Length - ", ".Length);
                }
            }
            return new JValue(mapped);
        }

        // Mapeo del elemento tender.tenderPeriod.startDate
        private static JToken MapTenderTenderPeriodStartDate(XElement[] toMap)
        {
            return new JValue($"{toMap[0].Value}T00:00:00Z");
        }

        // Mapeo del elemento tender.tenderPeriod.endDate
        private static JToken MapTenderTenderPeriodEndDate(XElement[] toMap)
        {
            return new JValue($"{toMap[0].Value}T00:00:00Z");
        }

        // Mapeo del elemento tender.tenderPeriod.durationInDays
        private static JToken MapTenderTenderPeriodDurationInDays(XElement[] toMap)
        {
            XElement element = toMap[0];
            if (element.FirstAttribute.Value.Equals("ANN"))
            {
                return new JValue(Convert.ToInt32(element.Value) * 365);
            }
            else if (element.FirstAttribute.Value.Equals("MON"))
            {
                return new JValue(Convert.ToInt32(element.Value) * 30);
            }
            else
            {
                return new JValue(Convert.ToInt32(element.Value));
            }
        }

        // Mapeo del elemento tender.title
        private static JToken MapTenderTitle(XElement[] toMap)
        {
            return new JValue(toMap[0].Value);
        }

        // Mapeo del elemento tender.value
        // @throws WrongMappingException : si se encuentra una moneda distinta a EUR
        private static JToken MapTenderValue(XElement[] toMap)
        {
            XElement element = toMap[0];
            if (element.FirstAttribute != null && element.FirstAttribute.Value != "EUR")
            {
                throw new WrongMappingException("tender.value.currency", element.FirstAttribute.Value);
            }
            return new JObject(new JProperty("amount", element.Value), new JProperty("currency", "EUR"));
        }
    
        // Mapeo del elemento planning.budget.amount
        private static JToken MapPlanningBudgetAmount(XElement[] toMap)
        {
            XElement element = toMap[0];
            if (element.FirstAttribute != null && element.FirstAttribute.Value != "EUR")
            {
                throw new WrongMappingException("planning.budget.amount", element.FirstAttribute.Value);
            }
            return new JObject(new JProperty("amount", element.Value), new JProperty("currency", "EUR"));
        }
    }
}
