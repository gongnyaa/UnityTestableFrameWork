using System.Text;
using TastableFrameFrameWork.CodeGenerator.Util;

namespace TastableFrameFrameWork.CodeGenerator.SystemInfra
{
    public class SystemManagerTestScriptBuilder : SystemBaseScriptBuilder
    {
        protected override string GetKindName()
        {
            return "{0}ManagerTest";
        }

        protected override void AddHeader(StringBuilder builder)
        {
            builder.Append(string.Format(@"using My.{2}.{0}.Interface;
using NUnit.Framework;

namespace My.{2}.{0}.Impl
{{
    public class {0}ManagerTest
    {{
        private {0}Manager {1}Manager;

        [SetUp]
        public void SetUp()
        {{
            {1}Manager = new {0}Manager();            
        }}
", ModuleName,NameConverter.ConvertPrivateField(ModuleName),LayerName));
        }
    }
}