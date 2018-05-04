using UnityEngine;

namespace My.Infra.UnityMVPFrameWork.Interface
{
    public abstract class BaseView<M, V, P> : MonoBehaviour
        where P : BasePresenter<M, V, P>
        where V : BaseView<M, V, P>
        where M : BaseModel<M, V, P>, new()
    {
        protected P Presenter;

        public void SetPresenter(P presenter)
        {
            Presenter = presenter;
        }
    }
}