using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MoreMountains.Tools;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExperienceBar : MonoBehaviour
{
    protected MMProgressBar _progressBar;
    public Experience exp;

    protected virtual void Start()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        _progressBar = GetComponent<MMProgressBar>();
        exp = GetComponent<Experience>();
    }

    protected virtual void Update()
    {
        // Debug.Log("XP: " + exp.getXp() + " / " + exp.getNextXp());
        _progressBar.UpdateBar(exp.getXp(), 0, exp.getNextXp());
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ExperienceBar))]
[CanEditMultipleObjects]
public class ExperienceBarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ExperienceBar myScript = (ExperienceBar)target;
        if (GUILayout.Button("Add EXP"))
        {
            myScript.exp.adjustXp(100);

            if (!Application.isPlaying)
            {
                Utils.markDirty(myScript.exp);

                // Register an undo as well
                Undo.RecordObject(myScript.exp, "Add EXP");
                serializedObject.ApplyModifiedProperties(); // Not really sure what this does...
            }
        }
    }
}
#endif