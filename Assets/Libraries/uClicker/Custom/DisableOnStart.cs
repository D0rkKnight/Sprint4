using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

        Debug.Log("Disabled " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
