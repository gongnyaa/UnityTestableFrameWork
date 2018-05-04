using UnityEngine;

namespace My.Infra.UnityMVPFrameWork.Interface
{
    public interface IBaseModel
    {
        GameObject GetGameObject();
    }

    public abstract class BaseModel<M, V, P> : IBaseModel where P : BasePresenter<M, V, P>
        where V : BaseView<M, V, P>
        where M : BaseModel<M, V, P>, new()
    {
        protected P Presenter;

        public GameObject GetGameObject()
        {
            return Presenter.GetGameObject();
        }

        public void SetPresenter(P presenter)
        {
            Presenter = presenter;
        }

        public P GetPresenter()
        {
            return Presenter;
        }

        public abstract void OnShow();
    }
}