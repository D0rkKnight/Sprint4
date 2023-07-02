using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

// SO for generable manifest
[CreateAssetMenu(fileName = "SOManifest", menuName = "Sprint4/SOManifest", order = 0)]
public class SOManifest : ScriptableObject
{
    public List<ScriptableObject> items;
}

#if UNITY_EDITOR
[CustomEditor(typeof(SOManifest))]
public class SOManifestEditor : UnityEditor.Editor
{
    public string searchInFolder = "Assets";
    public UnityEngine.Object template;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SOManifest manifest = (SOManifest)target;


        template = EditorGUILayout.ObjectField(template, typeof(UnityEngine.Object), false);

        if (GUILayout.Button("Generate Manifest"))
        {
            manifest.items.Clear();

            string wantedType = template.GetType().ToString();
            string[] guids = AssetDatabase.FindAssets($"t:{wantedType}", new[] { searchInFolder });

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

                manifest.items.Add(so);
            }

            // Save manifest
            EditorUtility.SetDirty(manifest);
            AssetDatabase.SaveAssets();
        }

        // Clear manifest button
        if (GUILayout.Button("Clear Manifest"))
        {
            manifest.items.Clear();
        }
    }
}
#endif