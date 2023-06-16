using UnityEngine;

public class Experience : MonoBehaviour
{

    public int xp = 0;
    public int nextXp = 100;
    public int level = 1;

    public void adjustXp(int amount)
    {
        xp += amount;
        if (xp >= nextXp)
        {
            level++;
            xp -= nextXp;
            nextXp = (int)(nextXp * 1.5f);
        }
    }

    public int getXp()
    {
        return xp;
    }

    public int getNextXp()
    {
        return nextXp;
    }

    public int getLevel()
    {
        return level;
    }

}