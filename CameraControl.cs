using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    public float camZ = 0f;
    public float camY = 2f;
    float rotOnX = 0;

    //private Vector3 position;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + camY, player.transform.position.z + camZ);
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * player.GetComponent<Controller>().rotationSpeed;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * player.GetComponent<Controller>().rotationSpeed;
        rotOnX -= mouseY;
        rotOnX = Mathf.Clamp(rotOnX, -90f, 90f);
        transform.localEulerAngles = new Vector3(rotOnX, player.transform.localEulerAngles.y, 0);

    }
    
}
