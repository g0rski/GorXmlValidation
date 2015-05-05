using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GorXmlValidation.Tests
{
    [TestClass]
    public class ValidationTests
    {

        private string TargetDIr;
        private string XmlFileName;

        [TestInitialize]
        public void Init()
        {
            TargetDIr = AppDomain.CurrentDomain.BaseDirectory;
            XmlFileName = @"\test.xml";
        }

        [TestMethod]
        public void TestSImpleValidation()
        {

            var validator = new Validator(TargetDIr + XmlFileName);

            var validationProperty = new ValidationProperty()
            {
                Greater = new SimpeRule<decimal>(){Value =  (decimal)12m,ErrorMessageKey="12 key"},
                Lower = new SimpeRule<decimal>() { Value = (decimal)14m, ErrorMessageKey = "14 key" },
                StringType = typeof(decimal).FullName,

            };

            validator.ValidateValidationProperty(validationProperty, 13m);
        }
    }
}
