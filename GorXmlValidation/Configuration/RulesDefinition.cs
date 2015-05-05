using System;
using System.IO;
using System.Xml.Serialization;



[XmlType(AnonymousType = true)]
[XmlRoot(IsNullable = false, ElementName = "applicationValidators")]
public class RulesDefinition
{
    [XmlArray(ElementName = "rules", IsNullable = false)]
    [XmlArrayItem(ElementName = "rule")]
    public Rule[] Rules { get; set; }

    public static RulesDefinition GetConfig(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        {
            var serializer = new XmlSerializer(typeof(RulesDefinition));
            return (RulesDefinition)serializer.Deserialize(reader);
        }
    }
}

public class Rule
{
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlArray(ElementName = "conditions", IsNullable = true)]
    [XmlArrayItem(ElementName = "condition")]
    public Condition[] Conditions { get; set; }

    [XmlArray(ElementName = "properties", IsNullable = true)]
    [XmlArrayItem(ElementName = "property")]
    public ValidationProperty[] Properties { get; set; }
}

[XmlType(AnonymousType = true)]
public class Condition
{
    [XmlElement(ElementName = "when")]
    public Case When { get; set; }
    [XmlElement(ElementName = "then")]
    public Case Then { get; set; }
}

[XmlType(AnonymousType = true)]
public class Case
{
    [XmlElement(ElementName = "property")]
    public ValidationProperty Property { get; set; }
}

[XmlType(AnonymousType = true)]
public class ValidationProperty
{
    [XmlElement(ElementName = "match")]
    public SimpeRule<string> Match { get; set; }
    
    [XmlElement(ElementName = "notEmpty")]
    public SimpeRule<bool> NotEmpty { get; set; }

    [XmlElement(ElementName = "greater")]
    public SimpeRule<decimal> Greater { get; set; }

    [XmlElement(ElementName = "lower")]
    public SimpeRule<decimal> Lower { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "type")]
    public string StringType { get; set; }

    public Type Type
    {
        get
        {
            return Type.GetType(StringType);
        }
    }
}

[XmlType(AnonymousType = true)]
public class SimpeRule<T>
{
    [XmlAttribute(AttributeName = "errorMessageKey")]
    public string ErrorMessageKey { get; set; }
    [XmlAttribute(AttributeName = "value")]
    public T Value { get; set; }
}

