using System.Text;
using TastableFrameFrameWork.CodeGenerator.Util;

namespace TastableFrameFrameWork.CodeGenerator.Module
{
    public class ModuleModelTestScriptBuilder : ModuleBaseScriptBuilder
    {
        protected override string GetKindName()
        {
            return "{0}ModelTest";
        }

        protected override void AddHeader(StringBuilder builder)
        {
            builder.Append(string.Format(@"using NSubstitute;
using NUnit.Framework;

namespace MyModule.{0}
{{
    public class {0}ModelTest
    {{   
        private {0}Model {1}Model;
        private {0}Presenter {1}Presenter;

        [SetUp]
        public void SetUp()
        {{
            {1}Presenter = Substitute.For<{0}Presenter>();
            {1}Model = new {0}Model();
            {1}Model.SetPresenter({1}Presenter);
            throw new System.NotImplementedException();
        }}

        [Test]
        public void TestOnShow()
        {{
            {1}Model.OnShow();
            throw new System.NotImplementedException();
        }}", ModuleName, NameConverter.ConvertPrivateField(ModuleName)));
        }


        protected override void AddOnButtonClickMethod(StringBuilder builder, string gameObjectName)
        {
            builder.Append(string.Format(@"        [Test]
        public void Test{0} ()
        {{
            // Set

            // Do
            {1}Model.{0} ();

            // Check

            throw new System.NotImplementedException();
        }}", NameConverter.RemoveButton(gameObjectName), NameConverter.ConvertPrivateField(ModuleName)));
        }
        protected override void AddOnToggleChangeMethod(StringBuilder builder, string gameObjectName)
        {
            builder.Append(string.Format(@"        [TestCase(0)]
        public void TestTurnOn{0} (int index)
        {{
            // Set

            // Do
            {1}Model.TurnOn{0}(index);

            // Check

            throw new System.NotImplementedException();
        }}", NameConverter.RemoveGroup(gameObjectName), NameConverter.ConvertPrivateField(ModuleName)));
        }
    }
}