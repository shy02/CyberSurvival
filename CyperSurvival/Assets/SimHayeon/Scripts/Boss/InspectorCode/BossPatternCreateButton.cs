using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Boss4Attack))]

public class BossPatternCreateButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Boss4Attack bossAttackpattern = (Boss4Attack)target;

        if (GUILayout.Button("Pattern1 Start")) { bossAttackpattern.Pattern1(); }
        if (GUILayout.Button("Pattern2 Start")) { bossAttackpattern.Pattern2(); }
        if (GUILayout.Button("Pattern3 Start")) { bossAttackpattern.Pattern3(); }
        if (GUILayout.Button("Pattern4 Start")) { bossAttackpattern.Pattern4(); }
    }
}
