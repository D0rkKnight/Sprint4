using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using uClicker;
using MoreMountains.Tools;

[RequireComponent(typeof(MMTouchButton))]
public class ClickerClickableButton : MonoBehaviour
{

    public Clickable clickable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onButtonPressed()
    {
        ClickerManagerComponent.Instance.Click(clickable);

        Debug.Log("Button pressed");
    }
}
