using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TastableFrameFrameWork.CodeGenerator.Util
{
    public interface IGameObjectFieldInfo
    {
        List<string> GetTextGameObjects();
        List<string> GetImageGameObjects();
        List<string> GetButtonGameObjects();
        List<string> GetToggleGameObjects();
        string GetFilePath(string kindName);
        string GetModuleName();
    }

    public class GameObjectFieldInfo : IGameObjectFieldInfo
    {
        private readonly List<string> _buttonGameObjects = new List<string>();
        private readonly List<string> _imageGameObjects = new List<string>();
        private readonly string _moduleName;
        private readonly string _path;
        private readonly List<string> _textGameObjects = new List<string>();
        private readonly List<string> _toggleGroupGameObjects = new List<string>();


        public GameObjectFieldInfo(GameObject gameObject, string path)
        {
            _path = path;
            _moduleName = gameObject.name;
            //To-do add script from gameObjectName
            StoreChildGameObject(gameObject);
        }

        public List<string> GetTextGameObjects()
        {
            return _textGameObjects;
        }

        public List<string> GetImageGameObjects()
        {
            return _imageGameObjects;
        }

        public List<string> GetButtonGameObjects()
        {
            return _buttonGameObjects;
        }

        public List<string> GetToggleGameObjects()
        {
            return _toggleGroupGameObjects;
        }

        public string GetFilePath(string kindName)
        {
            var oldValue = _moduleName + ".prefab";
            var newValue =  string.Format(kindName, _moduleName) + ".cs";
            return _path.Replace("Resources/", "").Replace(oldValue, newValue);
        }

        public string GetModuleName()
        {
            return _moduleName;
        }


        private void StoreChildGameObject(GameObject gameObject)
        {
            for (var i = 0; i < gameObject.transform.GetChildCount(); i++)
            {
                var childGameObject = gameObject.transform.GetChild(i).gameObject;

                if (childGameObject.GetComponent<Text>() != null && childGameObject.name !="Text" && childGameObject.name.Contains("Text"))
                    _textGameObjects.Add(childGameObject.name);
                else if (childGameObject.GetComponent<Button>() != null && childGameObject.name.Contains("Button"))
                    _buttonGameObjects.Add(childGameObject.name);
                else if (childGameObject.GetComponent<Image>() != null && childGameObject.name.Contains("Image"))
                    _imageGameObjects.Add(childGameObject.name);
                else if (childGameObject.GetComponent<ToggleGroup>() != null &&
                         childGameObject.name.Contains("ToggleGroup"))
                    _toggleGroupGameObjects.Add(childGameObject.name);

                StoreChildGameObject(childGameObject);
            }
        }
    }
}