using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public int score;
    public int currentSkin;
    public bool sunGlassesBought;

    public PlayerData (PlayerController player)
    {
        score = player.score;
        currentSkin = player.currentSkin;
        sunGlassesBought = player.sunGlassesBought;
    }
}
