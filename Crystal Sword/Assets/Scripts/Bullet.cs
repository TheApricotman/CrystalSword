using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    public float speed = 20;
    private Animator playerAnim;
    public Rigidbody2D bulletRB;

    private void Start()
    {
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
        direction = new Vector2(playerAnim.GetFloat("Horizontal"), playerAnim.GetFloat("Vertical"));
    }

    void Update()
    {     
        bulletRB.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

}
