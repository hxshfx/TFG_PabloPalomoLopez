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

        /*  atributo _awards => JArray
         *      Almacén temporal del objeto representando la colección de adjudicaciones
         */
        private readonly JArray _awards;

        /*  atributo _contractingParty => JObject
         *      Almacén temporal del objeto representando la entidad adjudicadora
         */
        private readonly JObject _contractingParty;

        /*  atributo _supplierParties => JArray
         *      Almacén temporal de los objetos representando las entidades adjudicatarias
         */
        private readonly JArray _supplierParties;


        /* Constructor */

        // @param Log : Puntero a la función de logging
        public Mapper(Action<object, string, Level> Log)
        {
            _Log = Log;

            // Inicializa el objeto JSON en el que se mapeará la información
            MappedEntry = new JObject();

            // Inicializa el array JSON para almacenar temporalmente la colección de adjudicaciones
            _awards = new JArray();

            // Inicializa el objeto JSON para almacenar temporalmente la entidad adjudicadora
            _contractingParty = new JObject();

            // Inicializa el objeto JSON para almacenar temporalmente las entidades adjudicatarias
            _supplierParties = new JArray();

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
                _Log(this, e.StackTrace, Level.Fatal);
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


        /*  función Commit() => void
         *      Introduce los cambios al JSON que no se pueden introducir
         *      mediante los mapeos unitarios de elementos
         */
        public void Commit()
        {
            if (_awards.Any())
            {
                MappedEntry.Add("awards", _awards);
            }

            JArray parties = new JArray();
            if (_contractingParty.Count != 0)
            {
                parties.Add(_contractingParty);
            }
            foreach (var supplierParty in _supplierParties)
            {
                parties.Add(supplierParty);
            }

            if (parties.Any())
            {
                MappedEntry.Add("parties", parties);
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
        private JToken GetElementContainerDepth1(IEnumerator<string> pathEnumerator, XElement[] parsedElement)
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
                case Mappings.MappingElement.Party:
                    mappingFunction = MapPartyFields;
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
        private JToken GetElementContainerDepth2(IEnumerator<string> pathEnumerator, XElement[] parsedElement)
        {
            // Establece el puntero a la función de mapeo
            Func<XElement[], JToken> mappingFunction = null;

            // Avanza el enumerador y dirige la función de mapeo
            pathEnumerator.MoveNext();
            string path = pathEnumerator.Current;
            switch (path)
            {
                case Mappings.MappingElement.Award:
                    pathEnumerator.MoveNext();
                    path = pathEnumerator.Current;
                    switch (path)
                    {
                        case Mappings.MappingElement.Awards.Date:
                            mappingFunction = MapAwardDate;
                            break;
                        case Mappings.MappingElement.Awards.Description:
                            mappingFunction = MapAwardDescription;
                            break;
                        case Mappings.MappingElement.Awards.Id:
                            mappingFunction = MapAwardId;
                            break;
                        case Mappings.MappingElement.Awards.Status:
                            mappingFunction = MapAwardStatus;
                            break;
                        case Mappings.MappingElement.Awards.Suppliers:
                            mappingFunction = MapAwardSuppliers;
                            break;
                        case Mappings.MappingElement.Awards.Value:
                            mappingFunction = MapAwardValue;
                            break;
                        default:
                            throw new WrongMappingException(path);
                    }
                    break;
                case Mappings.MappingElement.Party:
                    pathEnumerator.MoveNext();
                    path = pathEnumerator.Current;
                    switch (path)
                    {
                        case Mappings.MappingElement.Parties.Identifier:
                            mappingFunction = MapPartiesIdentifier;
                            break;
                        case Mappings.MappingElement.Parties.Name:
                            mappingFunction = MapPartiesName;
                            break;
                        default:
                            throw new WrongMappingException(path);
                    }
                    break;
                case Mappings.MappingElement.Tender:
                    pathEnumerator.MoveNext();
                    path = pathEnumerator.Current;
                    switch (path)
                    {
                        case Mappings.MappingElement.Tenders.MainProcurementCategory:
                            mappingFunction = MapTenderMainProcurementCategory;
                            break;
                        case Mappings.MappingElement.Tenders.NumberOfTenderers:
                            mappingFunction = MapTenderNumberOfTenderers;
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
        private JToken GetElementContainerDepth3(IEnumerator<string> pathEnumerator, XElement[] parsedElement)
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


        /*  función SimulateElement(int) => JObject
         *      Simula la creación de un elemento para el testing que
         *      en condiciones normales se habría creado ya
         *  @return : elemento creado
         */
        private JObject SimulateElement(int id)
        {
            JObject award = new JObject();
            award.Add("id", $"{id}");
            _awards.Add(award);

            return award;
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
            return new JValue($"ES-{toMap[0].Value}");
        }

        // Mapeo de los elementos awards[i].date
        private JToken MapAwardDate(XElement[] toMap)
        {
            JObject award;

            award = (JObject) _awards.First;

            if (toMap.Length == 1)
            {
                if (award == null)
                {
                    award = SimulateElement(1);
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
                        award = SimulateElement(count++);
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

        // Mapeo de los elementos awards[i].description
        private JToken MapAwardDescription(XElement[] toMap)
        {
            JObject award;

            award = (JObject) _awards.First;

            if (toMap.Length == 1)
            {
                if (award == null)
                {
                    award = SimulateElement(1);
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
                        award = SimulateElement(count++);
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

        // Mapeo de los elementos awards[i].id
        private JToken MapAwardId(XElement[] toMap)
        {
            JObject award;

            if (toMap.Length == 1)
            {
                award = new JObject();
                award.Add("id", "1");
                _awards.Add(award);
            }
            else
            {
                XElement awardedTenderedProject, procurementProjectLotID;
                foreach (XElement awardElement in toMap)
                {
                    award = new JObject();

                    awardedTenderedProject = Parser.GetSpecificElement(awardElement, "AwardedTenderedProject");
                    procurementProjectLotID = Parser.GetSpecificElement(awardedTenderedProject, "ProcurementProjectLotID");

                    if (procurementProjectLotID != null)
                    {
                        award.Add("id", procurementProjectLotID.Value);
                        _awards.Add(award);
                    }
                }
            }
            return null;
        }

        // Mapeo de los elementos awards[i].status
        private JToken MapAwardStatus(XElement[] toMap)
        {
            JObject award = (JObject) _awards.First;

            if (toMap.Length == 1)
            {
                if (award == null)
                {
                    award = SimulateElement(1);
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
                        award = SimulateElement(count++);
                    }

                    awardedTenderedProject = Parser.GetSpecificElement(awardElement, "AwardedTenderedProject");
                    procurementProjectLotID = Parser.GetSpecificElement(awardedTenderedProject, "ProcurementProjectLotID");
                    resultCode = Parser.GetSpecificElement(awardElement, "ResultCode");

                    if (procurementProjectLotID != null && !procurementProjectLotID.Value.Equals(award.First.First.ToString()))
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

        // Mapeo de los elementos awards[i].suppliers
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
                    award = SimulateElement(count++);
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
                        party.Add("id", partyIdentification.Value);
                        party.Add("name", partyName.Value);
                        party.Add("roles", new JArray("supplier"));

                        supplier.Add("id", partyIdentification.Value);
                        supplier.Add("name", partyName.Value);

                        _supplierParties.Add(party);
                        award.Add("suppliers", new JArray(supplier));
                    }
                }
                award = (JObject) award.Next;
            }
            return null;
        }

        // Mapeo de los elementos awards[i].value
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
                    award = SimulateElement(1);
                }
                
                payableAmount = Parser.GetSpecificElement(toMap[0], "PayableAmount");

                value.Add("amount", payableAmount.Value);
                value.Add("currency", "EUR");

                award.Add("value", value);
            }
            else
            {
                int count = 1;
                XElement awardedTenderedProject, legalMonetaryTotal;
                foreach (XElement awardElement in toMap)
                {
                    if (award == null)
                    {
                        award = SimulateElement(count++);
                    }

                    value = new JObject();

                    awardedTenderedProject = Parser.GetSpecificElement(awardElement, "AwardedTenderedProject");
                    legalMonetaryTotal = Parser.GetSpecificElement(awardedTenderedProject, "LegalMonetaryTotal");
                    
                    if (legalMonetaryTotal != null)
                    {
                        payableAmount = Parser.GetSpecificElement(legalMonetaryTotal, "PayableAmount");
                        if (payableAmount != null)
                        {
                            value.Add("amount", payableAmount.Value);
                            value.Add("currency", "EUR");

                            award.Add("value", value);
                            award = (JObject) award.Next;
                        }
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
            postalZone = Parser.GetSpecificElement(postalAddress, "PostalZone");

            if (addressLine != null)
            {
                address.Add("streetAddress", addressLine.Value);
            }
            if (cityName != null)
            {
                address.Add("locality", cityName.Value);
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

            identificationCode = Parser.GetSpecificElement(countryName, "IdentificationCode");

            if (identificationCode != null)
            {
                _contractingParty.Add("countryName", identificationCode.Value);
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
                    identifier.Add("additionalIdentifiers", new JArray(additionalIdentifier));
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
            return new JObject(new JProperty("amount", element.Value), new JProperty("currency", "EUR"));
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
                return new JValue(toMap[0].Value);
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
                string mapped = Parser.GetCodeValue("https://contrataciondelestado.es/codice/cl/2.08/ContractingSystemTypeCode-2.08.gc", element.Value);
                if (mapped == null)
                {
                    _Log(this, $"Mapping code {element.Value} at element tender.procurementMethodDetails unrecognized", Level.Warn);
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
            }
            return new JObject(new JProperty("amount", element.Value), new JProperty("currency", "EUR"));
        }
    }
}
