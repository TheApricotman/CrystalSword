using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float rotateAngle = 90f;
    private void OnCollisionEnter2D(Collision2D collision)
    { //Puzzle block rotates 90 degrees when shot
        if (collision.gameObject.CompareTag("Bullet"))
        {
            transform.Rotate(Vector3.back, rotateAngle);
        }
    }
}
