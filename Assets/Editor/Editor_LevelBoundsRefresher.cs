using UnityEngine;
using UnityEditor;
using MoreMountains.CorgiEngine;

[CustomEditor(typeof(LevelBoundsRefresher))]
public class LevelBoundsRefresherEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelBoundsRefresher boundsRefresher = (LevelBoundsRefresher)target;

        // Add a button for the Refresh action
        if (GUILayout.Button("Refresh"))
        {
            boundsRefresher.Refresh();
        }
    }
}
