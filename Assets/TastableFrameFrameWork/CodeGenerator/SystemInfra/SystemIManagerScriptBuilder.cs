using System.Text;

namespace TastableFrameFrameWork.CodeGenerator.SystemInfra
{
    public class SystemIManagerScriptBuilder : SystemBaseScriptBuilder
    {
        protected override string GetKindName()
        {
            return "I{0}Manager";
        }
        
        protected override void AddHeader(StringBuilder builder)
        {
            builder.Append(string.Format(@"namespace My.{1}.{0}.Interface
{{
    public interface I{0}Manager
    {{
       
", ModuleName,LayerName));
        }
    }
}