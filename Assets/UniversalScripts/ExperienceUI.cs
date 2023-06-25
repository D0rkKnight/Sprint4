using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;

public class ExperienceUI : MonoBehaviour, MMEventListener<MMCharacterEvent> {
    public BannerPopup lvlUpBanner;

    private void Reset() {
        lvlUpBanner = GetComponentInChildren<BannerPopup>();
    }

    private void OnEnable() {
        this.MMEventStartListening<MMCharacterEvent>();
    }

    private void OnDisable() {
        this.MMEventStopListening<MMCharacterEvent>();
    }

    public void OnMMEvent(MMCharacterEvent eventType) {
        if (eventType.EventType == MMCharacterEventTypes.LevelUp) {
            lvlUpBanner.Open();
        }
    }
}