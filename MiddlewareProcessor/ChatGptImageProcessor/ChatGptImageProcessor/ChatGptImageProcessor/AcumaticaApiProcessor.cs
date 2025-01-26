// See https://aka.ms/new-console-template for more information


using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

public class Component
{
    public int ID { get; set; }
    public string Manufacturer { get; set; }
    public string Information { get; set; }
    public string Description { get; set; }
    public List<string> Vendor { get; set; }
    public string Use { get; set; }
    public string Lead { get; set; }
}

internal class AcumaticaApiProcessor
{
    private static string? _acumaticaInstanceUrl;
    private static string? _acumaticaUser;
    private static string? _acumaticaPwd;
    private static string? _ImageFolder;
    private static string? _CollectionTargetID; 
    private static string? _base64Image;

    private static void SetVariables()
    {
        var directoryPath = AppDomain.CurrentDomain.BaseDirectory;
        var configFilePath = Path.Combine(directoryPath, "Config.json");
        if (!File.Exists(configFilePath))
        {
            throw new FileNotFoundException($"Config file not found at {configFilePath}");
        }
        var jsonContent = File.ReadAllText(configFilePath);
        var config = JObject.Parse(jsonContent);

        _acumaticaInstanceUrl = config["TargetERPInstanceURL"]?.ToString();
        _acumaticaUser = config["User"]?.ToString();
        _acumaticaPwd = config["Pwd"]?.ToString();
        _ImageFolder = config["ImageDirectory"]?.ToString();
        _CollectionTargetID = config["CollectionTargetID"]?.ToString();


        //todo: get other config variables here so we dont have redundant code.
    }


