using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BannerPopup : MonoBehaviour {
    
    public Animator anim;
    void Reset() {
        anim = GetComponent<Animator>();
    }

    public void Open() {
        if (anim != null) {
            anim.SetTrigger("Play");
            anim.SetBool("Test", true);
        }
    }

}

// Editor
#if UNITY_EDITOR
[CustomEditor(typeof(BannerPopup))]
public class BannerPopupEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        BannerPopup myScript = (BannerPopup)target;
        if (GUILayout.Button("Open")) {
            myScript.Open();
        }
    }

}
#endif