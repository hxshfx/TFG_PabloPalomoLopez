using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Nito.AsyncEx;
using Xunit;
using OCDS_Mapper.src.Exceptions;
using OCDS_Mapper.src.Interfaces;
using OCDS_Mapper.src.Model;
using OCDS_Mapper.src.Utils;

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
                _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
            }

            [Fact]
            public void TestConstructor()
            {
                Assert.NotNull(_mapper.MappedEntry);
                Assert.IsType<JObject>(_mapper.MappedEntry);
                Assert.True("{}".Equals(_mapper.MappedEntry.ToString()));
            }

            [Fact]
            public void TestMapElement1()
            {
                IEnumerable<string> pathMap = new LinkedList<string>(new string[] {});

                _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", "null") });

                Assert.Empty(_mapper.MappedEntry);
                Assert.Null(_mapper.MappedEntry.First);
            }

            [Fact]
            public void TestMapElement2()
            {
                IEnumerable<string> pathMap = new LinkedList<string>(new string[] { "a", "b", "c", "d" });

                _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", "null") });

                Assert.Empty(_mapper.MappedEntry);
                Assert.Null(_mapper.MappedEntry.First);
            }

            [Fact]
            public void TestMapElement3()
            {
                IEnumerable<string> pathMap = new LinkedList<string>(new string[] { "null" });

                _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", "null") });

                Assert.Empty(_mapper.MappedEntry);
                Assert.Null(_mapper.MappedEntry.First);
            }

            public class TagTests
            {
                private IMapper _mapper;

                public TagTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestTag1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tag
                    });
                    string parsedElement = "PRE";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tag = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tag.Equals(tag.Name));
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
                        Mappings.MappingElements.Tag
                    });
                    string parsedElement = "PUB";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tag = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tag.Equals(tag.Name));
                    Assert.Single(tag);
                    Assert.NotNull(tag.First);
                    Assert.IsType<JArray>(tag.First);

                    JArray tagArray = (JArray) tag.First;

                    Assert.Single(tagArray);
                    Assert.NotNull(tagArray.First);
                    Assert.IsType<JValue>(tagArray.First);

                    JValue tagValue = (JValue) tagArray.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tagValue.Value));

                    parsedElement = "EV";
                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.Last);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.Last);

                    tag = (JProperty) _mapper.MappedEntry.Last;

                    Assert.True(Mappings.MappingElements.Tag.Equals(tag.Name));
                    Assert.Single(tag);
                    Assert.NotNull(tag.First);
                    Assert.IsType<JArray>(tag.First);

                    tagArray = (JArray) tag.First;

                    Assert.Single(tagArray);
                    Assert.NotNull(tagArray.First);
                    Assert.IsType<JValue>(tagArray.First);

                    tagValue = (JValue) tagArray.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tagValue.Value));
                }

                [Fact]
                public void TestTag3()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tag
                    });
                    string parsedElement = "ADJ";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tag = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tag.Equals(tag.Name));
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
                        Mappings.MappingElements.Tag
                    });
                    string parsedElement = "RES";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tag = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tag.Equals(tag.Name));
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
                        Mappings.MappingElements.Tag
                    });
                    string parsedElement = "ANUL";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tag = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tag.Equals(tag.Name));
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
                        Mappings.MappingElements.Tag
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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }
                
                [Fact]
                public void TestOCID()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.OCID
                    });
                    string parsedElement = "someID";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty ocid = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.OCID.Equals(ocid.Name));
                    Assert.Single(ocid);
                    Assert.NotNull(ocid.First);
                    Assert.IsType<JValue>(ocid.First);

                    JValue ocidValue = (JValue) ocid.First;

                    Assert.True($"ocds-1xraxc-someID".Equals(ocidValue.Value));
                }
            }
            
            public class AwardDateTests
            {
                private IMapper _mapper;

                public AwardDateTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestAwardDate1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Award,
                        Mappings.MappingElements.Awards.Date
                    });
                    string elementDump = "<cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-02-25</cbc:AwardDate>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Award.Equals(awards.Name));
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
                        Mappings.MappingElements.Award,
                        Mappings.MappingElements.Awards.Date
                    });
                    string elementDump1 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Mejor oferta</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";
                    string elementDump2 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Oferta más ventajosa\n</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump1), XElement.Parse(elementDump2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Award.Equals(awards.Name));
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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestAwardDescription1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Award,
                        Mappings.MappingElements.Awards.Description
                    });
                    string elementDump = "<cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Art. 131 Ley 9/2007: Oferta con mejor relación calidad – precio.</cbc:Description>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Award.Equals(awards.Name));
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
                        Mappings.MappingElements.Award,
                        Mappings.MappingElements.Awards.Description
                    });
                    string elementDump1 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Mejor oferta</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";
                    string elementDump2 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Oferta más ventajosa</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump1), XElement.Parse(elementDump2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Award.Equals(awards.Name));
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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestAwardId1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Award,
                        Mappings.MappingElements.Awards.Id
                    });

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", "null") });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Award.Equals(awards.Name));
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

                    Assert.True("ocds-1xraxc--award-1".Equals(idValue.Value));
                }
            
                [Fact]
                public void TestAwardId2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Award,
                        Mappings.MappingElements.Awards.Id
                    });
                    string elementDump1 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Mejor oferta</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";
                    string elementDump2 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Oferta más ventajosa\n</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump1), XElement.Parse(elementDump2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Award.Equals(awards.Name));
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

                    Assert.True("ocds-1xraxc--award-1-1".Equals(idValue1.Value));
                    Assert.True("ocds-1xraxc--award-2-2".Equals(idValue2.Value));
                }
            }

            public class AwardStatusTests
            {
                private IMapper _mapper;

                public AwardStatusTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestAwardStatus1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Award,
                        Mappings.MappingElements.Awards.Status
                    });
                    string elementDump = "<cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Award.Equals(awards.Name));
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
                        Mappings.MappingElements.Award,
                        Mappings.MappingElements.Awards.Status
                    });
                    string elementDump1 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Mejor oferta</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";
                    string elementDump2 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">7</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Oferta más ventajosa</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump1), XElement.Parse(elementDump2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Award.Equals(awards.Name));
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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestAwardSuppliers()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Award,
                        Mappings.MappingElements.Awards.Suppliers
                    });
                    string elementDump = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Mejor oferta</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.True(_mapper.MappedEntry.Count == 2);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.NotNull(_mapper.MappedEntry.Last);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.Last);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Award.Equals(awards.Name));
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

                    Assert.True("A28885614-0".Equals(supplierIdValue.Value));
                    Assert.True("GRASAS DEL CENTRO, S.A".Equals(supplierNameValue.Value));

                    JProperty parties = (JProperty) _mapper.MappedEntry.Last;

                    Assert.True(Mappings.MappingElements.Party.Equals(parties.Name));
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
                    Assert.True("A28885614-0".Equals(partyIdProperty.Value.ToString()));

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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestAwardValue1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Award,
                        Mappings.MappingElements.Awards.Value
                    });
                    string elementDump = "<cac:LegalMonetaryTotal xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">543353.86</cbc:TaxExclusiveAmount>\n  <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">657458.17</cbc:PayableAmount>\n</cac:LegalMonetaryTotal>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Award.Equals(awards.Name));
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

                    Double value = 657458.17;
                    Assert.True(value.Equals(amountValue.Value));
                    Assert.True("EUR".Equals(currencyValue.Value));
                }

                [Fact]
                public void TestAwardValue2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Award,
                        Mappings.MappingElements.Awards.Value
                    });
                    string elementDump1 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Mejor oferta</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48970.35</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";
                    string elementDump2 = "<cac:TenderResult xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ResultCode listURI=\"http://contrataciondelestado.es/codice/cl/2.02/TenderResultCode-2.02.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">8</cbc:ResultCode>\n  <cbc:Description xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Oferta más ventajosa\n</cbc:Description>\n  <cbc:AwardDate xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2021-04-15</cbc:AwardDate>\n  <cbc:ReceivedTenderQuantity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ReceivedTenderQuantity>\n  <cbc:SMEAwardedIndicator xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">true</cbc:SMEAwardedIndicator>\n  <cac:WinningParty>\n    <cac:PartyIdentification>\n      <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">A28885614</cbc:ID>\n    </cac:PartyIdentification>\n    <cac:PartyName>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">GRASAS DEL CENTRO, S.A</cbc:Name>\n    </cac:PartyName>\n  </cac:WinningParty>\n  <cac:AwardedTenderedProject>\n    <cbc:ProcurementProjectLotID xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2</cbc:ProcurementProjectLotID>\n    <cac:LegalMonetaryTotal>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:TaxExclusiveAmount>\n      <cbc:PayableAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">48171.3</cbc:PayableAmount>\n    </cac:LegalMonetaryTotal>\n  </cac:AwardedTenderedProject>\n</cac:TenderResult>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump1), XElement.Parse(elementDump2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Award.Equals(awards.Name));
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

                    Double value1 = 48970.35;
                    Double value2 = 48171.3;
                    Assert.True(value1.Equals(amountValue1.Value));
                    Assert.True("EUR".Equals(currencyValue1.Value));
                    Assert.True(value2.Equals(amountValue2.Value));
                    Assert.True("EUR".Equals(currencyValue2.Value));
                }
            
                [Fact]
                public void TestAwardValue3()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Award,
                        Mappings.MappingElements.Awards.Value
                    });
                    string elementDump = "<cac:LegalMonetaryTotal xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">543353.86</cbc:TaxExclusiveAmount>\n</cac:LegalMonetaryTotal>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty awards = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Award.Equals(awards.Name));
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
                    Assert.IsType<JProperty>(awardsObject.Last);
    
                    JProperty idProperty = (JProperty) awardsObject.First;

                    Assert.True("id".Equals(idProperty.Name));
                    Assert.Single(idProperty);
                    Assert.NotNull(idProperty.First);
                    Assert.IsType<JValue>(idProperty.First);

                    JValue idValue = (JValue) idProperty.First;

                    Assert.True("1".Equals(idValue.Value));
                }
            }

            public class ContractIdTests
            {
                private IMapper _mapper;

                public ContractIdTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestContractId()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Contract,
                        Mappings.MappingElements.Contracts.Id
                    });

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", "1") });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty contracts = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Contract.Equals(contracts.Name));
                    Assert.Single(contracts);
                    Assert.NotNull(contracts.First);
                    Assert.IsType<JArray>(contracts.First);

                    JArray contractsArray = (JArray) contracts.First;

                    Assert.Single(contractsArray);
                    Assert.NotNull(contractsArray.First);
                    Assert.IsType<JObject>(contractsArray.First);

                    JObject contractsObject = (JObject) contractsArray.First;

                    Assert.True(contractsObject.Count == 2);
                    Assert.IsType<JProperty>(contractsObject.First);
                    Assert.IsType<JProperty>(contractsObject.Last);

                    JProperty idProperty = (JProperty) contractsObject.First;
                    JProperty awardIdProperty = (JProperty) contractsObject.Last;

                    Assert.True("id".Equals(idProperty.Name));
                    Assert.Single(idProperty);
                    Assert.NotNull(idProperty.First);
                    Assert.IsType<JValue>(idProperty.First);
                    Assert.True("awardID".Equals(awardIdProperty.Name));
                    Assert.Single(awardIdProperty);
                    Assert.NotNull(awardIdProperty.First);
                    Assert.IsType<JValue>(awardIdProperty.First);

                    JValue idValue = (JValue) idProperty.First;
                    JValue awardIDValue = (JValue) awardIdProperty.First;

                    Assert.True("ocds-1xraxc--contract-1".Equals(idValue.Value));
                    Assert.True("ocds-1xraxc--award-1".Equals(awardIDValue.Value));
                }
            }

            public class ContractPeriodStartDateTests
            {
                private IMapper _mapper;

                public ContractPeriodStartDateTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestContractPeriodStartDate()
                {
                    IEnumerable<string> pathMap1 = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Contract,
                        Mappings.MappingElements.Contracts.Id
                    });
                    IEnumerable<string> pathMap2 = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Contract,
                        Mappings.MappingElements.Contracts.Period,
                        Mappings.MappingElements.Contracts.Periods.StartDate
                    });
                    string parsedElement1 = "1";
                    string parsedElement2 = "2021-09-01";

                    _mapper.MapElement(pathMap1, new XElement[]{ new XElement("null", parsedElement1) });
                    _mapper.MapElement(pathMap2, new XElement[]{ new XElement("null", parsedElement2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty contracts = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Contract.Equals(contracts.Name));
                    Assert.Single(contracts);
                    Assert.NotNull(contracts.First);
                    Assert.IsType<JArray>(contracts.First);

                    JArray contractsArray = (JArray) contracts.First;

                    Assert.Single(contractsArray);
                    Assert.NotNull(contractsArray.First);
                    Assert.IsType<JObject>(contractsArray.First);

                    JObject contractsObject = (JObject) contractsArray.First;

                    Assert.True(contractsObject.Count == 3);
                    Assert.IsType<JProperty>(contractsObject.Last);

                    JProperty contractPeriodProperty = (JProperty) contractsObject.Last;

                    Assert.Single(contractPeriodProperty);
                    Assert.NotNull(contractPeriodProperty.First);
                    Assert.IsType<JObject>(contractPeriodProperty.First);

                    JObject contractPeriodObject = (JObject) contractPeriodProperty.First;

                    Assert.Single(contractPeriodObject);
                    Assert.NotNull(contractPeriodObject.First);
                    Assert.IsType<JProperty>(contractPeriodObject.First);

                    JProperty contractPeriodStartDateProperty = (JProperty) contractPeriodObject.First;

                    Assert.True("startDate".Equals(contractPeriodStartDateProperty.Name));
                    Assert.Single(contractPeriodStartDateProperty);
                    Assert.NotNull(contractPeriodStartDateProperty.First);
                    Assert.IsType<JValue>(contractPeriodStartDateProperty.First);

                    JValue contractPeriodValue = (JValue) contractPeriodStartDateProperty.First;

                    Assert.True("2021-09-01T00:00:00Z".Equals(contractPeriodValue.Value));
                }
            }

            public class PartyFieldsTests
            {
                private IMapper _mapper;

                public PartyFieldsTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestPartyFields()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Party
                    });
                    string elementDump = "<cac:Party xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:WebsiteURI xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">https://lapalmaaguas.com</cbc:WebsiteURI>\n  <cac:PartyIdentification>\n    <cbc:ID schemeName=\"DIR3\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">L03380010</cbc:ID>\n  </cac:PartyIdentification>\n  <cac:PartyIdentification>\n    <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">P3800058D</cbc:ID>\n  </cac:PartyIdentification>\n  <cac:PartyName>\n    <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Presidencia del Consejo Insular de Aguas de La Palma</cbc:Name>\n  </cac:PartyName>\n  <cac:PostalAddress>\n    <cbc:CityName xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Santa Cruz de la Palma</cbc:CityName>\n    <cbc:PostalZone xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">38700</cbc:PostalZone>\n    <cac:AddressLine>\n      <cbc:Line xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Avda. Marítima 34</cbc:Line>\n    </cac:AddressLine>\n    <cac:Country>\n      <cbc:IdentificationCode listURI=\"http://docs.oasis-open.org/ubl/os-ubl-2.0/cl/gc/default/CountryIdentificationCode-2.0.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">ES</cbc:IdentificationCode>\n      <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">España</cbc:Name>\n    </cac:Country>\n  </cac:PostalAddress>\n  <cac:Contact>\n    <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Presidencia del Consejo Insular de Aguas de La Palma</cbc:Name>\n    <cbc:ElectronicMail xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">gabinete.presidencia@cablapalma.es</cbc:ElectronicMail>\n    <cbc:Telefax xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">918562480</cbc:Telefax>\n    <cbc:Telephone xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">918562479</cbc:Telephone>\n  </cac:Contact>\n</cac:Party>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty party = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Party.Equals(party.Name));
                    Assert.Single(party);
                    Assert.NotNull(party.First);
                    Assert.IsType<JArray>(party.First);

                    JArray partyArray = (JArray) party.First;

                    Assert.Single(partyArray);
                    Assert.NotNull(partyArray.First);
                    Assert.IsType<JObject>(partyArray.First);

                    JObject partyObject = (JObject) partyArray.First;

                    Assert.True(partyObject.Count == 2);
                    Assert.IsType<JProperty>(partyObject.First);

                    JProperty addressProperty = (JProperty) partyObject.First;

                    Assert.Single(addressProperty);
                    Assert.NotNull(addressProperty.First);
                    Assert.IsType<JObject>(addressProperty.First);

                    JObject addressObject = (JObject) addressProperty.First;

                    Assert.True(addressObject.Count == 4);
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

                    JProperty countryProperty = (JProperty) localityProperty.Next;

                    Assert.True("countryName".Equals(countryProperty.Name));
                    Assert.Single(countryProperty);
                    Assert.NotNull(countryProperty.First);
                    Assert.IsType<JValue>(countryProperty.First);

                    JValue countryValue = (JValue) countryProperty.First;

                    Assert.True("ES".Equals(countryValue.Value));

                    Assert.IsType<JProperty>(countryProperty.Next);
                    
                    JProperty postalCodeProperty = (JProperty) countryProperty.Next;

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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestPartiesName()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Party,
                        Mappings.MappingElements.Parties.Name
                    });
                    string parsedElement = "ABCD";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty party = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Party.Equals(party.Name));
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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestPartiesIdentifier1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Party,
                        Mappings.MappingElements.Parties.Identifier
                    });
                    string elementDump = "<cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">S4611001A</cbc:ID>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty party = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Party.Equals(party.Name));
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
                        Mappings.MappingElements.Party,
                        Mappings.MappingElements.Parties.Identifier
                    });
                    string elementDump1 = "<cac:PartyIdentification xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ID schemeName=\"DIR3\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">L03380010</cbc:ID>\n</cac:PartyIdentification>";
                    string elementDump2 = "<cac:PartyIdentification xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ID schemeName=\"NIF\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">P3800058D</cbc:ID>\n</cac:PartyIdentification>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump1), XElement.Parse(elementDump2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty party = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Party.Equals(party.Name));
                    Assert.Single(party);
                    Assert.NotNull(party.First);
                    Assert.IsType<JArray>(party.First);

                    JArray partyArray = (JArray) party.First;

                    Assert.Single(partyArray);
                    Assert.NotNull(partyArray.First);
                    Assert.IsType<JObject>(partyArray.First);

                    JObject partyObject = (JObject) partyArray.First;

                    Assert.True(partyObject.Count == 4);
                    Assert.IsType<JProperty>(partyObject.First);

                    JProperty additionalIdentifiersProperty = (JProperty) partyObject.First;

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

                    JProperty identifierProperty = (JProperty) additionalIdentifiersProperty.Next;

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
                    Assert.True("ES-DIR3".Equals(schemeProperty.Value.ToString()));

                    Assert.True("id".Equals(identifierIdProperty.Name));
                    Assert.True("L03380010".Equals(identifierIdProperty.Value.ToString()));

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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestPlanningBudgetAmount1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Planning,
                        Mappings.MappingElements.Plannings.Budget,
                        Mappings.MappingElements.Plannings.Budgets.Amount
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

                    Double value = 189250;
                    Assert.True(value.Equals(amountValue.Value));
                    Assert.True("EUR".Equals(currencyValue.Value));
                }

                [Fact]
                public void TestPlanningBudgetAmount2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Planning,
                        Mappings.MappingElements.Plannings.Budget,
                        Mappings.MappingElements.Plannings.Budgets.Amount
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
                        Mappings.MappingElements.Planning,
                        Mappings.MappingElements.Plannings.Budget,
                        Mappings.MappingElements.Plannings.Budgets.Amount
                    });
                    string parsedElement = "189250";

                    XElement element = new XElement("null", parsedElement);
                    element.SetAttributeValue("null", "USD");

                    _mapper.MapElement(pathMap, new XElement[]{ element });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);
                }
            }

            public class TenderItemsClassificationTests
            {
                private IMapper _mapper;

                public TenderItemsClassificationTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestTenderItemsClassification1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.Item,
                        Mappings.MappingElements.Tenders.Items.Classification
                    });

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", "45000000") });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject tenderObject = (JObject) tender.First;

                    Assert.True(tenderObject.Count == 2);
                    Assert.IsType<JProperty>(tenderObject.First);
                    Assert.IsType<JProperty>(tenderObject.Last);

                    JProperty itemsProperty = (JProperty) tenderObject.First;
                    JProperty lotsProperty = (JProperty) tenderObject.Last;

                    Assert.True("items".Equals(itemsProperty.Name));
                    Assert.True("lots".Equals(lotsProperty.Name));
                    Assert.Single(itemsProperty);
                    Assert.Single(lotsProperty);
                    Assert.NotNull(itemsProperty.First);
                    Assert.NotNull(lotsProperty.First);
                    Assert.IsType<JArray>(itemsProperty.First);
                    Assert.IsType<JArray>(lotsProperty.First);

                    JArray lotsArray = (JArray) lotsProperty.First;
                    JArray itemsArray = (JArray) itemsProperty.First;

                    Assert.Single(lotsArray);
                    Assert.NotNull(lotsArray.First);
                    Assert.IsType<JObject>(lotsArray.First);

                    JObject lotIdObject = (JObject) lotsArray.First;

                    Assert.Single(lotIdObject);
                    Assert.NotNull(lotIdObject.First);
                    Assert.IsType<JProperty>(lotIdObject.First);

                    JProperty lotIdProperty = (JProperty) lotIdObject.First;

                    Assert.Single(lotIdProperty);
                    Assert.NotNull(lotIdProperty.First);
                    Assert.IsType<JValue>(lotIdProperty.First);

                    JValue lotIdValue = (JValue) lotIdProperty.First;

                    Assert.True("lot-1".Equals(lotIdValue.Value));

                    Assert.Single(itemsArray);
                    Assert.NotNull(itemsArray.First);
                    Assert.IsType<JObject>(itemsArray.First);

                    JObject itemObject = (JObject) itemsArray.First;

                    Assert.True(itemObject.Count == 2);
                    Assert.IsType<JProperty>(itemObject.First);
                    Assert.IsType<JProperty>(itemObject.Last);

                    JProperty itemIdProperty = (JProperty) itemObject.First;
                    JProperty itemClassificationProperty = (JProperty) itemObject.Last;

                    Assert.Single(itemIdProperty);
                    Assert.NotNull(itemIdProperty.First);
                    Assert.IsType<JValue>(itemIdProperty.First);

                    JValue itemIdValue = (JValue) itemIdProperty.First;

                    Assert.True("1".Equals(itemIdValue.Value));

                    Assert.Single(itemClassificationProperty);
                    Assert.NotNull(itemClassificationProperty.First);
                    Assert.IsType<JObject>(itemClassificationProperty.First);

                    JObject itemClassificationObject = (JObject) itemClassificationProperty.First;

                    Assert.True(itemClassificationObject.Count == 3);
                    Assert.IsType<JProperty>(itemClassificationObject.First);

                    JProperty classificationIdProperty = (JProperty) itemClassificationObject.First;

                    Assert.Single(classificationIdProperty);
                    Assert.NotNull(classificationIdProperty.First);
                    Assert.IsType<JValue>(classificationIdProperty.First);

                    JValue classificationIdValue = (JValue) classificationIdProperty.First;

                    Assert.True("45000000".Equals(classificationIdValue.Value));

                    JProperty schemeProperty = (JProperty) classificationIdProperty.Next;

                    Assert.Single(schemeProperty);
                    Assert.NotNull(schemeProperty.First);
                    Assert.IsType<JValue>(schemeProperty.First);

                    JValue schemeValue = (JValue) schemeProperty.First;

                    Assert.True("CPV".Equals(schemeValue.Value));

                    JProperty descriptionProperty = (JProperty) schemeProperty.Next;

                    Assert.Single(descriptionProperty);
                    Assert.NotNull(descriptionProperty.First);
                    Assert.IsType<JValue>(descriptionProperty.First);

                    JValue descriptionValue = (JValue) descriptionProperty.First;

                    Assert.True("Trabajos de construcción.".Equals(descriptionValue.Value));
                }
            
                [Fact]
                public void TestTenderItemsClassification2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.Item,
                        Mappings.MappingElements.Tenders.Items.Classification
                    });
                    string elementDump1 = "<cac:ProcurementProjectLot xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ID schemeName=\"ID_LOTE\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ID>\n  <cac:ProcurementProject>\n    <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Suministro de Agua natural mineral mediante botellones, vasos de papel y uso de fuentes refrigeradas para los centros de mutua universal en la CCAA de Baleares</cbc:Name>\n    <cac:BudgetAmount>\n      <cbc:TotalAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2215.68</cbc:TotalAmount>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1965.12</cbc:TaxExclusiveAmount>\n    </cac:BudgetAmount>\n    <cac:RequiredCommodityClassification>\n      <cbc:ItemClassificationCode listURI=\"http://contrataciondelestado.es/codice/cl/1.04/CPV2007-1.04.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">15981100</cbc:ItemClassificationCode>\n    </cac:RequiredCommodityClassification>\n    <cac:RealizedLocation>\n      <cbc:CountrySubentity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Barcelona</cbc:CountrySubentity>\n      <cbc:CountrySubentityCode listURI=\"http://contrataciondelestado.es/codice/cl/2.06/NUTS-2016.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">ES511</cbc:CountrySubentityCode>\n    </cac:RealizedLocation>\n  </cac:ProcurementProject>\n</cac:ProcurementProjectLot>";
                    string elementDump2 = "<cac:ProcurementProjectLot xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ID schemeName=\"ID_LOTE\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2</cbc:ID>\n  <cac:ProcurementProject>\n    <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Suministro de Agua natural mineral mediante botellones, vasos de papel y uso de fuentes refrigeradas para los centros de mutua universal en la CCAA de Canarias</cbc:Name>\n    <cac:BudgetAmount>\n      <cbc:TotalAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">6584.78</cbc:TotalAmount>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">5840.16</cbc:TaxExclusiveAmount>\n    </cac:BudgetAmount>\n    <cac:RequiredCommodityClassification>\n      <cbc:ItemClassificationCode listURI=\"http://contrataciondelestado.es/codice/cl/1.04/CPV2007-1.04.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">15981100</cbc:ItemClassificationCode>\n    </cac:RequiredCommodityClassification>\n    <cac:RealizedLocation>\n      <cbc:CountrySubentity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Barcelona</cbc:CountrySubentity>\n      <cbc:CountrySubentityCode listURI=\"http://contrataciondelestado.es/codice/cl/2.06/NUTS-2016.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">ES511</cbc:CountrySubentityCode>\n    </cac:RealizedLocation>\n  </cac:ProcurementProject>\n</cac:ProcurementProjectLot>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump1), XElement.Parse(elementDump2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject tenderObject = (JObject) tender.First;

                    Assert.True(tenderObject.Count == 2);
                    Assert.IsType<JProperty>(tenderObject.First);
                    Assert.IsType<JProperty>(tenderObject.Last);

                    JProperty itemsProperty = (JProperty) tenderObject.First;
                    JProperty lotsProperty = (JProperty) tenderObject.Last;

                    Assert.True("items".Equals(itemsProperty.Name));
                    Assert.True("lots".Equals(lotsProperty.Name));
                    Assert.Single(itemsProperty);
                    Assert.Single(lotsProperty);
                    Assert.NotNull(itemsProperty.First);
                    Assert.NotNull(lotsProperty.First);
                    Assert.IsType<JArray>(itemsProperty.First);
                    Assert.IsType<JArray>(lotsProperty.First);

                    JArray lotsArray = (JArray) lotsProperty.First;
                    JArray itemsArray = (JArray) itemsProperty.First;

                    Assert.True(lotsArray.Count == 2);
                    Assert.NotNull(lotsArray.First);
                    Assert.IsType<JObject>(lotsArray.First);
                    Assert.NotNull(lotsArray.Last);
                    Assert.IsType<JObject>(lotsArray.Last);

                    JObject lotIdObject1 = (JObject) lotsArray.First;
                    JObject lotIdObject2 = (JObject) lotsArray.Last;

                    Assert.Single(lotIdObject1);
                    Assert.NotNull(lotIdObject1.First);
                    Assert.IsType<JProperty>(lotIdObject1.First);
                    Assert.Single(lotIdObject2);
                    Assert.NotNull(lotIdObject2.First);
                    Assert.IsType<JProperty>(lotIdObject2.First);

                    JProperty lotIdProperty1 = (JProperty) lotIdObject1.First;
                    JProperty lotIdProperty2 = (JProperty) lotIdObject2.First;

                    Assert.Single(lotIdProperty1);
                    Assert.NotNull(lotIdProperty1.First);
                    Assert.IsType<JValue>(lotIdProperty1.First);
                    Assert.Single(lotIdProperty2);
                    Assert.NotNull(lotIdProperty2.First);
                    Assert.IsType<JValue>(lotIdProperty2.First);

                    JValue lotIdValue1 = (JValue) lotIdProperty1.First;
                    JValue lotIdValue2 = (JValue) lotIdProperty2.First;

                    Assert.True("lot-1".Equals(lotIdValue1.Value));
                    Assert.True("lot-2".Equals(lotIdValue2.Value));

                    Assert.True(itemsArray.Count == 2);
                    Assert.NotNull(itemsArray.First);
                    Assert.IsType<JObject>(itemsArray.First);
                    Assert.NotNull(itemsArray.Last);
                    Assert.IsType<JObject>(itemsArray.Last);

                    JObject itemObject1 = (JObject) itemsArray.First;
                    JObject itemObject2 = (JObject) itemsArray.Last;

                    Assert.True(itemObject1.Count == 2);
                    Assert.IsType<JProperty>(itemObject1.First);
                    Assert.IsType<JProperty>(itemObject1.Last);
                    Assert.True(itemObject2.Count == 2);
                    Assert.IsType<JProperty>(itemObject2.First);
                    Assert.IsType<JProperty>(itemObject2.Last);

                    JProperty itemIdProperty1 = (JProperty) itemObject1.First;
                    JProperty itemClassificationProperty1 = (JProperty) itemObject1.Last;
                    JProperty itemIdProperty2 = (JProperty) itemObject2.First;
                    JProperty itemClassificationProperty2 = (JProperty) itemObject2.Last;

                    Assert.Single(itemIdProperty1);
                    Assert.NotNull(itemIdProperty1.First);
                    Assert.IsType<JValue>(itemIdProperty1.First);
                    Assert.Single(itemIdProperty2);
                    Assert.NotNull(itemIdProperty2.First);
                    Assert.IsType<JValue>(itemIdProperty2.First);

                    JValue itemIdValue1 = (JValue) itemIdProperty1.First;
                    JValue itemIdValue2 = (JValue) itemIdProperty2.First;

                    Assert.True("1".Equals(itemIdValue1.Value));
                    Assert.True("2".Equals(itemIdValue2.Value));

                    Assert.Single(itemClassificationProperty1);
                    Assert.NotNull(itemClassificationProperty1.First);
                    Assert.IsType<JObject>(itemClassificationProperty1.First);
                    Assert.Single(itemClassificationProperty2);
                    Assert.NotNull(itemClassificationProperty2.First);
                    Assert.IsType<JObject>(itemClassificationProperty2.First);

                    JObject itemClassificationObject1 = (JObject) itemClassificationProperty1.First;
                    JObject itemClassificationObject2 = (JObject) itemClassificationProperty2.First;

                    Assert.True(itemClassificationObject1.Count == 3);
                    Assert.IsType<JProperty>(itemClassificationObject1.First);
                    Assert.True(itemClassificationObject2.Count == 3);
                    Assert.IsType<JProperty>(itemClassificationObject2.First);

                    JProperty classificationIdProperty1 = (JProperty) itemClassificationObject1.First;
                    JProperty classificationIdProperty2 = (JProperty) itemClassificationObject2.First;

                    Assert.Single(classificationIdProperty1);
                    Assert.NotNull(classificationIdProperty1.First);
                    Assert.IsType<JValue>(classificationIdProperty1.First);
                    Assert.Single(classificationIdProperty2);
                    Assert.NotNull(classificationIdProperty2.First);
                    Assert.IsType<JValue>(classificationIdProperty2.First);

                    JValue classificationIdValue1 = (JValue) classificationIdProperty1.First;
                    JValue classificationIdValue2 = (JValue) classificationIdProperty2.First;

                    Assert.True("15981100".Equals(classificationIdValue1.Value));
                    Assert.True("15981100".Equals(classificationIdValue2.Value));

                    JProperty schemeProperty1 = (JProperty) classificationIdProperty1.Next;
                    JProperty schemeProperty2 = (JProperty) classificationIdProperty2.Next;

                    Assert.Single(schemeProperty1);
                    Assert.NotNull(schemeProperty1.First);
                    Assert.IsType<JValue>(schemeProperty1.First);
                    Assert.Single(schemeProperty2);
                    Assert.NotNull(schemeProperty2.First);
                    Assert.IsType<JValue>(schemeProperty2.First);

                    JValue schemeValue1 = (JValue) schemeProperty1.First;
                    JValue schemeValue2 = (JValue) schemeProperty2.First;

                    Assert.True("CPV".Equals(schemeValue1.Value));
                    Assert.True("CPV".Equals(schemeValue2.Value));

                    JProperty descriptionProperty1 = (JProperty) schemeProperty1.Next;
                    JProperty descriptionProperty2 = (JProperty) schemeProperty2.Next;

                    Assert.Single(descriptionProperty1);
                    Assert.NotNull(descriptionProperty1.First);
                    Assert.IsType<JValue>(descriptionProperty1.First);
                    Assert.Single(descriptionProperty2);
                    Assert.NotNull(descriptionProperty2.First);
                    Assert.IsType<JValue>(descriptionProperty2.First);

                    JValue descriptionValue1 = (JValue) descriptionProperty1.First;
                    JValue descriptionValue2 = (JValue) descriptionProperty2.First;

                    Assert.True("Agua mineral sin gas.".Equals(descriptionValue1.Value));
                    Assert.True("Agua mineral sin gas.".Equals(descriptionValue2.Value));
                }
            }

            public class TenderLotsIdTests
            {
                private IMapper _mapper;

                public TenderLotsIdTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestTenderLotsId1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.Lot,
                        Mappings.MappingElements.Tenders.Lots.Id
                    });

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", "1234") });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject tenderObject = (JObject) tender.First;

                    Assert.True(tenderObject.Count == 2);
                    Assert.IsType<JProperty>(tenderObject.First);
                    Assert.IsType<JProperty>(tenderObject.Last);

                    JProperty itemsProperty = (JProperty) tenderObject.First;
                    JProperty lotsProperty = (JProperty) tenderObject.Last;

                    Assert.True("items".Equals(itemsProperty.Name));
                    Assert.True("lots".Equals(lotsProperty.Name));
                    Assert.Single(itemsProperty);
                    Assert.Single(lotsProperty);
                    Assert.NotNull(itemsProperty.First);
                    Assert.NotNull(lotsProperty.First);
                    Assert.IsType<JArray>(itemsProperty.First);
                    Assert.IsType<JArray>(lotsProperty.First);

                    JArray lotsArray = (JArray) lotsProperty.First;
                    JArray itemsArray = (JArray) itemsProperty.First;

                    Assert.Single(lotsArray);
                    Assert.NotNull(lotsArray.First);
                    Assert.IsType<JObject>(lotsArray.First);

                    JObject lotIdObject = (JObject) lotsArray.First;

                    Assert.Single(lotIdObject);
                    Assert.NotNull(lotIdObject.First);
                    Assert.IsType<JProperty>(lotIdObject.First);

                    JProperty lotIdProperty = (JProperty) lotIdObject.First;

                    Assert.Single(lotIdProperty);
                    Assert.NotNull(lotIdProperty.First);
                    Assert.IsType<JValue>(lotIdProperty.First);

                    JValue lotIdValue = (JValue) lotIdProperty.First;

                    Assert.True("lot-1234".Equals(lotIdValue.Value));

                    Assert.Single(itemsArray);
                    Assert.NotNull(itemsArray.First);
                    Assert.IsType<JObject>(itemsArray.First);

                    JObject itemObject = (JObject) itemsArray.First;

                    Assert.True(itemObject.Count == 2);
                    Assert.IsType<JProperty>(itemObject.First);
                    Assert.IsType<JProperty>(itemObject.Last);

                    JProperty itemIdProperty = (JProperty) itemObject.First;
                    JProperty itemRelatedLotProperty = (JProperty) itemObject.Last;

                    Assert.Single(itemIdProperty);
                    Assert.NotNull(itemIdProperty.First);
                    Assert.IsType<JValue>(itemIdProperty.First);

                    JValue itemIdValue = (JValue) itemIdProperty.First;

                    Assert.True("1234".Equals(itemIdValue.Value));

                    Assert.Single(itemRelatedLotProperty);
                    Assert.NotNull(itemRelatedLotProperty.First);
                    Assert.IsType<JValue>(itemRelatedLotProperty.First);

                    JValue itemRelatedValue = (JValue) itemRelatedLotProperty.First;

                    Assert.True("lot-1234".Equals(itemRelatedValue.Value));
                }
            
                [Fact]
                public void TestTenderLotsId2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.Lot,
                        Mappings.MappingElements.Tenders.Lots.Id
                    });
                    string elementDump1 = "<cac:ProcurementProjectLot xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ID schemeName=\"ID_LOTE\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ID>\n  <cac:ProcurementProject>\n    <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Suministro de Agua natural mineral mediante botellones, vasos de papel y uso de fuentes refrigeradas para los centros de mutua universal en la CCAA de Baleares</cbc:Name>\n    <cac:BudgetAmount>\n      <cbc:TotalAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2215.68</cbc:TotalAmount>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1965.12</cbc:TaxExclusiveAmount>\n    </cac:BudgetAmount>\n    <cac:RequiredCommodityClassification>\n      <cbc:ItemClassificationCode listURI=\"http://contrataciondelestado.es/codice/cl/1.04/CPV2007-1.04.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">15981100</cbc:ItemClassificationCode>\n    </cac:RequiredCommodityClassification>\n    <cac:RealizedLocation>\n      <cbc:CountrySubentity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Barcelona</cbc:CountrySubentity>\n      <cbc:CountrySubentityCode listURI=\"http://contrataciondelestado.es/codice/cl/2.06/NUTS-2016.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">ES511</cbc:CountrySubentityCode>\n    </cac:RealizedLocation>\n  </cac:ProcurementProject>\n</cac:ProcurementProjectLot>";
                    string elementDump2 = "<cac:ProcurementProjectLot xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ID schemeName=\"ID_LOTE\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2</cbc:ID>\n  <cac:ProcurementProject>\n    <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Suministro de Agua natural mineral mediante botellones, vasos de papel y uso de fuentes refrigeradas para los centros de mutua universal en la CCAA de Canarias</cbc:Name>\n    <cac:BudgetAmount>\n      <cbc:TotalAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">6584.78</cbc:TotalAmount>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">5840.16</cbc:TaxExclusiveAmount>\n    </cac:BudgetAmount>\n    <cac:RequiredCommodityClassification>\n      <cbc:ItemClassificationCode listURI=\"http://contrataciondelestado.es/codice/cl/1.04/CPV2007-1.04.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">15981100</cbc:ItemClassificationCode>\n    </cac:RequiredCommodityClassification>\n    <cac:RealizedLocation>\n      <cbc:CountrySubentity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Barcelona</cbc:CountrySubentity>\n      <cbc:CountrySubentityCode listURI=\"http://contrataciondelestado.es/codice/cl/2.06/NUTS-2016.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">ES511</cbc:CountrySubentityCode>\n    </cac:RealizedLocation>\n  </cac:ProcurementProject>\n</cac:ProcurementProjectLot>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump1), XElement.Parse(elementDump2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject tenderObject = (JObject) tender.First;

                    Assert.True(tenderObject.Count == 2);
                    Assert.IsType<JProperty>(tenderObject.First);
                    Assert.IsType<JProperty>(tenderObject.Last);

                    JProperty itemsProperty = (JProperty) tenderObject.First;
                    JProperty lotsProperty = (JProperty) tenderObject.Last;

                    Assert.True("items".Equals(itemsProperty.Name));
                    Assert.True("lots".Equals(lotsProperty.Name));
                    Assert.Single(itemsProperty);
                    Assert.Single(lotsProperty);
                    Assert.NotNull(itemsProperty.First);
                    Assert.NotNull(lotsProperty.First);
                    Assert.IsType<JArray>(itemsProperty.First);
                    Assert.IsType<JArray>(lotsProperty.First);

                    JArray lotsArray = (JArray) lotsProperty.First;
                    JArray itemsArray = (JArray) itemsProperty.First;

                    Assert.True(lotsArray.Count == 2);
                    Assert.NotNull(lotsArray.First);
                    Assert.IsType<JObject>(lotsArray.First);
                    Assert.NotNull(lotsArray.Last);
                    Assert.IsType<JObject>(lotsArray.Last);

                    JObject lotIdObject1 = (JObject) lotsArray.First;
                    JObject lotIdObject2 = (JObject) lotsArray.Last;

                    Assert.Single(lotIdObject1);
                    Assert.NotNull(lotIdObject1.First);
                    Assert.IsType<JProperty>(lotIdObject1.First);
                    Assert.Single(lotIdObject2);
                    Assert.NotNull(lotIdObject2.First);
                    Assert.IsType<JProperty>(lotIdObject2.First);

                    JProperty lotIdProperty1 = (JProperty) lotIdObject1.First;
                    JProperty lotIdProperty2 = (JProperty) lotIdObject2.First;

                    Assert.Single(lotIdProperty1);
                    Assert.NotNull(lotIdProperty1.First);
                    Assert.IsType<JValue>(lotIdProperty1.First);
                    Assert.Single(lotIdProperty2);
                    Assert.NotNull(lotIdProperty2.First);
                    Assert.IsType<JValue>(lotIdProperty2.First);

                    JValue lotIdValue1 = (JValue) lotIdProperty1.First;
                    JValue lotIdValue2 = (JValue) lotIdProperty2.First;

                    Assert.True("lot-1".Equals(lotIdValue1.Value));
                    Assert.True("lot-2".Equals(lotIdValue2.Value));

                    Assert.True(itemsArray.Count == 2);
                    Assert.NotNull(itemsArray.First);
                    Assert.IsType<JObject>(itemsArray.First);
                    Assert.NotNull(itemsArray.Last);
                    Assert.IsType<JObject>(itemsArray.Last);

                    JObject itemObject1 = (JObject) itemsArray.First;
                    JObject itemObject2 = (JObject) itemsArray.Last;

                    Assert.True(itemObject1.Count == 2);
                    Assert.IsType<JProperty>(itemObject1.First);
                    Assert.IsType<JProperty>(itemObject1.Last);
                    Assert.True(itemObject2.Count == 2);
                    Assert.IsType<JProperty>(itemObject2.First);
                    Assert.IsType<JProperty>(itemObject2.Last);

                    JProperty itemIdProperty1 = (JProperty) itemObject1.First;
                    JProperty itemRelatedLotProperty1 = (JProperty) itemObject1.Last;
                    JProperty itemIdProperty2 = (JProperty) itemObject2.First;
                    JProperty itemRelatedLotProperty2 = (JProperty) itemObject2.Last;

                    Assert.Single(itemIdProperty1);
                    Assert.NotNull(itemIdProperty1.First);
                    Assert.IsType<JValue>(itemIdProperty1.First);
                    Assert.Single(itemIdProperty2);
                    Assert.NotNull(itemIdProperty2.First);
                    Assert.IsType<JValue>(itemIdProperty2.First);

                    JValue itemIdValue1 = (JValue) itemIdProperty1.First;
                    JValue itemIdValue2 = (JValue) itemIdProperty2.First;

                    Assert.True("1".Equals(itemIdValue1.Value));
                    Assert.True("2".Equals(itemIdValue2.Value));

                    Assert.Single(itemRelatedLotProperty1);
                    Assert.NotNull(itemRelatedLotProperty1.First);
                    Assert.IsType<JValue>(itemRelatedLotProperty1.First);
                    Assert.Single(itemRelatedLotProperty2);
                    Assert.NotNull(itemRelatedLotProperty2.First);
                    Assert.IsType<JValue>(itemRelatedLotProperty2.First);

                    JValue itemRelatedValue1 = (JValue) itemRelatedLotProperty1.First;
                    JValue itemRelatedValue2 = (JValue) itemRelatedLotProperty2.First;

                    Assert.True("lot-1".Equals(itemRelatedValue1.Value));
                    Assert.True("lot-2".Equals(itemRelatedValue2.Value));
                }
            }

            public class TenderLotsNameTests
            {
                private IMapper _mapper;

                public TenderLotsNameTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestTenderLotsName1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.Lot,
                        Mappings.MappingElements.Tenders.Lots.Name
                    });

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", "ABCD") });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject tenderObject = (JObject) tender.First;

                    Assert.True(tenderObject.Count == 2);
                    Assert.IsType<JProperty>(tenderObject.First);
                    Assert.IsType<JProperty>(tenderObject.Last);

                    JProperty itemsProperty = (JProperty) tenderObject.First;
                    JProperty lotsProperty = (JProperty) tenderObject.Last;

                    Assert.True("items".Equals(itemsProperty.Name));
                    Assert.True("lots".Equals(lotsProperty.Name));
                    Assert.Single(itemsProperty);
                    Assert.Single(lotsProperty);
                    Assert.NotNull(itemsProperty.First);
                    Assert.NotNull(lotsProperty.First);
                    Assert.IsType<JArray>(itemsProperty.First);
                    Assert.IsType<JArray>(lotsProperty.First);

                    JArray lotsArray = (JArray) lotsProperty.First;
                    JArray itemsArray = (JArray) itemsProperty.First;

                    Assert.Single(lotsArray);
                    Assert.NotNull(lotsArray.First);
                    Assert.IsType<JObject>(lotsArray.First);

                    JObject lotObject = (JObject) lotsArray.First;

                    Assert.True(lotObject.Count == 2);
                    Assert.NotNull(lotObject.First);
                    Assert.IsType<JProperty>(lotObject.First);
                    Assert.NotNull(lotObject.Last);
                    Assert.IsType<JProperty>(lotObject.Last);

                    JProperty lotIdProperty = (JProperty) lotObject.First;
                    JProperty lotNameProperty = (JProperty) lotObject.Last;

                    Assert.Single(lotIdProperty);
                    Assert.NotNull(lotIdProperty.First);
                    Assert.IsType<JValue>(lotIdProperty.First);
                    Assert.Single(lotNameProperty);
                    Assert.NotNull(lotNameProperty.First);
                    Assert.IsType<JValue>(lotNameProperty.First);

                    JValue lotIdValue = (JValue) lotIdProperty.First;
                    JValue lotNameValue = (JValue) lotNameProperty.First;

                    Assert.True("lot-1".Equals(lotIdValue.Value));
                    Assert.True("ABCD".Equals(lotNameValue.Value));

                    Assert.Single(itemsArray);
                    Assert.NotNull(itemsArray.First);
                    Assert.IsType<JObject>(itemsArray.First);

                    JObject itemObject = (JObject) itemsArray.First;

                    Assert.Single(itemObject);
                    Assert.IsType<JProperty>(itemObject.First);

                    JProperty itemIdProperty = (JProperty) itemObject.First;

                    Assert.Single(itemIdProperty);
                    Assert.NotNull(itemIdProperty.First);
                    Assert.IsType<JValue>(itemIdProperty.First);

                    JValue itemIdValue = (JValue) itemIdProperty.First;

                    Assert.True("1".Equals(itemIdValue.Value));
                }
            
                [Fact]
                public void TestTenderLotsName2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.Lot,
                        Mappings.MappingElements.Tenders.Lots.Name
                    });
                    string elementDump1 = "<cac:ProcurementProjectLot xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ID schemeName=\"ID_LOTE\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ID>\n  <cac:ProcurementProject>\n    <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Suministro de Agua natural mineral mediante botellones, vasos de papel y uso de fuentes refrigeradas para los centros de mutua universal en la CCAA de Baleares</cbc:Name>\n    <cac:BudgetAmount>\n      <cbc:TotalAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2215.68</cbc:TotalAmount>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1965.12</cbc:TaxExclusiveAmount>\n    </cac:BudgetAmount>\n    <cac:RequiredCommodityClassification>\n      <cbc:ItemClassificationCode listURI=\"http://contrataciondelestado.es/codice/cl/1.04/CPV2007-1.04.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">15981100</cbc:ItemClassificationCode>\n    </cac:RequiredCommodityClassification>\n    <cac:RealizedLocation>\n      <cbc:CountrySubentity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Barcelona</cbc:CountrySubentity>\n      <cbc:CountrySubentityCode listURI=\"http://contrataciondelestado.es/codice/cl/2.06/NUTS-2016.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">ES511</cbc:CountrySubentityCode>\n    </cac:RealizedLocation>\n  </cac:ProcurementProject>\n</cac:ProcurementProjectLot>";
                    string elementDump2 = "<cac:ProcurementProjectLot xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ID schemeName=\"ID_LOTE\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2</cbc:ID>\n  <cac:ProcurementProject>\n    <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Suministro de Agua natural mineral mediante botellones, vasos de papel y uso de fuentes refrigeradas para los centros de mutua universal en la CCAA de Canarias</cbc:Name>\n    <cac:BudgetAmount>\n      <cbc:TotalAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">6584.78</cbc:TotalAmount>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">5840.16</cbc:TaxExclusiveAmount>\n    </cac:BudgetAmount>\n    <cac:RequiredCommodityClassification>\n      <cbc:ItemClassificationCode listURI=\"http://contrataciondelestado.es/codice/cl/1.04/CPV2007-1.04.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">15981100</cbc:ItemClassificationCode>\n    </cac:RequiredCommodityClassification>\n    <cac:RealizedLocation>\n      <cbc:CountrySubentity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Barcelona</cbc:CountrySubentity>\n      <cbc:CountrySubentityCode listURI=\"http://contrataciondelestado.es/codice/cl/2.06/NUTS-2016.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">ES511</cbc:CountrySubentityCode>\n    </cac:RealizedLocation>\n  </cac:ProcurementProject>\n</cac:ProcurementProjectLot>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump1), XElement.Parse(elementDump2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject tenderObject = (JObject) tender.First;

                    Assert.True(tenderObject.Count == 2);
                    Assert.IsType<JProperty>(tenderObject.First);
                    Assert.IsType<JProperty>(tenderObject.Last);

                    JProperty itemsProperty = (JProperty) tenderObject.First;
                    JProperty lotsProperty = (JProperty) tenderObject.Last;

                    Assert.True("items".Equals(itemsProperty.Name));
                    Assert.True("lots".Equals(lotsProperty.Name));
                    Assert.Single(itemsProperty);
                    Assert.Single(lotsProperty);
                    Assert.NotNull(itemsProperty.First);
                    Assert.NotNull(lotsProperty.First);
                    Assert.IsType<JArray>(itemsProperty.First);
                    Assert.IsType<JArray>(lotsProperty.First);

                    JArray lotsArray = (JArray) lotsProperty.First;
                    JArray itemsArray = (JArray) itemsProperty.First;

                    Assert.True(lotsArray.Count == 2);
                    Assert.NotNull(lotsArray.First);
                    Assert.IsType<JObject>(lotsArray.First);
                    Assert.NotNull(lotsArray.Last);
                    Assert.IsType<JObject>(lotsArray.Last);

                    JObject lotObject1 = (JObject) lotsArray.First;
                    JObject lotObject2 = (JObject) lotsArray.Last;

                    Assert.True(lotObject1.Count == 2);
                    Assert.NotNull(lotObject1.First);
                    Assert.IsType<JProperty>(lotObject1.First);
                    Assert.NotNull(lotObject1.Last);
                    Assert.IsType<JProperty>(lotObject1.Last);
                    Assert.True(lotObject2.Count == 2);
                    Assert.NotNull(lotObject2.First);
                    Assert.IsType<JProperty>(lotObject2.First);
                    Assert.NotNull(lotObject2.Last);
                    Assert.IsType<JProperty>(lotObject2.Last);

                    JProperty lotIdProperty1 = (JProperty) lotObject1.First;
                    JProperty lotNameProperty1 = (JProperty) lotObject1.Last;
                    JProperty lotIdProperty2 = (JProperty) lotObject2.First;
                    JProperty lotNameProperty2 = (JProperty) lotObject2.Last;

                    Assert.Single(lotIdProperty1);
                    Assert.NotNull(lotIdProperty1.First);
                    Assert.IsType<JValue>(lotIdProperty1.First);
                    Assert.Single(lotNameProperty1);
                    Assert.NotNull(lotNameProperty1.First);
                    Assert.IsType<JValue>(lotNameProperty1.First);
                    Assert.Single(lotIdProperty2);
                    Assert.NotNull(lotIdProperty2.First);
                    Assert.IsType<JValue>(lotIdProperty2.First);
                    Assert.Single(lotNameProperty2);
                    Assert.NotNull(lotNameProperty2.First);
                    Assert.IsType<JValue>(lotNameProperty2.First);

                    JValue lotIdValue1 = (JValue) lotIdProperty1.First;
                    JValue lotNameValue1 = (JValue) lotNameProperty1.First;
                    JValue lotIdValue2 = (JValue) lotIdProperty2.First;
                    JValue lotNameValue2 = (JValue) lotNameProperty2.First;

                    Assert.True("lot-1".Equals(lotIdValue1.Value));
                    Assert.True("Suministro de Agua natural mineral mediante botellones, vasos de papel y uso de fuentes refrigeradas para los centros de mutua universal en la CCAA de Baleares".Equals(lotNameValue1.Value));
                    Assert.True("lot-2".Equals(lotIdValue2.Value));
                    Assert.True("Suministro de Agua natural mineral mediante botellones, vasos de papel y uso de fuentes refrigeradas para los centros de mutua universal en la CCAA de Canarias".Equals(lotNameValue2.Value));

                    Assert.True(itemsArray.Count == 2);
                    Assert.NotNull(itemsArray.First);
                    Assert.IsType<JObject>(itemsArray.First);
                    Assert.NotNull(itemsArray.Last);
                    Assert.IsType<JObject>(itemsArray.Last);

                    JObject itemObject1 = (JObject) itemsArray.First;
                    JObject itemObject2 = (JObject) itemsArray.Last;

                    Assert.Single(itemObject1);
                    Assert.IsType<JProperty>(itemObject1.First);
                    Assert.Single(itemObject2);
                    Assert.IsType<JProperty>(itemObject2.First);

                    JProperty itemIdProperty1 = (JProperty) itemObject1.First;
                    JProperty itemIdProperty2 = (JProperty) itemObject2.First;

                    Assert.Single(itemIdProperty1);
                    Assert.NotNull(itemIdProperty1.First);
                    Assert.IsType<JValue>(itemIdProperty1.First);
                    Assert.Single(itemIdProperty2);
                    Assert.NotNull(itemIdProperty2.First);
                    Assert.IsType<JValue>(itemIdProperty2.First);

                    JValue itemIdValue1 = (JValue) itemIdProperty1.First;
                    JValue itemIdValue2 = (JValue) itemIdProperty2.First;

                    Assert.True("1".Equals(itemIdValue1.Value));
                    Assert.True("2".Equals(itemIdValue2.Value));
                }
            }

            public class TenderLotsValueTests
            {
                private IMapper _mapper;

                public TenderLotsValueTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestTenderLotsValue()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.Lot,
                        Mappings.MappingElements.Tenders.Lots.Value_
                    });
                    string elementDump1 = "<cac:ProcurementProjectLot xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ID schemeName=\"ID_LOTE\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1</cbc:ID>\n  <cac:ProcurementProject>\n    <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Suministro de Agua natural mineral mediante botellones, vasos de papel y uso de fuentes refrigeradas para los centros de mutua universal en la CCAA de Baleares</cbc:Name>\n    <cac:BudgetAmount>\n      <cbc:TotalAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2215.68</cbc:TotalAmount>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">1965.12</cbc:TaxExclusiveAmount>\n    </cac:BudgetAmount>\n    <cac:RequiredCommodityClassification>\n      <cbc:ItemClassificationCode listURI=\"http://contrataciondelestado.es/codice/cl/1.04/CPV2007-1.04.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">15981100</cbc:ItemClassificationCode>\n    </cac:RequiredCommodityClassification>\n    <cac:RealizedLocation>\n      <cbc:CountrySubentity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Barcelona</cbc:CountrySubentity>\n      <cbc:CountrySubentityCode listURI=\"http://contrataciondelestado.es/codice/cl/2.06/NUTS-2016.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">ES511</cbc:CountrySubentityCode>\n    </cac:RealizedLocation>\n  </cac:ProcurementProject>\n</cac:ProcurementProjectLot>";
                    string elementDump2 = "<cac:ProcurementProjectLot xmlns:cac=\"urn:dgpe:names:draft:codice:schema:xsd:CommonAggregateComponents-2\">\n  <cbc:ID schemeName=\"ID_LOTE\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">2</cbc:ID>\n  <cac:ProcurementProject>\n    <cbc:Name xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Suministro de Agua natural mineral mediante botellones, vasos de papel y uso de fuentes refrigeradas para los centros de mutua universal en la CCAA de Canarias</cbc:Name>\n    <cac:BudgetAmount>\n      <cbc:TotalAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">6584.78</cbc:TotalAmount>\n      <cbc:TaxExclusiveAmount currencyID=\"EUR\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">5840.16</cbc:TaxExclusiveAmount>\n    </cac:BudgetAmount>\n    <cac:RequiredCommodityClassification>\n      <cbc:ItemClassificationCode listURI=\"http://contrataciondelestado.es/codice/cl/1.04/CPV2007-1.04.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">15981100</cbc:ItemClassificationCode>\n    </cac:RequiredCommodityClassification>\n    <cac:RealizedLocation>\n      <cbc:CountrySubentity xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">Barcelona</cbc:CountrySubentity>\n      <cbc:CountrySubentityCode listURI=\"http://contrataciondelestado.es/codice/cl/2.06/NUTS-2016.gc\" xmlns:cbc=\"urn:dgpe:names:draft:codice:schema:xsd:CommonBasicComponents-2\">ES511</cbc:CountrySubentityCode>\n    </cac:RealizedLocation>\n  </cac:ProcurementProject>\n</cac:ProcurementProjectLot>";

                    _mapper.MapElement(pathMap, new XElement[]{ XElement.Parse(elementDump1), XElement.Parse(elementDump2) });

                    Assert.Empty(_mapper.MappedEntry);
                    Assert.Null(_mapper.MappedEntry.First);

                    _mapper.Commit();
                    RemoveMetadata(_mapper.MappedEntry);

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
                    Assert.Single(tender);
                    Assert.NotNull(tender.First);
                    Assert.IsType<JObject>(tender.First);

                    JObject tenderObject = (JObject) tender.First;

                    Assert.True(tenderObject.Count == 2);
                    Assert.IsType<JProperty>(tenderObject.First);
                    Assert.IsType<JProperty>(tenderObject.Last);

                    JProperty itemsProperty = (JProperty) tenderObject.First;
                    JProperty lotsProperty = (JProperty) tenderObject.Last;

                    Assert.True("items".Equals(itemsProperty.Name));
                    Assert.True("lots".Equals(lotsProperty.Name));
                    Assert.Single(itemsProperty);
                    Assert.Single(lotsProperty);
                    Assert.NotNull(itemsProperty.First);
                    Assert.NotNull(lotsProperty.First);
                    Assert.IsType<JArray>(itemsProperty.First);
                    Assert.IsType<JArray>(lotsProperty.First);

                    JArray lotsArray = (JArray) lotsProperty.First;
                    JArray itemsArray = (JArray) itemsProperty.First;

                    Assert.True(lotsArray.Count == 2);
                    Assert.NotNull(lotsArray.First);
                    Assert.IsType<JObject>(lotsArray.First);
                    Assert.NotNull(lotsArray.Last);
                    Assert.IsType<JObject>(lotsArray.Last);

                    JObject lotIdObject1 = (JObject) lotsArray.First;
                    JObject lotIdObject2 = (JObject) lotsArray.Last;

                    Assert.True(lotIdObject1.Count == 2);
                    Assert.NotNull(lotIdObject1.First);
                    Assert.IsType<JProperty>(lotIdObject1.First);
                    Assert.NotNull(lotIdObject1.Last);
                    Assert.IsType<JProperty>(lotIdObject1.Last);
                    Assert.True(lotIdObject2.Count == 2);
                    Assert.NotNull(lotIdObject2.First);
                    Assert.IsType<JProperty>(lotIdObject2.First);
                    Assert.NotNull(lotIdObject2.Last);
                    Assert.IsType<JProperty>(lotIdObject2.Last);

                    JProperty lotIdProperty1 = (JProperty) lotIdObject1.First;
                    JProperty lotValueProperty1 = (JProperty) lotIdObject1.Last;
                    JProperty lotIdProperty2 = (JProperty) lotIdObject2.First;
                    JProperty lotValueProperty2 = (JProperty) lotIdObject2.Last;

                    Assert.Single(lotIdProperty1);
                    Assert.NotNull(lotIdProperty1.First);
                    Assert.IsType<JValue>(lotIdProperty1.First);
                    Assert.Single(lotIdProperty2);
                    Assert.NotNull(lotIdProperty2.First);
                    Assert.IsType<JValue>(lotIdProperty2.First);

                    JValue lotIdValue1 = (JValue) lotIdProperty1.First;
                    JValue lotIdValue2 = (JValue) lotIdProperty2.First;

                    Assert.True("lot-1".Equals(lotIdValue1.Value));
                    Assert.True("lot-2".Equals(lotIdValue2.Value));

                    Assert.Single(lotValueProperty1);
                    Assert.NotNull(lotValueProperty1.First);
                    Assert.IsType<JObject>(lotValueProperty1.First);
                    Assert.Single(lotValueProperty2);
                    Assert.NotNull(lotValueProperty2.First);
                    Assert.IsType<JObject>(lotValueProperty2.First);

                    JObject lotValueObject1 = (JObject) lotValueProperty1.First;
                    JObject lotValueObject2 = (JObject) lotValueProperty2.First;

                    Assert.True(lotValueObject1.Count == 2);
                    Assert.IsType<JProperty>(lotValueObject1.First);
                    Assert.True(lotValueObject2.Count == 2);
                    Assert.IsType<JProperty>(lotValueObject2.First);

                    JProperty lotValueValueProperty1 = (JProperty) lotValueObject1.First;
                    JProperty lotValueValueProperty2 = (JProperty) lotValueObject2.First;

                    Assert.Single(lotValueValueProperty1);
                    Assert.NotNull(lotValueValueProperty1.First);
                    Assert.IsType<JValue>(lotValueValueProperty1.First);
                    Assert.Single(lotValueValueProperty2);
                    Assert.NotNull(lotValueValueProperty2.First);
                    Assert.IsType<JValue>(lotValueValueProperty2.First);

                    JValue lotValueValueValue1 = (JValue) lotValueValueProperty1.First;
                    JValue lotValueValueValue2 = (JValue) lotValueValueProperty2.First;

                    Double d1 = 2215.68;
                    Double d2 = 6584.78;
                    Assert.True(d1.Equals(lotValueValueValue1.Value));
                    Assert.True(d2.Equals(lotValueValueValue2.Value));

                    JProperty lotValueCurrencyProperty1 = (JProperty) lotValueValueProperty1.Next;
                    JProperty lotValueCurrencyProperty2 = (JProperty) lotValueValueProperty2.Next;

                    Assert.Single(lotValueCurrencyProperty1);
                    Assert.NotNull(lotValueCurrencyProperty1.First);
                    Assert.IsType<JValue>(lotValueCurrencyProperty1.First);
                    Assert.Single(lotValueCurrencyProperty2);
                    Assert.NotNull(lotValueCurrencyProperty2.First);
                    Assert.IsType<JValue>(lotValueCurrencyProperty2.First);

                    JValue lotValueCurrencyValue1 = (JValue) lotValueCurrencyProperty1.First;
                    JValue lotValueCurrencyValue2 = (JValue) lotValueCurrencyProperty2.First;

                    Assert.True("EUR".Equals(lotValueCurrencyValue1.Value));
                    Assert.True("EUR".Equals(lotValueCurrencyValue2.Value));

                    Assert.True(itemsArray.Count == 2);
                    Assert.NotNull(itemsArray.First);
                    Assert.IsType<JObject>(itemsArray.First);
                    Assert.NotNull(itemsArray.Last);
                    Assert.IsType<JObject>(itemsArray.Last);

                    JObject itemObject1 = (JObject) itemsArray.First;
                    JObject itemObject2 = (JObject) itemsArray.Last;

                    Assert.Single(itemObject1);
                    Assert.IsType<JProperty>(itemObject1.First);
                    Assert.Single(itemObject2);
                    Assert.IsType<JProperty>(itemObject2.First);

                    JProperty itemIdProperty1 = (JProperty) itemObject1.First;
                    JProperty itemIdProperty2 = (JProperty) itemObject2.First;

                    Assert.Single(itemIdProperty1);
                    Assert.NotNull(itemIdProperty1.First);
                    Assert.IsType<JValue>(itemIdProperty1.First);
                    Assert.Single(itemIdProperty2);
                    Assert.NotNull(itemIdProperty2.First);
                    Assert.IsType<JValue>(itemIdProperty2.First);

                    JValue itemIdValue1 = (JValue) itemIdProperty1.First;
                    JValue itemIdValue2 = (JValue) itemIdProperty2.First;

                    Assert.True("1".Equals(itemIdValue1.Value));
                    Assert.True("2".Equals(itemIdValue2.Value));
                }
                }

            public class TenderMainProcurementCategoryTests
            {
                private IMapper _mapper;

                public TenderMainProcurementCategoryTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestTenderMainProcurementCategory1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.MainProcurementCategory
                    });
                    string parsedElement = "1";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.MainProcurementCategory
                    });
                    string parsedElement = "2";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.MainProcurementCategory
                    });
                    string parsedElement = "3";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.MainProcurementCategory
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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestTenderNumberOfTenderers()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.NumberOfTenderers
                    });
                    string parsedElement = "1";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Assert.True(1 == Convert.ToInt32(notValue.Value));
                }
            }

            public class TenderProcurementMethodTests
            {
                private IMapper _mapper;

                public TenderProcurementMethodTests()
                {
                    Program.InitLogger();
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestTenderProcurementMethod1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.ProcurementMethod
                    });
                    string parsedElement = "1";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.ProcurementMethod
                    });
                    string parsedElement = "2";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.ProcurementMethod
                    });
                    string parsedElement = "3";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.ProcurementMethod
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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestTenderProcurementMethodDetails1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.ProcurementMethodDetails
                    });
                    string parsedElement = "3";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.ProcurementMethodDetails
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.ProcurementMethodDetails
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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestTenderProcurementMethod1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.SubmissionMethod
                    });
                    string parsedElement = "1";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.SubmissionMethod
                    });
                    string parsedElement = "2";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.SubmissionMethod
                    });
                    string parsedElement = "3";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.SubmissionMethod
                    });
                    string parsedElement = "true";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement(XName.Get("AuctionConstraintIndicator"), parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.SubmissionMethod
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.SubmissionMethod
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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestTenderProcurementMethod1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.SubmissionMethodDetails
                    });
                    string parsedElement = "es";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.SubmissionMethodDetails
                    });
                    string parsedElement1 = "es";
                    string parsedElement2 = "ca";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement1), new XElement("null", parsedElement2) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestTenderPeriodStartDate()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.TenderPeriod,
                        Mappings.MappingElements.Tenders.TenderPeriods.StartDate
                    });
                    string parsedElement = "2021-09-01";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestTenderPeriodEndDate()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.TenderPeriod,
                        Mappings.MappingElements.Tenders.TenderPeriods.EndDate
                    });
                    string parsedElement = "2021-09-01";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestTenderPeriodDurationInDays1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.TenderPeriod,
                        Mappings.MappingElements.Tenders.TenderPeriods.DurationInDays
                    });
                    string parsedElement = "3";

                    XElement element = new XElement("null", parsedElement);
                    element.SetAttributeValue("null", "ANN");

                    _mapper.MapElement(pathMap, new XElement[]{ element });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.TenderPeriod,
                        Mappings.MappingElements.Tenders.TenderPeriods.DurationInDays
                    });
                    string parsedElement = "3";

                    XElement element = new XElement("null", parsedElement);
                    element.SetAttributeValue("null", "MON");

                    _mapper.MapElement(pathMap, new XElement[]{ element });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.TenderPeriod,
                        Mappings.MappingElements.Tenders.TenderPeriods.DurationInDays
                    });
                    string parsedElement = "3";

                    XElement element = new XElement("null", parsedElement);
                    element.SetAttributeValue("null", "DAY");

                    _mapper.MapElement(pathMap, new XElement[]{ element });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.TenderPeriod,
                        Mappings.MappingElements.Tenders.TenderPeriods.DurationInDays
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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestTenderTitle()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.Title
                    });
                    string parsedElement = "ABCD";

                    _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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
                    _mapper = new Mapper(Program.Log, new Packager(Program.Log, ""));
                }

                [Fact]
                public void TestTenderValue1()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
                        Mappings.MappingElements.Tenders.Value
                    });
                    string parsedElement = "189250";
                    XElement element = new XElement("null", parsedElement);
                    element.SetAttributeValue("null", "EUR");

                    _mapper.MapElement(pathMap, new XElement[]{ element });

                    Assert.Single(_mapper.MappedEntry);
                    Assert.NotNull(_mapper.MappedEntry.First);
                    Assert.IsType<JProperty>(_mapper.MappedEntry.First);

                    JProperty tender = (JProperty) _mapper.MappedEntry.First;

                    Assert.True(Mappings.MappingElements.Tender.Equals(tender.Name));
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

                    Double value = 189250;
                    Assert.True(value.Equals(amountValue.Value));
                    Assert.True("EUR".Equals(currencyValue.Value));
                }

                [Fact]
                public void TestTenderValue2()
                {
                    IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                    {
                        Mappings.MappingElements.Tender,
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
                        Mappings.MappingElements.Tender,
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
        
        /* Tests unitarios del componente de empaquetado */
        public class PackagerTests
        {
            private IPackager _packager;

            public PackagerTests()
            {
                Program.InitLogger();
                _packager = new Packager(Program.Log, "");
            }

            [Fact]
            public void TestConstructor()
            {
                Assert.True(true); // TODO
            }

            [Fact]
            public void TestGetNamespaces()
            {
                Assert.True(true); // TODO
            }
        }

        /* Tests unitarios del componente de parseo */

        public class ParserTests
        {
            private IParser _parser;

            public ParserTests()
            {
                Program.InitLogger();
                _parser = new Parser(Program.Log, new Document("Examples/xml/exampleValid0.atom"));
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

        /* Tests unitarios del componente de provisión */

        public class ProviderTests
        {
            private IProvider _provider;

            public ProviderTests()
            {
                Program.InitLogger();
            }

            [Fact]
            public async Task TestConstructor1()
            {
                _provider = new Provider(Program.Log, EProviderOperationCode.PROVIDE_LATEST, null);

                Assert.NotNull(_provider.Files);
                Assert.IsType<AsyncCollection<Document>>(_provider.Files);

                Document document = await _provider.TakeFile();

                Assert.True("./tmp/document.atom".Equals(document.Path));
            }

            [Fact]
            public async Task TestConstructor2()
            {
                _provider = new Provider(
                    Program.Log, EProviderOperationCode.PROVIDE_SPECIFIC,
                    "Examples/xml/exampleValid0.atom"
                );

                Assert.NotNull(_provider.Files);
                Assert.IsType<AsyncCollection<Document>>(_provider.Files);

                Document document = await _provider.TakeFile();

                Assert.True("Examples/xml/exampleValid0.atom".Equals(document.Path));
            }

            [Fact]
            public async Task TestConstructor3()
            {
                _provider = new Provider(
                    Program.Log, EProviderOperationCode.PROVIDE_SPECIFIC,
                    "https://contrataciondelestado.es/sindicacion/sindicacion_643/licitacionesPerfilesContratanteCompleto3.atom"
                );

                Assert.NotNull(_provider.Files);
                Assert.IsType<AsyncCollection<Document>>(_provider.Files);

                Document document = await _provider.TakeFile();

                Assert.True("./tmp/document.atom".Equals(document.Path));
            }

            [Fact]
            public void TestConstructor4()
            {
                Action action = () => new Provider(
                    Program.Log, EProviderOperationCode.PROVIDE_SPECIFIC,
                    "httpz://contrataciondelestado.es/sindicacion/sindicacion_643/licitacionesPerfilesContratanteCompleto3.atom"
                );

                action.Should().Throw<FileNotFoundException>();
            }

            [Fact]
            public async Task TestTakeFile1()
            {
                _provider = new Provider(
                        Program.Log, EProviderOperationCode.PROVIDE_SPECIFIC,
                        "Examples/xml/exampleValid0.atom"
                );

                Document document = await _provider.TakeFile();

                Assert.True(document.Path.Equals("Examples/xml/exampleValid0.atom"));
                Assert.True(File.Exists("Examples/xml/exampleValid0.atom"));

                document = await _provider.TakeFile();

                Assert.Null(document);
            }

            [Fact]
            public async Task TestTakeFile2()
            {
                _provider = new Provider(Program.Log, EProviderOperationCode.PROVIDE_LATEST, null);

                Document document = await _provider.TakeFile();

                Assert.True(document.Path.Equals("./tmp/document.atom"));
                Assert.True(File.Exists("./tmp/document.atom"));
            }

            [Fact]
            public void TestRemoveFile1()
            {
                _provider = new Provider(Program.Log, EProviderOperationCode.PROVIDE_LATEST, null);

                string filePath = "./tmp/x.atom";

                File.Create(filePath);
                Assert.True(File.Exists(filePath));

                _provider.RemoveFile(filePath);
            }
             
            [Fact]
            public void TestRemoveFile2()
            {
                _provider = new Provider(Program.Log, EProviderOperationCode.PROVIDE_LATEST, null);

                string filePath = "./tmp/x.atom";

                _provider.RemoveFile(filePath);
                Assert.True(!File.Exists(filePath));
            }
        }
    
        /* Funciones auxiliares */

        /*  función estática RemoveMetadata(JObject) => void
         *      Elimina los metadatos de cada entrega para aislar la información para los tests unitarios
         */
        private static void RemoveMetadata(JObject mappedEntry)
        {
            mappedEntry.Remove("date");
            mappedEntry.Remove("id");
            mappedEntry.Remove("initiationType");
            mappedEntry.Remove("language");
        }
    }
}
