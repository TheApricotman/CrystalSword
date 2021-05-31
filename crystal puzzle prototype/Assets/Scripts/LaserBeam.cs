using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private Ray laserBeam;
    public GameObject puzzleBlock;
   
   
    // Start is called before the first frame update
    void Start()
    {
        laserBeam = new Ray(transform.position, new Vector3(0,0,180));
        
    }
   

    // Update is called once per frame
    void Update()
    {// raycasts for laser puzzle
        if (Input.GetKey(KeyCode.Space))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.InverseTransformDirection(Vector2.up), 10f);
            Debug.DrawRay(transform.position, transform.InverseTransformDirection(Vector2.up));

            //Checks what raycast hits, if puzzle block, shoots another ray out 90 degrees
            if (hit)
            {
                Debug.Log("Hit Something:" + hit.collider.name);
                if (hit.collider.name == "Bounce 1")
                {
                    Physics2D.Raycast(puzzleBlock.transform.position, puzzleBlock.transform.InverseTransformDirection(Vector2.right), 10f);
                    Debug.DrawRay(puzzleBlock.transform.position, puzzleBlock.transform.InverseTransformDirection(Vector2.right));

                }
                if (hit.collider.name == "Bounce 2")
                {
                    Physics2D.Raycast(puzzleBlock.transform.position, puzzleBlock.transform.InverseTransformDirection(Vector2.left), 10f);
                    Debug.DrawRay(puzzleBlock.transform.position, puzzleBlock.transform.InverseTransformDirection(Vector2.left));
                }
                if (hit.collider.CompareTag("Goal"))
                {
                    Debug.Log("Door Unlocked");
                }
                else Debug.Log("Hit Something:" + hit.collider.name);
            }
        }
    }
}
