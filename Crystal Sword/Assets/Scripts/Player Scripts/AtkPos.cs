using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkPos : MonoBehaviour
{
    public Animator anim;
    private Vector2 atkPos;

    private void Update()
    {
        //takes animator directions to move attack position circle
        atkPos.x = anim.GetFloat("Horizontal");
        atkPos.y = anim.GetFloat("Vertical");
        transform.localPosition = atkPos;
    }


}
