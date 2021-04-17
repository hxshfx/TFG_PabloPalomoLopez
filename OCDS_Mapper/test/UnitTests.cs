using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using OCDS_Mapper.src.Exceptions;
using OCDS_Mapper.src.Interfaces;
using OCDS_Mapper.src.Model;
using Xunit;


namespace OCDS_Mapper.test
{
    /* Clase de tests unitarios */

    public class UnitTests
    {
        /* Tests unitarios del componente de mapeo */

        public class MapperTests
        {
            private IMapper _mapper;

            public MapperTests()
            {
                Program.InitLogger();
                _mapper = new Mapper(Program.Log);
            }

            [Fact]
            public void TestConstructor()
            {
                Assert.NotNull(_mapper.MappedEntry);
                Assert.IsType<JObject>(_mapper.MappedEntry);
                Assert.True("{}".Equals(_mapper.MappedEntry.ToString()));
            }

            public class TagTests
            {
                private IMapper _mapper;

                public TagTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestTag1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tag"
                    });
                    string parsedElement = "PRE";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tag = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tag".Equals(tag.Name));
                    Assert.Single(tag);
                    Assert.NotNull(tag.First);
                    Assert.IsType<JArray>(tag.First);

                    JArray tagArray = (JArray) tag.First;

                    Assert.Single(tagArray);
                    Assert.NotNull(tagArray.First);
                    Assert.IsType<JValue>(tagArray.First);

                    JValue tagValue = (JValue) tagArray.First;

                    Assert.True("planning".Equals(tagValue.Value));
                }

