using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(LevelManager), true)]
public class LevelGeneratorEditor : Editor
{
    LevelManager levelGenerator;

    private void Awake()
    {
        levelGenerator= (LevelManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create Level"))
        {
            levelGenerator.GenerateLevel();
        }
    }
}
