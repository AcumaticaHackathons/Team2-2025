using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonException = Newtonsoft.Json.JsonException;

namespace ChatGptImageProcessor;

public static class ChatGptImageProcessor
{
    private static readonly string ImagePath = GetImagePathFromConfig();
    private static string? _acumaticaInstanceUrl;
    private static string? _acumaticaUser;
    private static string? _acumaticaPwd;
    private static string? _ImageFolder;
    private static string? _base64Image;


    private static string GetImagePathFromConfig()
    {
        var directoryPath = AppDomain.CurrentDomain.BaseDirectory;
        var configFilePath = Path.Combine(directoryPath, "Config.json");
        if (!File.Exists(configFilePath))
        {
            throw new FileNotFoundException($"Config file not found at {configFilePath}");
        }
        var jsonContent = File.ReadAllText(configFilePath);
        var config = JObject.Parse(jsonContent);
        var imagePath = config["TestImagePath"]?.ToString();
        if (string.IsNullOrEmpty(imagePath))
        {
            throw new InvalidOperationException("TestImagePath not found in the Config.json file.");
        }
        return imagePath;
    }

    private static readonly string Prompt = GetPrompt();

    private static string GetPrompt()
    {
        var directoryPath = AppDomain.CurrentDomain.BaseDirectory;
        var promptFilePath = Path.Combine(directoryPath, "Prompt.txt");
        if (!File.Exists(promptFilePath))
        {
            throw new FileNotFoundException($"Prompt file not found at {promptFilePath}");
        }
        var prompt = File.ReadAllText(promptFilePath);
        return prompt;
    }

    private static readonly string ApiKey = GetApiKey();

    private static string GetApiKey()
    {
        var directoryPath = AppDomain.CurrentDomain.BaseDirectory;
        var configFilePath = Path.Combine(directoryPath, "Config.json");
        if (!File.Exists(configFilePath))
        {
            throw new FileNotFoundException($"Config file not found at {configFilePath}");
        }
        var jsonContent = File.ReadAllText(configFilePath);
        var config = JObject.Parse(jsonContent);
        var apiKey = config["OpenAiApiKey"]?.ToString();
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("API key not found in the Config.json file.");
        }
        return apiKey;
    }


    static readonly HttpClient Client = new HttpClient();

    private static string ExecutePhotoProcessing(string imagePath)
    {
        _base64Image = EncodeImage(imagePath);
        var payload = new
        {
            //model = "gpt-4-turbo",
            model = "gpt-4o",
            messages = new[]
            {
                new
                {
                    role = "user",
                    content = new object[]
                    {
                        new { type = "text", text =  Prompt},
                        new { type = "image_url", image_url = new { url = $"data:image/jpeg;base64,{_base64Image}" } }
                        //the below example can be used if the image is hosted online.
                        //new { type = "image_url", image_url = new { url = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/dd/Gfp-wisconsin-madison-the-nature-boardwalk.jpg/2560px-Gfp-wisconsin-madison-the-nature-boardwalk.jpg" } }
                    }
                }
            },
            max_tokens = 300
        };
        
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);


        using var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        var response = Client.PostAsync("https://api.openai.com/v1/chat/completions", content).Result;
        var result = response.Content.ReadAsStringAsync().Result;

        var gptResponse = ExtractContentFromJson(result);
        
        AcumaticaApiProcessor.SendResultsToCollection(gptResponse,_base64Image);
        return gptResponse;
    }

    public static string ExtractContentFromJson(string inputJson)
    {
        try
        {
            var jsonDocument = JsonDocument.Parse(inputJson);
            var choices = jsonDocument.RootElement.GetProperty("choices");

            if (choices.GetArrayLength() > 0)
            {
                var content = choices[0].GetProperty("message").GetProperty("content").GetString();
                return content ?? "No content found.";
            }
            else
            {
                return "No choices found in JSON.";
            }
        }
        catch (JsonException ex)
        {
            return $"Error processing JSON: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Unexpected error: {ex.Message}";
        }
    }
    
    /// <summary>
    /// Converts an image into a Base64 string
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    static string EncodeImage(string path)
    {
        var imageBytes = File.ReadAllBytes(path);
        return Convert.ToBase64String(imageBytes);
    }
    
    public static void Execute()
    {
        SetVariables();
        //var gptResult =  ExecutePhotoProcessing(ImagePath);
        //AcumaticaApiProcessor.SendResultsToCollection(gptResult);
        
        string resutls = ExecutePhotoFolderProcessing(_ImageFolder);
        //Console.WriteLine(gptResult);
    }

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
        //todo: get other config variables here so we dont have redundant code.
    }



    public static string ExecutePhotoFolderProcessing(string folderPath, bool includeSubdirectories = true)
    {
        string result = "";
        
        try
        {
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"The folder '{folderPath}' does not exist.");
                return "Exception";
            }

            var files = Directory.GetFiles(folderPath);
            foreach (var file in files)
            {
                try
                {
                    var gptResult = ExecutePhotoProcessing(file);
                    result = result + gptResult + "\r\n";  
                    Console.WriteLine($"Processing {file}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

            }

            if (includeSubdirectories)
            {
                var subdirectories = Directory.GetDirectories(folderPath);
                foreach (var subdirectory in subdirectories)
                {
                    ExecutePhotoFolderProcessing(subdirectory, includeSubdirectories);
                }
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        return result;
    }





}