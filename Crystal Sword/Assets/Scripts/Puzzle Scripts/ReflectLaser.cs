using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectLaser : MonoBehaviour
{
    private LineRenderer lR;

    private void Start()
    {
        lR = GetComponent<LineRenderer>();       
        lR.positionCount = 2;
    }

    public void HitMirror1(Vector3[] positions)
    {
        //Sets puzzle blocks Line renderer to shoot out correct end
        lR.SetPositions(positions);
        Debug.DrawLine(positions[0], positions[1], Color.green);
        
        StartCoroutine(TurnOffLaser(1));

        Debug.Log("Im reflecting!");
    }
    public void HitMirror2(Vector3[] positions)
    {
        lR.SetPositions(positions);
        Debug.DrawLine(positions[0], positions[1], Color.green);
      
        StartCoroutine(TurnOffLaser(1));
    }
    IEnumerator TurnOffLaser(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        lR.enabled = false;
    }
}
