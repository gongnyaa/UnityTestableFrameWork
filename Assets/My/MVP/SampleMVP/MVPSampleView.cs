using My.Infra.UnityMVPFrameWork.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace MyModule.MVPSample
{
    public class MVPSampleView : BaseView<MVPSampleModel, MVPSampleView, MVPSamplePresenter> 
    {
        [SerializeField]
        private Text _test1Text;
        [SerializeField]
        private Text _test2Text;

        [SerializeField]
        private Image _test1Image;
        [SerializeField]
        private Image _test2Image;

        [SerializeField]
        private Button _test1Button;
        [SerializeField]
        private Button _test2Button;

        [SerializeField]
        private Toggle[] _test1ToggleGroup;
        [SerializeField]
        private Toggle[] _test2ToggleGroup;

        private void Awake()
        {
            _test1Button.onClick.AddListener(OnClickTest1Button);
            _test2Button.onClick.AddListener(OnClickTest2Button);
            foreach (var test1Toggle in _test1ToggleGroup)
                test1Toggle.onValueChanged.AddListener(OnTest1ToggleChange);
            foreach (var test2Toggle in _test2ToggleGroup)
                test2Toggle.onValueChanged.AddListener(OnTest2ToggleChange);
        }
        private void OnClickTest1Button ()
        {
            Presenter.Test1();
        }
        private void OnClickTest2Button ()
        {
            Presenter.Test2();
        }
        private void OnTest1ToggleChange(bool isOn)
        {
            if (isOn == false) return;
            for (var i = 0; i < _test1ToggleGroup.Length; i++)
                if (_test1ToggleGroup[i].isOn)
                    Presenter.TurnOnTest1Toggle(i);
        }
        private void OnTest2ToggleChange(bool isOn)
        {
            if (isOn == false) return;
            for (var i = 0; i < _test2ToggleGroup.Length; i++)
                if (_test2ToggleGroup[i].isOn)
                    Presenter.TurnOnTest2Toggle(i);
        }
    }
}