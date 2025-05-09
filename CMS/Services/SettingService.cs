using Newtonsoft.Json;

namespace CMS.Services;

public class SettingService
{
    public string GitUrl { get; set; }
    public string GitToken { get; set; }
    public string GitUser { get; set; }
    public static readonly string FileName = FileSystem.AppDataDirectory + "/settings.json";

    public SettingService()
    {
        GitUrl = string.Empty;
        GitToken = string.Empty;
        GitUser = string.Empty;
    }

    public SettingService(string gitUrl, string gitToken, string gitUser)
    {
        GitUrl = gitUrl;
        GitToken = gitToken;
        GitUser = gitUser;
    }

    public static void SetGitUrl(string gitUrl, string gitToken, string gitUser)
    {
        var settings = new SettingService(gitUrl, gitToken, gitUser);
        var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
        File.WriteAllText(FileName, json);
    }
    
    public void GetGitUrl()
    {
        if (File.Exists(FileName))
        {
            var existingData = File.ReadAllText(FileName);
            if (!string.IsNullOrEmpty(existingData))
            {
                var settings = JsonConvert.DeserializeObject<SettingService>(existingData);
                GitUrl = settings.GitUrl;
                GitToken = settings.GitToken;
                GitUser = settings.GitUser;
            }
        }
    }
    
    public static bool CheckSettingsJson()
    {
        return File.Exists(FileName);
    }
}