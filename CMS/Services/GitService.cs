using LibGit2Sharp;

namespace CMS.Services;

public class GitService
{
    private readonly static string clonePath = Path.Combine(FileSystem.AppDataDirectory, "Repo");

    public readonly SettingService SettingService;
    
    public GitService(SettingService settingService)
    {
        SettingService = settingService;
    }
    public void CloneRepo()
    {
        if (!Directory.Exists(clonePath))
        {
            var cloneOptions = new CloneOptions
            {
               FetchOptions =
               {
                   CredentialsProvider = (_url, _user, _cred) =>
                       new UsernamePasswordCredentials
                       {
                           Username = "git", 
                           Password = SettingService.GitToken
                       }
               }
            };

            Repository.Clone(SettingService.GitUrl, clonePath, cloneOptions);
        }
    }


    public static bool CheckRepo()
    {
        return Directory.Exists(clonePath);
    }
}