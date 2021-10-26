using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour
{ 
    private int bounce;
    private LineRenderer lR;
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private LayerMask mirrors;
    public Transform shootPoint1;
    public Transform shootPoint2;
    public ReflectLaser reflectLaser;



    // Start is called before the first frame update
    void Start()
    {
        lR = GetComponent<LineRenderer>();
        bounce = 5;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            CastRay(transform.position, -transform.up);
        }
    }

    private void CastRay(Vector2 position, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, direction);
        Debug.DrawRay(position, direction, Color.blue);
        lR.positionCount = 2;
        lR.SetPosition(0, transform.position);
        Vector3 savePos;
        if (hit.collider != null)
        {
            savePos = hit.point;
            if (hit.collider.name == "Mirror 1")
            {
                lR.SetPosition(1, savePos);
                position = shootPoint1.position;
                direction = shootPoint1.transform.up;
                hit = Physics2D.Raycast(position, direction);
                Debug.DrawRay(position, direction, Color.blue);
                reflectLaser.HitMirror1(hit.point);

            }
            if (hit.collider.name == "Mirror 2")
            {
                lR.SetPosition(1, savePos);
                position = shootPoint2.position;
                direction = shootPoint2.transform.up;
                hit = Physics2D.Raycast(position, direction);
                Debug.DrawRay(position, direction, Color.blue);
                reflectLaser.HitMirror2(hit.point);
            }
            if (hit.collider.CompareTag("Button"))
            {
                lR.SetPosition(1, savePos);
                Debug.Log("Win!");
            }
        }
    }

    /*private void CastRay(Vector2 position, Vector2 direction)
    {

        RaycastHit2D hit = Physics2D.Raycast(position, direction);
        lR.positionCount = 1;
        lR.SetPosition(0, transform.position);
        for (int i = 0; i < bounce; i++)
        {
            lR.positionCount += 1;
            lR.SetPosition(lR.positionCount - 1, hit.point);

            if (hit.collider.name == "Mirror 1")
            {
                Debug.Log("Boing!");
                position = hit.point;
                direction = transform.right;
                hit = Physics2D.Raycast(position, direction);

            }
            if (hit.collider.name == "Mirror 2")
            {
                Debug.Log("Boing 2!");
                position = hit.point;
                direction = hit.transform.up;
                hit = Physics2D.Raycast(position, direction);
            }
            if (hit.collider.CompareTag("Button"))
            {
                Debug.Log("Win!");
            }


        }
    }*/
}
