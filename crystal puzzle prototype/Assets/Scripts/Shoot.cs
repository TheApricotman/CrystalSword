using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Rigidbody2D shootRb;
    private float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        shootRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {//makes bullet move
        shootRb.velocity = transform.up * speed;
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {//Destroys bullets when they hit the wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Block"))
        {
            Destroy(gameObject);
        }
    }
}
