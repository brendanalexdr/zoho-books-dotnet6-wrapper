using System.Diagnostics;
using System.Web;
using zohobooks.exceptions;
using zohobooks.parser;

namespace zohobooks.util;

public class ZohoHttpClient
{
    static HttpClient GetClient()
    {
        HttpClient client = new HttpClient();
        client.Timeout = new TimeSpan(0, 0, 60);
        client.DefaultRequestHeaders.Add("Accept-Charset", "UTF-8");
        client.DefaultRequestHeaders.Add("User-Agent", "ZohoBooks-dotnet-Wrappers/1.0");
        return client;
    }
     static string GetQueryString(string url, Dictionary<object, object> parameters)
    {
        var ub = new UriBuilder(url);
        var param = HttpUtility.ParseQueryString(ub.Query);
        foreach (var parameter in parameters)
            param.Add(parameter.Key.ToString(), parameter.Value.ToString());
        ub.Query = param.ToString();
        return ub.ToString();
    }

    public static async Task<HttpResponseMessage> GetAsync(string url, Dictionary<object, object> parameters)
    {
        var client = GetClient();
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        var responce = await client.GetAsync(GetQueryString(url, parameters));
        if (responce.IsSuccessStatusCode)
            return responce;
        else
            throw new BooksException(ErrorParser.getErrorMessage(responce));
    }

    public static async Task<HttpResponseMessage> PostAsync(string url, Dictionary<object, object> requestBody)
    {
        var client = GetClient();
        List<KeyValuePair<string, string>> contentBody = new List<KeyValuePair<string, string>>();
        foreach (var requestbodyParam in requestBody)
        {
            var temp = new KeyValuePair<string, string>(requestbodyParam.Key.ToString(), requestbodyParam.Value.ToString());
            contentBody.Add(temp);
        }
        var content = new FormUrlEncodedContent(contentBody);
        var responce = await client.PostAsync(url, content);
        if (responce.IsSuccessStatusCode)
            return responce;
        else
            throw new BooksException(ErrorParser.getErrorMessage(responce));
    }

     public static async Task<HttpResponseMessage> PostAsync(string url, Dictionary<object, object> parameters, Dictionary<object, object> requestBody, KeyValuePair<string, string[]> attachments)
    {
        var client = GetClient();
        var boundary = DateTime.Now.Ticks.ToString();
        client.DefaultRequestHeaders.Add("Accept", "application/json");

        MultipartFormDataContent content = new MultipartFormDataContent("--boundary--");
        if (requestBody != null)
            foreach (var requestbodyParam in requestBody)
                content.Add(new StringContent(requestbodyParam.Value.ToString()), requestbodyParam.Key.ToString());
        if (attachments.Value != null)
        {
            foreach (var file_path in attachments.Value)
                if (file_path != null)
                {
                    string _filename = Path.GetFileName(file_path);
                    FileStream fileStream = new FileStream(file_path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    StreamContent fileContent = new StreamContent(fileStream);
                    content.Add(fileContent, attachments.Key, _filename);
                }
        }
        var responce = await client.PostAsync(GetQueryString(url, parameters), content);
        if (responce.IsSuccessStatusCode)
            return responce;
        else
            throw new BooksException(ErrorParser.getErrorMessage(responce));
    }

     public static async Task<HttpResponseMessage> PutAsync(string url, Dictionary<object, object> requestBody)
    {
        var client = GetClient();
        List<KeyValuePair<string, string>> contentBody = new List<KeyValuePair<string, string>>();
        foreach (var requestbodyParam in requestBody)
        {
            var temp = new KeyValuePair<string, string>(requestbodyParam.Key.ToString(), requestbodyParam.Value.ToString());
            contentBody.Add(temp);
        }
        var content = new FormUrlEncodedContent(contentBody);
        var responce = await client.PutAsync(url, content);
        if (responce.IsSuccessStatusCode)
            return responce;
        else
            throw new BooksException(ErrorParser.getErrorMessage(responce));
    }
    public static async Task<HttpResponseMessage> PutAsync(string url, Dictionary<object, object> parameters, Dictionary<object, object> requestBody, KeyValuePair<string, string> attachment)
    {
        var client = GetClient();
        var boundary = DateTime.Now.Ticks.ToString();
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        MultipartFormDataContent content = new MultipartFormDataContent(boundary);
        foreach (var requestBodyParam in requestBody)
            content.Add(new StringContent(requestBodyParam.Value.ToString()), requestBodyParam.Key.ToString());
        if (attachment.Value != null)
        {
            string _filename = Path.GetFileName(attachment.Value);
            FileStream fileStream = new FileStream(attachment.Value, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamContent fileContent = new StreamContent(fileStream);
            content.Add(fileContent, attachment.Key, _filename);
        }
        var responce = await client.PutAsync(GetQueryString(url, parameters), content);
        if (responce.IsSuccessStatusCode)
            return responce;
        else
            throw new BooksException(ErrorParser.getErrorMessage(responce));
    }

    public static async Task<HttpResponseMessage> DeleteAsync(string url, Dictionary<object, object> parameters)
    {
        var client = GetClient();
        var responce = await client.DeleteAsync(GetQueryString(url, parameters));
        if (responce.IsSuccessStatusCode)
            return responce;
        else
            throw new BooksException(ErrorParser.getErrorMessage(responce));
    }

     public static async Task GetFileAsync(string url, Dictionary<object, object> parameters)
    {
        var client = GetClient();
        client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        var responce = client.GetAsync(GetQueryString(url, parameters)).Result;
        if (responce.IsSuccessStatusCode)
        {
            string contentDisposition = responce.Content.Headers.ContentDisposition.ToString();
            const string contentFileNamePortion = "filename=\"";
            var fileNameStartIndex = contentDisposition.IndexOf(contentFileNamePortion, StringComparison.InvariantCulture) + contentFileNamePortion.Length;
            var originalFileNameLength = contentDisposition.Length - fileNameStartIndex - 1;
            var filename = contentDisposition.Substring(fileNameStartIndex, originalFileNameLength);
            FileStream fileStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            await responce.Content.CopyToAsync(fileStream);
            fileStream.Close();
            Process.Start(filename);
        }
        else
            throw new BooksException(ErrorParser.getErrorMessage(responce));
    }

}
