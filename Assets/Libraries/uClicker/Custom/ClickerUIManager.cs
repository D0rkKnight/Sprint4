using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using uClicker;
using TMPro;
using MoreMountains.Tools;

using System.Reflection;

public class ClickerUIManager : MMPersistentSingleton<ClickerUIManager>
{
    public TMP_Text incomeLabel;
    public Currency currency;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Force access the private method :/
        // MethodInfo incomeMethod = ClickerManagerComponent.Instance.clickerManager.GetType().GetMethod("PerSecondAmount");
        // double income = (double)incomeMethod.Invoke(ClickerManagerComponent.Instance.clickerManager, new object[] { currency });
        // incomeLabel.text = BigNumberFormatter.Format(income);

        // I've given up on this for now, would take too long

    }
}
