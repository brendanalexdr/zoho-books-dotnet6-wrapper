namespace zohobooks.parser;

public class ErrorParser
{
    internal static string getErrorMessage(HttpResponseMessage responce)
    {
        var message = "";
        var jsonObj =  ZohoSerializer.Deserialize<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
        if (jsonObj.ContainsKey("message"))
        {
            message = jsonObj["message"].ToString();
        }
        return message;
    }
}
