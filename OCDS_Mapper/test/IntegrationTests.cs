using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Xunit;
using OCDS_Mapper.src.Interfaces;
using OCDS_Mapper.src.Model;
using OCDS_Mapper.src.Exceptions;
using System.IO;
using System.Threading;

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
            XElement validElement = _GetEntryElement(true);
            _parser.SetEntryRootElement(validElement);

            Assert.NotNull(_parser.EntryRoot);
            Assert.True(_parser.EntryRoot.HasElements);
            Assert.True("ContractFolderStatus".Equals(_parser.EntryRoot.Name.LocalName));
        }

        [Fact]
        public void TestParserSetEntryRootElement2()
        {
            XElement validElement = _GetEntryElement(false);
            Assert.Throws<InvalidParsedElementException>(() => _parser.SetEntryRootElement(validElement));
        }

        [Fact]
        public void TestParserGetElement1()
        {
            IDictionary<string, XNamespace> namespaces = _parser.GetNamespaces();

            IEnumerable<XName> validPath = new LinkedList<XName>(new XName[]
            {
                namespaces["cac"] + "ProcurementProject",
                namespaces["cbc"] + "Name"
            }
            );

            XElement entry = _GetEntryElement(true);
            _parser.SetEntryRootElement(entry);

            XElement[] queriedElement = _parser.GetElement(validPath);

            Assert.NotNull(queriedElement[0]);
            Assert.Equal(queriedElement[0].Name.Namespace, namespaces["cbc"]);
            Assert.True("Name".Equals(queriedElement[0].Name.LocalName));
            Assert.Equal(queriedElement[0].Parent.Name.Namespace, namespaces["cac"]);
            Assert.True("ProcurementProject".Equals(queriedElement[0].Parent.Name.LocalName));
        }

        [Fact]
        public void TestParserGetElement2()
        {
            IDictionary<string, XNamespace> namespaces = _parser.GetNamespaces();

            IEnumerable<XName> validPath = new LinkedList<XName>(new XName[]
            {
                namespaces["cac"] + "Invalid"
            }
            );

            XElement entry = _GetEntryElement(true);
            _parser.SetEntryRootElement(entry);

            XElement[] queriedElement = _parser.GetElement(validPath);
            Assert.Null(queriedElement);
        }

        [Fact]
        public void TestParserGetNextFile1()
        {
            Uri uri = _parser.GetNextFile();
            Assert.True(uri.Equals(new Uri("https://contrataciondelestado.es/sindicacion/sindicacion_643/licitacionesPerfilesContratanteCompleto3_20210403_150021.atom")));

            Cleanup();
        }

        [Fact]
        public void TestParserGetNextFile2()
        {
            IParser otherParser = new Parser(Program.Log, "Examples/xml/licitacionesPerfilesContratanteCompleto3_without_next.atom");
            Assert.Null(otherParser.GetNextFile());

            Cleanup();
        }


        [Fact]
        public void TestProviderHasNext_TakeFile1()
        {
            _provider = new Provider(
                    Program.Log, Provider.EProviderOperationCode.PROVIDE_SPECIFIC,
                    "Examples/xml/licitacionesPerfilesContratanteCompleto3.atom"
            );

            Assert.True(_provider.HasNext());
            Assert.True(_provider.TakeFile().Equals("Examples/xml/licitacionesPerfilesContratanteCompleto3.atom"));
            Assert.True(File.Exists("Examples/xml/licitacionesPerfilesContratanteCompleto3.atom"));

            Cleanup();
        }

        [Fact]
        public void TestProviderHasNext_TakeFile2()
        {
            _provider = new Provider(Program.Log, Provider.EProviderOperationCode.PROVIDE_LATEST);

            Assert.True(_provider.HasNext());
            Assert.True(_provider.TakeFile().Equals("./tmp/document.atom"));
            Assert.True(File.Exists("./tmp/document.atom"));

            Cleanup();
        }

        [Fact]
        public void TestProviderParser()
        {
            _provider = new Provider(Program.Log, Provider.EProviderOperationCode.PROVIDE_ALL);

            Thread.Sleep(8 * 1000);
            _provider.SetParser(new Parser(Program.Log, "./tmp/document1.atom"));

            Assert.True(_provider.HasNext());
            Assert.True(_provider.TakeFile().Equals("./tmp/document1.atom"));
            Assert.True(File.Exists("./tmp/document1.atom"));

            Cleanup();
        }


        /* Funciones auxiliares */

        private XElement _GetEntryElement(bool valid)
        {
            IEnumerable<XElement> entrySet = _parser.GetEntrySet(_parser.GetNamespaces());
            XElement entryElement = entrySet.OrderBy(x => new Random().Next()).Take(1).First();
            if (!valid)
            {
                entryElement.Elements().Last().Remove();
            }
            return entryElement;
        }

        private void Cleanup()
        {
            if (Directory.Exists("./tmp"))
            {
                DirectoryInfo di = new DirectoryInfo("./tmp");
                foreach (FileInfo fi in di.GetFiles())
                {
                    fi.Delete();
                }
            }
        }
    }
}