    public static Component ParseJsonToComponent(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<Component>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while parsing JSON: {ex.Message}");
            return null;
        }
    }


    public static int GenerateRandomSixDigitNumber()
    {
        Random random = new Random();
        return random.Next(100000, 1000000); // Upper bound is exclusive, so use 1000000
    }

    internal static string SendResultsToCollection(string jsonToParse, string base64Image = "")
    {
        _base64Image = base64Image;
        SetVariables();


        var client = new HttpClient();
        Login(client);
        //string collectionTarget = GetCollectionTarget(client);



        jsonToParse = jsonToParse.Replace("```json", "");
        jsonToParse = jsonToParse.Replace("```", "");


        Component cp = new Component();  //ParseJsonToComponent(jsonToParse);

        var cpJson = JObject.Parse(jsonToParse);

        cp.Description = cpJson["description"]?.ToString();
        cp.Information = cpJson["information"]?.ToString();
        cp.Manufacturer = cpJson["manufacturer"]?.ToString();
        cp.Use = cpJson["use"]?.ToString();
        cp.Lead = cpJson["lead"]?.ToString();
        //cp.Vendor. = "Digi-Key, Mouser, Newark";
        cp.ID = GenerateRandomSixDigitNumber();

        string testResult = PutCollectionAwnser(client,cp);
        //string imageSetResults = PutImage(client, testResult, base64Image);
        
        Logout(client);

        //return Content.Result;

        /*
        //var client = new HttpClient();
        request = new HttpRequestMessage(HttpMethod.Get, "https://hackathon.acumatica.com/Beta/entity/StockWise360/24.200.001/CollectionTarget");
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("Authorization", "••••••");
        request.Headers.Add("Cookie", ".ASPXAUTH=351C1ED2660205555923692245AA9BFCCEFD080B32BB79158DFD02946B47AC7AAE4C7BC47BB2FD626031581BE45975CA64C8BB0EE45ED6AFFFC4CF249786D6FD5AB748A004E2EAA2D08834F3058C8CFA0D436D5D98FEA10FEEDC104D613C9631A9C0E91A67970963301D673A866B1820C5F4B93E; ASP.NET_SessionId=lf5dgowehqehfyqkf0t50abh; Locale=TimeZone=GMTM0800A&Culture=en-US; UserBranch=16; requestid=B36837D993125C8311EFDBA62448DFB5; requeststat=+st:158+sc:~/entity/auth/login+start:638734660131262952+tg:");
        var content = new StringContent(string.Empty);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        */


        return "";
    }

    private static string PutImage(HttpClient client, string testResult, string base64Image)
    {
        try
        {//var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, "https://hackathon.acumatica.com/Beta/entity/StockWise360/24.200.001/CollectionTarget/1/files/2.txt");
            //request.Headers.Add("Cookie", ".ASPXAUTH=A1B2F06E0CBB8A9B4710FF11DB03802BB71DF3A565F089696A179E9B4885FA52015E3833F1F37EE7396EA0372AD60FF06435A8670B7BC514314995010C78899C383B90A0CCC0200FC181E40028533048C113D9D7BB4AB23C152C040B362FE251F8B1F5362A42445FE9F81FFD30B488A9AC68C2DF; ASP.NET_SessionId=zcqi1fbhjnplafxmkodibbqe; Locale=TimeZone=GMTM0800A&Culture=en-US; UserBranch=16; requestid=B36837D993125C8311EFDBB498F3BB84; requeststat=+st:338+sc:~/entity/stockwise360/24.200.001/collectiontarget/1/files/2.txt+start:638734722188481028+tg:");
            var content = new StringContent("JGNzdkZpbGVQYXRoID0gImM6XFVzZXJzXFpvbHRhbiBGZWJlcnRcRG93bmxvYWRzXEZhYnV3b29kXzEzMzAwMC5jc3YiIA0KDQokdG90YWxEZWJpdCA9IDANCiR0b3RhbENyZWRpdCA9IDANCg0KJHN0cmVhbSA9IFtTeXN0ZW0uSU8uU3RyZWFtUmVhZGVyXTo6bmV3KCRjc3ZGaWxlUGF0aCkNCg0KdHJ5IHsNCiAgICB3aGlsZSAoKCRsaW5lID0gJHN0cmVhbS5SZWFkTGluZSgpKSAtbmUgJG51bGwpIHsNCiAgICAgICAgICAgICAgICAkY29sdW1ucyA9ICRsaW5lIC1zcGxpdCAnLCcNCg0KICAgICAgICAjIEFkZCB0aGUgdmFsdWVzIG9mIHRoZSA1cmQgYW5kIDZ0aCBjb2x1bW5zIHRvIHRoZSB0b3RhbHMNCiAgICAgICAgJHRvdGFsRGViaXQgKz0gW2RlY2ltYWxdJGNvbHVtbnNbNV0gIA0KICAgICAgICAkdG90YWxDcmVkaXQgKz0gW2RlY2ltYWxdJGNvbHVtbnNbNl0gDQogICAgfQ0KfSBmaW5hbGx5IHsNCiAgICAjIEVuc3VyZSB0aGUgc3RyZWFtIGlzIHByb3Blcmx5IGNsb3NlZA0KICAgICRzdHJlYW0uQ2xvc2UoKQ0KfQ0KDQojIE91dHB1dCB0aGUgcmVzdWx0cw0KV3JpdGUtT3V0cHV0ICJUb3RhbCBEZWJpdDogJHRvdGFsRGViaXQiDQpXcml0ZS1PdXRwdXQgIlRvdGFsIENyZWRpdDogJHRvdGFsQ3JlZGl0Ig==", null, "text/plain");
            request.Content = content;
            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();

            var r = response.Content.ReadAsStringAsync();


            Console.WriteLine(r);
            return r.Result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while processing image post: {ex.Message}");
            return "Exception";
        }




    }

    private static string PutCollectionAwnser(HttpClient client,Component cp)
    {
        //var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Put, "https://hackathon.acumatica.com/Beta/entity/StockWise360/24.200.001/CollectionTarget?$expand=answers");
        request.Headers.Add("Accept", "application/json");
        //request.Headers.Add("Cookie", ".ASPXAUTH=E2FA9B2BF34D0E38599DD4132852B9AD196DACC5C98F970A64E0EAE50B99CADC0ADF5A6ADA59F513FFE41DB767BA877F70E83A4DC5A8F61553087ED1DEE62DB26966AB654C0987BB55A107133BA89622448B9E6728EB9625BDB65EFE7C5D2A39631B161CF2BA19D48DE23CC83A1665A2B4EBF0C7; ASP.NET_SessionId=3azwklghdba3muna3s2yjoyq; Locale=TimeZone=GMTM0500G&Culture=en-US; UserBranch=22; requestid=B36837D993125C8311EFDBAF72D5D2EA; requeststat=+st:71+sc:~/entity/stockwise360/24.200.001/collectiontarget+start:638734700074150250+tg:");

        //var content = new StringContent("\r\n {\r\n        \"id\": \"e3d2d820-9adb-ef11-835c-1293d93768b3\",\r\n        \"rowNumber\": 1,\r\n        \"note\": {\r\n            \"value\": \"\"\r\n        },\r\n        \"Answers\": [\r\n            {\r\n               \r\n                \"Description\": {\"value\": \"12345\"},\r\n                \"Information\": {\r\n                    \"value\": \"12345\"\r\n                },\r\n                \"ItemID\": {\r\n                    \"value\": \"12345\"\r\n                },\r\n                \"JsonResult\": {},\r\n                \"Lead\": {\"value\": \"12345\"},\r\n                \"Manufacturer\": {\r\n                    \"value\": \"12345\"\r\n                },\r\n                   \"vendors\": {\r\n                    \"value\": \"12345\"\r\n                },   \"use\": {\r\n                    \"value\": \"12345\"\r\n                }\r\n        \r\n            }\r\n        ]\r\n }\r\n \r\n\r\n              \r\n\r\n                \r\n            \r\n \r\n\r\n\r\n", null, "application/json");
        var content = new StringContent("\r\n {\r\n        \"id\": \"e3d2d820-9adb-ef11-835c-1293d93768b3\",\r\n        \"rowNumber\": 1,\r\n        \"note\": {\r\n            \"value\": \"\"\r\n        },\r\n        \"Answers\": [\r\n            {\r\n               \r\n                \"Description\": {\"value\": \"" +
            cp.Description +
            "\"},\r\n                \"Information\": {\r\n                    \"value\": \"" +
            cp.Information +
            "\"\r\n                },\r\n                \"ItemID\": {\r\n                    \"value\": \"" +
            cp.ID +
            "\"\r\n                },\r\n                \"JsonResult\": {},\r\n                \"Lead\": {\"value\": \"" +
            cp.Lead +
            "\"},\r\n                \"Manufacturer\": {\r\n                    \"value\": \"" +
            cp.Manufacturer +
            "\"\r\n                },\r\n                   \"vendors\": {\r\n                    \"value\": \"" +
            "Digi-Key, Mouser, Newark" +
            "\"\r\n                },   \"use\": {\r\n                    \"value\": \"" +
            cp.Use +
            "\"\r\n                }\r\n        \r\n            }\r\n        ]\r\n }\r\n \r\n\r\n              \r\n\r\n                \r\n            \r\n \r\n\r\n\r\n", null, "application/json");



        request.Content = content;
        var response = client.SendAsync(request).Result;
        response.EnsureSuccessStatusCode();
        var r = response.Content.ReadAsStringAsync().Result;
        Console.WriteLine(r);
        return r;
    }

    private static string GetCollectionTarget(HttpClient client)
    {

        //string urlCheck = "https://hackathon.acumatica.com/Beta/entity/StockWise360/24.200.001/CollectionTarget?$filter=CollectionTargetID eq 1&$Expand=Answers";

        string url = _acumaticaInstanceUrl +
            "/entity/StockWise360/24.200.001/CollectionTarget?$filter=CollectionTargetID eq " +
            _CollectionTargetID +
            "&$Expand=Answers";

        //bool test = url == urlCheck;

        //var request = new HttpRequestMessage(HttpMethod.Get, "https://hackathon.acumatica.com/Beta/entity/StockWise360/24.200.001/CollectionTarget?$filter=CollectionTargetID eq 1&$Expand=Answers");
        var request = new HttpRequestMessage(HttpMethod.Get, url);



        /*this didnt work
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_acumaticaInstanceUrl}/StockWise360/24.200.001/CollectionTarget?$filter=CollectionTargetID eq " +
            _CollectionTargetID +
            $"&$Expand=Answers");
        */
        request.Headers.Add("Accept", "application/json");
        var content = new StringContent(string.Empty);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        request.Content = content;
        var response = client.SendAsync(request).Result;
        response.EnsureSuccessStatusCode();
        var ContentResult = response.Content.ReadAsStringAsync().Result;
        Console.WriteLine(ContentResult);
        return ContentResult;
    }

    private static void Logout(HttpClient client)
    {
        //var request = new HttpRequestMessage(HttpMethod.Post, "https://hackathon.acumatica.com/Beta/entity/auth/logout");
        var request = new HttpRequestMessage(HttpMethod.Post, _acumaticaInstanceUrl +
            "/entity/auth/logout");



        var content = new StringContent("", null, "application/json");
        request.Content = content;
        var response = client.SendAsync(request).Result;
        response.EnsureSuccessStatusCode();
        Console.WriteLine(response.Content.ReadAsStringAsync().Result);

    }

    private static void Login(HttpClient client)
    {


        //var request = new HttpRequestMessage(HttpMethod.Post, "https://hackathon.acumatica.com/Beta/entity/auth/login");
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_acumaticaInstanceUrl}/entity/auth/login");

        request.Headers.Add("Accept", "*/*");
        request.Headers.Add("Authorization", "Basic SVRQdGVrCjpyQWF2SzdcPzBCaDMK");
        //request.Headers.Add("Cookie", ".ASPXAUTH=C3C1A7C1C4DCD6872C67DAD6B0394DF7B5242C5E2D18F440B1129F093EF70C2AF02EE966C9E32562BF74142E2DC41B7B5759A9A00194DEF29D304153B3029D9D74A9FA5405B8DB41324AE972535768956B86EB86307DA664EF6F7A35B37071E869EC28E0563A945C5BE2DC268BB4B3A08BB78173; ASP.NET_SessionId=3d53frqdhdlcrzrtg345csta; Locale=Culture=en-US&TimeZone=GMTM0800A; UserBranch=16; requestid=B36837D993125C8311EFDBA1B891294D; requeststat=+st:136+sc:~/entity/auth/login+start:638734641114513357+tg:");
        //var content = new StringContent("{\r\n    \"name\": \"Admin\",\r\n    \"password\": \"team2banana\",\r\n    \"company\": \"Company\"\r\n}\r\n\r\n", null, "application/json");

        var content = new StringContent("{" +
            "\r\n    \"name\": \"" +
            _acumaticaUser +
            "\",\r\n    \"password\": \"" +
            _acumaticaPwd +
            "\",\r\n    \"company\": \"Company\"\r\n}\r\n\r\n", null, "application/json");


        request.Content = content;
        var response = client.SendAsync(request).Result;
        response.EnsureSuccessStatusCode();
        var Content = response.Content.ReadAsStringAsync();
        Console.WriteLine(Content);
    }
}