namespace ACO.UI
{
    public class WindowStarter : UnityEngine.MonoBehaviour
    {
        public WindowBase windowToStart;
        void Start()
        {
            windowToStart.Open();
        }
    }
}