using UnityEngine;
using UnityEditor;
using System.Linq;
namespace ACO.UI
{
    [CustomEditor(typeof(WindowBase))]
    public class WindowBaseEditor : Editor
    {
        //WindowBase myTarget;
        string windows;
        /*private void OnEnable() {
            myTarget = (WindowBase)target;
        }*/
        public void Start() { }
        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                if (WindowBase.GetStack().Count > 0)
                {
                    windows = "->" + WindowBase.GetStack().Select(el => el.gameObject.name).
                        Aggregate((f, s) => f + ", " + s);
                }
                else
                {
                    windows = "---";
                }
                EditorGUILayout.LabelField(windows);
                EditorGUILayout.Space();
            }
            DrawDefaultInspector();
        }
    }
}