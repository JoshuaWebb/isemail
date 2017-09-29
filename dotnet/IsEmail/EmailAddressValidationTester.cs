using System.Collections;
using System.IO;
using IsEmail.Sample;
using NUnit.Framework;

namespace IsEmail
{
    [TestFixture]
    public class EmailAddressValidationTester
    {
        [Test, TestCaseSource(typeof(EmailAddressTestCaseSource), nameof(EmailAddressTestCaseSource.TestCases))]
        public void TestValidation(string address, int value)
        {
            var validator = new SampleAddressValidator();
            var isValid = validator.IsValid(address);

            string message;
            bool shouldBeValid;
            if (value <= 1)
            {
                message = "This address is valid";
                shouldBeValid = true;
            }
            // everything at (or above) this is always invalid (this is the most permissive test)
            else if (value <= 127)
            {
                message = "This address is valid under some circumstances";
                shouldBeValid = true;
            }
            else
            {
                message = "This address is invalid";
                shouldBeValid = false;
            }

            Assert.AreEqual(shouldBeValid, isValid, message);
        }
    }

    public class EmailAddressTestCaseSource
    {
        public static IEnumerable TestCases
        {
            get
            {
                var baseDir = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData");
                var metaPath = Path.Combine(baseDir, @"meta.xml");
                var testPath = Path.Combine(baseDir, @"tests.xml");

                var categories = Category.LoadCategories(metaPath);
                foreach (var testData in TestData.LoadTestData(testPath))
                {
                    var category = categories[testData.Category];

                    var testCase = new TestCaseData(testData.Address, category.Value)
                        .SetCategory(testData.Category)
                        .SetName($"{testData.Diagnosis}: {testData.Address}")
                        .SetDescription(category.Description)
                        .SetProperty(nameof(testData.Comment), testData.Comment)
                        .SetProperty(nameof(testData.Source), testData.Source)
                        .SetProperty(nameof(testData.SourceLink), testData.SourceLink);

                    yield return testCase;
                }
            }
        }
    }
}
