using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;
    [SerializeField]
    private Vector2 playerPos;
    [SerializeField]
    private VectorValue playerStorage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& !other.isTrigger)
        {
            playerStorage.intialValue = playerPos;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
