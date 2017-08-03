void Exec(string path, string args = "", string workingDir = ".")
{
    var settings = new ProcessSettings
    {
        Arguments = args,
        WorkingDirectory = workingDir,
    };
    if (path.EndsWith(".exe") && !IsRunningOnWindows())
    {
        settings.Arguments = path + " " + args;
        path = "mono";
    }
    var exitCode = StartProcess(path, settings);
    if (exitCode != 0)
        throw new Exception(path + " failed!");
}

void Premake(string file, string args, string action)
{
    var premakePath = Directory("./external/CppSharp/build/") + (IsRunningOnWindows() ?
        File("premake5.exe") : File("premake5-osx"));
    Exec(premakePath, $"--file={file} {args} {action}");
}