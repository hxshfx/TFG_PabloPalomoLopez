using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Xunit;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using OCDS_Mapper.src.Interfaces;
using OCDS_Mapper.src.Model;
using System.Collections.Concurrent;
using System.Threading;
using System;
using System.IO;
using OCDS_Mapper.src.Exceptions;

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

            [Fact]
            public void TestMapElementTag1()
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
            public void TestMapElementTag2()
            {
                IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                {
                    "tag"
                });
                string parsedElement = "EV";

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
            }

            [Fact]
            public void TestMapElementTag3()
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
            public void TestMapElementTag4()
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
            public void TestMapElementTag5()
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
            public void TestMapElementTag6()
            {
                IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                {
                    "tag"
                });
                string parsedElement = "wrong";

                _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

                Assert.Empty(_mapper.MappedEntry);
            }

            [Fact]
            public void TestMapElementOCID()
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

            [Fact]
            public void TestMapElementTenderTitle()
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

            [Fact]
            public void TestMapElementTenderValue()
            {
                IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                {
                    "tender",
                    "value"
                });
                string parsedElement = "189250";

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
            public void TestMapElementPlanningBudgetAmount()
            {
                IEnumerable<string> pathMap = new LinkedList<string>(new string[]
                {
                    "planning",
                    "budget",
                    "amount"
                });
                string parsedElement = "189250";

                _mapper.MapElement(pathMap, new XElement[]{ new XElement("null", parsedElement) });

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