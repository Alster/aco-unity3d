using UnityEngine.Assertions;
using System.Collections.Generic;

namespace ACO.Util.MTDispatcher
{
    public abstract class MTDispatcher<T>
    {
        private readonly object _tasksLock = new object();
        private class Task
        {
            public Task(System.Action<T> act, T msg)
            {
                this._act = act;
                this._msg = msg;
            }

            private readonly System.Action<T> _act;
            private readonly T _msg;
            public void Execute()
            {
                Assert.AreNotEqual(_act, null);
                _act(_msg);
            }
        }
        private readonly Queue<Task> _tasks = new Queue<Task>();
        protected void AddTask(System.Action<T> act, T msg)
        {
            lock (_tasksLock)
            {
                _tasks.Enqueue(new Task(act, msg));
            }
        }
        private void ExecuteTasks()
        {
            lock (_tasksLock)
            {
                while (_tasks.Count > 0)
                {
                    _tasks.Dequeue().Execute();
                }
            }
        }
        public void Update()
        {
            ExecuteTasks();
        }
    }

    public abstract class MTDispatcher
    {
        private readonly object _tasksLock = new object();

        private class Task
        {
            public Task(System.Action act)
            {
                this._act = act;
            }

            readonly System.Action _act;
            public void Execute()
            {
                Assert.AreNotEqual(_act, null);
                _act();
            }
        }
        private readonly Queue<Task> _tasks = new Queue<Task>();
        protected void AddTask(System.Action act)
        {
            lock (_tasksLock)
            {
                _tasks.Enqueue(new Task(act));
            }
        }
        private void ExecuteTasks()
        {
            lock (_tasksLock)
            {
                while (_tasks.Count > 0)
                {
                    _tasks.Dequeue().Execute();
                }
            }
        }
        public void Update()
        {
            ExecuteTasks();
        }
    }
}