using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public Text scoreView;
    public Text scoreViewWide;
    public Transform gameOver;
    public Transform gameStart;
    public Transform skinDefault;
    public Transform skinSunglasses;
    public Transform shop;
    Touch touch;
    public AudioSource death;
    public AudioSource fall;
    public AudioSource point;
    public AudioSource tap;
    int deathcount = 0;
    bool begin = false;
    public int score;
    public float jump;
    public int currentSkin = 0;
    public bool sunGlassesBought;
    public Text skinCost;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        speed = 1;
        enabled = false;
        score = data.score;
        Time.timeScale = 0;
        rb = GetComponent<Rigidbody2D>();
        scoreView.text = score.ToString();
        scoreViewWide.text = score.ToString();
        currentSkin = data.currentSkin;
        sunGlassesBought = data.sunGlassesBought;
        if (currentSkin == 0)
        {
            skinDefault.gameObject.SetActive(true);
            skinSunglasses.gameObject.SetActive(false);
            Invoke("EquippedText", 0);
        }
        else if (currentSkin == 1)
        {
            skinDefault.gameObject.SetActive(false);
            skinSunglasses.gameObject.SetActive(true);
            Invoke("EquippedText", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            // Keyboard Space Button
            if (Input.GetKeyDown(KeyCode.Space) || touch.phase == TouchPhase.Began)
            {
                rb.velocity = Vector2.up * jump;
                tap.Play();
            }
            if (deathcount >= 1)
            {
                if(Input.anyKeyDown)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
        transform.eulerAngles = new Vector3(0, 0, (rb.velocity.y * 5));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AddScore"))
        {
            score++;
            point.Play();
            scoreView.text = score.ToString();
            scoreViewWide.text = score.ToString();
            speed = speed + 0.05f;
        }

        if (collision.CompareTag("Pipe"))
        {
            enabled = false;
            gameOver.gameObject.SetActive(true);
            Invoke("Pause", 1.5f);
            if (deathcount == 0)
            {
                death.Play();
                Invoke("FallSound", 0.65f);
                deathcount++;
                PlayerPrefs.SetInt("Player Score", score);
                SaveSystem.SavePlayer(this);
            }
        }
    }
    void Pause()
    {
        Time.timeScale = 0;
    }
    void FallSound()
    {
        fall.Play();
    }
    public void StartGame()
    {
        if (begin == false)
        {
            Time.timeScale = 1;
            begin = true;
            gameStart.gameObject.SetActive(false);
            rb.velocity = Vector2.up * jump;
            tap.Play();
        }
    }
    public void SkinSelection()
    {
        if (sunGlassesBought == true)
        {
            if (currentSkin == 1)
            {
                skinDefault.gameObject.SetActive(true);
                skinSunglasses.gameObject.SetActive(false);
                currentSkin = 0;
                Invoke("EquippedText", 0);
            }
            else if (currentSkin == 0)
            {
                skinDefault.gameObject.SetActive(false);
                skinSunglasses.gameObject.SetActive(true);
                currentSkin = 1;
                Invoke("EquippedText", 0);
            }
            SaveSystem.SavePlayer(this);
        }
    }
    public void SkinBuy()
    {
        sunGlassesBought = true;
        score = score - 250;
        scoreView.text = score.ToString();
        scoreViewWide.text = score.ToString();
        currentSkin = 1;
        SaveSystem.SavePlayer(this);
    }
    public void EquippedText()
    {
        if (sunGlassesBought == true)
        {
            if (currentSkin == 0)
            {
                skinCost.text = "Equip";
            }
            else if (currentSkin == 1)
            {
                skinCost.text = "Equipped!";
            }
        }
    }
}
