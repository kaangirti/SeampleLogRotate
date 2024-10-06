using System.IO;
using System.Reflection.Metadata;

string[] parameters = Environment.GetCommandLineArgs();

int maxFileCount = parameters.Length > 1 && parameters[1] != null ? Convert.ToInt32(parameters[1]) : 3;
string logFolderPath = parameters.Length > 2 && parameters[2] != null ? parameters[2] : "";

if (Directory.Exists(logFolderPath))
{
    int fileCount = Directory.GetFiles(logFolderPath, "*", SearchOption.AllDirectories).Length;
    Console.WriteLine("FileCount: " + fileCount + " / Max: " + maxFileCount);
    if (fileCount > maxFileCount)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(logFolderPath);
        List<FileInfo> fileList = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories).OrderByDescending(t => t.LastWriteTime).ToList();

        for (int i = maxFileCount; i < fileList.Count(); i++)
        {
            FileInfo item = fileList[i];
            Console.WriteLine(item.FullName + " | " + item.LastWriteTime + " | deleted!");
            item.Delete();
        }
    }
}
else
{
    Console.WriteLine("Directory path is not found!");
}
