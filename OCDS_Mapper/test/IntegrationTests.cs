using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;
using OCDS_Mapper.src.Interfaces;
using OCDS_Mapper.src.Model;
using OCDS_Mapper.src.Exceptions;

namespace OCDS_Mapper.test
{
    /* Clase de tests de integración */

    public class IntegrationTests
    {
        
        private IParser _parser;
        private IProvider _provider;

        public IntegrationTests()
        {
            Program.InitLogger();

            _parser = new Parser(Program.Log, "Examples/xml/licitacionesPerfilesContratanteCompleto3.atom");
            _provider = new Provider(Program.Log, Provider.EProviderOperationCode.PROVIDE_ALL);
        }

        public class ParserTests
        {

            private IParser _parser;

            public ParserTests()
            {
                Program.InitLogger();

                _parser = new Parser(Program.Log, "Examples/xml/licitacionesPerfilesContratanteCompleto3.atom");
            }

            [Fact]
            public void TestParserGetEntrySet()
            {
                IEnumerable<XElement> entrySet = _parser.GetEntrySet(_parser.GetNamespaces());

                Assert.NotEmpty(entrySet);
                foreach (var entry in entrySet)
                {
                    IEnumerable<XElement> entryElements = entry.Elements();
                    bool containsID = false, containsFolder = false;

                    using (IEnumerator<XElement> entryElementsEnumerator = entryElements.GetEnumerator())
                    {
                        while (entryElementsEnumerator.MoveNext() && (!containsID || !containsFolder))
                        {
                            XElement currentElement = entryElementsEnumerator.Current;
                            if (!containsID && currentElement.Name.LocalName.Equals("id"))
                            {
                                containsID = true;
                            }
                            else if (!containsFolder && currentElement.Name.LocalName.Equals("ContractFolderStatus"))
                            {
                                containsFolder = true;
                            }
                        }
                    }
                    Assert.True(containsID && containsFolder);
                }
            }

            [Fact]
            public void TestParserSetEntryRootElement1()
            {
                XElement validElement = _GetEntryElement(_parser, true);
                _parser.SetEntryRootElement(validElement);

                Assert.NotNull(_parser.EntryRoot);
                Assert.True(_parser.EntryRoot.HasElements);
                Assert.True("ContractFolderStatus".Equals(_parser.EntryRoot.Name.LocalName));
            }

            [Fact]
            public void TestParserSetEntryRootElement2()
            {
                XElement validElement = _GetEntryElement(_parser, false);
                Assert.Throws<InvalidParsedElementException>(() => _parser.SetEntryRootElement(validElement));
            }

            [Fact]
            public void TestParserGetElements1()
            {
                IDictionary<string, XNamespace> namespaces = _parser.GetNamespaces();

                IEnumerable<XName> validPath = new LinkedList<XName>(new XName[]
                    {
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cbc"] + "Name"
                    }
                );

                XElement entry = _GetEntryElement(_parser, true);
                _parser.SetEntryRootElement(entry);

                XElement[] queriedElements = _parser.GetElements(validPath);

                Assert.NotNull(queriedElements[0]);
                Assert.Equal(queriedElements[0].Name.Namespace, namespaces["cbc"]);
                Assert.True("Name".Equals(queriedElements[0].Name.LocalName));
                Assert.Equal(queriedElements[0].Parent.Name.Namespace, namespaces["cac"]);
                Assert.True("ProcurementProject".Equals(queriedElements[0].Parent.Name.LocalName));
            }

            [Fact]
            public void TestParserGetElements2()
            {
                IDictionary<string, XNamespace> namespaces = _parser.GetNamespaces();

                IEnumerable<XName> validPath = new LinkedList<XName>(new XName[]
                    {
                        namespaces["cac"] + "Invalid"
                    }
                );

                XElement entry = _GetEntryElement(_parser, true);
                _parser.SetEntryRootElement(entry);

                XElement[] queriedElements = _parser.GetElements(validPath);
                Assert.Null(queriedElements);
            }

            [Fact]
            public void TestParserGetElements3()
            {
                IDictionary<string, XNamespace> namespaces = _parser.GetNamespaces();

                IEnumerable<XName> validPath = new LinkedList<XName>(new XName[]
                    {
                        namespaces["cac"] + "AdditionalDocumentReference"
                    }
                );

                IEnumerable<XElement> entrySet = _parser.GetEntrySet(_parser.GetNamespaces());
                XElement entry = entrySet.ElementAt(10);

                _parser.SetEntryRootElement(entry);

                XElement[] queriedElements = _parser.GetElements(validPath);

                Assert.True(queriedElements.Count() == 2);

                foreach (var queriedElement in queriedElements)
                {
                    Assert.Equal(queriedElement.Name.Namespace, namespaces["cac"]);
                    Assert.True("AdditionalDocumentReference".Equals(queriedElement.Name.LocalName));
                }
            }

            [Fact]
            public void TestParserGetNextFile1()
            {
                Uri uri = _parser.GetNextFile();
                Assert.True(uri.Equals(new Uri("https://contrataciondelestado.es/sindicacion/sindicacion_643/licitacionesPerfilesContratanteCompleto3_20210403_150021.atom")));
            }

            [Fact]
            public void TestParserGetNextFile2()
            {
                IParser otherParser = new Parser(Program.Log, "Examples/xml/licitacionesPerfilesContratanteCompleto3_without_next.atom");
                Assert.Null(otherParser.GetNextFile());
            }
        }

        [Fact]
        public async Task TestProviderParser()
        {
            string filePath1 = await _provider.TakeFile();
            Assert.True(filePath1.Equals("./tmp/document1.atom"));

            _provider.SetParser(new Parser(Program.Log, "./tmp/document1.atom"));

            string filePath2 = await _provider.TakeFile();
            Assert.True(filePath2.Equals("./tmp/document2.atom"));

            Assert.True(File.Exists("./tmp/document1.atom"));
            Assert.True(File.Exists("./tmp/document2.atom"));
        }


        /* Funciones auxiliares */

        private static XElement _GetEntryElement(IParser parser, bool valid)
        {
            IEnumerable<XElement> entrySet = parser.GetEntrySet(parser.GetNamespaces());
            XElement entryElement = entrySet.OrderBy(x => new Random().Next()).Take(1).First();
            if (!valid)
            {
                entryElement.Elements().Last().Remove();
            }
            return entryElement;
        }
    }
}