﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    Rigidbody2D playerRb;
    Vector2 move;
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + move * speed * Time.fixedDeltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody hitRigidbody = hit.collider.attachedRigidbody;
        if (hitRigidbody != null && hitRigidbody.isKinematic == false)
        {
            hitRigidbody.AddForceAtPosition(hit.moveDirection * (playerRb.velocity.magnitude / hitRigidbody.mass), hit.point, ForceMode.Impulse);
        }
    }
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            {
                Instantiate(projectilePrefab, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
            }
        }
    }

}

