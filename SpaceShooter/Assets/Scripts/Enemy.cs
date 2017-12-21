using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int health;
    Rigidbody2D rb;
    public float Delay = 0.5f;
    private float coolDownTimer =0;
    public GameObject bullet;
    public float speed = 2.0f;
    public int points;
    Transform player;
    private Vector3 smoothVel = Vector3.zero;
    public float time = 2.0f;
    private int bulletCount = 0;
    public GameObject special;
    public static bool isBossAround = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (tag == "Boss")
        {
            isBossAround = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!ButtonController.PausedGame)
        {
            coolDownTimer -= Time.deltaTime;
            if (coolDownTimer <= 0 && tag!="WeakEnemy")
            {
                ShootBullet();
                coolDownTimer = Delay;
            }
            if (tag == "Kamikaze")
            {
                //falls aiming at player
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, transform.position.y, 0), ref smoothVel, time);
            }
            else if (tag == "HeavyEnemy")
            {
                //transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), speed);
                //follows player, shoot at player, bombs
                if (bulletCount == 5)
                {
                    bulletCount = 0;
                    ShootSpecial();
                }
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, transform.position.y, 0), ref smoothVel, time);
            }
            else if (tag == "Boss")
            {
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, transform.position.y, 0), ref smoothVel, time);
                if (bulletCount == 5)
                {
                    ShootSpecial();
                    bulletCount = 0;
                }
                //Target player and Special attack
            }
        }
        
        
	}

    void ShootSpecial()
    {
        Instantiate(special, transform.position, transform.rotation);
    }

    void ShootBullet()
    {
        Instantiate(bullet,transform.position,transform.rotation);
        bulletCount++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            health--;
            Debug.Log("Health=" + health);
            if (health == 0)
            {
                Player.SetScore(Player.GetScore() + points);
                Player.UpdateScore();
                Destroy(gameObject);
                if (tag == "Boss")
                {
                    isBossAround = false;
                }
            }
            Destroy(collision.gameObject);
        }
        if (collision.tag == "DownBorder")
        {
           // Debug.Log("Collided");
            Destroy(gameObject);
        }
        if (collision.tag == "LeftBorder")
        {
            transform.position = new Vector2(transform.position.x + .5f, transform.position.y);
        }
        if (collision.tag == "RightBorder")
        {
            transform.position = new Vector2(transform.position.x - .5f, transform.position.y);
        }
    }
}
