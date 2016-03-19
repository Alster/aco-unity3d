//using System.Collections;
//using System.Threading;

//public class ThreadedJob
//{
//    private bool m_IsDone = false;
//    private object m_Handle = new object();
//    private Thread m_Thread = null;
//    System.Action act;

//    public ThreadedJob(System.Action action)
//    {
//        act = action;
//    }

//    public bool IsDone
//    {
//        Get
//        {
//            bool tmp;
//            lock (m_Handle)
//            {
//                tmp = m_IsDone;
//            }
//            return tmp;
//        }
//        set
//        {
//            lock (m_Handle)
//            {
//                m_IsDone = value;
//            }
//        }
//    }

//    public virtual void Start()
//    {
//        m_Thread = new Thread(Run);
//        m_Thread.Start();
//    }
//    public virtual void Abort()
//    {
//        m_Thread.Abort();
//    }

//    protected virtual void ThreadFunction() { }

//    protected virtual void OnFinished() { }

//    public virtual bool Update()
//    {
//        if (IsDone)
//        {
//            OnFinished();
//            return true;
//        }
//        return false;
//    }
//    IEnumerator WaitFor()
//    {
//        while (!Update())
//        {
//            yield return null;
//        }
//    }
//    private void Run()
//    {
//        ThreadFunction();
//        IsDone = true;
//    }
//}