using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using uClicker;
using TMPro;

public class ClickerResourceCounter : MonoBehaviour
{
    public TMP_Text text;
    public Currency currency;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ClickerManagerComponent.Instance.clickerManager.State.CurrencyCurrentTotals[currency].ToString();
    }
}
