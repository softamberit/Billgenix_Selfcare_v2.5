using Newtonsoft.Json;

namespace BillgenixProcessorService.Models;

public class ErrorDetails
{
    public ErrorDetails()
    {

    }
    public ErrorDetails(string msg)
    {
        Message = msg;
    }
    public ErrorDetails(string msg, string code)
    {
        Message = msg;
        StatusCode = code;
    }
    public string StatusCode { get; set; }
    public string Message { get; set; }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
