using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mover : MonoBehaviour
{
    public Vector2 speed;
    public int LockCount;

    private Rigidbody2D rb;
    private GameController gamecontroller;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.forward * speed;
        audioSource = GetComponent<AudioSource>();

        GameObject gamecontrollerObject = GameObject.FindWithTag("GameController");
        if (gamecontrollerObject != null)
        {
            gamecontroller = gamecontrollerObject.GetComponent<GameController>();
        }
        if (gamecontroller == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    private void Update()
    {
        rb.velocity = speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Lock"))
        {
            audioSource.Play();
            other.gameObject.SetActive(false);
            Destroy(gameObject);
            gamecontroller.SubtractLock(LockCount);
        }
    }
}