using System;
using System.Collections.Generic;

namespace TastableFrameFrameWork.CodeGenerator.Util
{
    [Serializable]
    public class AssemblyDefinitionObject
    {
        [NonSerialized] private readonly string _path;
        public string name;

        public List<string> references = new List<string>();

        public AssemblyDefinitionObject(string name,string path)
        {
            this.name = name;
            _path = path + "/_" + name;
        }

        public void AddReference(string reference)
        {
            references.Add(reference);
        }

        public string GetPath()
        {
            return _path;
        }
    }
}