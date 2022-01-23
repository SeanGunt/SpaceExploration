using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;


public class ShuttleController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRenderer;
    public static float speed = 4.0f;
    float horizontal;
    float vertical;
    public GameObject loseText;
    public GameObject winText;
    public GameObject child;
    public GameObject startText;
    private bool lost;
    private bool won;
    float startTextTimer = 2.0f;
    float timer = 12.0f;
    public Text timerText;
    public AudioSource audioSource;
    public AudioSource bgAudio;
    public AudioSource explosionSource;
    public AudioClip explosion;
    public AudioClip winMusic;
    public AudioClip loseMusic;
    public AudioClip bgMusic;
    public AudioClip introAudio;
    PolygonCollider2D polygonCollider2d;
    
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider2d = GetComponent<PolygonCollider2D>();
        loseText.SetActive(false);
        winText.SetActive(false);
        timerText.text = timer.ToString("n2");
        bgAudio.clip = bgMusic;
        audioSource.PlayOneShot(introAudio);
        bgAudio.Play();
        bgAudio.volume = 0.05f;
        explosionSource.volume = 0.10f;
        startText.SetActive(true);
        
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        timer -= Time.deltaTime;
        timerText.text = timer.ToString("n2");

        startTextTimer -= Time.deltaTime;

        if (timer < 0 && won == false)
        {
            lost = true;
            audioSource.PlayOneShot(loseMusic);
        }
        
        if (startTextTimer < 0)
        {
            startText.SetActive(false);
            bgAudio.volume = 0.15f;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (lost)
        {
            loseText.SetActive(true);
            speed = 0.0f;
            timer = 0.02f;
            spriteRenderer.sortingOrder = -1;
            child.SetActive(false);
            polygonCollider2d.enabled = false;
            bgAudio.Stop();
            
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("Space");
                speed = 4.0f;
            }
        }
        if (won)
        {
            winText.SetActive(true);
            speed = 0.0f;
            timer = 0.02f;
            bgAudio.Stop();

            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("Space");
                speed = 4.0f;
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;

        position.x = position.x + speed * horizontal * Time.deltaTime; 
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Asteroid")
        {
            lost = true;
            explosionSource.PlayOneShot(explosion);
            audioSource.PlayOneShot(loseMusic);
        }
        if(other.tag == "Flag")
        {
            won = true;
            audioSource.PlayOneShot(winMusic);
        }
    }
    
}
