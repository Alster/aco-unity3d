using UnityEditor;
using UnityEngine;

namespace ACO.Edt
{
    public static class ApplySceneCameraPosition
    {
        static ApplySceneCameraPosition()
        {
            SceneView.onSceneGUIDelegate -= OnSceneGUI;
            SceneView.onSceneGUIDelegate += OnSceneGUI;
        }
        const string nameFollow = "Follow for SceneView";
        const string nameObject = "Apply from GameObject";

        static bool active = false;
        [MenuItem(Edt.com.name + "/" + nameFollow)]
        static void ApplyFromCamera()
        {
            active = !active;
        }
        static void OnSceneGUI(SceneView sceneView)
        {
            if (active && Selection.activeGameObject != null)
            {
                Selection.activeGameObject.transform.position = SceneView.lastActiveSceneView.camera.transform.position;
                Selection.activeGameObject.transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
            }
        }

        //[MenuItem(Edt.Com.name + "/" + nameObject)]
        //static void ApplyFromObject()
        //{
        //    SceneView.lastActiveSceneView.pivot = Selection.activeGameObject.transform.position - new Vector3(0, -5, 0);
        //    SceneView.lastActiveSceneView.rotation = Selection.activeGameObject.transform.rotation;
        //    SceneView.lastActiveSceneView.Repaint();
        //    SceneView.lastActiveSceneView.position.
        //}
    }
}