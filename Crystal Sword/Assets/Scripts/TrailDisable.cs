using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDisable : MonoBehaviour
{
    //Simple timer for dash trails
    private float timer;
    [SerializeField]
    private float startTimer;

    private void OnEnable()
    {
        timer = startTimer;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
