//command line parameters
string sourcePath;
string targetPath;
int syncInterval;
string logPath;

if (args.Length < 4)
{
    Console.WriteLine("Please enter all the parameters through the command line. Exiting...");
    return;
}
else
{
    sourcePath = args[0];
    targetPath = args[1];
    syncInterval = Int32.Parse(args[2]);
    logPath = args[3];
}

//running synchronization one time before the periodic execution starts
InitiateSynchronization();

//periodic snychronization
PeriodicTimer periodicSyncTimer = new PeriodicTimer(TimeSpan.FromSeconds(syncInterval));
while (await periodicSyncTimer.WaitForNextTickAsync())
{
    InitiateSynchronization();
}

//Synchronization logic
void InitiateSynchronization()
{
    string[] sourceDirectories = Directory.GetDirectories(sourcePath, "", SearchOption.AllDirectories);
    string[] sourceFiles = Directory.GetFiles(sourcePath, "", SearchOption.AllDirectories);

    string[] targetDirectories = Directory.GetDirectories(targetPath, "", SearchOption.AllDirectories);
    string[] targetFiles = Directory.GetFiles(targetPath, "", SearchOption.AllDirectories);

    //remove base path from all directories and files to make it easier to work with
    RemoveBasePath(ref sourceDirectories, sourcePath);
    RemoveBasePath(ref sourceFiles, sourcePath);

    RemoveBasePath(ref targetDirectories, targetPath);
    RemoveBasePath(ref targetFiles, targetPath);

    //Compare source directory to target directory
    foreach (string sourceDir in sourceDirectories)
    {
        //to check if the source directory was found in the target path
        bool isFound = Array.Exists(targetDirectories, directory => directory == sourceDir);
        if (!isFound)
        {
            string targetDirectory = Path.Combine(targetPath, sourceDir);

            //create directory
            Directory.CreateDirectory(targetDirectory);
            LogMessage("directory created -" + targetDirectory);
        }
    }

    //Compare source files to target files
    foreach (string sourceFile in sourceFiles)
    {
        //check if the source file was found in the target path
        bool isFound = Array.Exists(targetFiles, file => file == sourceFile);

        string sourceFile_FullPath = Path.Combine(sourcePath, sourceFile);
        string targetFile_FullPath = Path.Combine(targetPath, sourceFile);

        //if not found or the content is different, then do a copy
        if (!isFound || !IsSameContent(sourceFile_FullPath, targetFile_FullPath))
        {
            //copy file
            File.Copy(sourceFile_FullPath, targetFile_FullPath, true);
            LogMessage("File copied - " + targetFile_FullPath);

        }
    }

    // Removing any extra files and directories in target folder
    // Compare target files to source files
    foreach (string targetFile in targetFiles)
    {
        //check if the target file was found in the source path
        bool isFound = Array.Exists(sourceFiles, file => file == targetFile);

        // string sourceFile_FullPath = Path.Combine(sourcePath, sourceFile);
        string targetFile_FullPath = Path.Combine(targetPath, targetFile);

        //if not found or the content is different, then do a copy
        if (!isFound)
        {
            //delete file
            File.Delete(targetFile_FullPath);
            LogMessage("File deleted - " + targetFile_FullPath);
        }
    }

    //Compare target directory to source directory
    for (int i = targetDirectories.Length - 1; i >= 0; i--)
    {
        // to check if the source directory was found in the target path
        bool isFound = Array.Exists(sourceDirectories, directory => directory == targetDirectories[i]);
        if (!isFound)
        {
            string targetDirectory_FullPath = Path.Combine(targetPath, targetDirectories[i]);

            //delete directory
            Directory.Delete(targetDirectory_FullPath, true);
            LogMessage("directory deleted -" + targetDirectory_FullPath);
        }
    }
    
    LogMessage("Synchronization Complete.");
}

// Changes an array of paths from full path to only relative path
void RemoveBasePath(ref string[] dir, string basePath)
{
    for (int i = 0; i < dir.Length; i++)
    {
        dir[i] = dir[i].Substring(basePath.Length + 1);
    }
}

// Returns a MD5 hash of a given string
string GetHash(string input)
{
    string result;

    System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

    byte[] tempBytes = System.Text.Encoding.ASCII.GetBytes(input);
    byte[] tempHash = md5.ComputeHash(tempBytes);

    result = Convert.ToHexString(tempHash);

    return result;
}

// Checks the content of files using their MD5 hash
bool IsSameContent(string sourceFilePath, string targetFilePath)
{
    bool result = false;
    if (GetHash(File.ReadAllText(sourceFilePath)) == GetHash(File.ReadAllText(targetFilePath)))
    {
        result = true;
    }
    return result;
}

// Writes the given message to the log file and console
void LogMessage(string message)
{
    string logMessage = DateTime.Now + " - " + message;

    Console.WriteLine(logMessage);
    using (StreamWriter writer = File.AppendText(logPath))
    {
        writer.WriteLine(logMessage);
    }
}
