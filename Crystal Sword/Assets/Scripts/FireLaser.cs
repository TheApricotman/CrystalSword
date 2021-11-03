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
    [SerializeField]
    private PuzzleStartButton button;
    public bool win;
    public bool lose;


    // Start is called before the first frame update
    void Start()
    {
        lR = GetComponent<LineRenderer>();
        bounce = 5;
    }

    private void Update()
    {
        //for debugging purposes, input activated, but will activate when player interacts with sword in stone in future
        if (button.startPuzzle)
        {
            CastRay(transform.position, -transform.up);
            lR.enabled = true;
           
            StartCoroutine(TurnOffLaser(1));
        }
    }

    IEnumerator TurnOffLaser(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        lR.enabled = false;
    }

    private void CastRay(Vector2 position, Vector2 direction)
    {
        //casts ray from laser emitter and draws a line to where it hits
        RaycastHit2D hit = Physics2D.Raycast(position, direction);
        Debug.DrawRay(position, direction, Color.blue);

        lR.positionCount = 2;

        lR.SetPosition(0, transform.position);
        Vector3 savePos;
        if (hit.collider != null)
        {
            //draws first line
            savePos = hit.point;
            lR.SetPosition(1, savePos);
            for (int i = 0; i < bounce; i++)
            {
                hit = Physics2D.Raycast(position, direction);
                if (hit.collider.name == "Mirror 1")
                {
                    
                    //gets puzzle blocks transforms to get proper positions for next raycast
                    position = hit.transform.Find("ShootPoint 1").position;
                    direction = hit.transform.Find("ShootPoint 1").transform.up;
                    hit = Physics2D.Raycast(position, direction);
                    //creates an array to give puzzle blocks line renderer coordinates
                    Vector3[] positions = {position, hit.point};

                    Debug.DrawRay(position, direction, Color.blue);
                    //calls puzzle blocks linerenderer function and gives positions
                    hit.transform.gameObject.GetComponentInChildren<ReflectLaser>().HitMirror1(positions);
                }
                if (hit.collider.name == "Mirror 2")
                {
                    position = hit.transform.Find("ShootPoint 2").position;
                    direction = hit.transform.Find("ShootPoint 2").transform.up;
                    hit = Physics2D.Raycast(position, direction);
                    Vector3[] positions = {position, hit.point};
                    Debug.DrawRay(position, direction, Color.blue);
                    hit.transform.gameObject.GetComponentInChildren<ReflectLaser>().HitMirror2(positions);
                }
                if (hit.collider.CompareTag("Button"))
                {
                    //Win condition, dungeon opens
                    //no idea why but linerenderer is backwards, each objects render the line back to origin
                    Debug.Log("Win!");
                    Vector3[] positions = { position, hit.point };
                    hit.transform.gameObject.GetComponentInChildren<ReflectLaser>().HitMirror1(positions);
                    win = true;
                    break;
                }
                if (hit.collider.CompareTag("Wall"))
                {
                    lose = true;
                }
                else
                {
                    lose = true;
                }
            }
        }
    }
}