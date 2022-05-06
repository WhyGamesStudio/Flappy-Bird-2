using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Transform shop;
    public PlayerController player;
    public Text cost;
    public AudioSource song;
    public AudioSource[] sounds;
    bool sound = true;
    bool music = true;

    public void ResetGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OpenShop()
    {
        shop.gameObject.SetActive(true);
    }
    public void StartGame()
    {
        player.enabled = true;
        player.Invoke("StartGame", 0);
    }
    public void ExitShop()
    {
        shop.gameObject.SetActive(false);
    }
    public void BuySkin()
    {
        if (player.currentSkin == 0)
        {
            if (player.sunGlassesBought == true)
            {
                player.Invoke("SkinSelection", 0);
            } else if (player.score >= 250)
            {
                player.Invoke("SkinBuy", 0);
                player.Invoke("SkinSelection", 0);
            }
        } else if (player.currentSkin == 1)
        {
            player.Invoke("SkinSelection", 0);
        }
    }
    // This one is to setup when the music must run or me muted.
    public void MusicToggle()
    {
        if (music == true)
        {
            music = false;
            song.volume = 0;
        } else if (music == false)
        {
            music = true;
            song.volume = 1;
        }
    }
    // And this one is to mute all sounds, except for the music (Why not?)
    public void SoundToggle()
    {
        if (sound == true)
        {
            sound = false;
            sounds[0].volume = 0;
            sounds[1].volume = 0;
            sounds[2].volume = 0;
            sounds[3].volume = 0;
        }
        else if (sound == false)
        {
            sound = true;
            sounds[0].volume = 0.65f;
            sounds[1].volume = 0.65f;
            sounds[2].volume = 0.65f;
            sounds[3].volume = 0.65f;
        }
    }
}
