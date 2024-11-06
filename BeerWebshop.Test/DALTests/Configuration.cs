using System.Security.Cryptography.X509Certificates;

namespace BeerWebshop.Test.DALTests;

public static class Configuration
{

    public static string ConnectionString()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(currentDirectory, "..", "ConnectionString.txt");
        string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string filePathWindows = Path.Combine(userProfile, "source", "repos", "BeerWebshop", "ConnectionString.txt");
        string path = File.ReadAllText(filePathWindows);
        return path;
    }
}