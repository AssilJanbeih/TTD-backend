using Microsoft.AspNetCore.Http;

namespace Utils.FileSystem;

public static class FileSystemHelper
{
    public static FileInfo[] GetFilesInDirectory(string directoryPath)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

        return directoryInfo.GetFiles("*.*")
            .Where(file => !file.Name.Equals(".gitkeep", StringComparison.OrdinalIgnoreCase))
            .ToArray();
    }

    public static async Task WriteFileToDisk(IFormFile file, string filePathWithoutName)
    {
        var fileName = file.FileName.Trim();
        var filePath = Path.Combine(filePathWithoutName, fileName);
        await using Stream fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);
    }

    public static void CreateDirectory(string destinationDir)
    {
        Directory.CreateDirectory(destinationDir);
    }

    public static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
    {
        var dir = new DirectoryInfo(sourceDir);

        if (!dir.Exists)
            throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

        DirectoryInfo[] dirs = dir.GetDirectories();

        Directory.CreateDirectory(destinationDir);

        foreach (FileInfo file in dir.GetFiles())
        {
            string targetFilePath = Path.Combine(destinationDir, file.Name);
            file.CopyTo(targetFilePath);
        }

        if (recursive)
        {
            foreach (DirectoryInfo subDir in dirs)
            {
                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                CopyDirectory(subDir.FullName, newDestinationDir, true);
            }
        }
    }
}