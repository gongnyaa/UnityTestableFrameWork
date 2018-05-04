using System.Text;

namespace TastableFrameFrameWork.CodeGenerator.SystemInfra
{
    public class SystemManagerScriptBuilder : SystemBaseScriptBuilder
    {
        protected override string GetKindName()
        {
            return "{0}Manager";
        }

        protected override void AddHeader(StringBuilder builder)
        {
            builder.Append(string.Format(@"using My.{1}.{0}.Interface;

namespace My.{1}.{0}.Impl
{{
    public class {0}Manager : I{0}Manager
    {{

", ModuleName,LayerName));
        }
    }
}