using ACO.Util.MTDispatcher;

namespace ACO
{
    public class Core : Singleton<Core>
    {
        protected Core() { }

        private void Awake()
        {
            SetInstance(this);
        }

        public DispatcherAction DispatcherAction = new DispatcherAction();

        private void Update()
        {
            DispatcherAction.Update();
        }
    }
}