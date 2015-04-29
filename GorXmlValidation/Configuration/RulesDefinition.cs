using System.Xml.Serialization;

[XmlType(AnonymousType = true)]
[XmlRoot(IsNullable = false, ElementName = "applicationValidators")]
public class RulesDefinition
{
    [XmlArray(ElementName = "rules", IsNullable = false)]
    [XmlArrayItem(ElementName = "rule")]
    public Rule[] Rules { get; set; }
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
    public SimpeRule Match { get; set; }

    [XmlElement(ElementName = "notEmpty")]
    public SimpeRule NotEmpty { get; set; }

    [XmlElement(ElementName = "greater")]
    public SimpeRule Greater { get; set; }

    [XmlElement(ElementName = "lower")]
    public SimpeRule Lower { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "type")]
    public string Type { get; set; }
}

[XmlType(AnonymousType = true)]
public class SimpeRule
{
    [XmlAttribute(AttributeName = "errorMessageKey")]
    public string ErrorMessageKey { get; set; }
    [XmlAttribute(AttributeName = "value")]
    public string Value { get; set; }
}
