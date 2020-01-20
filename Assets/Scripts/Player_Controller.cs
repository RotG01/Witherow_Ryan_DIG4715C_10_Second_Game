using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class Player_Controller : MonoBehaviour
{
    public GameObject BulletSprite;
    public GameObject Gunshoot;
    public Transform firePos;
    public float jumpForce;
    public int time;

    private AudioSource audioSource;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        time = 12;

        StartCoroutine("Countdown");
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && time <= 10 && time > 0)
        {
            Instantiate(Gunshoot, firePos.position, firePos.rotation);
            Instantiate(BulletSprite, firePos.position, firePos.rotation);
            audioSource.Play();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && time <= 10 && time > 0)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    IEnumerator Countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            time--;
        }
    }
}