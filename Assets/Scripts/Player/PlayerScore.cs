using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    
    private int enemiesKilled = 0;
    private int levelCleared = 0;
    private int bossesKilled = 0;


    public int GetKilled()
    {
        return enemiesKilled;
    }

    public int GetCleared()
    {
        return levelCleared;
    }

    public int GetBosses()
    {
        return bossesKilled;
    }

    public void AddKilled()
    {
        enemiesKilled += 1;
    } 

    public void AddClear()
    {
        levelCleared += 1;
        bossesKilled += 1;
    }


}
