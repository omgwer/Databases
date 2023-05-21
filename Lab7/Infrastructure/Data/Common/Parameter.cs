namespace Infrastructure.Data.Common;

public class Parameter
{
    public string Name { get; set; } = String.Empty;
    public string Value { get; set; } = String.Empty;
    public string ValueType { get; set; } = String.Empty;

    public Parameter(string name, string value, string valueType)
    {
        Name = name;
        Value = value;
        ValueType = valueType;
    }
    
    public Parameter(string name, string value)
    {
        Name = name;
        Value = value;
    }
}