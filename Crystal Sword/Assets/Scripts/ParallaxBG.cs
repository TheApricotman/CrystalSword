using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    //old script from square adventure, for the background of ocean level, effect multiplier is changed in inspector
    private Transform camTransform;
    private Vector3 lastCameraPos;
    [SerializeField] Vector2 parallaxEffectMulti;
    //private float textureUnitSizeX;

    private void Start()
    {
        camTransform = Camera.main.transform;
        lastCameraPos = camTransform.position;
        //Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        //Texture2D texture = sprite.texture;
        //textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }
    private void LateUpdate()
    {
        Vector3 deltaMovement = camTransform.position - lastCameraPos;
        transform.position += new Vector3 (deltaMovement.x * parallaxEffectMulti.x, deltaMovement.y * parallaxEffectMulti.y);
        lastCameraPos = camTransform.position;

       
        /*if(Mathf.Abs(camTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPosX = (camTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(camTransform.position.x + offsetPosX, transform.position.y);
        }*/
    }
}
