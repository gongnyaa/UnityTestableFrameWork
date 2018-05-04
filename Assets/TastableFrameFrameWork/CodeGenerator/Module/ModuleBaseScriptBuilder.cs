using System.Collections.Generic;
using System.Text;
using TastableFrameFrameWork.CodeGenerator.Util;

namespace TastableFrameFrameWork.CodeGenerator.Module
{
    public abstract class ModuleBaseScriptBuilder
    {
        protected string ModuleName;

        public void GenerateScript(IGameObjectFieldInfo gameObjectFieldInfo)
        {
            ModuleName = gameObjectFieldInfo.GetModuleName();
            var builder = new StringBuilder();

            AddHeader(builder);

            // add fields
            foreach (var gameObjectName in gameObjectFieldInfo.GetTextGameObjects())
                AddField(builder, gameObjectName, "Text");

            if (gameObjectFieldInfo.GetTextGameObjects().Count > 0) builder.AppendLine();

            foreach (var gameObjectName in gameObjectFieldInfo.GetImageGameObjects())
                AddField(builder, gameObjectName, "Image");

            if (gameObjectFieldInfo.GetImageGameObjects().Count > 0) builder.AppendLine();
            foreach (var gameObjectName in gameObjectFieldInfo.GetButtonGameObjects())
                AddField(builder, gameObjectName, "Button");

            if (gameObjectFieldInfo.GetToggleGameObjects().Count > 0) builder.AppendLine();
            foreach (var gameObjectName in gameObjectFieldInfo.GetToggleGameObjects())
                AddField(builder, gameObjectName, "Toggle[]");

            if (gameObjectFieldInfo.GetButtonGameObjects().Count > 0) builder.AppendLine();

            AddAwake(builder, gameObjectFieldInfo.GetButtonGameObjects(), gameObjectFieldInfo.GetToggleGameObjects());

            // add methods
            foreach (var gameObjectName in gameObjectFieldInfo.GetButtonGameObjects())
            {
                AddOnButtonClickMethod(builder, gameObjectName);
                builder.AppendLine();
            }

            foreach (var gameObjectName in gameObjectFieldInfo.GetToggleGameObjects())
            {
                AddOnToggleChangeMethod(builder, gameObjectName);
                builder.AppendLine();
            }


            AddFotter(builder);

            var filePath = gameObjectFieldInfo.GetFilePath(GetKindName());

            FileIoLocater.Write(filePath, builder.ToString());
        }


        protected virtual void AddAwake(StringBuilder builder, IEnumerable<string> buttonGameObjects,
            IEnumerable<string> toggleGameObjects)
        {
        }


        protected abstract string GetKindName();

        protected abstract void AddHeader(StringBuilder builder);

        protected virtual void AddField(StringBuilder builder, string gameObjectName, string typeName)
        {
        }

        protected virtual void AddOnButtonClickMethod(StringBuilder builder, string gameObjectName)
        {
        }

        protected virtual void AddOnToggleChangeMethod(StringBuilder builder, string gameObjectName)
        {
        }


        private void AddFotter(StringBuilder builder)
        {
            builder.Append(@"    }
}");
        }
    }
}