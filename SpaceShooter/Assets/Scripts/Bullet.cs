using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Rigidbody2D rb;
    private float speed = 4.0f;

	// Use this for initialization
	void Start () {
       rb = GetComponent<Rigidbody2D>();;
	}

    // Update is called once per frame
    void Update () {
        if (!ButtonController.PausedGame)
        {
            Vector3 pos = transform.position;
            Vector3 velocity = new Vector3(0, speed * Time.deltaTime, 0);
            pos += transform.rotation * velocity;
            transform.position = pos;
        }
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Destroyer" || collision.tag=="DownBorder")
        {
            //Debug.Log("Crashed with down border");
            Destroy(gameObject);
        }
    }
}
