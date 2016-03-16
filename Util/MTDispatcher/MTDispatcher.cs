using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace ACO.Util.MTDispatcher
{
    //Explain: Execute In Main Thread
    public abstract class MTDispatcher<T> : MonoBehaviour
    {
        static object _tasks_lock = new object();
        class Task
        {
            public Task(System.Action<T> act, T msg)
            {
                this.act = act;
                this.msg = msg;
            }
            System.Action<T> act;
            T msg;
            public void Execute()
            {
                Assert.AreNotEqual(act, null);
                act(msg);
            }
        }
        static Queue<Task> tasks = new Queue<Task>();
        protected static void AddTask(System.Action<T> act, T msg)
        {
            lock (_tasks_lock)
            {
                tasks.Enqueue(new Task(act, msg));
            }
        }
        static void ExecuteTasks()
        {
            lock (_tasks_lock)
            {
                while (tasks.Count > 0)
                {
                    tasks.Dequeue().Execute();
                }
            }
        }
        void Update()
        {
            ExecuteTasks();
        }
    }

    public abstract class MTDispatcher : MonoBehaviour
    {
        static object _tasks_lock = new object();
        class Task
        {
            public Task(System.Action act)
            {
                this.act = act;
            }
            System.Action act;
            public void Execute()
            {
                Assert.AreNotEqual(act, null);
                act();
            }
        }
        static Queue<Task> tasks = new Queue<Task>();
        protected static void AddTask(System.Action act)
        {
            lock (_tasks_lock)
            {
                tasks.Enqueue(new Task(act));
            }
        }
        static void ExecuteTasks()
        {
            lock (_tasks_lock)
            {
                while (tasks.Count > 0)
                {
                    tasks.Dequeue().Execute();
                }
            }
        }
        void Update()
        {
            ExecuteTasks();
        }
    }
}