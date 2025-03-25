using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(ScenManager))]
public class MoveSceneInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ScenManager scene = (ScenManager)target;

        if (GUILayout.Button("GoNextScene")) { scene.GoNextStage(); }
    }
}
