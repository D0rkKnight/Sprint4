using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BannerPopup : MonoBehaviour {
    
    public Animator anim;

    public float lifetime = 2f;
    public bool open = false;
    float closeAt = 0f;


    void Reset() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        anim.SetBool("Open", open);
        
        if (open && Time.time > closeAt) {
            open = false;
        }
    }

    public void Open() {
        open = true;
        closeAt = Time.time + lifetime;
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