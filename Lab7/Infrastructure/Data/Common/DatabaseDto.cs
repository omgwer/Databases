
namespace Infrastructure.Data.Common;

public class DatabaseDto
{
    private List<List<string>> response { get; } = new ();

    public void Add(List<string> inputList)
    {
        response.Add(inputList);
    }

    public List<List<string>> Get()
    {
        return response;
    }
}