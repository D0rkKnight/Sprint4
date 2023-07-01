using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using uClicker;
using MoreMountains.Tools;

public class ClickerUpgradeButton : MonoBehaviour
{
    public Upgrade upgrade;

    public Image icon;

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
        if (ClickerManagerComponent.Instance.clickerManager.CanBuy(upgrade)) {
            ClickerManagerComponent.Instance.clickerManager.BuyUpgrade(upgrade);

            Debug.Log("Upgrade purchased");
        }
    }
}
