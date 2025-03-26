using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyDamage_3))]

public class EnemyDamageTestInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EnemyDamage_3 enemydamage = (EnemyDamage_3)target;

        if (GUILayout.Button("GetDamage")) { enemydamage.GetDamage(30); }
    }
}
