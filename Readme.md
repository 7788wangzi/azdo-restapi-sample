## Processing strings
In this project, there are sample code of processing string so that the generated string result could be used as part of a path, the string processing  includes:
+ Lower case the strings
+ Seprate the string with hyphen
+ Remove characters that are invalid for a path
+ Remove small words which could be configured in config.json
+ Limit the string length to a maimum number
+ Remove redundancy from a part with other sections in the path

## Azure DevOps work item RESTful API with C#
In this project, there are sample code of accessing the Azure DevOps work items with REST APIs in C# language.

For the API, you could reference AzDo Work Item API at [azure-devops-rest-5.0](https://docs.microsoft.com/en-us/rest/api/azure/devops/wit/?view=azure-devops-rest-5.0)

GetService
```CS
//GetService
public static async Task<string> GetService(string url, string patToken)
{
    using (HttpClient client = new HttpClient())
    {
        client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                    string.Format("{0}:{1}", "", patToken))));

        using (HttpResponseMessage response = await client.GetAsync(url))
        {
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
    }
}
```

PostService for create a work item
```CS
//PostService
public static async Task<string> PostService(string url, string stringContent, string patToken)
{
    using (HttpClient client = new HttpClient())
    {
        client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                    string.Format("{0}:{1}", "", patToken))));

        HttpContent httpContent = new StringContent(stringContent, Encoding.UTF8, "application/json-patch+json"); //media type is application/json-patch+json
        using (HttpResponseMessage response = await client.PostAsync(url, httpContent))
        {
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;

        }
    }
}
```

PatchService for update a work item
```CS
//PatchService
public static async Task<string> PatchService(string url, string stringContent, string patToken)
{
    using (HttpClient client = new HttpClient())
    {
        client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                    string.Format("{0}:{1}", "", patToken))));

        HttpContent httpContent = new StringContent(stringContent, Encoding.UTF8, "application/json-patch+json"); //media type is application/json-patch+json
        using (HttpResponseMessage response = await client.PatchAsync(url, httpContent))
        {
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;

        }
    }
}
```

DeleteService
```CS
//DeleteService
public static async Task<string> DeleteService(string url, string patToken)
{
    using(HttpClient client = new HttpClient())
    {
        client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                    string.Format("{0}:{1}", "", patToken))));

        using(HttpResponseMessage response = await client.DeleteAsync(url))
        {
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
    }
}
```

You could [email me](mailto:qijiexue@outlook.com) if  you have any problems with getting understanding of this sample code.
