using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using uClicker;
using MoreMountains.Tools;
using TwoPm.TooltipDemo;

[RequireComponent(typeof(MMTouchButton))]
public class ClickerUpgradeButton : MonoBehaviour
{
    public Upgrade upgrade;

    public Image bg;
    public Image icon;
    public ReactToPointer tooltip;

    bool purchased = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check if purchased. If so, grey out button.
        if (purchased)
        {
            GetComponent<MMTouchButton>().Interactable = false;

            // Grey out icon and bg
            icon.color = new Color(0.5f, 0.5f, 0.5f, 1);
            bg.color = new Color(0.3f, 0.3f, 0.3f);
        }
        else
        {
            GetComponent<MMTouchButton>().Interactable = true;

            // Check if can buy. If so, highlight button.
            if (ClickerManagerComponent.Instance.clickerManager.CanBuy(upgrade))
            {
                bg.color = new Color(0.2f, 0.5f, 0.2f);
            }
            else
            {
                bg.color = new Color(0.3f, 0.3f, 0.3f);
            }
        }
    }

    public void onButtonPressed()
    {
        if (ClickerManagerComponent.Instance.clickerManager.CanBuy(upgrade))
        {
            ClickerManagerComponent.Instance.clickerManager.BuyUpgrade(upgrade);

            Debug.Log("Upgrade purchased");
            purchased = true;
        }
    }
}
