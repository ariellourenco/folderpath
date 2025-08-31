// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;

const string userSecretsFallbackDir = "DOTNET_USER_SECRETS_FALLBACK_DIR";

// On Windows it goes to %APPDATA%\Microsoft\UserSecrets\
// On Linux it goes to ~/.local/share/.microsoft/usersecrets/ unless XDG_ is set.
// On Mac it goes to ~/Library/Application Support/Microsoft/Usersecrets/ unless XDG_ is set.
string? appData = Environment.GetEnvironmentVariable("APPDATA");
string? root = appData                                                               
    ?? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)   
    ?? Environment.GetEnvironmentVariable("HOME")
    ?? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
    ?? Environment.GetEnvironmentVariable(userSecretsFallbackDir);

if (string.IsNullOrEmpty(root))
{
    throw new InvalidOperationException($"Could not determine a suitable directory for user secrets. Set the {userSecretsFallbackDir} environment variable to a directory path to use.");
}

string path;

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
{
    path = Path.Combine(root, "Microsoft", "UserSecrets");
}
else
{
    path = Path.Combine(root, ".microsoft", "usersecrets");
}

Console.WriteLine();
Console.WriteLine("GetFolderPath: {0}", path);

Console.ReadKey();