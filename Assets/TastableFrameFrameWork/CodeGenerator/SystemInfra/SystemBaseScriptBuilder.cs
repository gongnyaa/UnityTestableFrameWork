using System.IO;
using System.Text;
using TastableFrameFrameWork.CodeGenerator.Util;

namespace TastableFrameFrameWork.CodeGenerator.SystemInfra
{
    public abstract class SystemBaseScriptBuilder
    {
        protected string ModuleName;
        protected string LayerName;

        public void GenerateScript(string directory,string layerName)
        {
            LayerName = layerName;
            ModuleName = directory.Substring(directory.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            var builder = new StringBuilder();

            AddHeader(builder);

            AddFooter(builder);

            var directoryPath = "";
            switch (GetKindName())
            {
                case "{0}Locator":
                case "I{0}Manager":
                case "{0}ManagerMock":
                    directoryPath = Path.Combine(directory, "Interface");
                    break;
                case "{0}Manager":
                case "{0}ManagerTest":
                    directoryPath = Path.Combine(directory, "Impl");
                    break;
            }

            var fileName = string.Format(GetKindName(), ModuleName) + ".cs";
            var filePath = Path.Combine(directoryPath, fileName);

            FileIoLocater.Write(filePath, builder.ToString());
        }

        protected abstract string GetKindName();

        protected abstract void AddHeader(StringBuilder builder);

        private void AddFooter(StringBuilder builder)
        {
            builder.Append(@"    }
}");
        }
    }
}