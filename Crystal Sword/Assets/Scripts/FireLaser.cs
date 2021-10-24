using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour
{
    private Vector3 bendLaser = new Vector3(0, 0, 90);
    private int bounce;
    private LineRenderer lR;
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private LayerMask mirrors;
    public Transform shootPoint1;
    public Transform shootPoint2;



    // Start is called before the first frame update
    void Start()
    {
        lR = GetComponent<LineRenderer>();
        bounce = 5;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CastRay(transform.position, -transform.up);
        }
    }

    private void CastRay(Vector2 position, Vector2 direction)
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
    }
}
