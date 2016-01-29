namespace ACO
{
    public struct Event
    {
        private event System.Action action;
        public void Invoke() { if (action != null) { action(); } }
        public static Event operator +(Event eve, System.Action act) { eve.action += act; return eve; }
        public static Event operator -(Event eve, System.Action act) { eve.action -= act; return eve; }
    }
    public struct Event<T1>
    {
        private event System.Action<T1> action;
        public void Invoke(T1 arg1) { if (action != null) { action(arg1); } }
        public static Event<T1> operator +(Event<T1> eve, System.Action<T1> act) { eve.action += act; return eve; }
        public static Event<T1> operator -(Event<T1> eve, System.Action<T1> act) { eve.action -= act; return eve; }
    }
    public struct Event<T1, T2>
    {
        private event System.Action<T1, T2> action;
        public void Invoke(T1 arg1, T2 arg2) { if (action != null) { action(arg1, arg2); } }
        public static Event<T1, T2> operator +(Event<T1, T2> eve, System.Action<T1, T2> act) { eve.action += act; return eve; }
        public static Event<T1, T2> operator -(Event<T1, T2> eve, System.Action<T1, T2> act) { eve.action -= act; return eve; }
    }
    public struct Event<T1, T2, T3>
    {
        private event System.Action<T1, T2, T3> action;
        public void Invoke(T1 arg1, T2 arg2, T3 arg3) { if (action != null) { action(arg1, arg2, arg3); } }
        public static Event<T1, T2, T3> operator +(Event<T1, T2, T3> eve, System.Action<T1, T2, T3> act) { eve.action += act; return eve; }
        public static Event<T1, T2, T3> operator -(Event<T1, T2, T3> eve, System.Action<T1, T2, T3> act) { eve.action -= act; return eve; }
    }
    public struct Event<T1, T2, T3, T4>
    {
        private event System.Action<T1, T2, T3, T4> action;
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4) { if (action != null) { action(arg1, arg2, arg3, arg4); } }
        public static Event<T1, T2, T3, T4> operator +(Event<T1, T2, T3, T4> eve, System.Action<T1, T2, T3, T4> act) { eve.action += act; return eve; }
        public static Event<T1, T2, T3, T4> operator -(Event<T1, T2, T3, T4> eve, System.Action<T1, T2, T3, T4> act) { eve.action -= act; return eve; }
    }
}