using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Quaternion ogRot;
    private Vector3 rotate = new Vector3(0, 0, 90);
    private float speed = 0.1f;
    public bool spinning;
    private Vector3 destination;
    private void Start()
    {
        ogRot = transform.rotation;
    }

    private void Update()
    {
        destination = transform.rotation.eulerAngles + rotate;
        if (spinning)
        {

            //Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90), Time.time * speed);
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, destination, speed * Time.time);



        }
        if (transform.rotation.eulerAngles == destination)
        {
            spinning = false;
            transform.eulerAngles = destination;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            spinning = true;
        }
    }
}
