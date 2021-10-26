using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectLaser : MonoBehaviour
{
    private LineRenderer lR;

    public Transform shootPoint1;
    public Transform shootPoint2;
    private void Start()
    {
        lR = GetComponent<LineRenderer>();
        lR.positionCount = 2;
    }

    public void HitMirror1(Vector3 position)
    {
        lR.SetPosition(0, shootPoint1.position);
        lR.SetPosition(1, position);
        Debug.Log("Im reflecting!");
    }
    public void HitMirror2(Vector3 position)
    {
        lR.SetPosition(0, shootPoint2.position);
        lR.SetPosition(1, position);
    }
}
