using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitter2 : MonoBehaviour
{
    public LayerMask mask;
    //this game object's Transform
    [SerializeField]
    private GameObject shootPoint;
    private Transform goTransform;
    //the attached line renderer
    private LineRenderer lineRenderer;
    private Ray ray;
    //the number of reflections
    public int nReflections = 5;
    //max length
    public float maxLength = 500f;
    //the number of points at the line renderer
    public bool win;
    [SerializeField]
    private GameObject startLaserHit;
    [SerializeField]
    private GameObject endLaserHit;
    private bool once = false;
    [SerializeField]
    private GameObject laserHitAnim;

    void Awake()
    {
        //get the attached Transform component  
        goTransform = shootPoint.transform;
        //get the attached LineRenderer component  
        lineRenderer = GetComponent<LineRenderer>();
        win = false;
    }

    public void StartPuzzle()
    {
        startLaserHit.SetActive(true);
        //clamp the number of reflections between 1 and int capacity  
        nReflections = Mathf.Clamp(nReflections, 1, nReflections);
        ray = new Ray(goTransform.position, -goTransform.up);
        //start with just the origin
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, goTransform.position);
        float remainingLength = maxLength;
        //bounce up to n times
        for (int i = 0; i < nReflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, remainingLength, mask.value);
            Debug.DrawRay(ray.origin, ray.direction);
            Debug.Log("I've bounced " + i + "times");
            // ray cast
            if (hit)
            {
                //gets puzzle box linerenderer to continue visual reflections
                LineRenderer reflecter = hit.collider.GetComponentInParent<LineRenderer>();
                Animator anim = hit.collider.GetComponentInParent<Animator>();
                if (i <= 0)
                {
                    lineRenderer.positionCount += 1;
                    lineRenderer.SetPosition(1, hit.point);
                }
                // update remaining length and set up ray for next loop
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                if (hit.collider.name == "Mirror 1")
                {
                    anim.enabled = true;
                    ray = new Ray(hit.transform.Find("ShootPoint 1").position, hit.transform.Find("ShootPoint 1").transform.up);
                    Debug.DrawRay(hit.transform.Find("ShootPoint 1").position, hit.transform.Find("ShootPoint 1").transform.up, Color.blue);
                    RaycastHit2D tempHit = Physics2D.Raycast(ray.origin, ray.direction, remainingLength, mask.value);
                    
                    reflecter.SetPosition(0, hit.transform.Find("ShootPoint 1").position);
                    reflecter.SetPosition(1, tempHit.point);
                }
                if (hit.collider.name == "Mirror 2")
                {
                    anim.enabled = true;
                    ray = new Ray(hit.transform.Find("ShootPoint 2").position, hit.transform.Find("ShootPoint 2").transform.up);
                    Debug.DrawRay(hit.transform.Find("ShootPoint 2").position, hit.transform.Find("ShootPoint 2").transform.up, Color.green);
                    RaycastHit2D tempHit = Physics2D.Raycast(ray.origin, ray.direction, remainingLength, mask.value);

                    reflecter.SetPosition(0, hit.transform.Find("ShootPoint 2").position);
                    reflecter.SetPosition(1, tempHit.point);
                }
                if (hit.collider.name == "Sword button")
                {
                    endLaserHit.SetActive(true);
                    Debug.Log("You Win with " + i + " reflections");
                    win = true;
                    break;
                }

                // break loop if we don't hit a Mirror
                if (hit.collider.tag != "Mirror" && !win)
                {
                    if (!once)
                    {
                        Instantiate(laserHitAnim, hit.point, Quaternion.identity);
                        once = true;
                    }
                    Debug.Log("You Lose");
                    
                    break;
                }
            }

            else
            {
                // We didn't hit anything, draw line to end of remainingLength
                if (i <= 0)
                {
                    lineRenderer.positionCount += 1;
                    lineRenderer.SetPosition(1, ray.origin + ray.direction * remainingLength);
                }
                break;
            }
        }

    }

}


