using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using uClicker;
using MoreMountains.Tools;

public class ClickerManagerComponent : MMPersistentSingleton<ClickerManagerComponent>
{

    public ClickerManager clickerManager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(onTick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator onTick()
    {
        while (true)
        {
            clickerManager.Tick();
            yield return new WaitForSeconds(1);
        }
    }

    public void Click(Clickable clickable)
    {
        clickerManager.Click(clickable);
    }
}
