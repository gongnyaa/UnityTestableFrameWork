using System.Text;
using TastableFrameFrameWork.CodeGenerator.Util;

namespace TastableFrameFrameWork.CodeGenerator.Module
{
    public class ModulePresenterScriptBuilder : ModuleBaseScriptBuilder
    {
        protected override string GetKindName()
        {
            return "{0}Presenter";
        }

        protected override void AddHeader(StringBuilder builder)
        {
            var header = string.Format(@"using My.Infra.UnityMVPFrameWork.Interface;
namespace MyModule.{0}
{{
    public class {0}Presenter : BasePresenter<{0}Model, {0}View, {0}Presenter> 
    {{", ModuleName);
            builder.Append(header);
        }

        protected override void AddOnButtonClickMethod(StringBuilder builder, string gameObjectName)
        {
            var method = string.Format(@"        public virtual void {0} ()
        {{
            Model.{0}();
        }}", NameConverter.RemoveButton(gameObjectName));
            builder.Append(method);
        }
        protected override void AddOnToggleChangeMethod(StringBuilder builder, string gameObjectName)
        {
            builder.Append(string.Format(@"        public virtual void TurnOn{0}(int index)
        {{
            Model.TurnOn{0}(index);
        }}", NameConverter.RemoveGroup(gameObjectName)));
        }
    }
}