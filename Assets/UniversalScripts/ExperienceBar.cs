using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MoreMountains.Tools;
using MoreMountains.CorgiEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExperienceBar : MonoBehaviour
{
    protected MMProgressBar _progressBar;
    public Experience exp;

    protected virtual void Start()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        _progressBar = GetComponent<MMProgressBar>();

        tryConnectPlayer();
    }

    protected virtual void Update()
    {
        tryConnectPlayer();

        if (exp == null)
        {
            return;
        }

        // Debug.Log("XP: " + exp.getXp() + " / " + exp.getNextXp());
        _progressBar.UpdateBar(exp.getXp(), 0, exp.getNextXp());
    }

    protected void tryConnectPlayer()
    {
        if (exp == null)
        {
            Character player = Utils.getPlayerByID("Player1");
            if (player != null)
            {
                exp = player.GetComponentInChildren<Experience>();
            }
        }
    }
}