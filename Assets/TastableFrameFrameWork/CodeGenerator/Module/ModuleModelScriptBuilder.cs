using System.Text;
using TastableFrameFrameWork.CodeGenerator.Util;

namespace TastableFrameFrameWork.CodeGenerator.Module
{
    public class ModuleModelScriptBuilder : ModuleBaseScriptBuilder
    {
        protected override void AddHeader(StringBuilder builder)
        {
            builder.Append(string.Format(@"using My.Infra.UnityMVPFrameWork.Interface;

namespace MyModule.{0}
{{
    public class {0}Model : BaseModel<{0}Model, {0}View, {0}Presenter> 
    {{
        public override void OnShow()
        {{
            throw new System.NotImplementedException();
        }}", ModuleName));
        }

        protected override void AddOnButtonClickMethod(StringBuilder builder, string gameObjectName)
        {
            builder.Append(string.Format(@"        public void {0} ()
        {{
            throw new System.NotImplementedException();
        }}", NameConverter.RemoveButton(gameObjectName)));
        }
        protected override void AddOnToggleChangeMethod(StringBuilder builder, string gameObjectName)
        {
            builder.Append(string.Format(@"        public void TurnOn{0}(int index)
        {{
            throw new System.NotImplementedException();
        }}", NameConverter.RemoveGroup(gameObjectName)));
        }

        protected override string GetKindName()
        {
            return "{0}Model";
        }
    }
}