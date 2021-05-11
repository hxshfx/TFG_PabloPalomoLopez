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

        /*  propiedad MappedEntry => JObject
         *      Representa el elemento JSON con la información mapeada
         */
        public JObject MappedEntry { get; set; }



        /* Atributos privados */

        /*  atributo _Log => Action<object, string, Level>
         *      Puntero a la función de logging
         */
        private readonly Action<object, string, Level> _Log;


        /*  atributo _awards => JArray
         *      Almacén temporal del objeto representando la colección de adjudicaciones
         */
        private readonly JArray _awards;


        /*  atributo _contractingParty => JObject
         *      Almacén temporal del objeto representando la entidad adjudicadora
         */
        private readonly JObject _contractingParty;


        /*  atributo _contracts => JArray
         *      Almacén temporal del objeto representando la colección de contratos
         */
        private readonly JArray _contracts;


        /*  atributo _lots => JArray
         *      Almacén temporal del objeto representando la colección de lotes
         */
        private readonly JArray _lots;


        /*  atributo _id => string
         *      Copia del identificador único de la licitación
         */
        private string _id { get; set; }


        /*  atributo _items => JArray
         *      Almacén temporal del objeto representando la colección de items
         */
        private readonly JArray _items;


        /*  atributo _packager => IPackager
         *      Instancia del Parser para obtener la información de identificadores
         */
        private IPackager _packager;


        /*  atributo _releaseId => string
         *      Copia del identificador único de la entrega
         */
        private string _releaseId { get; set; }


        /*  atributo _supplierParties => JArray
         *      Almacén temporal de los objetos representando las entidades adjudicatarias
         */
        private readonly JArray _supplierParties;


        /*  constante _PREFIX => string
         *      Prefijo de los identificadores
        */
        private static string _PREFIX = Program.Configuration["ID_prefix"];



        /* Constructor */

        // @param Log : Puntero a la función de logging
        // @param packager : instancia del componente de empaquetado
        public Mapper(Action<object, string, Level> Log, IPackager packager)
        {
            _Log = Log;
            _packager = packager;

            // Inicializa el objeto JSON en el que se mapeará la información
            MappedEntry = new JObject();

            // Inicializa el array JSON para almacenar temporalmente la colección de adjudicaciones
            _awards = new JArray();

            // Inicializa el objeto JSON para almacenar temporalmente la entidad adjudicadora
            _contractingParty = new JObject();

            // Inicializa el objeto JSON para almacenar temporalmente la colección de contratos
            _contracts = new JArray();

            // Inicializa el objeto JSON para almacenar temporalmente la coleccion de lotes
            _lots = new JArray();

            // Inicializa el objeto JSON para almacenar temporalmente la coleccion de items
            _items = new JArray();

            // Inicializa el objeto JSON para almacenar temporalmente las entidades adjudicatarias
            _supplierParties = new JArray();

            _Log(this, "JSON initialized", Level.Debug);
        }



        /* Implementación de IMapper */

        /*  función MapElement(IEnumerable<string>, XElement[]) => void
         *      Realiza el mapeo de elemento(s) CODICE a elemento(s) OCDS
         *  @param pathMap : ruta del elemento cuando sea mapeado
         *  @param parsedElement : elemento(s) a mapear
         *  @ej : MapElement( [ "tender", "title" ], [ "ABCD" ] )
         *      => MappedEntry = { "tender": { "title": "ABCD" } }
         *  @ej : MapElement( [ "tag" ], [ "PRE" ] )
         *      => MappedEntry = { "tag": [ "planning" ] }
         */
        public void MapElement(IEnumerable<string> pathMap, XElement[] parsedElement)
        {
            // Obtiene el JToken (clase genérica) con el elemento mapeado
            JToken toInsert = null;
            try 
            {
                toInsert = GetElementToken(pathMap, parsedElement);
                if (toInsert == null)
                {
                    return;
                }
            }
            // Realiza la captura de las posibles excepciones lanzadas
            catch (EmptyMappingRuleException)
            {
                _Log(this, "An empty mapping rule was provided", Level.Warn);
                return;
            }
            catch (InvalidPathLengthException)
            {
                _Log(this, "A mapping rule with invalid length was provided", Level.Warn);
                return;
            }
            catch (WrongMappingException e)
            {
                _Log(this, e.Message, Level.Error);
                return;
            }
            catch (Exception e)
            {
                _Log(this, e.Message, Level.Fatal);
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
                        _Log(this, $"{currentPath} wasn't found, it's created", Level.Debug);
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
                _Log(this, $"Adding element in {currentPath}", Level.Debug);
            }
        }


        /*  función Commit() => void
         *      Introduce los cambios al JSON que no se pueden introducir
         *      mediante los mapeos unitarios de elementos (metadatos, colecciones, etc.)
         */
        public void Commit()
        {
            // Inserta los metadatos de la entrada
            MappedEntry.Add("date", GetDate()); // TODO ?
            MappedEntry.Add("id", $"{_id}-{_releaseId}");
            MappedEntry.Add("initiationType", "tender");
            MappedEntry.Add("language", "es");

            // Incluye las adjudicaciones, si las hubiera
            if (_awards.Any())
            {
                MappedEntry.Add("awards", _awards);
            }

            // Incluye el identificador del objeto de licitación, si lo hubiera
            JObject tender = (JObject) MappedEntry["tender"];
            if (tender != null)
            {
                tender.Add("id", $"{_PREFIX}-{_id}-tender-{_releaseId}");
            }
            
            // Incluye las colecciones de ítems y lotes, si los hubiera
            if (_items.Any() && _lots.Any())
            {
                // Si no se ha incluído el tender ya, lo crea para insertar las colecciones
                if (tender == null)
                {
                    MappedEntry.Add(new JProperty("tender", new JObject()));
                    tender = (JObject) MappedEntry["tender"];
                }

                tender.Add("items", _items);
                tender.Add("lots", _lots);
            }

            // Incluye la colección de contratos, si lo hubiera
            if (_contracts.Any())
            {
                MappedEntry.Add("contracts", _contracts);
            }

            // Incluye en la colección de participantes los adjudicadores, si los hubiera
            JArray parties = new JArray();
            if (_contractingParty.Count != 0)
            {
                parties.Add(_contractingParty);
            }

            // Incluye en la colección de participantes los adjudicatarios, si los hubiera
            foreach (var supplierParty in _supplierParties)
            {
                parties.Add(supplierParty);
            }

            // Incluye la colección de participantes, si los hubiera
            if (parties.Any())
            {
                MappedEntry.Add("parties", parties);
            }
        }



        /* Funciones auxiliares */

        /*  función GetElementToken(IEnumerable<string>, XElement[]) => JToken
         *      Devuelve el elemento mapeado y serializado a JSON
         *  @param pathMap : ruta del elemento cuando sea mapeado
         *  @param parsedElement : elemento(s) a mapear
         *  @return : el elemento a introducir, o null si es un elemento inválido o para introducir en Commit()
         *  @throws EmptyMappingRuleException : si pathMap está vacío
         *  @throws WrongMappingException : si se encuentra un mapping inválido
         *  @throws InvalidPathLengthException : si pathMap tiene una longitud inválida
         *  @ej : GetElementToken( [ "tender", "title" ], [ "ABCD" ] )
         *      => JToken( { "tender": { "title": "ABCD" } } )
         *  @ej : GetElementToken( [ "tag" ], [ "PRE" ] )
         *      => JToken ( { "tag": [ "planning" ] } )
         */
        private JToken GetElementToken(IEnumerable<string> pathMap, XElement[] parsedElement)
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
                    return GetElementTokenDepth1(pathMap.GetEnumerator(), parsedElement);
                }
                else if (pathMap.Count() == 2)
                {
                    return GetElementTokenDepth2(pathMap.GetEnumerator(), parsedElement);
                }
                else if (pathMap.Count() == 3)
                {
                    return GetElementTokenDepth3(pathMap.GetEnumerator(), parsedElement);
                }
                else
                {
                    throw new InvalidPathLengthException();
                }
            }
        }


        /*  función GetElementTokenDepth1(IEnumerator<string>, XElement[]) => JToken
         *      Devuelve el elemento mapeado y serializado a JSON (profundidad 1)
         *  @param pathEnumerator : enumerador de la ruta del elemento a mapear
         *  @param parsedElement : elemento a mapear
         *  @return : el elemento a introducir, o null si es un elemento inválido o para introducir en Commit()
         *  @throws WrongMappingException : si se encuentra un mapping inválido
         */
        private JToken GetElementTokenDepth1(IEnumerator<string> pathEnumerator, XElement[] parsedElement)
        {
            // Establece el puntero a la función de mapeo
            Func<XElement[], JToken> mappingFunction = null;

            // Avanza el enumerador y dirige la función de mapeo
            pathEnumerator.MoveNext();
            string path = pathEnumerator.Current;
            switch (path)
            {
                case Mappings.MappingElements.Tag:
                    mappingFunction = MapTag;
                    break;
                case Mappings.MappingElements.OCID:
                    mappingFunction = MapOCID;
                    break;
                case Mappings.MappingElements.Party:
                    mappingFunction = MapPartyFields;
                    break;
                default:
                    throw new WrongMappingException(path);
            }

            // Devuelve el elemento de la función específica de mapeo
            return mappingFunction.Invoke(parsedElement);
        }


        /*  función GetElementTokenDepth2(IEnumerator<string>, XElement[]) => JToken
         *      Devuelve el elemento mapeado y serializado a JSON (profundidad 2)
         *  @param pathEnumerator : enumerador de la ruta del elemento a mapear
         *  @param parsedElement : elemento a mapear
         *  @return : el elemento a introducir, o null si es un elemento inválido o para introducir en Commit()
         *  @throws WrongMappingException : si se encuentra un mapping inválido
         */
        private JToken GetElementTokenDepth2(IEnumerator<string> pathEnumerator, XElement[] parsedElement)
        {
            // Establece el puntero a la función de mapeo
            Func<XElement[], JToken> mappingFunction = null;

            // Avanza el enumerador y dirige la función de mapeo
            pathEnumerator.MoveNext();
            string path = pathEnumerator.Current;
            switch (path)
            {
                case Mappings.MappingElements.Award:
                    pathEnumerator.MoveNext();
                    path = pathEnumerator.Current;
                    switch (path)
                    {
                        case Mappings.MappingElements.Awards.Date:
                            mappingFunction = MapAwardDate;
                            break;
                        case Mappings.MappingElements.Awards.Description:
                            mappingFunction = MapAwardDescription;
                            break;
                        case Mappings.MappingElements.Awards.Id:
                            mappingFunction = MapAwardId;
                            break;
                        case Mappings.MappingElements.Awards.Status:
                            mappingFunction = MapAwardStatus;
                            break;
                        case Mappings.MappingElements.Awards.Suppliers:
                            mappingFunction = MapAwardSuppliers;
                            break;
                        case Mappings.MappingElements.Awards.Value:
                            mappingFunction = MapAwardValue;
                            break;
                        default:
                            throw new WrongMappingException(path);
                    }
                    break;
                case Mappings.MappingElements.Contract:
                    pathEnumerator.MoveNext();
                    path = pathEnumerator.Current;
                    switch (path)
                    {
                        case Mappings.MappingElements.Contracts.Id:
                            mappingFunction = MapContractId;
                            break;
                        default:
                            throw new WrongMappingException(path);
                    }
                    break;
                case Mappings.MappingElements.Party:
                    pathEnumerator.MoveNext();
                    path = pathEnumerator.Current;
                    switch (path)
                    {
                        case Mappings.MappingElements.Parties.Identifier:
                            mappingFunction = MapPartiesIdentifier;
                            break;
                        case Mappings.MappingElements.Parties.Name:
                            mappingFunction = MapPartiesName;
                            break;
                        default:
                            throw new WrongMappingException(path);
                    }
                    break;
                case Mappings.MappingElements.Tender:
                    pathEnumerator.MoveNext();
                    path = pathEnumerator.Current;
                    switch (path)
                    {
                        case Mappings.MappingElements.Tenders.MainProcurementCategory:
                            mappingFunction = MapTenderMainProcurementCategory;
                            break;
                        case Mappings.MappingElements.Tenders.NumberOfTenderers:
                            mappingFunction = MapTenderNumberOfTenderers;
                            break;
                        case Mappings.MappingElements.Tenders.ProcurementMethod:
                            mappingFunction = MapTenderProcurementMethod;
                            break;
                        case Mappings.MappingElements.Tenders.ProcurementMethodDetails:
                            mappingFunction = MapTenderProcurementMethodDetails;
                            break;
                        case Mappings.MappingElements.Tenders.SubmissionMethod:
                            mappingFunction = MapTenderSubmissionMethod;
                            break;
                        case Mappings.MappingElements.Tenders.SubmissionMethodDetails:
                            mappingFunction = MapTenderSubmissionMethodDetails;
                            break;
                        case Mappings.MappingElements.Tenders.Title:
                            mappingFunction = MapTenderTitle;
                            break;
                        case Mappings.MappingElements.Tenders.Value:
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


        /*  función GetElementTokenDepth3(IEnumerator<string>, XElement[]) => JToken
         *      Devuelve el elemento mapeado y serializado a JSON (profundidad 3)
         *  @param pathEnumerator : enumerador de la ruta del elemento a mapear
         *  @param parsedElement : elemento a mapear
         *  @return : el elemento a introducir, o null si es un elemento inválido o para introducir en Commit()
         *  @throws WrongMappingException : si se encuentra un mapping inválido
         */
        private JToken GetElementTokenDepth3(IEnumerator<string> pathEnumerator, XElement[] parsedElement)
        {
            // Establece el puntero a la función de mapeo
            Func<XElement[], JToken> mappingFunction = null;

            // Avanza el enumerador y dirige la función de mapeo
            pathEnumerator.MoveNext();
            string path = pathEnumerator.Current;
            switch (path)
            {
                case Mappings.MappingElements.Contract:
                    pathEnumerator.MoveNext();
                    path = pathEnumerator.Current;
                    switch (path)
                    {
                        case Mappings.MappingElements.Contracts.Period:
                            pathEnumerator.MoveNext();
                            path = pathEnumerator.Current;
                            switch (path)
                            {
                                case Mappings.MappingElements.Contracts.Periods.StartDate:
                                    mappingFunction = MapContractPeriodStartDate;
                                    break;
                                default:
                                    throw new WrongMappingException(path);
                            }
                            break;
                        default:
                            throw new WrongMappingException(path);
                    }
                    break;
                case Mappings.MappingElements.Tender:
                    pathEnumerator.MoveNext();
                    path = pathEnumerator.Current;
                    switch (path)
                    {
                        case Mappings.MappingElements.Tenders.Item:
                            pathEnumerator.MoveNext();
                            path = pathEnumerator.Current;
                            switch (path)
                            {
                                case Mappings.MappingElements.Tenders.Items.Classification:
                                    mappingFunction = MapTenderItemsClassification;
                                    break;
                                default:
                                    throw new WrongMappingException(path);
                            }
                            break;
                        case Mappings.MappingElements.Tenders.Lot:
                            pathEnumerator.MoveNext();
                            path = pathEnumerator.Current;
                            switch (path)
                            {
                                case Mappings.MappingElements.Tenders.Lots.Id:
                                    mappingFunction = MapTenderLotsId;
                                    break;
                                case Mappings.MappingElements.Tenders.Lots.Name:
                                    mappingFunction = MapTenderLotsName;
                                    break;
                                case Mappings.MappingElements.Tenders.Lots.Value_:
                                    mappingFunction = MapTenderLotsValue;
                                    break;
                                default:
                                    throw new WrongMappingException(path);
                            }
                            break;
                        case Mappings.MappingElements.Tenders.TenderPeriod:
                            pathEnumerator.MoveNext();
                            path = pathEnumerator.Current;
                            switch (path)
                            {
                                case Mappings.MappingElements.Tenders.TenderPeriods.StartDate:
                                    mappingFunction = MapTenderTenderPeriodStartDate;
                                    break;
                                case Mappings.MappingElements.Tenders.TenderPeriods.EndDate:
                                    mappingFunction = MapTenderTenderPeriodEndDate;
                                    break;
                                case Mappings.MappingElements.Tenders.TenderPeriods.DurationInDays:
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
                case Mappings.MappingElements.Planning:
                    pathEnumerator.MoveNext();
                    path = pathEnumerator.Current;
                    switch (path)
                    {
                        case Mappings.MappingElements.Plannings.Budget:
                            pathEnumerator.MoveNext();
                            path = pathEnumerator.Current;
                            switch (path)
                            {
                                case Mappings.MappingElements.Plannings.Budgets.Amount:
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


        /*  función SimulateElement(string, int) => JObject
         *      Simula la creación de un elemento para el testing que
         *      en condiciones normales se habría creado ya
         *  @param type : tipo del elemento a simular
         *  @param id : identificador del elemento a simular
         *  @return : elemento creado
         */
        private JObject SimulateElement(string type, int id)
        {
            JObject jobj = new JObject();

            if (type.Equals("award"))
            {
                jobj.Add("id", $"{id}");
                _awards.Add(jobj);
            }
            else if (type.Equals("lot"))
            {
                jobj.Add("id", $"lot-{id}");
                _lots.Add(jobj);
            }
            else if (type.Equals("item"))
            {
                jobj.Add("id", $"{id}");
                _items.Add(jobj);
            }
            
            return jobj;
        }


        /*  función estática IsContained(JArray, JObject) => bool
         *      Evalúa si un objeto se encuentra dentro del array (si su ID lo está)
         *  @param array : colección en la que buscar
         *  @param jobj : objeto que buscar por su identificador
         *  @return : True si lo contiene, False si no lo hace
         */
        private static bool IsContained(JArray array, JObject jobj)
        {
            if (!array.Any())
            {
                return false;
            }
            else
            {
                foreach (JToken item in array)
                {
                    if (item["id"].Equals(jobj["id"]))
                    {
                        return true;
                    }
                }
                return false;
            }
        }


        /*  función estática GetDate() => string
         *      Devuelve la fecha actual en el formato OCDS
         *  @return : fecha actual (formato YYYY-MM-DDTHH:MM:SSZ)
         */
        private static string GetDate()
        {
            DateTime now = DateTime.Now;
            return $@"{now.Year}-{
                    now.Month.ToString().PadLeft(2, '0')
                }-{
                    now.Day.ToString().PadLeft(2, '0')
                }T{
                    now.Hour.ToString().PadLeft(2, '0')
                }:{
                    now.Minute.ToString().PadLeft(2, '0')
                }:{
                    now.Second.ToString().PadLeft(2, '0')
                }Z";
        }



        /* Funciones específicas de mapeo */

        // Mapeo del elemento tag
        private JToken MapTag(XElement[] toMap)
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
                _Log(this, $"Mapping code {element.Value} at element tag unrecognized", Level.Warn);
                return null;
            }

            return new JArray(mapped);
        }

        // Mapeo del elemento OCID
        private JToken MapOCID(XElement[] toMap)
        {
            _id = toMap[0].Value;
            _releaseId = _packager.GetIdentifier(_id);

            return new JValue($"{_PREFIX}-{_id}");
        }

        // Mapeo del elemento awards[i].date
        private JToken MapAwardDate(XElement[] toMap)
        {
            JObject award;

            award = (JObject) _awards.First;

            if (toMap.Length == 1)
            {
                if (award == null)
                {
                    award = SimulateElement("award", 1);
                }

                award.Add("date", $"{toMap[0].Value}T00:00:00Z");
            }
            else
            {
                int count = 1;
                XElement date;
                foreach (XElement awardElement in toMap)
                {
                    if (award == null)
                    {
                        award = SimulateElement("award", count++);
                    }

                    date = Parser.GetSpecificElement(awardElement, "AwardDate");

                    if (date != null)
                    {
                        award.Add("date", $"{date.Value}T00:00:00Z");
                        award = (JObject) award.Next;
                    }
                }
            }
            return null;
        }

        // Mapeo del elemento awards[i].description
        private JToken MapAwardDescription(XElement[] toMap)
        {
            JObject award;

            award = (JObject) _awards.First;

            if (toMap.Length == 1)
            {
                if (award == null)
                {
                    award = SimulateElement("award", 1);
                }

                award.Add("description_es", toMap[0].Value);
            }
            else
            {
                int count = 1;
                XElement description;
                foreach (XElement awardElement in toMap)
                {
                    if (award == null)
                    {
                        award = SimulateElement("award", count++);
                    }

                    description = Parser.GetSpecificElement(awardElement, "Description");

                    if (description != null)
                    {
                        award.Add("description_es", description.Value);
                        award = (JObject) award.Next;
                    }
                }
            }
            return null;
        }

        // Mapeo del elemento awards[i].id
        private JToken MapAwardId(XElement[] toMap)
        {
            JObject award;

            if (toMap.Length == 1)
            {
                award = new JObject();
                award.Add("id", $"{_PREFIX}-{_id}-award-1");
                _awards.Add(award);
            }
            else
            {
                XElement awardedTenderedProject, procurementProjectLotID;
                foreach (XElement awardElement in toMap)
                {
                    award = new JObject();

                    awardedTenderedProject = Parser.GetSpecificElement(awardElement, "AwardedTenderedProject");

                    if (awardedTenderedProject != null)
                    {
                        procurementProjectLotID = Parser.GetSpecificElement(awardedTenderedProject, "ProcurementProjectLotID");

                        if (procurementProjectLotID != null)
                        {
                            award.Add("id", $"{_PREFIX}-{_id}-award-{procurementProjectLotID.Value}-{_awards.Count + 1}");
                            _awards.Add(award);
                        }
                    }
                }
            }
            return null;
        }

        // Mapeo del elemento awards[i].status
        private JToken MapAwardStatus(XElement[] toMap)
        {
            JObject award = (JObject) _awards.First;

            if (toMap.Length == 1)
            {
                if (award == null)
                {
                    award = SimulateElement("award", 1);
                }

                if (toMap[0].Value.Equals("1") || toMap[0].Value.Equals("6"))
                {
                    award.Add("status", "pending");
                }
                else if (toMap[0].Value.Equals("2") || toMap[0].Value.Equals("8") ||
                            toMap[0].Value.Equals("9") || toMap[0].Value.Equals("10"))
                {
                    award.Add("status", "active");
                }
                else if (toMap[0].Value.Equals("3") || toMap[0].Value.Equals("7"))
                {
                    award.Add("status", "cancelled");
                }
                else if (toMap[0].Value.Equals("4") || toMap[0].Value.Equals("5"))
                {
                    award.Add("status", "unsuccessful");
                }
                else
                {
                    _Log(this, $"Mapping code {toMap[0].Value} at element awards.status unrecognized", Level.Warn);
                    return null;
                }
            }
            else
            {
                int count = 1;
                XElement awardedTenderedProject, procurementProjectLotID, resultCode;
                foreach (XElement awardElement in toMap)
                {
                    if (award == null)
                    {
                        award = SimulateElement("award", count++);
                    }

                    awardedTenderedProject = Parser.GetSpecificElement(awardElement, "AwardedTenderedProject");
                    procurementProjectLotID = Parser.GetSpecificElement(awardedTenderedProject, "ProcurementProjectLotID");
                    resultCode = Parser.GetSpecificElement(awardElement, "ResultCode");

                    string[] awardIDs = award.First.First.ToString().Split("-");

                    if (procurementProjectLotID != null && awardIDs.Length > 2 && !procurementProjectLotID.Value.Equals(awardIDs[awardIDs.Length - 2]))
                    {
                        _Log(this, "Award IDs discrepancy", Level.Error);
                    }

                    if (resultCode.Value.Equals("1") || resultCode.Value.Equals("6"))
                    {
                        award.Add("status", "pending");
                    }
                    else if (resultCode.Value.Equals("2") || resultCode.Value.Equals("8") ||
                             resultCode.Value.Equals("9") || resultCode.Value.Equals("10"))
                    {
                        award.Add("status", "active");
                    }
                    else if (resultCode.Value.Equals("3") || resultCode.Value.Equals("7"))
                    {
                        award.Add("status", "cancelled");
                    }
                    else if (resultCode.Value.Equals("4") || resultCode.Value.Equals("5"))
                    {
                        award.Add("status", "unsuccessful");
                    }
                    else
                    {
                        _Log(this, $"Mapping code {resultCode.Value} at element awards.status unrecognized", Level.Warn);
                        return null;
                    }
                    award = (JObject) award.Next;
                }
            }
            return null;
        }

        // Mapeo del elemento awards[i].suppliers
        private JToken MapAwardSuppliers(XElement[] toMap)
        {
            int count = 1;
            JObject award, identifier, party, supplier;
            XElement winningParty;

            award = (JObject) _awards.First;

            foreach (var tenderElement in toMap)
            {
                if (award == null)
                {
                    award = SimulateElement("award", count++);
                }

                winningParty = Parser.GetSpecificElement(tenderElement, "WinningParty");

                if (winningParty != null)
                {
                    XElement partyIdentification, partyName;

                    identifier = new JObject();
                    party = new JObject();
                    supplier = new JObject();

                    partyIdentification = Parser.GetSpecificElement(winningParty, "PartyIdentification");
                    partyName = Parser.GetSpecificElement(winningParty, "PartyName");

                    if (partyIdentification != null && partyName != null)
                    {
                        XAttribute attribute = partyIdentification.Elements().First().FirstAttribute;
                        if (attribute != null)
                        {
                            if (attribute.Value.Equals("DIR3"))
                            {
                                identifier.Add("scheme", "ES-DIR3");
                            }
                            else if (attribute.Value.Equals("NIF"))
                            {
                                identifier.Add("scheme", "ES-RMC");
                            }
                            else
                            {
                                _Log(this, $"Identifier {attribute.Value} not recognized", Level.Warn);
                            }
                            identifier.Add("id", partyIdentification.Value);
                        }

                        party.Add("identifier", identifier);
                        party.Add("id", $"{partyIdentification.Value}-{_supplierParties.Count}");
                        party.Add("name", partyName.Value);
                        party.Add("roles", new JArray("supplier"));

                        supplier.Add("id", $"{partyIdentification.Value}-{_supplierParties.Count}");
                        supplier.Add("name", partyName.Value);

                        if (!IsContained(_supplierParties, party))
                        {
                            _supplierParties.Add(party);
                        }
                        
                        award.Add("suppliers", new JArray(supplier));
                    }
                }
                award = (JObject) award.Next;
            }
            return null;
        }

        // Mapeo del elemento awards[i].value
        private JToken MapAwardValue(XElement[] toMap)
        {
            JObject award, value;
            XElement payableAmount;

            award = (JObject) _awards.First;
            value = new JObject();

            if (toMap.Length == 1)
            {
                if (award == null)
                {
                    award = SimulateElement("award", 1);
                }
                
                payableAmount = Parser.GetSpecificElement(toMap[0], "PayableAmount");
                if (payableAmount != null)
                {
                    if (payableAmount.FirstAttribute == null || payableAmount.FirstAttribute.Value != "EUR")
                    {
                        _Log(this, $"Mapping attribute code {payableAmount.FirstAttribute.Value} at element awards.value unrecognized", Level.Warn);
                        return null;
                    }
                    value.Add("amount", Convert.ToDouble(payableAmount.Value, System.Globalization.CultureInfo.InvariantCulture));
                    value.Add("currency", "EUR");

                    award.Add("value", value);
                }
            }
            else
            {
                int count = 1;
                XElement awardedTenderedProject, legalMonetaryTotal;
                foreach (XElement awardElement in toMap)
                {
                    if (award == null)
                    {
                        award = SimulateElement("award", count++);
                    }

                    value = new JObject();

                    awardedTenderedProject = Parser.GetSpecificElement(awardElement, "AwardedTenderedProject");
                    legalMonetaryTotal = Parser.GetSpecificElement(awardedTenderedProject, "LegalMonetaryTotal");
                    
                    if (legalMonetaryTotal != null)
                    {
                        payableAmount = Parser.GetSpecificElement(legalMonetaryTotal, "PayableAmount");
                        if (payableAmount != null)
                        {
                            if (payableAmount.FirstAttribute == null || payableAmount.FirstAttribute.Value != "EUR")
                            {
                                _Log(this, $"Mapping attribute code {payableAmount.FirstAttribute.Value} at element awards.value unrecognized", Level.Warn);
                                return null;
                            }

                            value.Add("amount", Convert.ToDouble(payableAmount.Value, System.Globalization.CultureInfo.InvariantCulture));
                            value.Add("currency", "EUR");

                            award.Add("value", value);
                            award = (JObject) award.Next;
                        }
                    }
                }
            }
            return null;
        }

        // Mapeo del elemento contracts[i].id
        // Añade de manera adicional el elemento contracts[i].awardID
        private JToken MapContractId(XElement[] toMap)
        {
            JObject contract;

            if (toMap.Length == 1)
            {
                contract = new JObject();
                contract.Add("id", $"{_PREFIX}-{_id}-contract-{toMap[0].Value}");
                contract.Add("awardID", $"{_PREFIX}-{_id}-award-1");
                _contracts.Add(contract);
            }
            else    
            {
                XElement contractElement, idElement;
                foreach (XElement tenderElement in toMap)
                {
                    contract = new JObject();

                    contractElement = Parser.GetSpecificElement(tenderElement, "Contract");

                    if (contractElement != null)
                    {
                        idElement = Parser.GetSpecificElement(contractElement, "ID");

                        if (idElement != null)
                        {
                            string[] awardIDs = _awards[_contracts.Count]["id"].ToString().Split("-");

                            if (awardIDs.Length > 1)
                            {
                                contract.Add("id", $"{_PREFIX}-{_id}-contract-{idElement.Value}-{_contracts.Count + 1}");
                                contract.Add("awardID", $"{_PREFIX}-{_id}-award-{awardIDs[awardIDs.Length - 2]}-{awardIDs[awardIDs.Length - 1]}");
                                _contracts.Add(contract);
                            }
                        }
                    }
                }
            }
            return null;
        }

        // Mapeo del elemento contracts[i].period.startDate
        private JToken MapContractPeriodStartDate(XElement[] toMap)
        {
            JObject contract, period;

            contract = (JObject) _contracts.First;

            if (toMap.Length == 1)
            {
                if (contract != null)
                {
                    period = new JObject();
                
                    period.Add("startDate", $"{toMap[0].Value}T00:00:00Z");
                    contract.Add("period", period);
                }
            }
            else
            {
                XElement startDate;
                foreach (XElement contractElement in toMap)
                {
                    period = new JObject();

                    startDate = Parser.GetSpecificElement(contractElement, "StartDate");
                    if (startDate != null && contract != null)
                    {
                        period.Add("startDate", $"{startDate.Value}T00:00:00Z");
                        contract.Add("period", period);

                        contract = (JObject) contract.Next;
                    }
                }
            }
            return null;
        }

        // Mapeo de los elementos adicionales de parties de la entidad adjudicadora
        private JToken MapPartyFields(XElement[] toMap)
        {
            JObject address, contactPoint;
            XElement addressLine, cityName, electronicMail, identificationCode, name, postalZone, telefax, telephone;

            address = new JObject();
            contactPoint = new JObject();

            XElement contact = Parser.GetSpecificElement(toMap[0], "Contact");
            XElement postalAddress = Parser.GetSpecificElement(toMap[0], "PostalAddress");
            XElement websiteUri = Parser.GetSpecificElement(toMap[0], "WebsiteURI");

            XElement countryName = Parser.GetSpecificElement(postalAddress, "Country");

            addressLine = Parser.GetSpecificElement(postalAddress, "AddressLine");
            cityName = Parser.GetSpecificElement(postalAddress, "CityName");
            identificationCode = Parser.GetSpecificElement(countryName, "IdentificationCode");
            postalZone = Parser.GetSpecificElement(postalAddress, "PostalZone");

            if (addressLine != null)
            {
                address.Add("streetAddress", addressLine.Value);
            }
            if (cityName != null)
            {
                address.Add("locality", cityName.Value);
            }
            if (identificationCode != null)
            {
                address.Add("countryName", identificationCode.Value);
            }
            if (postalZone != null)
            {
                address.Add("postalCode", postalZone.Value);
            }

            electronicMail = Parser.GetSpecificElement(contact, "ElectronicMail");
            name = Parser.GetSpecificElement(contact, "Name");
            telefax = Parser.GetSpecificElement(contact, "Telefax");
            telephone = Parser.GetSpecificElement(contact, "Telephone");
            
            
            if (electronicMail != null)
            {
                contactPoint.Add("email", electronicMail.Value);
            }
            if (name != null)
            {
                contactPoint.Add("name", name.Value);
            }
            if (telefax != null)
            {
                contactPoint.Add("faxNumber", telefax.Value);
            }
            if (telephone != null)
            {
                contactPoint.Add("telephone", telephone.Value);
            }
            if (websiteUri != null)
            {
                contactPoint.Add("url", websiteUri.Value);
            }
            
            _contractingParty.Add("address", address);
            _contractingParty.Add("contactPoint", contactPoint);

            return null;
        }

        // Mapeo del elemento parties.name de la entidad adjudicadora
        private JToken MapPartiesName(XElement[] toMap)
        {
            _contractingParty.Add("name", toMap[0].Value);
            return null;
        }

        // Mapeo del elemento parties.identifier de la entidad adjudicadora
        // Añade de manera adicional los elementos parties.id y parties.roles
        private JToken MapPartiesIdentifier(XElement[] toMap)
        {
            JObject identifier = new JObject();

            if (toMap.Length == 1)
            {
                XAttribute attribute = toMap[0].FirstAttribute;
                if (attribute != null)
                {
                    if (attribute.Value.Equals("DIR3"))
                    {
                        identifier.Add("scheme", "ES-DIR3");
                    }
                    else if (attribute.Value.Equals("NIF"))
                    {
                        identifier.Add("scheme", "ES-RMC");
                    }
                    else
                    {
                        _Log(this, $"Identifier {attribute.Value} not recognized", Level.Warn);
                    }
                    identifier.Add("id", toMap[0].Value);
                }
                else
                {
                    _Log(this, $"Identifier without scheme", Level.Warn);
                }
            }
            else
            {
                XAttribute id1, id2;
                id1 = toMap[0].Elements().First().FirstAttribute;
                id2 = toMap[1].Elements().First().FirstAttribute;
                if (id1 != null)
                {
                    if (id1.Value.Equals("DIR3"))
                    {
                        identifier.Add("scheme", "ES-DIR3");
                    }
                    else if (id1.Value.Equals("NIF"))
                    {
                        identifier.Add("scheme", "ES-RMC");
                    }
                    else
                    {
                        _Log(this, $"Identifier {id1.Value} not recognized", Level.Warn);
                    }
                    identifier.Add("id", toMap[0].Value);
                }
                else
                {
                    _Log(this, $"Identifier without scheme", Level.Warn);
                }
                if (id2 != null)
                {
                    JObject additionalIdentifier = new JObject();
                    if (id2.Value.Equals("DIR3"))
                    {
                        additionalIdentifier.Add("scheme", "ES-DIR3");
                    }
                    else if (id2.Value.Equals("NIF"))
                    {
                        additionalIdentifier.Add("scheme", "ES-RMC");
                    }
                    else
                    {
                        _Log(this, $"Identifier {id2.Value} not recognized", Level.Warn);
                    }
                    additionalIdentifier.Add("id", toMap[1].Value);
                    _contractingParty.Add("additionalIdentifiers", new JArray(additionalIdentifier));
                }
            }

            _contractingParty.Add("identifier", identifier);
            _contractingParty.Add("id", identifier["id"]);
            _contractingParty.Add("roles", new JArray("procuringEntity"));

            return null;
        }

        // Mapeo del elemento planning.budget.amount
        private JToken MapPlanningBudgetAmount(XElement[] toMap)
        {
            XElement element = toMap[0];
            if (element.FirstAttribute == null || element.FirstAttribute.Value != "EUR")
            {
                _Log(this, $"Mapping attribute code {element.FirstAttribute.Value} at element planning.budget.amount unrecognized", Level.Warn);
                return null;
            }
            return new JObject(new JProperty("amount", Convert.ToDouble(element.Value, System.Globalization.CultureInfo.InvariantCulture)), new JProperty("currency", "EUR"));
        }

        // Mapeo del elemento tender.items.classification
        private JToken MapTenderItemsClassification(XElement[] toMap)
        {
            string code;
            JObject item, classification;

            item = (JObject) _items.First;

            if (toMap.Length == 1)
            {
                if (item == null)
                {
                    item = SimulateElement("item", 1);
                    SimulateElement("lot", 1);
                }

                classification = new JObject();

                classification.Add("id", toMap[0].Value);
                classification.Add("scheme", "CPV");

                code = Parser.GetCodeValue("CPV", toMap[0].Value);
                if (code == null)
                {
                    _Log(this, $"CPV element {toMap[0].Value} unrecognized", Level.Warn);
                }
                classification.Add("description_es", code);

                item.Add("classification", classification);
            }
            else
            {
                int count = 1;
                XElement procurementProject, requiredCommodityClassification, itemClassificationCode;
                foreach (XElement awardElement in toMap)
                {
                    if (item == null)
                    {
                        item = SimulateElement("item", count);
                        SimulateElement("lot", count++);
                    }

                    classification = new JObject();

                    procurementProject = Parser.GetSpecificElement(awardElement, "ProcurementProject");
                    if (procurementProject != null)
                    {
                        requiredCommodityClassification = Parser.GetSpecificElement(procurementProject, "RequiredCommodityClassification");
                        if (requiredCommodityClassification != null)
                        {
                            itemClassificationCode = Parser.GetSpecificElement(requiredCommodityClassification, "ItemClassificationCode");
                            if (itemClassificationCode != null)
                            {
                                classification.Add("id", itemClassificationCode.Value);
                                classification.Add("scheme", "CPV");

                                code = Parser.GetCodeValue("CPV", itemClassificationCode.Value);
                                if (code == null)
                                {
                                    _Log(this, $"CPV element {itemClassificationCode.Value} unrecognized", Level.Warn);
                                }
                                classification.Add("description_es", code);

                                item.Add("classification", classification);
                            }
                        }
                    }

                    item = (JObject) item.Next;
                }
            }
            return null;
        }

        // Mapeo del elemento tender.lots.id
        // Añade de manera adicional los elementos tender.items.id y tender.items.relatedLot
        private JToken MapTenderLotsId(XElement[] toMap)
        {
            JObject item, lot;

            if (toMap.Length == 1)
            {
                item = new JObject();
                lot = new JObject();

                item.Add("id", toMap[0].Value);
                item.Add("relatedLot", $"lot-{toMap[0].Value}");
                _items.Add(item);

                lot.Add("id", $"lot-{toMap[0].Value}");
                _lots.Add(lot);
            }
            else
            {
                XElement id;
                foreach (XElement awardElement in toMap)
                {
                    item = new JObject();
                    lot = new JObject();

                    id = Parser.GetSpecificElement(awardElement, "ID");
                    
                    if (id != null)
                    {
                        item.Add("id", id.Value);
                        item.Add("relatedLot", $"lot-{id.Value}");
                        _items.Add(item);

                        lot.Add("id", $"lot-{id.Value}");
                        _lots.Add(lot);
                    }
                }
            }
            return null;
        }

        // Mapeo del elemento tender.lots.name
        private JToken MapTenderLotsName(XElement[] toMap)
        {
            JObject lot;

            lot = (JObject) _lots.First;

            if (toMap.Length == 1)
            {
                if (lot == null)
                {
                    lot = SimulateElement("lot", 1);
                    SimulateElement("item", 1);
                }

                lot.Add("title", toMap[0].Value);
            }
            else
            {
                int count = 1;
                XElement procurementProject, name;
                foreach (XElement awardElement in toMap)
                {
                    if (lot == null)
                    {
                        lot = SimulateElement("lot", count);
                        SimulateElement("item", count++);
                    }

                    procurementProject = Parser.GetSpecificElement(awardElement, "ProcurementProject");

                    if (procurementProject != null)
                    {
                        name = Parser.GetSpecificElement(procurementProject, "Name");
                        if (name != null)
                        {
                            lot.Add("title", name.Value);
                        }
                    }
                    
                    lot = (JObject) lot.Next;
                }
            }
            return null;
        }

        // Mapeo del elemento tender.lots.value
        private JToken MapTenderLotsValue(XElement[] toMap)
        {
            JObject lot, value;

            lot = (JObject) _lots.First;

            if (toMap.Length == 1)
            {
                if (lot == null)
                {
                    lot = SimulateElement("lot", 1);
                    SimulateElement("item", 1);
                }

                value = new JObject();

                value.Add("amount", Convert.ToDouble(toMap[0].Value, System.Globalization.CultureInfo.InvariantCulture));
                value.Add("currency", "EUR");

                lot.Add("value", value);
            }
            else
            {
                int count = 1;
                XElement procurementProject, budgetAmount, totalAmount;
                foreach (XElement awardElement in toMap)
                {
                    if (lot == null)
                    {
                        lot = SimulateElement("lot", count);
                        SimulateElement("item", count++);
                    }
                    value = new JObject();

                    procurementProject = Parser.GetSpecificElement(awardElement, "ProcurementProject");
                    if (procurementProject != null)
                    {
                        budgetAmount = Parser.GetSpecificElement(procurementProject, "BudgetAmount");
                        if (budgetAmount != null)
                        {
                            totalAmount = Parser.GetSpecificElement(budgetAmount, "TotalAmount");
                            if (totalAmount != null)
                            {
                                value.Add("amount", Convert.ToDouble(totalAmount.Value, System.Globalization.CultureInfo.InvariantCulture));
                                value.Add("currency", "EUR");

                                lot.Add("value", value);
                            }
                        }
                    }

                    lot = (JObject) lot.Next;
                }
            }
            return null;
        }

        // Mapeo del elemento tender.mainProcurementCategory
        private JToken MapTenderMainProcurementCategory(XElement[] toMap)
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
                _Log(this, $"Mapping code {element.Value} at element tender.mainProcurementCategory unrecognized", Level.Warn);
                return null;
            }

            return new JValue(mapped);
        }

        // Mapeo del elemento tender.numberOfTenderers
        private JToken MapTenderNumberOfTenderers(XElement[] toMap)
        {
            if (toMap.Length == 1)
            {
                return new JValue(Convert.ToInt32(toMap[0].Value));
            }
            return null;
        }

        // Mapeo del elemento tender.procurementMethod
        private JToken MapTenderProcurementMethod(XElement[] toMap)
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
                _Log(this, $"Mapping code {element.Value} at element tender.procurementMethod unrecognized", Level.Warn);
                    return null;
            }

            return new JValue(mapped);
        }

        // Mapeo del elemento tender.procurementMethodDetails
        private JToken MapTenderProcurementMethodDetails(XElement[] toMap)
        {
            XElement element = toMap[0];
            if (!element.Value.Equals("0"))
            {
                string mapped = Parser.GetCodeValue("ContractingSTC", element.Value);
                if (mapped == null)
                {
                    _Log(this, $"Mapping code {element.Value} at element tender.procurementMethodDetails unrecognized", Level.Warn);
                    return null;
                }
                return new JValue(mapped);
            }
            else
            {
                return null;
            }
        }

        // Mapeo del elemento tender.submissionMethod
        private JToken MapTenderSubmissionMethod(XElement[] toMap)
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
                    _Log(this, $"Mapping code {element.Value} at element tender.submissionMethod unrecognized", Level.Warn);
                    return null;
                }
            }

            return new JArray(mapped);
        }

        // Mapeo del elemento tender.submissionMethodDetails
        private JToken MapTenderSubmissionMethodDetails(XElement[] toMap)
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
        private JToken MapTenderTenderPeriodStartDate(XElement[] toMap)
        {
            return new JValue($"{toMap[0].Value}T00:00:00Z");
        }

        // Mapeo del elemento tender.tenderPeriod.endDate
        private JToken MapTenderTenderPeriodEndDate(XElement[] toMap)
        {
            return new JValue($"{toMap[0].Value}T00:00:00Z");
        }

        // Mapeo del elemento tender.tenderPeriod.durationInDays
        private JToken MapTenderTenderPeriodDurationInDays(XElement[] toMap)
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
            else if (element.FirstAttribute.Value.Equals("DAY"))
            {
                return new JValue(Convert.ToInt32(element.Value));
            }
            else
            {
                _Log(this, $"Mapping attribute code {element.FirstAttribute.Value} at element tender.tenderPeriod.durationInDays unrecognized", Level.Warn);
                return null;
            }
        }

        // Mapeo del elemento tender.title
        private JToken MapTenderTitle(XElement[] toMap)
        {
            return new JValue(toMap[0].Value);
        }
    
        // Mapeo del elemento tender.value
        private JToken MapTenderValue(XElement[] toMap)
        {
            XElement element = toMap[0];
            if (element.FirstAttribute == null || element.FirstAttribute.Value != "EUR")
            {
                _Log(this, $"Mapping attribute code {element.FirstAttribute.Value} at element tender.value unrecognized", Level.Warn);
                return null;
            }
            return new JObject(new JProperty("amount", Convert.ToDouble(element.Value, System.Globalization.CultureInfo.InvariantCulture)), new JProperty("currency", "EUR"));
        }
    }
}
