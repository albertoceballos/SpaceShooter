using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    float speed = 4.0f;
    public GameObject bullet;
    float coolDownTimer = 0;
    float Delay = 0.25f;
    private int damage = 2;
    private static int score = 0;
    private int lives = 3;
    private int highScore;
    private float spawningTime = 0;

    public static void UpdateScore()
    {
        var scoreText = GameObject.Find("Score").GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    public static int GetScore()
    {
        return score;
    }

    public static void SetScore(int Nscore)
    {
        score = Nscore;
    }
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        highScore = PlayerPrefs.GetInt("HighScore");
	}

    void Movement()
    {
        float horMov = Input.GetAxisRaw("Horizontal");
        float verMov = Input.GetAxisRaw("Vertical");
        Vector2 mov;
        coolDownTimer -= Time.deltaTime;
        if (Input.GetKeyDown("a"))
        {
            mov = new Vector2(horMov, 0);
            rb.velocity = mov * speed;
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        if (Input.GetKeyDown("d"))
        {
            mov = new Vector2(horMov, 0);
            rb.velocity = mov * speed;
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        if (Input.GetKeyDown("w"))
        {
            mov = new Vector2(0, verMov);
            rb.velocity = mov * speed;
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        if (Input.GetKeyDown("s"))
        {
            mov = new Vector2(0, verMov);
            rb.velocity = mov * speed;
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.Space) && coolDownTimer<=0)
        {
            coolDownTimer = Delay;
            ShootLaser();
        }
    }

    void ShootLaser()
    {
        Instantiate(bullet,transform.position,transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LeftBorder" || collision.tag == "RightBorder")
        {
            //Debug.Log("Crashed with border");
            if (transform.position.x > 0)//right crash
            {
                transform.position = new Vector2(transform.position.x - .2f, transform.position.y);
            }
            else if (transform.position.x > 0) // left crash
            {
                transform.position = new Vector2(transform.position.x + .5f, transform.position.y);
            }

            rb.velocity = Vector2.zero;
        }
        if (collision.tag == "DownBorder")
        {
            if (transform.position.y < 0) {//dpwn
                transform.position = new Vector2(transform.position.x, transform.position.y + .5f);
            }
        }
        if (spawningTime<= 0)
        {
            if (collision.tag == "Kamikaze" || collision.tag == "WeakEnemy" || collision.tag == "EnemyBullet" || collision.tag=="EnemySpecial")
            {
                LoseALive();
                if (lives == 0)
                {
                    GameOver();
                }
            }
        }
    }

    void LoseALive()
    {
        lives--;
        if (lives == 2)
        {
            Destroy(GameObject.Find("Live3"));
        }else if (lives == 1)
        {
            Destroy(GameObject.Find("Live2"));
        }else if (lives == 0)
        {
            Destroy(GameObject.Find("Live1"));
        }
        transform.position = new Vector2(0, -4);
        spawningTime = 5.0f;
    }

    void GameOver()
    {
        if(highScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }

    // Update is called once per frame
    void Update () {
        if (!ButtonController.PausedGame)
        {
            Movement();
            if (spawningTime > 0)
            {
                spawningTime -= Time.deltaTime;
            }
        }
        
	}
}
