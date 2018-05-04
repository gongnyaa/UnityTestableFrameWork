namespace TastableFrameFrameWork.CodeGenerator.Util
{
    public static class NameConverter
    {
        public static string ConvertPrivateField(string name)
        {
            return "_" + name.Substring(0, 1).ToLower() + name.Substring(1);
        }

        public static string ToCamel(string name)
        {
            return name.Substring(0, 1).ToLower() + name.Substring(1);
        }

        public static object RemoveButton(string gameObjectName)
        {
            return gameObjectName.Replace("Button", "");
        }
        public static object RemoveGroup(string gameObjectName)
        {
            return gameObjectName.Replace("Group", "");
        }

        
        
        public static string ToPascalFromPrivate(string name)
        {
            var removeUnder = name.Substring(1);
            return removeUnder.Substring(0, 1).ToUpper() + removeUnder.Substring(1);
            ;
        }
    }
}