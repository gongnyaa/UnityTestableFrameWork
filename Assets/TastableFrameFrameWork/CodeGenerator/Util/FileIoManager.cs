using System.IO;

namespace TastableFrameFrameWork.CodeGenerator.Util
{
    public class FileIoManager : IFileIoManager
    {
        public void WriteFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}