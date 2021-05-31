using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    Rigidbody2D playerRb;
    Vector2 move;
    public GameObject projectilePrefab;
    private Transform shootPoint;

    // Start is called before the first frame update
    void Start()
    {
        shootPoint = GameObject.Find("Shoot Point").GetComponent<Transform>();
        playerRb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        Rotate();
    }
    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + move * speed * Time.fixedDeltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {//Push puzzle blocks
        Rigidbody hitRigidbody = hit.collider.attachedRigidbody;
        if (hitRigidbody != null && hitRigidbody.isKinematic == false)
        {
            hitRigidbody.AddForceAtPosition(hit.moveDirection * (playerRb.velocity.magnitude / hitRigidbody.mass), hit.point, ForceMode.Impulse);
        }
    }
    void Shoot()
    {//fires bullet
        if (Input.GetKeyDown(KeyCode.Q))
        {
            {
                Instantiate(projectilePrefab, shootPoint.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
            }
        }
    }
    void Rotate()
    {//rotates player to face direction inputted
        if (move.x == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        if (move.x == -1)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        if (move.y == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (move.y == -1)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
      
    }
}

