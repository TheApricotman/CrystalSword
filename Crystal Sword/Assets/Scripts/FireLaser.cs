using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour
{
    private int maxBounces = 5;
    private LineRenderer lR;
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private LayerMask mirrors;
    
    // Start is called before the first frame update
    void Start()
    {
        lR = GetComponent<LineRenderer>();
        lR.SetPosition(0, startPos.position);
    }

    // Update is called once per frame
    void Update()
    {
        CastLaser(transform.position, -transform.up);
        Debug.DrawRay(transform.position, -transform.up * 300, Color.green);
    }

    private void CastLaser(Vector3 pos, Vector3 dir)
    {

        lR.SetPosition(0, startPos.position);

        for (int i = 0; i < maxBounces; i++)
        {
            Ray2D ray = new Ray2D(pos, dir);
            RaycastHit2D hit;
            hit = Physics2D.Raycast(ray.origin, ray.direction, 300, mirrors);
            if (hit.collider != null)
            {
                Debug.Log("Bounce!");
                pos = hit.point;
                dir = Vector2.Reflect(dir, hit.normal);
                lR.SetPosition(i + 1, hit.point);

                if (hit.collider)
                {
                    for (int j = (i + 1); j < 5; j++)
                    {
                        lR.SetPosition(j, hit.point);
                    }
                    break;
                }
            }
            else lR.SetPosition(1, startPos.position + new Vector3(0, -300, 0));
        }
    }
}
