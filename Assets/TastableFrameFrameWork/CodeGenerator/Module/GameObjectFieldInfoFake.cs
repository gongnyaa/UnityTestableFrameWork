using System.Collections.Generic;
using TastableFrameFrameWork.CodeGenerator.Util;

namespace TastableFrameFrameWork.CodeGenerator.Module
{
    public class GameObjectFieldInfoFake : IGameObjectFieldInfo
    {
        public List<string> GetTextGameObjects()
        {
            var list = new List<string> {"Test1Text", "Test2Text"};
            return list;
        }

        public List<string> GetImageGameObjects()
        {
            var list = new List<string> {"Test1Image", "Test2Image"};
            return list;
        }

        public List<string> GetButtonGameObjects()
        {
            var list = new List<string> {"Test1Button", "Test2Button"};
            return list;
        }

        public List<string> GetToggleGameObjects()
        {
            var list = new List<string> {"Test1ToggleGroup", "Test2ToggleGroup"};
            return list;
        }

        public string GetFilePath(string kindName)
        {
            return "TestPath";
        }

        public string GetModuleName()
        {
            return "MVPSample";
        }
    }
}