using System.Collections;
using UnityEngine;

public class SOPopulator : MonoBehaviour
{
    public SOManifest manifest;
    public Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < manifest.items.Count; i++) {
            if (manifest.items[i] is IGenerableToGObj) {
                GameObject entry = (manifest.items[i] as IGenerableToGObj).generateEntryObj();
                entry.transform.SetParent(parent);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
