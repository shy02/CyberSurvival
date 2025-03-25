using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnManager_s_3))]
public class SpawnTimerInspecter : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SpawnManager_s_3 spawn = (SpawnManager_s_3)target;

        if (GUILayout.Button("BossTime")) { spawn.BossTime(); }
    }
}
