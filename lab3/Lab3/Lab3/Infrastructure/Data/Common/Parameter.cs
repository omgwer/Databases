namespace Lab3.Infrastructure.Data.Common;

public class Parameter
{
    public string Name { get; set; } = String.Empty;
    public string Value { get; set; } = String.Empty;

    public Parameter(string name, string value)
    {
        Name = name;
        Value = value;
    }
}