using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Quaternion ogRot;
    private Quaternion rotate = Quaternion.Euler(0, 0, 90);
    private Quaternion destination;
    private float speed = 100f;
    private bool spinning;
    private bool returning;


    private void Start()
    {
        ogRot = transform.rotation;
    }

    private void Update()
    {
        RotCalc();
    }

    private void RotCalc()
    {
        //checks if bullet has collided with block, rotates smoothly 90 degrees
        if (spinning && !returning)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, destination, speed * Time.deltaTime);
        }
        if (transform.rotation == destination)
        {
            spinning = false;
            transform.rotation = destination;
        }
    }

    public void ReturnCalc()
    {
        //Logic for returning puzzle blocks to original rotation for restart, functionally the same as RotCalc()
        //using input for control for now, but will be controlled with sword in stone
        returning = true;

        if (returning && transform.rotation != ogRot)
        {
  
            transform.rotation = Quaternion.RotateTowards(transform.rotation, ogRot, speed * Time.deltaTime);

            if (transform.rotation == ogRot)
            {
                PuzzleManager.Resetting -= ReturnCalc;
                returning = false;
            }
        }
    }
    private void OnEnable()
    {
        PuzzleManager.Resetting += ReturnCalc;
    }
    private void OnDisable()
    {
        PuzzleManager.Resetting -= ReturnCalc;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if player shoots block, starts logic for rotating it
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            spinning = true;
            destination = transform.rotation * rotate;
        }
    }
}
