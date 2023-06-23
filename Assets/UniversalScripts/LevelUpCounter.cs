using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using TMPro;

public class LevelUpCounter : MonoBehaviour, MMEventListener<MMCharacterEvent>
{
    public TMP_Text levelUpText;

    private void OnEnable() {
        this.MMEventStartListening<MMCharacterEvent>();
    }

    private void OnDisable() {
        this.MMEventStopListening<MMCharacterEvent>();
    }

    public void OnMMEvent(MMCharacterEvent eventType)
    {
        if (eventType.EventType == MMCharacterEventTypes.LevelUp)
        {
            // Get Experience component from child object
            Experience experience = eventType.TargetCharacter.GetComponentInChildren<Experience>();

            Debug.Log("Level up! Level: " + experience.getLevel() + ", XP: " + experience.getXp() + ", Next XP: " + experience.getNextXp());

            if (levelUpText != null)
            {
                levelUpText.text = "Lvl. " + (experience.getLevel() + 1);
                levelUpText.gameObject.SetActive(true);
            }
        }
    }

    private void Reset() {
        levelUpText = GetComponentInChildren<TMP_Text>();
    }
}