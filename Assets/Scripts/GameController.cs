using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class GameController : MonoBehaviour
{
    public GameObject JailSprite;
    public Text LockCount;
    public Text WinText;
    public Text LoseText;
    public Text Timer;
    public Text restartText;
    public Text gameOverText;
    public Text Instructions;
    public Text controls;
    public int time;
    public int Locks;
    public int jail;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;
    public AudioSource musicSource;

    private bool restart;
    private bool gameOver;
    private Music_Manager stagemusic;
    private Music_Manager loseMusic;
    private Music_Manager winMusic;
    private AudioSource audioSource;

    Rigidbody2D rb2d;

    void Start()
    {
        stagemusic = FindObjectOfType<Music_Manager>();
        loseMusic = FindObjectOfType<Music_Manager>();
        winMusic = FindObjectOfType<Music_Manager>();

        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        Locks = 2;
        time = 12;
        jail = 0;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        controls.text = "Press 'W' to Jump and 'Space' to shoot!";
        WinText.text = "";
        LoseText.text = "";
        Instructions.text = "Shoot both locks before time is up";
        LockCount.text = "Locks Left: " + Locks;
        SetAllText();

        StartCoroutine("Countdown");
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        if (restart == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("10 Second Game");
            }
        }

        if (time <= 10 && LoseText.text != "You Lose!" && WinText.text != "You Win!!!")
        {
            stagemusic.ChangeBM(musicClipOne);
            musicSource.loop = true;
        }
    }

    public void SubtractLock(int newLockCount)
    {
        Locks -= newLockCount;
        UpdateText();
        SetAllText();
    }

    IEnumerator Countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            time--;
            UpdateText();
            SetAllText();
        }
    }

    void UpdateText()
    {
        LockCount.text = "Locks Left: " + Locks;
    }

    void SetAllText()
    {
        if (time > 0)
        {
            Timer.text = "Time Left: " + time;
        }
        else if (time <= 0)
        {
            Timer.text = "Time Left: 0";
        }

        if (time <= 10)
        {
            Instructions.text = "";
        }

        if (time > 0 && Locks <= 0 && LoseText.text != "You Lose!")
        {
            WinText.text = "You Win!!!";
            gameOverText.text = "Game Created By Ryan Witherow";
            winMusic.ChangeBM(musicClipTwo);
            musicSource.loop = false;

            gameOver = true;
            restart = true;

            restartText.text = "Press 'Q' to Restart";
        }

        else if (Locks != 0 && Timer.text == "Time Left: 0" && WinText.text != "You Win!!!")
        {
            if (jail == 0)
            {
                Instantiate(JailSprite, transform.position, Quaternion.identity);
                jail += 1;
            }
            LoseText.text = "You Lose!";
            gameOverText.text = "Game Created By Ryan Witherow";
            loseMusic.ChangeBM(musicClipThree);
            musicSource.loop = false;

            gameOver = true;
            restart = true;

            restartText.text = "Press 'Q' to Restart";
        }
    }
}