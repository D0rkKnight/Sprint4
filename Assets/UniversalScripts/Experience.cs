using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Experience : MonoBehaviour
{

    public int xp = 0;
    public int nextXp = 100;
    public int level = 1;

    public void adjustXp(int amount)
    {
        xp += amount;
        if (xp >= nextXp)
        {
            level++;
            xp -= nextXp;
            nextXp = (int)(nextXp * 1.5f);
        }
    }

    public int getXp()
    {
        return xp;
    }

    public int getNextXp()
    {
        return nextXp;
    }

    public int getLevel()
    {
        return level;
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(Experience))]
[CanEditMultipleObjects]
public class ExperienceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Experience myScript = (Experience)target;
        if (GUILayout.Button("Add EXP"))
        {
            myScript.adjustXp(100);

            if (!Application.isPlaying)
            {
                Utils.markDirty(myScript);

                // Register an undo as well
                Undo.RecordObject(myScript, "Add EXP");
                serializedObject.ApplyModifiedProperties(); // Not really sure what this does...
            }
        }
    }
}
#endif