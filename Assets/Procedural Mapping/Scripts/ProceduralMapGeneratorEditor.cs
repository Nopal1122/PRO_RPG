#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProceduralMapGenerator))]
public class ProceduralMapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ProceduralMapGenerator generator = (ProceduralMapGenerator)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Generate Map"))
        {
            generator.GenerateMap();
        }

        if (GUILayout.Button("Clear Map"))
        {
            generator.ClearMap();
        }
    }
}
#endif
