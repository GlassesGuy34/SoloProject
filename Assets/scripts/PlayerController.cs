using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    public float ammo = 10.0f;
    public float timeLimit = 12.0f;
    bool gameOver = false;
    bool soundPlayed;
    public float introTime = 2.0f;

    public AudioClip looseArrow;
    public AudioClip victorySound;
    public AudioClip defeatSound;
    public AudioClip hitSound;
    public AudioClip introSound;
    AudioSource audioSource;

    Rigidbody2D rigidbody2d;
    bool introPlayed = false;
    float timer;
    int direction = 1;
    Vector2 lookDirection = new Vector2(1, 0);
    public GameObject winText;
    public GameObject loseText;
    public GameObject introScreen;

    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoLeft;
    public TextMeshProUGUI timeLeft;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //Bow up and down
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
        //Clock
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
        }
        else
        {
            timeLimit = 0;
        }
        timeLeft.text = timeLimit.ToString();

        //Winning and losing
        if (ammo == 0)
        {
            //lose
            loseText.SetActive(true);
            gameOver = true;
            //print("you lose");
            if(!soundPlayed)
            {
                PlaySound(defeatSound);
                soundPlayed = true;
            }
        }
        if (timeLimit == 0)
        {
            //lose
            loseText.SetActive(true);
            gameOver = true;
            //print("you lose");
            if (!soundPlayed)
            {
                PlaySound(defeatSound);
                soundPlayed = true;
            }
        }
        if (score == 5)
        {
            //win
            winText.SetActive(true);
            gameOver = true;
            timeLimit = 10;
            //print("you win");
            if (!soundPlayed)
            {
                PlaySound(victorySound);
                soundPlayed = true;
            }
        }
        if(Input.GetKey(KeyCode.R))
        {
            if(gameOver == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        //introduction
        introTime -= Time.deltaTime;
        if(introTime > 0)
        {
            introScreen.SetActive(true);
            if(!introPlayed)
            {
                PlaySound(introSound);
                introPlayed = true;
            }
        }
        else
        {
            introScreen.SetActive(false);
        }
        if (introTime < 0)
        {
            introTime = 0;
        }
    }
    //Targets Hit
    public void ChangeScore(int scoreAmount)
    {
        score = (score + scoreAmount);
        scoreText.text = score.ToString();
        PlaySound(hitSound);
    }

    void FixedUpdate()
    {
        Vector2 position = GetComponent<Rigidbody2D>().position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        GetComponent<Rigidbody2D>().MovePosition(position);
    }
    //Shooting
    void Launch()
    {
        if (ammo > 0)
        {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Arrow projectile = projectileObject.GetComponent<Arrow>();
        projectile.Launch(lookDirection, 500);

        ammo = ammo - 1;

        ammoLeft.text = ammo.ToString();

        PlaySound(looseArrow);
        }

    }
    //soundPlayer
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}
    
