using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace IsEmail
{
    public struct Category
    {
        public Category(string id, int value, string description)
        {
            Id = id;
            Value = value;
            Description = description;
        }

        public int Value { get; }
        public string Id { get; }
        public string Description { get; }

        public static IDictionary<string, Category> LoadCategories(string path)
        {
            XDocument metaDocument;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                metaDocument = XDocument.Load(fs, LoadOptions.None);

            var categories = new Dictionary<string, Category>();
            foreach (var categoryElement in metaDocument.Descendants("Categories").Single().Descendants("item"))
            {
                var id = (string)categoryElement.Attribute("id");
                var value = (int)categoryElement.Element("value");
                var description = (string)categoryElement.Element("description");
                categories[id] = new Category(id, value, description);
            }

            return categories;
        }
    }
}
