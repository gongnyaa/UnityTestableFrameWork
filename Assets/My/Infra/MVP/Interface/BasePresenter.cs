using UnityEngine;

namespace My.Infra.UnityMVPFrameWork.Interface
{
    public abstract class BasePresenter<M, V, P>
        where V : BaseView<M, V, P>
        where P : BasePresenter<M, V, P>
        where M : BaseModel<M, V, P>, new()
    {
        protected M Model;
        protected V View;

        private void SetManager(M model)
        {
            Model = model;
        }

        // Update is called once per frame
        private void SetView(V view)
        {
            View = view;
        }

        public V GetView()
        {
            return View;
        }

        public GameObject GetGameObject()
        {
            if (View == null) return null;
            return View.gameObject;
        }

        public V ShowView(V view)
        {
            view.SetPresenter((P) this);
            SetView(view);

            if (Model != null) Model.OnShow();

            return view;
        }

        public M CreateModel()
        {
            var model = new M();
            model.SetPresenter((P) this);
            SetManager(model);
            return model;
        }

        public M SetModel(M model)
        {
            model.SetPresenter((P) this);
            SetManager(model);
            return model;
        }
    }
}