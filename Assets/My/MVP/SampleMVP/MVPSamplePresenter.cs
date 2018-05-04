using My.Infra.UnityMVPFrameWork.Interface;
namespace MyModule.MVPSample
{
    public class MVPSamplePresenter : BasePresenter<MVPSampleModel, MVPSampleView, MVPSamplePresenter> 
    {



        public virtual void Test1 ()
        {
            Model.Test1();
        }
        public virtual void Test2 ()
        {
            Model.Test2();
        }
        public virtual void TurnOnTest1Toggle(int index)
        {
            Model.TurnOnTest1Toggle(index);
        }
        public virtual void TurnOnTest2Toggle(int index)
        {
            Model.TurnOnTest2Toggle(index);
        }
    }
}