using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.CorgiEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Experience : MonoBehaviour, MMEventListener<MMDamageTakenEvent>
{

    public int xp = 0;
    public int nextXp = 100;
    public int initialNextXp = 100;
    public int level = 1;

    public Character character;

    private void Start() {
        nextXp = initialNextXp;
    }

    public void adjustXp(int amount)
    {
        xp += amount;
        if (xp >= nextXp)
        {
            level++;
            xp -= nextXp;
            nextXp = (int)(nextXp * 1.5f);

            // Broadcast event
            MMEventManager.TriggerEvent(new MMCharacterEvent(character, MMCharacterEventTypes.LevelUp, MMCharacterEvent.Moments.OneTime));
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

    private void OnEnable() {
        this.MMEventStartListening<MMDamageTakenEvent>();
    }

    private void OnDisable() {
        this.MMEventStopListening<MMDamageTakenEvent>();
    }

    public void OnMMEvent(MMDamageTakenEvent eventType)
    {
        Debug.Log(eventType.Instigator.name + " caused " + eventType.DamageCaused + " damage to " + eventType.AffectedHealth.name + " which now has " + eventType.CurrentHealth + " health.");

        if (eventType.CurrentHealth <= 0)
        {
            // Find the Grant Experience component in a child and add the appropriate amount of XP
            GrantExperience grantExperience = eventType.AffectedHealth.GetComponentInChildren<GrantExperience>();

            if (grantExperience != null)
            {
                adjustXp(grantExperience.quant);

                Debug.Log("You now have " + xp + " XP.");
            }
        }
    }

    private void Reset() {
        character = GetComponent<Character>();
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