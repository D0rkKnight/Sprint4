using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TilingBackground))]
public class Editor_TilingBackground : Editor
{
    public override void OnInspectorGUI()
    {
        TilingBackground tilingBackground = (TilingBackground)target;

        DrawDefaultInspector();

        EditorGUILayout.Space();

        if (GUILayout.Button("Redraw"))
        {
            tilingBackground.Redraw();

            // Mark scene dirty
            if (!Application.isPlaying)
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(tilingBackground.gameObject.scene);
        }
    }
}