                [Fact]
                public void TestTag2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tag"
                    });
                    string parsedElement = "PUB";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tag = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tag".Equals(tag.Name));
                    Assert.Single(tag);
                    Assert.NotNull(tag.First);
                    Assert.IsType<JArray>(tag.First);

                    JArray tagArray = (JArray) tag.First;

                    Assert.Single(tagArray);
                    Assert.NotNull(tagArray.First);
                    Assert.IsType<JValue>(tagArray.First);

                    JValue tagValue = (JValue) tagArray.First;

                    Assert.True("tender".Equals(tagValue.Value));

                    parsedElement = "EV";
                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.Last);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.Last);

                    tag = (JProperty) _mapper.MappedEntry.Last;

                    Assert.True("tag".Equals(tag.Name));
                    Assert.Single(tag);
                    Assert.NotNull(tag.First);
                    Assert.IsType<JArray>(tag.First);

                    tagArray = (JArray) tag.First;

                    Assert.Single(tagArray);
                    Assert.NotNull(tagArray.First);
                    Assert.IsType<JValue>(tagArray.First);

                    tagValue = (JValue) tagArray.First;

                    Assert.True("tender".Equals(tagValue.Value));
                }

                [Fact]
                public void TestTag3()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tag"
                    });
                    string parsedElement = "ADJ";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tag = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tag".Equals(tag.Name));
                    Assert.Single(tag);
                    Assert.NotNull(tag.First);
                    Assert.IsType<JArray>(tag.First);

                    JArray tagArray = (JArray) tag.First;

                    Assert.Single(tagArray);
                    Assert.NotNull(tagArray.First);
                    Assert.IsType<JValue>(tagArray.First);

                    JValue tagValue = (JValue) tagArray.First;

                    Assert.True("award".Equals(tagValue.Value));
                }

                [Fact]
                public void TestTag4()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tag"
                    });
                    string parsedElement = "RES";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tag = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tag".Equals(tag.Name));
                    Assert.Single(tag);
                    Assert.NotNull(tag.First);
                    Assert.IsType<JArray>(tag.First);

                    JArray tagArray = (JArray) tag.First;

                    Assert.Single(tagArray);
                    Assert.NotNull(tagArray.First);
                    Assert.IsType<JValue>(tagArray.First);

                    JValue tagValue = (JValue) tagArray.First;

                    Assert.True("contract".Equals(tagValue.Value));
                }

                [Fact]
                public void TestTag5()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tag"
                    });
                    string parsedElement = "ANUL";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tag = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tag".Equals(tag.Name));
                    Assert.Single(tag);
                    Assert.NotNull(tag.First);
                    Assert.IsType<JArray>(tag.First);

                    JArray tagArray = (JArray) tag.First;

                    Assert.Single(tagArray);
                    Assert.NotNull(tagArray.First);
                    Assert.IsType<JValue>(tagArray.First);

                    JValue tagValue = (JValue) tagArray.First;

                    Assert.True("awardCancellation".Equals(tagValue.Value));
                }

                [Fact]
                public void TestTag6()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tag"
                    });
                    string parsedElement = "wrong";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Empty(_mapper.MappedEntry);
                }
            }

            public class OCIDTests
            {
                private IMapper _mapper;
                
                public OCIDTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }
                
                [Fact]
                public void TestOCID()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "ocid"
                    });
                    string parsedElement = "someID";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty ocid = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("ocid".Equals(ocid.Name));
                    Assert.Single(ocid);
                    Assert.NotNull(ocid.First);
                    Assert.IsType<JValue>(ocid.First);

                    JValue ocidValue = (JValue) ocid.First;

                    Assert.True("ES-someID".Equals(ocidValue.Value));
                }
            }
            
            public class AwardDateTests
            {
                private IMapper _mapper;

                public AwardDateTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestAwardDate1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "awards",
                        "date"
                    });
                    string elementDump = "<cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-02-25</cbc:AwardDate>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("awards".Equals(awards.Name));
                    Assert.Single(awards);
                    Assert.NotNull(awards.First);
                    Assert.IsType<JArray>(awards.First);

                    JArray awardsArray = (JArray) awards.First;

                    Assert.Single(awardsArray);
                    Assert.NotNull(awardsArray.First);
                    Assert.IsType<JObject>(awardsArray.First);

                    JObject awardsObject = (JObject) awardsArray.First;

                    Assert.True(awardsObject.Count == 2);
                    Assert.IsType<JProperty>(awardsObject.First);
                    Assert.IsType<JProperty>(awardsObject.Last);

                    JProperty idProperty = (JProperty) awardsObject.First;
                    JProperty dateProperty = (JProperty) awardsObject.Last;

                    Assert.True("id".Equals(idProperty.Name));
                    Assert.True("date".Equals(dateProperty.Name));
                    Assert.Single(idProperty);
                    Assert.Single(dateProperty);
                    Assert.NotNull(idProperty.First);
                    Assert.NotNull(dateProperty.First);
                    Assert.IsType<JValue>(idProperty.First);
                    Assert.IsType<JValue>(dateProperty.First);

                    JValue idValue = (JValue) idProperty.First;
                    JValue dateValue = (JValue) dateProperty.First;

                    Assert.True("1".Equals(idValue.Value));
                    Assert.True("2021-02-25T00:00:00Z".Equals(dateValue.Value));
                }

                [Fact]
                public void TestAwardDate2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "awards",
                        "date"
                    });
                    string elementDump1 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Mejor oferta</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";
                    string elementDump2 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Oferta más ventajosa\n</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump1), XElement.Parse(elementDump2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("awards".Equals(awards.Name));
                    Assert.Single(awards);
                    Assert.NotNull(awards.First);
                    Assert.IsType<JArray>(awards.First);

                    JArray awardsArray = (JArray) awards.First;

                    Assert.True(awardsArray.Count == 2);
                    Assert.NotNull(awardsArray.First);
                    Assert.NotNull(awardsArray.Last);
                    Assert.IsType<JObject>(awardsArray.First);
                    Assert.IsType<JObject>(awardsArray.Last);

                    JObject awardsObject1 = (JObject) awardsArray.First;
                    JObject awardsObject2 = (JObject) awardsArray.Last;

                    Assert.True(awardsObject1.Count == 2);
                    Assert.True(awardsObject2.Count == 2);
                    Assert.IsType<JProperty>(awardsObject1.First);
                    Assert.IsType<JProperty>(awardsObject2.First);
                    Assert.IsType<JProperty>(awardsObject1.Last);
                    Assert.IsType<JProperty>(awardsObject2.Last);

                    JProperty idProperty1 = (JProperty) awardsObject1.First;
                    JProperty idProperty2 = (JProperty) awardsObject2.First;
                    JProperty dateProperty1 = (JProperty) awardsObject1.Last;
                    JProperty dateProperty2 = (JProperty) awardsObject2.Last;

                    Assert.True("id".Equals(idProperty1.Name));
                    Assert.True("date".Equals(dateProperty1.Name));
                    Assert.True("id".Equals(idProperty2.Name));
                    Assert.True("date".Equals(dateProperty2.Name));
                    Assert.Single(idProperty1);
                    Assert.Single(dateProperty1);
                    Assert.Single(idProperty2);
                    Assert.Single(dateProperty2);
                    Assert.NotNull(idProperty1.First);
                    Assert.NotNull(dateProperty1.First);
                    Assert.NotNull(idProperty2.First);
                    Assert.NotNull(dateProperty2.First);
                    Assert.IsType<JValue>(idProperty1.First);
                    Assert.IsType<JValue>(dateProperty1.First);
                    Assert.IsType<JValue>(idProperty2.First);
                    Assert.IsType<JValue>(dateProperty2.First);

                    JValue idValue1 = (JValue) idProperty1.First;
                    JValue idValue2 = (JValue) idProperty2.First;
                    JValue dateValue1 = (JValue) dateProperty1.First;
                    JValue dateValue2 = (JValue) dateProperty2.First;

                    Assert.True("1".Equals(idValue1.Value));
                    Assert.True("2".Equals(idValue2.Value));
                    Assert.True("2021-04-15T00:00:00Z".Equals(dateValue1.Value));
                    Assert.True("2021-04-15T00:00:00Z".Equals(dateValue2.Value));
                }
            }

            public class AwardDescriptionTests
            {
                private IMapper _mapper;

                public AwardDescriptionTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestAwardDescription1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "awards",
                        "description_es"
                    });
                    string elementDump = "<cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Art. 131 Ley 9/2007: Oferta con mejor relación calidad – precio.</cbc:Description>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("awards".Equals(awards.Name));
                    Assert.Single(awards);
                    Assert.NotNull(awards.First);
                    Assert.IsType<JArray>(awards.First);

                    JArray awardsArray = (JArray) awards.First;

                    Assert.Single(awardsArray);
                    Assert.NotNull(awardsArray.First);
                    Assert.IsType<JObject>(awardsArray.First);

                    JObject awardsObject = (JObject) awardsArray.First;

                    Assert.True(awardsObject.Count == 2);
                    Assert.IsType<JProperty>(awardsObject.First);
                    Assert.IsType<JProperty>(awardsObject.Last);

                    JProperty idProperty = (JProperty) awardsObject.First;
                    JProperty descriptionProperty = (JProperty) awardsObject.Last;

                    Assert.True("id".Equals(idProperty.Name));
                    Assert.True("description_es".Equals(descriptionProperty.Name));
                    Assert.Single(idProperty);
                    Assert.Single(descriptionProperty);
                    Assert.NotNull(idProperty.First);
                    Assert.NotNull(descriptionProperty.First);
                    Assert.IsType<JValue>(idProperty.First);
                    Assert.IsType<JValue>(descriptionProperty.First);

                    JValue idValue = (JValue) idProperty.First;
                    JValue descriptionValue = (JValue) descriptionProperty.First;

                    Assert.True("1".Equals(idValue.Value));
                    Assert.True("Art. 131 Ley 9/2007: Oferta con mejor relación calidad – precio.".Equals(descriptionValue.Value));
                }

                [Fact]
                public void TestAwardDescription2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "awards",
                        "description_es"
                    });
                    string elementDump1 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Mejor oferta</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";
                    string elementDump2 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Oferta más ventajosa</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump1), XElement.Parse(elementDump2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("awards".Equals(awards.Name));
                    Assert.Single(awards);
                    Assert.NotNull(awards.First);
                    Assert.IsType<JArray>(awards.First);

                    JArray awardsArray = (JArray) awards.First;

                    Assert.True(awardsArray.Count == 2);
                    Assert.NotNull(awardsArray.First);
                    Assert.NotNull(awardsArray.Last);
                    Assert.IsType<JObject>(awardsArray.First);
                    Assert.IsType<JObject>(awardsArray.Last);

                    JObject awardsObject1 = (JObject) awardsArray.First;
                    JObject awardsObject2 = (JObject) awardsArray.Last;

                    Assert.True(awardsObject1.Count == 2);
                    Assert.True(awardsObject2.Count == 2);
                    Assert.IsType<JProperty>(awardsObject1.First);
                    Assert.IsType<JProperty>(awardsObject2.First);
                    Assert.IsType<JProperty>(awardsObject1.Last);
                    Assert.IsType<JProperty>(awardsObject2.Last);

                    JProperty idProperty1 = (JProperty) awardsObject1.First;
                    JProperty idProperty2 = (JProperty) awardsObject2.First;
                    JProperty descriptionProperty1 = (JProperty) awardsObject1.Last;
                    JProperty descriptionProperty2 = (JProperty) awardsObject2.Last;

                    Assert.True("id".Equals(idProperty1.Name));
                    Assert.True("description_es".Equals(descriptionProperty1.Name));
                    Assert.True("id".Equals(idProperty2.Name));
                    Assert.True("description_es".Equals(descriptionProperty2.Name));
                    Assert.Single(idProperty1);
                    Assert.Single(descriptionProperty1);
                    Assert.Single(idProperty2);
                    Assert.Single(descriptionProperty2);
                    Assert.NotNull(idProperty1.First);
                    Assert.NotNull(descriptionProperty1.First);
                    Assert.NotNull(idProperty2.First);
                    Assert.NotNull(descriptionProperty2.First);
                    Assert.IsType<JValue>(idProperty1.First);
                    Assert.IsType<JValue>(descriptionProperty1.First);
                    Assert.IsType<JValue>(idProperty2.First);
                    Assert.IsType<JValue>(descriptionProperty2.First);

                    JValue idValue1 = (JValue) idProperty1.First;
                    JValue idValue2 = (JValue) idProperty2.First;
                    JValue descriptionValue1 = (JValue) descriptionProperty1.First;
                    JValue descriptionValue2 = (JValue) descriptionProperty2.First;

                    Assert.True("1".Equals(idValue1.Value));
                    Assert.True("2".Equals(idValue2.Value));
                    Assert.True("Mejor oferta".Equals(descriptionValue1.Value));
                    Assert.True("Oferta más ventajosa".Equals(descriptionValue2.Value));
                }
            }

            public class AwardIdTests
            {
                private IMapper _mapper;

                public AwardIdTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestAwardId1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "awards",
                        "id"
                    });

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", "null") });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("awards".Equals(awards.Name));
                    Assert.Single(awards);
                    Assert.NotNull(awards.First);
                    Assert.IsType<JArray>(awards.First);

                    JArray awardsArray = (JArray) awards.First;

                    Assert.Single(awardsArray);
                    Assert.NotNull(awardsArray.First);
                    Assert.IsType<JObject>(awardsArray.First);

                    JObject awardsObject = (JObject) awardsArray.First;

                    Assert.Single(awardsObject);
                    Assert.IsType<JProperty>(awardsObject.First);

                    JProperty idProperty = (JProperty) awardsObject.First;

                    Assert.True("id".Equals(idProperty.Name));
                    Assert.Single(idProperty);
                    Assert.NotNull(idProperty.First);
                    Assert.IsType<JValue>(idProperty.First);

                    JValue idValue = (JValue) idProperty.First;

                    Assert.True("1".Equals(idValue.Value));
                }
            
                [Fact]
                public void TestAwardId2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "awards",
                        "id"
                    });
                    string elementDump1 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Mejor oferta</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";
                    string elementDump2 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Oferta más ventajosa\n</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump1), XElement.Parse(elementDump2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("awards".Equals(awards.Name));
                    Assert.Single(awards);
                    Assert.NotNull(awards.First);
                    Assert.IsType<JArray>(awards.First);

                    JArray awardsArray = (JArray) awards.First;

                    Assert.True(awardsArray.Count == 2);
                    Assert.NotNull(awardsArray.First);
                    Assert.NotNull(awardsArray.Last);
                    Assert.IsType<JObject>(awardsArray.First);
                    Assert.IsType<JObject>(awardsArray.Last);

                    JObject awardsObject1 = (JObject) awardsArray.First;
                    JObject awardsObject2 = (JObject) awardsArray.Last;

                    Assert.Single(awardsObject1);
                    Assert.Single(awardsObject2);
                    Assert.IsType<JProperty>(awardsObject1.First);
                    Assert.IsType<JProperty>(awardsObject2.First);

                    JProperty idProperty1 = (JProperty) awardsObject1.First;
                    JProperty idProperty2 = (JProperty) awardsObject2.First;

                    Assert.True("id".Equals(idProperty1.Name));
                    Assert.True("id".Equals(idProperty2.Name));
                    Assert.Single(idProperty1);
                    Assert.Single(idProperty2);
                    Assert.NotNull(idProperty1.First);
                    Assert.NotNull(idProperty2.First);
                    Assert.IsType<JValue>(idProperty1.First);
                    Assert.IsType<JValue>(idProperty2.First);

                    JValue idValue1 = (JValue) idProperty1.First;
                    JValue idValue2 = (JValue) idProperty2.First;

                    Assert.True("1".Equals(idValue1.Value));
                    Assert.True("2".Equals(idValue2.Value));
                }
            }

            public class AwardStatusTests
            {
                private IMapper _mapper;

                public AwardStatusTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestAwardStatus1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "awards",
                        "status"
                    });
                    string elementDump = "<cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("awards".Equals(awards.Name));
                    Assert.Single(awards);
                    Assert.NotNull(awards.First);
                    Assert.IsType<JArray>(awards.First);

                    JArray awardsArray = (JArray) awards.First;

                    Assert.Single(awardsArray);
                    Assert.NotNull(awardsArray.First);
                    Assert.IsType<JObject>(awardsArray.First);

                    JObject awardsObject = (JObject) awardsArray.First;

                    Assert.True(awardsObject.Count == 2);
                    Assert.IsType<JProperty>(awardsObject.First);
                    Assert.IsType<JProperty>(awardsObject.Last);

                    JProperty idProperty = (JProperty) awardsObject.First;
                    JProperty statusProperty = (JProperty) awardsObject.Last;

                    Assert.True("id".Equals(idProperty.Name));
                    Assert.True("status".Equals(statusProperty.Name));
                    Assert.Single(idProperty);
                    Assert.Single(statusProperty);
                    Assert.NotNull(idProperty.First);
                    Assert.NotNull(statusProperty.First);
                    Assert.IsType<JValue>(idProperty.First);
                    Assert.IsType<JValue>(statusProperty.First);

                    JValue idValue = (JValue) idProperty.First;
                    JValue statusValue = (JValue) statusProperty.First;

                    Assert.True("1".Equals(idValue.Value));
                    Assert.True("active".Equals(statusValue.Value));
                }
            
                [Fact]
                public void TestAwardStatus2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "awards",
                        "status"
                    });
                    string elementDump1 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Mejor oferta</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";
                    string elementDump2 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">7</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Oferta más ventajosa</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump1), XElement.Parse(elementDump2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("awards".Equals(awards.Name));
                    Assert.Single(awards);
                    Assert.NotNull(awards.First);
                    Assert.IsType<JArray>(awards.First);

                    JArray awardsArray = (JArray) awards.First;

                    Assert.True(awardsArray.Count == 2);
                    Assert.NotNull(awardsArray.First);
                    Assert.NotNull(awardsArray.Last);
                    Assert.IsType<JObject>(awardsArray.First);
                    Assert.IsType<JObject>(awardsArray.Last);

                    JObject awardsObject1 = (JObject) awardsArray.First;
                    JObject awardsObject2 = (JObject) awardsArray.Last;

                    Assert.True(awardsObject1.Count == 2);
                    Assert.True(awardsObject2.Count == 2);
                    Assert.IsType<JProperty>(awardsObject1.First);
                    Assert.IsType<JProperty>(awardsObject2.First);
                    Assert.IsType<JProperty>(awardsObject1.Last);
                    Assert.IsType<JProperty>(awardsObject2.Last);

                    JProperty idProperty1 = (JProperty) awardsObject1.First;
                    JProperty idProperty2 = (JProperty) awardsObject2.First;
                    JProperty statusProperty1 = (JProperty) awardsObject1.Last;
                    JProperty statusProperty2 = (JProperty) awardsObject2.Last;

                    Assert.True("id".Equals(idProperty1.Name));
                    Assert.True("status".Equals(statusProperty1.Name));
                    Assert.True("id".Equals(idProperty2.Name));
                    Assert.True("status".Equals(statusProperty2.Name));
                    Assert.Single(idProperty1);
                    Assert.Single(statusProperty1);
                    Assert.Single(idProperty2);
                    Assert.Single(statusProperty2);
                    Assert.NotNull(idProperty1.First);
                    Assert.NotNull(statusProperty1.First);
                    Assert.NotNull(idProperty2.First);
                    Assert.NotNull(statusProperty2.First);
                    Assert.IsType<JValue>(idProperty1.First);
                    Assert.IsType<JValue>(statusProperty1.First);
                    Assert.IsType<JValue>(idProperty2.First);
                    Assert.IsType<JValue>(statusProperty2.First);

                    JValue idValue1 = (JValue) idProperty1.First;
                    JValue idValue2 = (JValue) idProperty2.First;
                    JValue statusValue1 = (JValue) statusProperty1.First;
                    JValue statusValue2 = (JValue) statusProperty2.First;

                    Assert.True("1".Equals(idValue1.Value));
                    Assert.True("2".Equals(idValue2.Value));
                    Assert.True("pending".Equals(statusValue1.Value));
                    Assert.True("cancelled".Equals(statusValue2.Value));
                }
            }
        
            public class AwardSuppliersTests
            {
                private IMapper _mapper;

                public AwardSuppliersTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestAwardSuppliers()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "awards",
                        "suppliers"
                    });
                    string elementDump = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Mejor oferta</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();

                    Assert.True(_mapper.MappedEntry.Count == 2);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.NotNull(_mapper.MappedEntry.Last);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.Last);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("awards".Equals(awards.Name));
                    Assert.Single(awards);
                    Assert.NotNull(awards.First);
                    Assert.IsType<JArray>(awards.First);

                    JArray awardsArray = (JArray) awards.First;

                    Assert.Single(awardsArray);
                    Assert.NotNull(awardsArray.First);
                    Assert.IsType<JObject>(awardsArray.First);

                    JObject awardsObject = (JObject) awardsArray.First;

                    Assert.True(awardsObject.Count == 2);
                    Assert.IsType<JProperty>(awardsObject.First);
                    Assert.IsType<JProperty>(awardsObject.Last);

                    JProperty idProperty = (JProperty) awardsObject.First;
                    JProperty suppliersProperty = (JProperty) awardsObject.Last;

                    Assert.True("id".Equals(idProperty.Name));
                    Assert.True("suppliers".Equals(suppliersProperty.Name));
                    Assert.Single(idProperty);
                    Assert.Single(suppliersProperty);
                    Assert.NotNull(idProperty.First);
                    Assert.NotNull(suppliersProperty.First);
                    Assert.IsType<JValue>(idProperty.First);
                    Assert.IsType<JArray>(suppliersProperty.First);

                    JValue idValue = (JValue) idProperty.First;
                    JArray supplierArray = (JArray) suppliersProperty.First;

                    Assert.True("1".Equals(idValue.Value));
                    
                    Assert.Single(supplierArray);
                    Assert.NotNull(supplierArray.First);
                    Assert.IsType<JObject>(supplierArray.First);

                    JObject supplierObject = (JObject) supplierArray.First;

                    Assert.True(supplierObject.Count == 2);
                    Assert.IsType<JProperty>(supplierObject.First);
                    Assert.IsType<JProperty>(supplierObject.Last);

                    JProperty supplierIdProperty = (JProperty) supplierObject.First;
                    JProperty supplierNameProperty = (JProperty) supplierObject.Last;

                    Assert.True("id".Equals(supplierIdProperty.Name));
                    Assert.True("name".Equals(supplierNameProperty.Name));
                    Assert.Single(supplierIdProperty);
                    Assert.Single(supplierNameProperty);
                    Assert.NotNull(supplierIdProperty.First);
                    Assert.NotNull(supplierNameProperty.First);
                    Assert.IsType<JValue>(supplierIdProperty.First);
                    Assert.IsType<JValue>(supplierNameProperty.First);

                    JValue supplierIdValue = (JValue) supplierIdProperty.First;
                    JValue supplierNameValue = (JValue) supplierNameProperty.First;

                    Assert.True("A28885614".Equals(supplierIdValue.Value));
                    Assert.True("GRASAS DEL CENTRO, S.A".Equals(supplierNameValue.Value));

                    JProperty parties = (JProperty) _mapper.MappedEntry.Last;

                    Assert.True("parties".Equals(parties.Name));
                    Assert.Single(parties);
                    Assert.NotNull(parties.First);
                    Assert.IsType<JArray>(parties.First);

                    JArray partyArray = (JArray) parties.First;

                    Assert.Single(partyArray);
                    Assert.NotNull(partyArray.First);
                    Assert.IsType<JObject>(partyArray.First);

                    JObject partyObject = (JObject) partyArray.First;

                    Assert.True(partyObject.Count == 4);
                    Assert.IsType<JProperty>(partyObject.First);

                    JProperty identifierProperty = (JProperty) partyObject.First;

                    Assert.True("identifier".Equals(identifierProperty.Name));
                    Assert.Single(identifierProperty);
                    Assert.NotNull(identifierProperty.First);
                    Assert.IsType<JObject>(identifierProperty.First);

                    JObject identifierObject = (JObject) identifierProperty.First;

                    Assert.True(identifierObject.Count == 2);
                    Assert.IsType<JProperty>(identifierObject.First);
                    Assert.IsType<JProperty>(identifierObject.Last);

                    JProperty schemeProperty = (JProperty) identifierObject.First;
                    JProperty identifierIdProperty = (JProperty) schemeProperty.Next;

                    Assert.True("scheme".Equals(schemeProperty.Name));
                    Assert.True("ES-RMC".Equals(schemeProperty.Value.ToString()));

                    Assert.True("id".Equals(identifierIdProperty.Name));
                    Assert.True("A28885614".Equals(identifierIdProperty.Value.ToString()));

                    Assert.IsType<JProperty>(identifierProperty.Next);

                    JProperty partyIdProperty = (JProperty) identifierProperty.Next;

                    Assert.True("id".Equals(partyIdProperty.Name));
                    Assert.True("A28885614".Equals(partyIdProperty.Value.ToString()));

                    Assert.IsType<JProperty>(partyIdProperty.Next);

                    JProperty partyNameProperty = (JProperty) partyIdProperty.Next;

                    Assert.True("name".Equals(partyNameProperty.Name));
                    Assert.True("GRASAS DEL CENTRO, S.A".Equals(partyNameProperty.Value.ToString()));

                    Assert.IsType<JProperty>(partyNameProperty.Next);

                    JProperty rolesProperty = (JProperty) partyNameProperty.Next;

                    Assert.True("roles".Equals(rolesProperty.Name));
                    Assert.IsType<JArray>(rolesProperty.First);

                    JArray rolesArray = (JArray) rolesProperty.First;

                    Assert.Single(rolesArray);
                    Assert.NotNull(rolesArray.First);
                    Assert.IsType<JValue>(rolesArray.First);

                    JValue rolesValue = (JValue) rolesArray.First;

                    Assert.True("supplier".Equals(rolesValue.Value));
                }
            }

            public class AwardValueTests
            {
                private IMapper _mapper;

                public AwardValueTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestAwardValue1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "awards",
                        "value"
                    });
                    string elementDump = "<cac:LegalMonetaryTotal xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">543353.86</cbc:TaxExclusiveAmount>\n  <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">657458.17</cbc:PayableAmount>\n</cac:LegalMonetaryTotal>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("awards".Equals(awards.Name));
                    Assert.Single(awards);
                    Assert.NotNull(awards.First);
                    Assert.IsType<JArray>(awards.First);

                    JArray awardsArray = (JArray) awards.First;

                    Assert.Single(awardsArray);
                    Assert.NotNull(awardsArray.First);
                    Assert.IsType<JObject>(awardsArray.First);

                    JObject awardsObject = (JObject) awardsArray.First;

                    Assert.True(awardsObject.Count == 2);
                    Assert.IsType<JProperty>(awardsObject.First);
                    Assert.IsType<JProperty>(awardsObject.Last);
    
                    JProperty idProperty = (JProperty) awardsObject.First;
                    JProperty valueProperty = (JProperty) awardsObject.Last;

                    Assert.True("id".Equals(idProperty.Name));
                    Assert.True("value".Equals(valueProperty.Name));
                    Assert.Single(idProperty);
                    Assert.Single(valueProperty);
                    Assert.NotNull(idProperty.First);
                    Assert.NotNull(valueProperty.First);
                    Assert.IsType<JValue>(idProperty.First);
                    Assert.IsType<JObject>(valueProperty.First);

                    JValue idValue = (JValue) idProperty.First;

                    Assert.True("1".Equals(idValue.Value));

                    JObject valueObject = (JObject) valueProperty.First;

                    Assert.True(valueObject.Count == 2);
                    Assert.NotNull(valueObject.First);
                    Assert.NotNull(valueObject.Last);
                    Assert.IsType<JProperty>(valueObject.First);
                    Assert.IsType<JProperty>(valueObject.Last);

                    JProperty amountProperty = (JProperty) valueObject.First;
                    JProperty currencyProperty = (JProperty) valueObject.Last;

                    Assert.True("amount".Equals(amountProperty.Name));
                    Assert.True("currency".Equals(currencyProperty.Name));
                    Assert.Single(amountProperty);
                    Assert.Single(currencyProperty);
                    Assert.NotNull(amountProperty.First);
                    Assert.NotNull(currencyProperty.First);
                    Assert.IsType<JValue>(amountProperty.First);
                    Assert.IsType<JValue>(currencyProperty.First);

                    JValue amountValue = (JValue) amountProperty.First;
                    JValue currencyValue = (JValue) currencyProperty.First;

                    Assert.True("657458.17".Equals(amountValue.Value));
                    Assert.True("EUR".Equals(currencyValue.Value));
                }

                [Fact]
                public void TestAwardValue2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "awards",
                        "value"
                    });
                    string elementDump1 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Mejor oferta</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";
                    string elementDump2 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Oferta más ventajosa\n</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump1), XElement.Parse(elementDump2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("awards".Equals(awards.Name));
                    Assert.Single(awards);
                    Assert.NotNull(awards.First);
                    Assert.IsType<JArray>(awards.First);

                    JArray awardsArray = (JArray) awards.First;

                    Assert.True(awardsArray.Count == 2);
                    Assert.NotNull(awardsArray.First);
                    Assert.NotNull(awardsArray.Last);
                    Assert.IsType<JObject>(awardsArray.First);
                    Assert.IsType<JObject>(awardsArray.Last);

                    JObject awardsObject1 = (JObject) awardsArray.First;
                    JObject awardsObject2 = (JObject) awardsArray.Last;

                    Assert.True(awardsObject1.Count == 2);
                    Assert.True(awardsObject2.Count == 2);
                    Assert.IsType<JProperty>(awardsObject1.First);
                    Assert.IsType<JProperty>(awardsObject2.First);
                    Assert.IsType<JProperty>(awardsObject1.Last);
                    Assert.IsType<JProperty>(awardsObject2.Last);

                    JProperty idProperty1 = (JProperty) awardsObject1.First;
                    JProperty idProperty2 = (JProperty) awardsObject2.First;
                    JProperty valueProperty1 = (JProperty) awardsObject1.Last;
                    JProperty valueProperty2 = (JProperty) awardsObject2.Last;

                    Assert.True("id".Equals(idProperty1.Name));
                    Assert.True("value".Equals(valueProperty1.Name));
                    Assert.True("id".Equals(idProperty2.Name));
                    Assert.True("value".Equals(valueProperty2.Name));
                    Assert.Single(idProperty1);
                    Assert.Single(valueProperty1);
                    Assert.Single(idProperty2);
                    Assert.Single(valueProperty2);
                    Assert.NotNull(idProperty1.First);
                    Assert.NotNull(valueProperty1.First);
                    Assert.NotNull(idProperty2.First);
                    Assert.NotNull(valueProperty2.First);
                    Assert.IsType<JValue>(idProperty1.First);
                    Assert.IsType<JObject>(valueProperty1.First);
                    Assert.IsType<JValue>(idProperty2.First);
                    Assert.IsType<JObject>(valueProperty2.First);

                    JValue idValue1 = (JValue) idProperty1.First;
                    JValue idValue2 = (JValue) idProperty2.First;

                    Assert.True("1".Equals(idValue1.Value));
                    Assert.True("2".Equals(idValue2.Value));

                    JObject valueObject1 = (JObject) valueProperty1.First;
                    JObject valueObject2 = (JObject) valueProperty2.First;

                    Assert.True(valueObject1.Count == 2);
                    Assert.True(valueObject2.Count == 2);
                    Assert.NotNull(valueObject1.First);
                    Assert.NotNull(valueObject1.Last);
                    Assert.NotNull(valueObject2.First);
                    Assert.NotNull(valueObject2.Last);
                    Assert.IsType<JProperty>(valueObject1.First);
                    Assert.IsType<JProperty>(valueObject1.Last);
                    Assert.IsType<JProperty>(valueObject2.First);
                    Assert.IsType<JProperty>(valueObject2.Last);

                    JProperty amountProperty1 = (JProperty) valueObject1.First;
                    JProperty currencyProperty1 = (JProperty) valueObject1.Last;
                    JProperty amountProperty2 = (JProperty) valueObject2.First;
                    JProperty currencyProperty2 = (JProperty) valueObject2.Last;

                    Assert.True("amount".Equals(amountProperty1.Name));
                    Assert.True("currency".Equals(currencyProperty1.Name));
                    Assert.True("amount".Equals(amountProperty2.Name));
                    Assert.True("currency".Equals(currencyProperty2.Name));
                    Assert.Single(amountProperty1);
                    Assert.Single(currencyProperty1);
                    Assert.Single(amountProperty2);
                    Assert.Single(currencyProperty2);
                    Assert.NotNull(amountProperty1.First);
                    Assert.NotNull(currencyProperty1.First);
                    Assert.NotNull(amountProperty2.First);
                    Assert.NotNull(currencyProperty2.First);
                    Assert.IsType<JValue>(amountProperty1.First);
                    Assert.IsType<JValue>(currencyProperty1.First);
                    Assert.IsType<JValue>(amountProperty2.First);
                    Assert.IsType<JValue>(currencyProperty2.First);

                    JValue amountValue1 = (JValue) amountProperty1.First;
                    JValue currencyValue1 = (JValue) currencyProperty1.First;
                    JValue amountValue2 = (JValue) amountProperty2.First;
                    JValue currencyValue2 = (JValue) currencyProperty2.First;

                    Assert.True("48970.35".Equals(amountValue1.Value));
                    Assert.True("EUR".Equals(currencyValue1.Value));
                    Assert.True("48171.3".Equals(amountValue2.Value));
                    Assert.True("EUR".Equals(currencyValue2.Value));
                }
            }

            public class PartyFieldsTests
            {
                private IMapper _mapper;

                public PartyFieldsTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestPartyFields()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "parties"
                    });
                    string elementDump = "<cac:Party xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:WebsiteURI xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">https://lapalmaaguas.com</cbc:WebsiteURI>\n  <cac:PartyIdentification>\n    <cbc:ID schemeName=\"DIR3\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">L03380010</cbc:ID>\n  </cac:PartyIdentification>\n  <cac:PartyIdentification>\n    <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">P3800058D</cbc:ID>\n  </cac:PartyIdentification>\n  <cac:PartyName>\n    <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Presidencia del Consejo Insular de Aguas de La Palma</cbc:Name>\n  </cac:PartyName>\n  <cac:PostalAddress>\n    <cbc:CityName xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Santa Cruz de la Palma</cbc:CityName>\n    <cbc:PostalZone xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">38700</cbc:PostalZone>\n    <cac:AddressLine>\n      <cbc:Line xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Avda. Marítima 34</cbc:Line>\n    </cac:AddressLine>\n    <cac:Country>\n      <cbc:IdentificationCode listURI=\"http://docs.oasis-open.org/ubl/os-ubl-2.0/cl/gc/default/CountryIdentificationCode-2.0.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">ES</cbc:IdentificationCode>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">España</cbc:Name>\n    </cac:Country>\n  </cac:PostalAddress>\n  <cac:Contact>\n    <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Presidencia del Consejo Insular de Aguas de La Palma</cbc:Name>\n    <cbc:ElectronicMail xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">gabinete.presidencia@cablapalma.es</cbc:ElectronicMail>\n    <cbc:Telefax xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">918562480</cbc:Telefax>\n    <cbc:Telephone xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">918562479</cbc:Telephone>\n  </cac:Contact>\n</cac:Party>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty party = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("parties".Equals(party.Name));
                    Assert.Single(party);
                    Assert.NotNull(party.First);
                    Assert.IsType<JArray>(party.First);

                    JArray partyArray = (JArray) party.First;

                    Assert.Single(partyArray);
                    Assert.NotNull(partyArray.First);
                    Assert.IsType<JObject>(partyArray.First);

                    JObject partyObject = (JObject) partyArray.First;

                    Assert.True(partyObject.Count == 3);
                    Assert.IsType<JProperty>(partyObject.First);

                    JProperty countryProperty = (JProperty) partyObject.First;

                    Assert.True("countryName".Equals(countryProperty.Name));
                    Assert.Single(countryProperty);
                    Assert.NotNull(countryProperty.First);
                    Assert.IsType<JValue>(countryProperty.First);

                    JValue countryValue = (JValue) countryProperty.First;

                    Assert.True("ES".Equals(countryValue.Value));

                    Assert.IsType<JProperty>(countryProperty.Next);

                    JProperty addressProperty = (JProperty) countryProperty.Next;

                    Assert.Single(addressProperty);
                    Assert.NotNull(addressProperty.First);
                    Assert.IsType<JObject>(addressProperty.First);

                    JObject addressObject = (JObject) addressProperty.First;

                    Assert.True(addressObject.Count == 3);
                    Assert.IsType<JProperty>(addressObject.First);
                    
                    JProperty streetProperty = (JProperty) addressObject.First;

                    Assert.Single(streetProperty);
                    Assert.NotNull(streetProperty.First);
                    Assert.IsType<JValue>(streetProperty.First);

                    JValue streetValue = (JValue) streetProperty.First;

                    Assert.True("Avda. Marítima 34".Equals(streetValue.Value));
                    
                    Assert.IsType<JProperty>(streetProperty.Next);
                    
                    JProperty localityProperty = (JProperty) streetProperty.Next;

                    Assert.Single(localityProperty);
                    Assert.NotNull(localityProperty.First);
                    Assert.IsType<JValue>(localityProperty.First);

                    JValue localityValue = (JValue) localityProperty.First;

                    Assert.True("Santa Cruz de la Palma".Equals(localityValue.Value));
                    
                    Assert.IsType<JProperty>(localityProperty.Next);
                    
                    JProperty postalCodeProperty = (JProperty) localityProperty.Next;

                    Assert.Single(postalCodeProperty);
                    Assert.NotNull(postalCodeProperty.First);
                    Assert.IsType<JValue>(postalCodeProperty.First);

                    JValue postalCodeValue = (JValue) postalCodeProperty.First;

                    Assert.True("38700".Equals(postalCodeValue.Value));

                    Assert.IsType<JProperty>(addressProperty.Next);

                    JProperty contactProperty = (JProperty) addressProperty.Next;

                    Assert.Single(contactProperty);
                    Assert.NotNull(contactProperty.First);
                    Assert.IsType<JObject>(contactProperty.First);

                    JObject contactObject = (JObject) contactProperty.First;

                    Assert.True(contactObject.Count == 5);
                    Assert.IsType<JProperty>(contactObject.First);
                    
                    JProperty emailProperty = (JProperty) contactObject.First;

                    Assert.Single(emailProperty);
                    Assert.NotNull(emailProperty.First);
                    Assert.IsType<JValue>(emailProperty.First);

                    JValue emailValue = (JValue) emailProperty.First;

                    Assert.True("gabinete.presidencia@cablapalma.es".Equals(emailValue.Value));
                    
                    Assert.IsType<JProperty>(emailProperty.Next);
                    
                    JProperty nameProperty = (JProperty) emailProperty.Next;

                    Assert.Single(nameProperty);
                    Assert.NotNull(nameProperty.First);
                    Assert.IsType<JValue>(nameProperty.First);

                    JValue nameValue = (JValue) nameProperty.First;

                    Assert.True("Presidencia del Consejo Insular de Aguas de La Palma".Equals(nameValue.Value));
                    
                    Assert.IsType<JProperty>(nameProperty.Next);
                    
                    JProperty faxProperty = (JProperty) nameProperty.Next;

                    Assert.Single(faxProperty);
                    Assert.NotNull(faxProperty.First);
                    Assert.IsType<JValue>(faxProperty.First);

                    JValue faxValue = (JValue) faxProperty.First;

                    Assert.True("918562480".Equals(faxValue.Value));

                    Assert.IsType<JProperty>(nameProperty.Next);
                    
                    JProperty telephoneProperty = (JProperty) faxProperty.Next;

                    Assert.Single(telephoneProperty);
                    Assert.NotNull(telephoneProperty.First);
                    Assert.IsType<JValue>(telephoneProperty.First);

                    JValue telephoneValue = (JValue) telephoneProperty.First;

                    Assert.True("918562479".Equals(telephoneValue.Value));

                    Assert.IsType<JProperty>(telephoneProperty.Next);
                    
                    JProperty urlProperty = (JProperty) telephoneProperty.Next;

                    Assert.Single(urlProperty);
                    Assert.NotNull(urlProperty.First);
                    Assert.IsType<JValue>(urlProperty.First);

                    JValue urlValue = (JValue) urlProperty.First;

                    Assert.True("https://lapalmaaguas.com".Equals(urlValue.Value));
                }
            }

            public class PartiesNameTests
            {
                private IMapper _mapper;

                public PartiesNameTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestPartiesName()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "parties",
                        "name"
                    });
                    string parsedElement = "ABCD";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty party = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("parties".Equals(party.Name));
                    Assert.Single(party);
                    Assert.NotNull(party.First);
                    Assert.IsType<JArray>(party.First);

                    JArray partyArray = (JArray) party.First;

                    Assert.Single(partyArray);
                    Assert.NotNull(partyArray.First);
                    Assert.IsType<JObject>(partyArray.First);

                    JObject partyObject = (JObject) partyArray.First;

                    Assert.Single(partyObject);
                    Assert.NotNull(partyObject.First);
                    Assert.IsType<JProperty>(partyObject.First);

                    JProperty nameProperty = (JProperty) partyObject.First;

                    Assert.True("name".Equals(nameProperty.Name));
                    Assert.Single(nameProperty);
                    Assert.NotNull(nameProperty.First);
                    Assert.IsType<JValue>(nameProperty.First);

                    JValue nameValue = (JValue) nameProperty.First;

                    Assert.True("ABCD".Equals(nameValue.Value));
                }
            }
            
            public class PartiesIdentifierTests
            {
                private IMapper _mapper;

                public PartiesIdentifierTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestPartiesIdentifier1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "parties",
                        "identifier"
                    });
                    string elementDump = "<cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">S4611001A</cbc:ID>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty party = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("parties".Equals(party.Name));
                    Assert.Single(party);
                    Assert.NotNull(party.First);
                    Assert.IsType<JArray>(party.First);

                    JArray partyArray = (JArray) party.First;

                    Assert.Single(partyArray);
                    Assert.NotNull(partyArray.First);
                    Assert.IsType<JObject>(partyArray.First);

                    JObject partyObject = (JObject) partyArray.First;

                    Assert.True(partyObject.Count == 3);
                    Assert.IsType<JProperty>(partyObject.First);

                    JProperty identifierProperty = (JProperty) partyObject.First;

                    Assert.True("identifier".Equals(identifierProperty.Name));
                    Assert.Single(identifierProperty);
                    Assert.NotNull(identifierProperty.First);
                    Assert.IsType<JObject>(identifierProperty.First);

                    JObject identifierObject = (JObject) identifierProperty.First;

                    Assert.True(identifierObject.Count == 2);
                    Assert.IsType<JProperty>(identifierObject.First);
                    Assert.IsType<JProperty>(identifierObject.Last);

                    JProperty schemeProperty = (JProperty) identifierObject.First;
                    JProperty identifierIdProperty = (JProperty) schemeProperty.Next;

                    Assert.True("scheme".Equals(schemeProperty.Name));
                    Assert.True("ES-RMC".Equals(schemeProperty.Value.ToString()));

                    Assert.True("id".Equals(identifierIdProperty.Name));
                    Assert.True("S4611001A".Equals(identifierIdProperty.Value.ToString()));

                    Assert.IsType<JProperty>(identifierProperty.Next);

                    JProperty idProperty = (JProperty) identifierProperty.Next;

                    Assert.True("id".Equals(idProperty.Name));
                    Assert.True("S4611001A".Equals(idProperty.Value.ToString()));

                    Assert.IsType<JProperty>(idProperty.Next);

                    JProperty rolesProperty = (JProperty) idProperty.Next;

                    Assert.True("roles".Equals(rolesProperty.Name));
                    Assert.IsType<JArray>(rolesProperty.First);

                    JArray rolesArray = (JArray) rolesProperty.First;

                    Assert.Single(rolesArray);
                    Assert.NotNull(rolesArray.First);
                    Assert.IsType<JValue>(rolesArray.First);

                    JValue rolesValue = (JValue) rolesArray.First;

                    Assert.True("procuringEntity".Equals(rolesValue.Value));
                }

                [Fact]
                public void TestPartiesIdentifier2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "parties",
                        "identifier"
                    });
                    string elementDump1 = "<cac:PartyIdentification xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ID schemeName=\"DIR3\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">L03380010</cbc:ID>\n</cac:PartyIdentification>";
                    string elementDump2 = "<cac:PartyIdentification xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">P3800058D</cbc:ID>\n</cac:PartyIdentification>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump1), XElement.Parse(elementDump2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty party = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("parties".Equals(party.Name));
                    Assert.Single(party);
                    Assert.NotNull(party.First);
                    Assert.IsType<JArray>(party.First);

                    JArray partyArray = (JArray) party.First;

                    Assert.Single(partyArray);
                    Assert.NotNull(partyArray.First);
                    Assert.IsType<JObject>(partyArray.First);

                    JObject partyObject = (JObject) partyArray.First;

                    Assert.True(partyObject.Count == 3);
                    Assert.IsType<JProperty>(partyObject.First);

                    JProperty identifierProperty = (JProperty) partyObject.First;

                    Assert.True("identifier".Equals(identifierProperty.Name));
                    Assert.Single(identifierProperty);
                    Assert.NotNull(identifierProperty.First);
                    Assert.IsType<JObject>(identifierProperty.First);

                    JObject identifierObject = (JObject) identifierProperty.First;

                    Assert.True(identifierObject.Count == 3);
                    Assert.IsType<JProperty>(identifierObject.First);
                    Assert.IsType<JProperty>(identifierObject.Last);

                    JProperty schemeProperty = (JProperty) identifierObject.First;
                    JProperty identifierIdProperty = (JProperty) schemeProperty.Next;
                    JProperty additionalIdentifiersProperty = (JProperty) identifierIdProperty.Next;

                    Assert.True("scheme".Equals(schemeProperty.Name));
                    Assert.True("ES-DIR3".Equals(schemeProperty.Value.ToString()));

                    Assert.True("id".Equals(identifierIdProperty.Name));
                    Assert.True("L03380010".Equals(identifierIdProperty.Value.ToString()));

                    Assert.Single(additionalIdentifiersProperty);
                    Assert.IsType<JArray>(additionalIdentifiersProperty.First);

                    JArray additionalIdentifiersArray = (JArray) additionalIdentifiersProperty.First;

                    Assert.Single(additionalIdentifiersArray);
                    Assert.NotNull(additionalIdentifiersArray.First);
                    Assert.IsType<JObject>(additionalIdentifiersArray.First);

                    JObject additionalIdentifiersObject = (JObject) additionalIdentifiersArray.First;

                    Assert.True(additionalIdentifiersObject.Count == 2);
                    Assert.IsType<JProperty>(additionalIdentifiersObject.First);
                    Assert.IsType<JProperty>(additionalIdentifiersObject.Last);

                    JProperty additionalSchemeProperty = (JProperty) additionalIdentifiersObject.First;
                    JProperty additionalIdProperty = (JProperty) additionalIdentifiersObject.Last;

                    Assert.True("scheme".Equals(additionalSchemeProperty.Name));
                    Assert.True("id".Equals(additionalIdProperty.Name));
                    Assert.Single(additionalSchemeProperty);
                    Assert.Single(additionalIdProperty);
                    Assert.NotNull(additionalSchemeProperty.First);
                    Assert.NotNull(additionalIdProperty.First);
                    Assert.IsType<JValue>(additionalSchemeProperty.First);
                    Assert.IsType<JValue>(additionalIdProperty.First);

                    JValue additionalSchemeValue = (JValue) additionalSchemeProperty.First;
                    JValue additionalIdValue = (JValue) additionalIdProperty.First;

                    Assert.True("ES-RMC".Equals(additionalSchemeValue.Value));
                    Assert.True("P3800058D".Equals(additionalIdValue.Value));

                    Assert.IsType<JProperty>(identifierProperty.Next);

                    JProperty idProperty = (JProperty) identifierProperty.Next;

                    Assert.True("id".Equals(idProperty.Name));
                    Assert.True("L03380010".Equals(idProperty.Value.ToString()));

                    Assert.IsType<JProperty>(idProperty.Next);

                    JProperty rolesProperty = (JProperty) idProperty.Next;

                    Assert.True("roles".Equals(rolesProperty.Name));
                    Assert.IsType<JArray>(rolesProperty.First);

                    JArray rolesArray = (JArray) rolesProperty.First;

                    Assert.Single(rolesArray);
                    Assert.NotNull(rolesArray.First);
                    Assert.IsType<JValue>(rolesArray.First);

                    JValue rolesValue = (JValue) rolesArray.First;

                    Assert.True("procuringEntity".Equals(rolesValue.Value));
                }
            }

            public class PlanningBudgetAmountTests
            {
                private IMapper _mapper;

                public PlanningBudgetAmountTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestPlanningBudgetAmount1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "planning",
                        "budget",
                        "amount"
                    });
                    string parsedElement = "189250";

                    XElement element = new XElement("null", parsedElement);
                    element.SetAttributeValue("null", "EUR");

                    _mapper.MapElement(pathMap, new XElement[]{ element });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty planning = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("planning".Equals(planning.Name));
                    Assert.Single(planning);
                    Assert.NotNull(planning.First);
                    Assert.IsType<JObject>(planning.First);

                    JObject planningBudgetObject = (JObject) planning.First;

                    Assert.Single(planningBudgetObject);
                    Assert.NotNull(planningBudgetObject.First);
                    Assert.IsType<JProperty>(planningBudgetObject.First);

                    JProperty planningBudgetProperty = (JProperty) planningBudgetObject.First;

                    Assert.Single(planningBudgetProperty);
                    Assert.NotNull(planningBudgetProperty.First);
                    Assert.IsType<JObject>(planningBudgetProperty.First);

                    JObject planningBudgetAmountObject = (JObject) planningBudgetProperty.First;

                    Assert.Single(planningBudgetAmountObject);
                    Assert.NotNull(planningBudgetAmountObject.First);
                    Assert.IsType<JProperty>(planningBudgetAmountObject.First);

                    JProperty planningBudgetAmountProperty = (JProperty) planningBudgetAmountObject.First;

                    Assert.Single(planningBudgetAmountProperty);
                    Assert.NotNull(planningBudgetAmountProperty.First);
                    Assert.IsType<JObject>(planningBudgetAmountProperty.First);

                    JObject planningBudgetAmountAmountObject = (JObject) planningBudgetAmountProperty.First;

                    Assert.True(planningBudgetAmountAmountObject.Count == 2);
                    Assert.NotNull(planningBudgetAmountAmountObject.First);
                    Assert.NotNull(planningBudgetAmountAmountObject.Last);
                    Assert.IsType<JProperty>(planningBudgetAmountAmountObject.First);
                    Assert.IsType<JProperty>(planningBudgetAmountAmountObject.Last);

                    JProperty amountProperty = (JProperty) planningBudgetAmountAmountObject.First;
                    JProperty currencyProperty = (JProperty) planningBudgetAmountAmountObject.Last;

                    Assert.True("amount".Equals(amountProperty.Name));
                    Assert.True("currency".Equals(currencyProperty.Name));
                    Assert.Single(amountProperty);
                    Assert.Single(currencyProperty);
                    Assert.NotNull(amountProperty.First);
                    Assert.NotNull(currencyProperty.First);
                    Assert.IsType<JValue>(amountProperty.First);
                    Assert.IsType<JValue>(currencyProperty.First);

                    JValue amountValue = (JValue) amountProperty.First;
                    JValue currencyValue = (JValue) currencyProperty.First;

                    Assert.True("189250".Equals(amountValue.Value));
                    Assert.True("EUR".Equals(currencyValue.Value));
                }

                [Fact]
                public void TestPlanningBudgetAmount2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "planning",
                        "budget",
                        "amount"
                    });
                    string parsedElement = "189250";

                    XElement element = new XElement("null", parsedElement);

                    _mapper.MapElement(pathMap, new XElement[]{ element });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);
                }

                [Fact]
                public void TestPlanningBudgetAmount3()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "planning",
                        "budget",
                        "amount"
                    });
                    string parsedElement = "189250";

                    XElement element = new XElement("null", parsedElement);
                    element.SetAttributeValue("null", "USD");

                    _mapper.MapElement(pathMap, new XElement[]{ element });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);
                }
            }

            public class TenderMainProcurementCategoryTests
            {
                private IMapper _mapper;

                public TenderMainProcurementCategoryTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestTenderMainProcurementCategory1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "mainProcurementCategory"
                    });
                    string parsedElement = "1";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    JProperty procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("mainProcurementCategory".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    JValue procurementValue = (JValue) procurementProperty.First;

                    Assert.True("goods".Equals(procurementValue.Value));
                }
            
                [Fact]
                public void TestTenderMainProcurementCategory2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "mainProcurementCategory"
                    });
                    string parsedElement = "2";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    JProperty procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("mainProcurementCategory".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    JValue procurementValue = (JValue) procurementProperty.First;

                    Assert.True("services".Equals(procurementValue.Value));

                    parsedElement = "21";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("mainProcurementCategory".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    procurementValue = (JValue) procurementProperty.First;

                    Assert.True("services".Equals(procurementValue.Value));

                    parsedElement = "22";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("mainProcurementCategory".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    procurementValue = (JValue) procurementProperty.First;

                    Assert.True("services".Equals(procurementValue.Value));
                }

                [Fact]
                public void TestTenderMainProcurementCategory3()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "mainProcurementCategory"
                    });
                    string parsedElement = "3";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    JProperty procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("mainProcurementCategory".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    JValue procurementValue = (JValue) procurementProperty.First;

                    Assert.True("works".Equals(procurementValue.Value));

                    parsedElement = "31";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("mainProcurementCategory".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    procurementValue = (JValue) procurementProperty.First;

                    Assert.True("works".Equals(procurementValue.Value));

                    parsedElement = "32";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("mainProcurementCategory".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    procurementValue = (JValue) procurementProperty.First;

                    Assert.True("works".Equals(procurementValue.Value));
                }

                [Fact]
                public void TestTenderMainProcurementCategory4()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "mainProcurementCategory"
                    });
                    string parsedElement = "null";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);
                }
            }

            public class TenderNumberOfTenderersTests
            {
                private IMapper _mapper;

                public TenderNumberOfTenderersTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestTenderNumberOfTenderers()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "numberOfTenderers"
                    });
                    string parsedElement = "1";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject notObject = (JObject) tender.First;

                    Assert.Single(notObject);
                    Assert.NotNull(notObject.First);
                    Assert.IsType<JProperty>(notObject.First);

                    JProperty notProperty = (JProperty) notObject.First;

                    Assert.True("numberOfTenderers".Equals(notProperty.Name));
                    Assert.Single(notProperty);
                    Assert.NotNull(notProperty.First);
                    Assert.IsType<JValue>(notProperty.First);

                    JValue notValue = (JValue) notProperty.First;

                    Assert.True("1".Equals(notValue.Value));
                }
            }

            public class TenderProcurementMethodTests
            {
                private IMapper _mapper;

                public TenderProcurementMethodTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestTenderProcurementMethod1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "procurementMethod"
                    });
                    string parsedElement = "1";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    JProperty procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("procurementMethod".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    JValue procurementValue = (JValue) procurementProperty.First;

                    Assert.True("open".Equals(procurementValue.Value));

                    parsedElement = "5";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("procurementMethod".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    procurementValue = (JValue) procurementProperty.First;

                    Assert.True("open".Equals(procurementValue.Value));

                    parsedElement = "6";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("procurementMethod".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    procurementValue = (JValue) procurementProperty.First;

                    Assert.True("open".Equals(procurementValue.Value));

                    parsedElement = "8";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("procurementMethod".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    procurementValue = (JValue) procurementProperty.First;

                    Assert.True("open".Equals(procurementValue.Value));

                    parsedElement = "9";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("procurementMethod".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    procurementValue = (JValue) procurementProperty.First;

                    Assert.True("open".Equals(procurementValue.Value));

                    parsedElement = "12";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("procurementMethod".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    procurementValue = (JValue) procurementProperty.First;

                    Assert.True("open".Equals(procurementValue.Value));

                    parsedElement = "13";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("procurementMethod".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    procurementValue = (JValue) procurementProperty.First;

                    Assert.True("open".Equals(procurementValue.Value));
                }

                [Fact]
                public void TestTenderProcurementMethod2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "procurementMethod"
                    });
                    string parsedElement = "2";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    JProperty procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("procurementMethod".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    JValue procurementValue = (JValue) procurementProperty.First;

                    Assert.True("selective".Equals(procurementValue.Value));

                    parsedElement = "7";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("procurementMethod".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    procurementValue = (JValue) procurementProperty.First;

                    Assert.True("selective".Equals(procurementValue.Value));
                }

                [Fact]
                public void TestTenderProcurementMethod3()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "procurementMethod"
                    });
                    string parsedElement = "3";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    JProperty procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("procurementMethod".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    JValue procurementValue = (JValue) procurementProperty.First;

                    Assert.True("limited".Equals(procurementValue.Value));

                    parsedElement = "4";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("procurementMethod".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    procurementValue = (JValue) procurementProperty.First;

                    Assert.True("limited".Equals(procurementValue.Value));

                    parsedElement = "10";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("procurementMethod".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    procurementValue = (JValue) procurementProperty.First;

                    Assert.True("limited".Equals(procurementValue.Value));

                    parsedElement = "11";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("procurementMethod".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    procurementValue = (JValue) procurementProperty.First;

                    Assert.True("limited".Equals(procurementValue.Value));

                    parsedElement = "100";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("procurementMethod".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    procurementValue = (JValue) procurementProperty.First;

                    Assert.True("limited".Equals(procurementValue.Value));
                }
            
                [Fact]
                public void TestTenderProcurementMethod4()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "procurementMethod"
                    });
                    string parsedElement = "null";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);
                }
            }

            public class TenderProcurementMethodDetailsTests
            {
                private IMapper _mapper;

                public TenderProcurementMethodDetailsTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestTenderProcurementMethodDetails1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "procurementMethodDetails_es"
                    });
                    string parsedElement = "3";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject procurementObject = (JObject) tender.First;

                    Assert.Single(procurementObject);
                    Assert.NotNull(procurementObject.First);
                    Assert.IsType<JProperty>(procurementObject.First);

                    JProperty procurementProperty = (JProperty) procurementObject.First;

                    Assert.True("procurementMethodDetails_es".Equals(procurementProperty.Name));
                    Assert.Single(procurementProperty);
                    Assert.NotNull(procurementProperty.First);
                    Assert.IsType<JValue>(procurementProperty.First);

                    JValue procurementValue = (JValue) procurementProperty.First;

                    Assert.True("Contrato basado en un Acuerdo Marco".Equals(procurementValue.Value));
                }

                [Fact]
                public void TestTenderProcurementMethodDetails2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "procurementMethodDetails_es"
                    });
                    string parsedElement = "0";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);
                }

                [Fact]
                public void TestTenderProcurementMethodDetails3()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "procurementMethodDetails_es"
                    });
                    string parsedElement = "null";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);
                }
            }

            public class TenderSubmissionMethodTests
            {
                private IMapper _mapper;

                public TenderSubmissionMethodTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestTenderProcurementMethod1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "submissionMethod"
                    });
                    string parsedElement = "1";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject submissionObject = (JObject) tender.First;

                    Assert.Single(submissionObject);
                    Assert.NotNull(submissionObject.First);
                    Assert.IsType<JProperty>(submissionObject.First);

                    JProperty submissionProperty = (JProperty) submissionObject.First;

                    Assert.True("submissionMethod".Equals(submissionProperty.Name));
                    Assert.Single(submissionProperty);
                    Assert.NotNull(submissionProperty.First);
                    Assert.IsType<JArray>(submissionProperty.First);

                    JArray submissionArray = (JArray) submissionProperty.First;

                    Assert.Single(submissionArray);
                    Assert.NotNull(submissionArray.First);
                    Assert.IsType<JValue>(submissionArray.First);

                    JValue submissionValue = (JValue) submissionArray.First;

                    Assert.True("electronicSubmission".Equals(submissionValue.Value));
                }

                [Fact]
                public void TestTenderProcurementMethod2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "submissionMethod"
                    });
                    string parsedElement = "2";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject submissionObject = (JObject) tender.First;

                    Assert.Single(submissionObject);
                    Assert.NotNull(submissionObject.First);
                    Assert.IsType<JProperty>(submissionObject.First);

                    JProperty submissionProperty = (JProperty) submissionObject.First;

                    Assert.True("submissionMethod".Equals(submissionProperty.Name));
                    Assert.Single(submissionProperty);
                    Assert.NotNull(submissionProperty.First);
                    Assert.IsType<JArray>(submissionProperty.First);

                    JArray submissionArray = (JArray) submissionProperty.First;

                    Assert.Single(submissionArray);
                    Assert.NotNull(submissionArray.First);
                    Assert.IsType<JValue>(submissionArray.First);

                    JValue submissionValue = (JValue) submissionArray.First;

                    Assert.True("written".Equals(submissionValue.Value));
                }

                [Fact]
                public void TestTenderProcurementMethod3()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "submissionMethod"
                    });
                    string parsedElement = "3";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject submissionObject = (JObject) tender.First;

                    Assert.Single(submissionObject);
                    Assert.NotNull(submissionObject.First);
                    Assert.IsType<JProperty>(submissionObject.First);

                    JProperty submissionProperty = (JProperty) submissionObject.First;

                    Assert.True("submissionMethod".Equals(submissionProperty.Name));
                    Assert.Single(submissionProperty);
                    Assert.NotNull(submissionProperty.First);
                    Assert.IsType<JArray>(submissionProperty.First);

                    JArray submissionArray = (JArray) submissionProperty.First;

                    Assert.True(submissionArray.Count == 2);
                    Assert.NotNull(submissionArray.First);
                    Assert.IsType<JValue>(submissionArray.First);
                    Assert.NotNull(submissionArray.Last);
                    Assert.IsType<JValue>(submissionArray.Last);

                    JValue submissionValue1 = (JValue) submissionArray.First;
                    JValue submissionValue2 = (JValue) submissionArray.Last;

                    Assert.True("electronicSubmission".Equals(submissionValue1.Value));
                    Assert.True("written".Equals(submissionValue2.Value));
                }

                [Fact]
                public void TestTenderProcurementMethod4()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "submissionMethod"
                    });
                    string parsedElement = "true";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement(XName.Get("AuctionConstraintIndicator"), parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject submissionObject = (JObject) tender.First;

                    Assert.Single(submissionObject);
                    Assert.NotNull(submissionObject.First);
                    Assert.IsType<JProperty>(submissionObject.First);

                    JProperty submissionProperty = (JProperty) submissionObject.First;

                    Assert.True("submissionMethod".Equals(submissionProperty.Name));
                    Assert.Single(submissionProperty);
                    Assert.NotNull(submissionProperty.First);
                    Assert.IsType<JArray>(submissionProperty.First);

                    JArray submissionArray = (JArray) submissionProperty.First;

                    Assert.Single(submissionArray);
                    Assert.NotNull(submissionArray.First);
                    Assert.IsType<JValue>(submissionArray.First);

                    JValue submissionValue = (JValue) submissionArray.First;

                    Assert.True("electronicAuction".Equals(submissionValue.Value));
                }

                [Fact]
                public void TestTenderProcurementMethod5()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "submissionMethod"
                    });
                    string parsedElement = "false";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement(XName.Get("AuctionConstraintIndicator"), parsedElement) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);
                }

                [Fact]
                public void TestTenderProcurementMethod6()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "submissionMethod"
                    });
                    string parsedElement = "null";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);
                }
            }

            public class TenderSubmissionMethodDetailsTests
            {
                private IMapper _mapper;

                public TenderSubmissionMethodDetailsTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestTenderProcurementMethod1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "submissionMethodDetails"
                    });
                    string parsedElement = "es";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject submissionObject = (JObject) tender.First;

                    Assert.Single(submissionObject);
                    Assert.NotNull(submissionObject.First);
                    Assert.IsType<JProperty>(submissionObject.First);

                    JProperty submissionProperty = (JProperty) submissionObject.First;

                    Assert.True("submissionMethodDetails".Equals(submissionProperty.Name));
                    Assert.Single(submissionProperty);
                    Assert.NotNull(submissionProperty.First);
                    Assert.IsType<JValue>(submissionProperty.First);

                    JValue submissionValue = (JValue) submissionProperty.First;

                    Assert.True("Languages: es".Equals(submissionValue.Value));
                }

                [Fact]
                public void TestTenderProcurementMethod2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "submissionMethodDetails"
                    });
                    string parsedElement1 = "es";
                    string parsedElement2 = "ca";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement1), new XElement("null", parsedElement2) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject submissionObject = (JObject) tender.First;

                    Assert.Single(submissionObject);
                    Assert.NotNull(submissionObject.First);
                    Assert.IsType<JProperty>(submissionObject.First);

                    JProperty submissionProperty = (JProperty) submissionObject.First;

                    Assert.True("submissionMethodDetails".Equals(submissionProperty.Name));
                    Assert.Single(submissionProperty);
                    Assert.NotNull(submissionProperty.First);
                    Assert.IsType<JValue>(submissionProperty.First);

                    JValue submissionValue = (JValue) submissionProperty.First;

                    Assert.True("Languages: es, ca".Equals(submissionValue.Value));
                }

            }

            public class TenderPeriodStartDateTests
            {
                private IMapper _mapper;

                public TenderPeriodStartDateTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestTenderPeriodStartDate()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "tenderPeriod",
                        "startDate"
                    });
                    string parsedElement = "2021-09-01";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject tenderObject = (JObject) tender.First;

                    Assert.Single(tenderObject);
                    Assert.NotNull(tenderObject.First);
                    Assert.IsType<JProperty>(tenderObject.First);

                    JProperty tenderProperty = (JProperty) tenderObject.First;

                    Assert.True("tenderPeriod".Equals(tenderProperty.Name));
                    Assert.Single(tenderProperty);
                    Assert.NotNull(tenderProperty.First);
                    Assert.IsType<JObject>(tenderProperty.First);

                    JObject tenderPeriodObject = (JObject) tenderProperty.First;

                    Assert.Single(tenderPeriodObject);
                    Assert.NotNull(tenderPeriodObject.First);
                    Assert.IsType<JProperty>(tenderPeriodObject.First);

                    JProperty tenderPeriodProperty = (JProperty) tenderPeriodObject.First;

                    Assert.True("startDate".Equals(tenderPeriodProperty.Name));
                    Assert.Single(tenderPeriodProperty);
                    Assert.NotNull(tenderPeriodProperty.First);
                    Assert.IsType<JValue>(tenderPeriodProperty.First);

                    JValue tenderPeriodValue = (JValue) tenderPeriodProperty.First;

                    Assert.True("2021-09-01T00:00:00Z".Equals(tenderPeriodValue.Value));
                }
            }

            public class TenderPeriodEndDateTests
            {
                private IMapper _mapper;

                public TenderPeriodEndDateTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestTenderPeriodEndDate()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "tenderPeriod",
                        "endDate"
                    });
                    string parsedElement = "2021-09-01";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject tenderObject = (JObject) tender.First;

                    Assert.Single(tenderObject);
                    Assert.NotNull(tenderObject.First);
                    Assert.IsType<JProperty>(tenderObject.First);

                    JProperty tenderProperty = (JProperty) tenderObject.First;

                    Assert.True("tenderPeriod".Equals(tenderProperty.Name));
                    Assert.Single(tenderProperty);
                    Assert.NotNull(tenderProperty.First);
                    Assert.IsType<JObject>(tenderProperty.First);

                    JObject tenderPeriodObject = (JObject) tenderProperty.First;

                    Assert.Single(tenderPeriodObject);
                    Assert.NotNull(tenderPeriodObject.First);
                    Assert.IsType<JProperty>(tenderPeriodObject.First);

                    JProperty tenderPeriodProperty = (JProperty) tenderPeriodObject.First;

                    Assert.True("endDate".Equals(tenderPeriodProperty.Name));
                    Assert.Single(tenderPeriodProperty);
                    Assert.NotNull(tenderPeriodProperty.First);
                    Assert.IsType<JValue>(tenderPeriodProperty.First);

                    JValue tenderPeriodValue = (JValue) tenderPeriodProperty.First;

                    Assert.True("2021-09-01T00:00:00Z".Equals(tenderPeriodValue.Value));
                }
            }

            public class TenderPeriodDurationInDaysTests
            {
                private IMapper _mapper;

                public TenderPeriodDurationInDaysTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestTenderPeriodDurationInDays1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "tenderPeriod",
                        "durationInDays"
                    });
                    string parsedElement = "3";

                    XElement element = new XElement("null", parsedElement);
                    element.SetAttributeValue("null", "ANN");

                    _mapper.MapElement(pathMap, new XElement[]{ element });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject tenderObject = (JObject) tender.First;

                    Assert.Single(tenderObject);
                    Assert.NotNull(tenderObject.First);
                    Assert.IsType<JProperty>(tenderObject.First);

                    JProperty tenderProperty = (JProperty) tenderObject.First;

                    Assert.True("tenderPeriod".Equals(tenderProperty.Name));
                    Assert.Single(tenderProperty);
                    Assert.NotNull(tenderProperty.First);
                    Assert.IsType<JObject>(tenderProperty.First);

                    JObject tenderPeriodObject = (JObject) tenderProperty.First;

                    Assert.Single(tenderPeriodObject);
                    Assert.NotNull(tenderPeriodObject.First);
                    Assert.IsType<JProperty>(tenderPeriodObject.First);

                    JProperty tenderPeriodProperty = (JProperty) tenderPeriodObject.First;

                    Assert.True("durationInDays".Equals(tenderPeriodProperty.Name));
                    Assert.Single(tenderPeriodProperty);
                    Assert.NotNull(tenderPeriodProperty.First);
                    Assert.IsType<JValue>(tenderPeriodProperty.First);

                    JValue tenderPeriodValue = (JValue) tenderPeriodProperty.First;

                    Assert.True("1095".Equals(tenderPeriodValue.Value.ToString()));
                }

                [Fact]
                public void TestTenderPeriodDurationInDays2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "tenderPeriod",
                        "durationInDays"
                    });
                    string parsedElement = "3";

                    XElement element = new XElement("null", parsedElement);
                    element.SetAttributeValue("null", "MON");

                    _mapper.MapElement(pathMap, new XElement[]{ element });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject tenderObject = (JObject) tender.First;

                    Assert.Single(tenderObject);
                    Assert.NotNull(tenderObject.First);
                    Assert.IsType<JProperty>(tenderObject.First);

                    JProperty tenderProperty = (JProperty) tenderObject.First;

                    Assert.True("tenderPeriod".Equals(tenderProperty.Name));
                    Assert.Single(tenderProperty);
                    Assert.NotNull(tenderProperty.First);
                    Assert.IsType<JObject>(tenderProperty.First);

                    JObject tenderPeriodObject = (JObject) tenderProperty.First;

                    Assert.Single(tenderPeriodObject);
                    Assert.NotNull(tenderPeriodObject.First);
                    Assert.IsType<JProperty>(tenderPeriodObject.First);

                    JProperty tenderPeriodProperty = (JProperty) tenderPeriodObject.First;

                    Assert.True("durationInDays".Equals(tenderPeriodProperty.Name));
                    Assert.Single(tenderPeriodProperty);
                    Assert.NotNull(tenderPeriodProperty.First);
                    Assert.IsType<JValue>(tenderPeriodProperty.First);

                    JValue tenderPeriodValue = (JValue) tenderPeriodProperty.First;

                    Assert.True("90".Equals(tenderPeriodValue.Value.ToString()));
                }
            
                [Fact]
                public void TestTenderPeriodDurationInDays3()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "tenderPeriod",
                        "durationInDays"
                    });
                    string parsedElement = "3";

                    XElement element = new XElement("null", parsedElement);
                    element.SetAttributeValue("null", "DAY");

                    _mapper.MapElement(pathMap, new XElement[]{ element });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject tenderObject = (JObject) tender.First;

                    Assert.Single(tenderObject);
                    Assert.NotNull(tenderObject.First);
                    Assert.IsType<JProperty>(tenderObject.First);

                    JProperty tenderProperty = (JProperty) tenderObject.First;

                    Assert.True("tenderPeriod".Equals(tenderProperty.Name));
                    Assert.Single(tenderProperty);
                    Assert.NotNull(tenderProperty.First);
                    Assert.IsType<JObject>(tenderProperty.First);

                    JObject tenderPeriodObject = (JObject) tenderProperty.First;

                    Assert.Single(tenderPeriodObject);
                    Assert.NotNull(tenderPeriodObject.First);
                    Assert.IsType<JProperty>(tenderPeriodObject.First);

                    JProperty tenderPeriodProperty = (JProperty) tenderPeriodObject.First;

                    Assert.True("durationInDays".Equals(tenderPeriodProperty.Name));
                    Assert.Single(tenderPeriodProperty);
                    Assert.NotNull(tenderPeriodProperty.First);
                    Assert.IsType<JValue>(tenderPeriodProperty.First);

                    JValue tenderPeriodValue = (JValue) tenderPeriodProperty.First;

                    Assert.True("3".Equals(tenderPeriodValue.Value.ToString()));
                }

                [Fact]
                public void TestTenderPeriodDurationInDays4()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "tenderPeriod",
                        "durationInDays"
                    });
                    string parsedElement = "3";

                    XElement element = new XElement("null", parsedElement);
                    element.SetAttributeValue("null", "null");

                    _mapper.MapElement(pathMap, new XElement[]{ element });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);
                }
            }

            public class TenderTitleTests
            {
                private IMapper _mapper;

                public TenderTitleTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestTenderTitle1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "title"
                    });
                    string parsedElement = "ABCD";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject titleObject = (JObject) tender.First;

                    Assert.Single(titleObject);
                    Assert.NotNull(titleObject.First);
                    Assert.IsType<JProperty>(titleObject.First);

                    JProperty titleProperty = (JProperty) titleObject.First;

                    Assert.True("title".Equals(titleProperty.Name));
                    Assert.Single(titleProperty);
                    Assert.NotNull(titleProperty.First);
                    Assert.IsType<JValue>(titleProperty.First);

                    JValue titleValue = (JValue) titleProperty.First;

                    Assert.True("ABCD".Equals(titleValue.Value));
                }
            }

            public class TenderValueTests
            {
                private IMapper _mapper;

                public TenderValueTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log);
                }

                [Fact]
                public void TestTenderValue1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "value"
                    });
                    string parsedElement = "189250";
                    XElement element = new XElement("null", parsedElement);
                    element.SetAttributeValue("null", "EUR");

                    _mapper.MapElement(pathMap, new XElement[]{ element });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True("tender".Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject tenderObject = (JObject) tender.First;

                    Assert.Single(tenderObject);
                    Assert.NotNull(tenderObject.First);
                    Assert.IsType<JProperty>(tenderObject.First);

                    JProperty valueProperty = (JProperty) tenderObject.First;

                    Assert.True("value".Equals(valueProperty.Name));
                    Assert.Single(valueProperty);
                    Assert.NotNull(valueProperty.First);
                    Assert.IsType<JObject>(valueProperty.First);

                    JObject valueObject = (JObject) valueProperty.First;

                    Assert.True(valueObject.Count == 2);
                    Assert.NotNull(valueObject.First);
                    Assert.NotNull(valueObject.Last);
                    Assert.IsType<JProperty>(valueObject.First);
                    Assert.IsType<JProperty>(valueObject.Last);

                    JProperty amountProperty = (JProperty) valueObject.First;
                    JProperty currencyProperty = (JProperty) valueObject.Last;

                    Assert.Single(amountProperty);
                    Assert.Single(currencyProperty);
                    Assert.NotNull(amountProperty.First);
                    Assert.NotNull(currencyProperty.First);
                    Assert.IsType<JValue>(amountProperty.First);
                    Assert.IsType<JValue>(currencyProperty.First);

                    JValue amountValue = (JValue) amountProperty.First;
                    JValue currencyValue = (JValue) currencyProperty.First;

                    Assert.True("189250".Equals(amountValue.Value));
                    Assert.True("EUR".Equals(currencyValue.Value));
                }

                [Fact]
                public void TestTenderValue2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "value"
                    });
                    string parsedElement = "189250";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);
                }

                [Fact]
                public void TestTenderValue3()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        "tender",
                        "value"
                    });
                    string parsedElement = "189250";

                    XElement element = new XElement("null", parsedElement);
                    element.SetAttributeValue("null", "null");

                    _mapper.MapElement(pathMap, new XElement[]{ element });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);
                }
            }
        }
        
        /* Tests unitarios del componente de parseo */

        public class ParserTests
        {
            private IParser _parser;

            public ParserTests()
            {
                Program.InitLogger();
                _parser = new Parser(Program.Log, "Examples/xml/licitacionesPerfilesContratanteCompleto3.atom");
            }

            [Fact]
            public void TestConstructor()
            {
                Assert.NotNull(_parser.DocumentRoot);
                Assert.IsType<XElement>(_parser.DocumentRoot);
            }

            [Fact]
            public void TestGetNamespaces()
            {
                IDictionary<string, XNamespace> namespacesDict = _parser.GetNamespaces();

                Assert.NotEmpty(namespacesDict);
                Assert.True(namespacesDict.Count == 6);

                IList<string> expectedStrings = new List<string>()
                {
                    "xmlns", "cbc-place-ext", "cac-place-ext", "cbc", "cac", "ns1"
                };

                IList<XNamespace> expectedNamespaces = new List<XNamespace>()
                {
                    XNamespace.Get("http://www.w3.org/2005/Atom"),
                    XNamespace.Get("urn:dgpe:names:draft:codice-place-ext:schema:xsd:CommonBasicComponents-2"),
                    XNamespace.Get("urn:dgpe:names:draft:codice-place-ext:schema:xsd:CommonAggregateComponents-2"),
                    XNamespace.Get("urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2"),
                    XNamespace.Get("urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2"),
                    XNamespace.Get("urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
                };

                IList<string> strings = namespacesDict.Keys.ToList();
                IList<XNamespace> namespaces = namespacesDict.Values.ToList();

                strings.Should().Equal(expectedStrings);
                namespaces.Should().Equal(expectedNamespaces);
            }
        }

        /* Tests unitarios del componen de provisión */

        public class ProviderTests
        {
            private IProvider _provider;

            public ProviderTests()
            {
                Program.InitLogger();
            }

            [Fact]
            public void TestConstructor1()
            {
                _provider = new Provider(Program.Log, Provider.EProviderOperationCode.PROVIDE_LATEST);

                Assert.NotNull(_provider.Files);
                Assert.IsType<BlockingCollection<string>>(_provider.Files);
                Assert.Empty(_provider.Files);

                Thread.Sleep(15 * 1000);

                Assert.Single(_provider.Files);
                Assert.True(_provider.Files.First().Equals("./tmp/document.atom"));
            }

            [Fact]
            public void TestConstructor2()
            {
                _provider = new Provider(
                    Program.Log, Provider.EProviderOperationCode.PROVIDE_SPECIFIC,
                    "Examples/xml/licitacionesPerfilesContratanteCompleto3.atom"
                );

                Assert.NotNull(_provider.Files);
                Assert.IsType<BlockingCollection<string>>(_provider.Files);
                Assert.Single(_provider.Files);
                Assert.True(_provider.Files.First().Equals("Examples/xml/licitacionesPerfilesContratanteCompleto3.atom"));
            }

            [Fact]
            public void TestConstructor3()
            {
                _provider = new Provider(
                    Program.Log, Provider.EProviderOperationCode.PROVIDE_SPECIFIC,
                    "https://contrataciondelestado.es/sindicacion/sindicacion_643/licitacionesPerfilesContratanteCompleto3.atom"
                );

                Assert.NotNull(_provider.Files);
                Assert.IsType<BlockingCollection<string>>(_provider.Files);
                Assert.Empty(_provider.Files);

                Thread.Sleep(15 * 1000);

                Assert.Single(_provider.Files);
                Assert.True(_provider.Files.First().Equals("./tmp/document.atom"));
            }

            [Fact]
            public void TestConstructor4()
            {
                Action action = () => new Provider(
                    Program.Log, Provider.EProviderOperationCode.PROVIDE_SPECIFIC,
                    "httpz://contrataciondelestado.es/sindicacion/sindicacion_643/licitacionesPerfilesContratanteCompleto3.atom"
                );

                action.Should().Throw<FileNotFoundException>();
            }

            [Fact]
            public void TestConstructor5()
            {
                Action action = () => new Provider(
                    Program.Log, Provider.EProviderOperationCode.PROVIDE_ALL,
                    "https://contrataciondelestado.es/sindicacion/sindicacion_643/licitacionesPerfilesContratanteCompleto3.atom"
                );

                action.Should().Throw<InvalidOperationCodeException>();
            }
        }
    }
}