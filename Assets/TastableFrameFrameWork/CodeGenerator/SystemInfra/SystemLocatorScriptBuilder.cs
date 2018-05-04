using System.Text;
using TastableFrameFrameWork.CodeGenerator.Util;

namespace TastableFrameFrameWork.CodeGenerator.SystemInfra
{
    public class SystemLocatorScriptBuilder : SystemBaseScriptBuilder
    {
        protected override string GetKindName()
        {
            return "{0}Locator";
        }

        protected override void AddHeader(StringBuilder builder)
        {
            builder.Append(string.Format(@"namespace My.{3}.{0}.Interface
{{
    public class {0}Locator : I{0}Manager
    {{
        private static I{0}Manager {1}Manager;

        public static readonly {0}Locator Instance = new {0}Locator();
       
        private {0}Locator()
        {{
        }}
        
        public static void Set(I{0}Manager {2}Manager)
        {{
            {1}Manager = {2}Manager;
        }}
", ModuleName, NameConverter.ConvertPrivateField(ModuleName), NameConverter.ToCamel(ModuleName), LayerName));
        }
    }
}