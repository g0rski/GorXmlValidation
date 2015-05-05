using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GorXmlValidation.Results;

namespace GorXmlValidation
{
    public sealed class Validator
    {
        private readonly RulesDefinition _rulesDefinition;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rulesDefinitionFilePath">Path to xml file with rules definition</param>
        public Validator(string rulesDefinitionFilePath)
        {
            if (!File.Exists(rulesDefinitionFilePath))
                throw new FileNotFoundException(string.Format("File '{0}' not exists", rulesDefinitionFilePath));
            if (Path.GetExtension(rulesDefinitionFilePath).ToUpper() != ".XML")
                throw new FileLoadException(string.Format("File '{0}' in not a xml", rulesDefinitionFilePath));
            _rulesDefinition = RulesDefinition.GetConfig(rulesDefinitionFilePath);
        }

        public ValidationResult ValidateValidationProperty(ValidationProperty validationPropertyRule, object propertyValue)
        {
            var result = new ValidationResult();
            if (propertyValue.GetType() != validationPropertyRule.Type)
                return result;

            if(validationPropertyRule.Greater != default(SimpeRule<decimal>) && validationPropertyRule.Greater.Value != default(decimal) && propertyValue.GetType() == typeof(decimal))
            {
                var decimaledValue = (decimal)propertyValue;

                if (decimaledValue <= validationPropertyRule.Greater.Value)
                    if (result.Errors.ContainsKey(validationPropertyRule.Name))
                        result.Errors[validationPropertyRule.Name].Add(validationPropertyRule.Greater.ErrorMessageKey);
                    else
                        result.Errors.Add(validationPropertyRule.Name, new List<string>() { validationPropertyRule.Greater.ErrorMessageKey });

            }
            return result;
        }
    }
}
