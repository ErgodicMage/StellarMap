using Microsoft.Extensions.Configuration;

namespace TestCatalogues;

public class TestCategories
{
    public const string UnitTest = "UnitTest";
    public const string FunctionalTest = "FunctionalTest";
}

public static class TestingUtilities
{
    const string testnamespace = "TestCatalogues";

    public static IConfiguration Config { get; } = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    public static void LoadAppSettings()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    }
    public static string ReadResource(string folder, string resourcefile)
    {
        string filename = folder.Replace(" ", "_").Replace("\\", ".").Replace("/", ".") + "." +  resourcefile;
        return ReadResource(filename);
    }

    public static string ReadResource(string resourcefile)
    {
        string filename = testnamespace + "." + resourcefile;

        string retString = string.Empty;
        using (StreamReader reader = LoadResourceFile(filename))
        {
            retString = reader.ReadToEnd();
        }

        return retString;
    }
    public static StreamReader LoadResourceFile(string resourcefile)
    {
        StreamReader reader = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcefile));
        return reader;
    }
}
