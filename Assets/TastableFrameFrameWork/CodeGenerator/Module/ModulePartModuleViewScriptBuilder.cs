using System.Text;
using TastableFrameFrameWork.CodeGenerator.Util;

namespace TastableFrameFrameWork.CodeGenerator.Module
{
    public class ModulePartModuleViewScriptBuilder : ModuleViewScriptBuilder
    {
        protected override string GetKindName()
        {
            return "{0}PartView";
        }

        protected override void AddHeader(StringBuilder builder)
        {
            builder.Append(string.Format(@"using MySystem.MVP.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace MyModule.{0}
{{
    public class {0}PartView : MonoBehaviour 
    {{
", ModuleName));
        }

        protected override void AddOnButtonClickMethod(StringBuilder builder, string gameObjectName)
        {
            builder.Append(string.Format(@"        private void OnClick{0} ()
        {{
            
        }}", gameObjectName, NameConverter.RemoveButton(gameObjectName)));
        }
    }
}