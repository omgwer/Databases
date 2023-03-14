
namespace Lab3.Infrastructure.Data.Common;

public class DatabaseDto
{
    private List<List<string>> response { get; set; } = new List<List<string>>();

    public void Add(List<string> inputList)
    {
        response.Add(inputList);
    }

    public List<List<string>> Get()
    {
        return response;
    }
}