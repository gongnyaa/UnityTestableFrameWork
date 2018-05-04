using System.Collections.Generic;
using System.Text;
using TastableFrameFrameWork.CodeGenerator.Util;

namespace TastableFrameFrameWork.CodeGenerator.Module
{
    public class ModuleViewScriptBuilder : ModuleBaseScriptBuilder
    {
        protected override string GetKindName()
        {
            return "{0}View";
        }

        protected override void AddHeader(StringBuilder builder)
        {
            builder.Append(string.Format(@"using My.Infra.UnityMVPFrameWork.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace MyModule.{0}
{{
    public class {0}View : BaseView<{0}Model, {0}View, {0}Presenter> 
    {{
", ModuleName));
        }

        protected override void AddField(StringBuilder builder, string gameObjectName, string typeName)
        {
            builder.Append(string.Format(@"        [SerializeField]
        private {0} {1};", typeName, NameConverter.ConvertPrivateField(gameObjectName)));
            builder.AppendLine();
        }

        protected override void AddAwake(StringBuilder builder, IEnumerable<string> buttonGameObjects,
            IEnumerable<string> toggleGameObjects)
        {
            builder.Append(@"        private void Awake()
        {");
            builder.AppendLine();
            foreach (var gameObjectName in buttonGameObjects)
            {
                builder.Append(string.Format(@"            {0}.onClick.AddListener(OnClick{1});",
                    NameConverter.ConvertPrivateField(gameObjectName), gameObjectName));
                builder.AppendLine();
            }

            foreach (var gameObjectName in toggleGameObjects)
            {
                builder.Append(string.Format(@"            foreach (var {0} in {1})
                {0}.onValueChanged.AddListener(On{2}Change);",
                    NameConverter.ToCamel(gameObjectName.Replace("Group", "")),
                    NameConverter.ConvertPrivateField(gameObjectName), 
                    NameConverter.RemoveGroup(gameObjectName)));
                builder.AppendLine();
            }


            builder.Append("        }");
            builder.AppendLine();
        }


        protected override void AddOnButtonClickMethod(StringBuilder builder, string gameObjectName)
        {
            builder.Append(string.Format(@"        private void OnClick{0} ()
        {{
            Presenter.{1}();
        }}", gameObjectName, NameConverter.RemoveButton(gameObjectName)));
        }

        protected override void AddOnToggleChangeMethod(StringBuilder builder, string gameObjectName)
        {
            builder.Append(string.Format(@"        private void On{0}Change(bool isOn)
        {{
            if (isOn == false) return;
            for (var i = 0; i < {1}.Length; i++)
                if ({1}[i].isOn)
                    Presenter.TurnOn{0}(i);
        }}", NameConverter.RemoveGroup(gameObjectName), NameConverter.ConvertPrivateField(gameObjectName)));
        }
    }
}