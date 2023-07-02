using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SOPopulator : MonoBehaviour
{
    public SOManifest manifest;
    public Transform parent;

    public MonoBehaviour comparer;
    public bool mantainLocalScale = true;

    // Start is called before the first frame update
    void Start()
    {
        // Clone items
        List<ScriptableObject> items = new List<ScriptableObject>();
        for (int i = 0; i < manifest.items.Count; i++)
        {
            items.Add(manifest.items[i]);
        }

        // Sort items
        if (comparer != null && comparer is IComparer<ScriptableObject>)
        {
            items.Sort(comparer as IComparer<ScriptableObject>);

            Debug.Log("Sorted items");
        }

        for (int i = 0; i < manifest.items.Count; i++)
        {
            if (items[i] is IGenerableToGObj)
            {
                GameObject entry = (items[i] as IGenerableToGObj).generateEntryObj();

                Vector3 localScale = entry.transform.localScale;
                entry.transform.SetParent(parent);

                if (mantainLocalScale)
                {
                    entry.transform.localScale = localScale;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
