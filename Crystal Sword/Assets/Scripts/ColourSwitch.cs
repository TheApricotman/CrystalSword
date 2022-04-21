using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourSwitch : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().material.SetColor("_Color", HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * speed, 1), 0.5f, 1)));
    }
}
