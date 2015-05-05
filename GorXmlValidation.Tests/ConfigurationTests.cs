using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GorXmlValidation.Tests
{
    [TestClass]
    public class ConfigurationTests
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
        public void TestSerialization()
        {

            var simpleRule =  new SimpeRule<decimal>(){
                    ErrorMessageKey="message",
                    Value=12};
            var simpleRuleString = new SimpeRule<string>()
            {
                ErrorMessageKey = "message",
                Value = "asdasd"
            };

            var simpleRulebool = new SimpeRule<bool>()
            {
                ErrorMessageKey = "message",
                Value = true
            };

            var property = new ValidationProperty()
            {
                Greater = simpleRule,
               Lower = simpleRule,
                Match = simpleRuleString,
               Name = "name",
                NotEmpty = simpleRulebool,
               StringType = typeof(int).FullName
                };
                
                var conditions = new List<Condition>();
            conditions.Add(new Condition(){When = new Case{Property = property},Then = new Case {Property = property}});
            conditions.Add(new Condition(){When = new Case{Property = property},Then = new Case {Property = property}});

            var properties = new List<ValidationProperty>();

            properties.Add(property);
            properties.Add(property);
            properties.Add(property);

            var rules = new List<Rule>();
            rules.Add(new Rule(){Conditions = conditions.ToArray(),Name="test1"});
            rules.Add(new Rule(){Properties = properties.ToArray(),Name="test2"});

            var obj = new RulesDefinition()
            {
                Rules = rules.ToArray()
            };

            var serializer = new XmlSerializer(typeof(RulesDefinition));
            var filePath = TargetDIr + XmlFileName;
            using (var fileStream = new StreamWriter(filePath))
            {
                serializer.Serialize(fileStream, obj);
            }
        }
    
        [TestMethod]
        public void TestDeserialization()
        {
            var rules = RulesDefinition.GetConfig(TargetDIr + XmlFileName);
            Assert.IsNotNull(rules);
        }
    }
}
