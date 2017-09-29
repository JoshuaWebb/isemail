using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace IsEmail
{
    public struct TestData
    {
        public TestData(string address, string comment, string category,
            string diagnosis, string source, string sourceLink)
        {
            Address = address;
            Comment = comment ?? "";
            Category = category;
            Diagnosis = diagnosis;
            Source = source ?? "";
            SourceLink = sourceLink ?? "";
        }

        public string Address { get; }
        public string Comment { get; }
        public string Category { get; }
        public string Diagnosis { get; }
        public string Source { get; }
        public string SourceLink { get; }

        public static IEnumerable<TestData> LoadTestData(string path)
        {
            XDocument testsDocument;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                testsDocument = XDocument.Load(fs, LoadOptions.None);

            foreach (var testElement in testsDocument.Descendants("test"))
            {
                var category = (string) testElement.Element("category");
                var address = (string) testElement.Element("address");
                var comment = (string) testElement.Element("comment");
                var diagnosis = (string) testElement.Element("diagnosis");
                var source = (string) testElement.Element("source");
                var sourceLink = (string) testElement.Element("sourceLink");

                yield return new TestData(address, comment, category, diagnosis, source, sourceLink);
            }
        }
    }
}
