namespace TastableFrameFrameWork.CodeGenerator.Util
{
    public static class FileIoLocater
    {
        public static IFileIoManager _fileIoManager;

        public static void Set(IFileIoManager fileIoManager)
        {
            _fileIoManager = fileIoManager;
        }

        public static void Write(string path, string content)
        {
            _fileIoManager.WriteFile(path, content);
        }
    }
}