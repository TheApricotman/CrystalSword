using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;
    [SerializeField]
    //private AudioClip songToPlay;
    private AudioSource audioSource;
    [SerializeField]
    private Vector2 playerPos;
    [SerializeField]
    private VectorValue playerStorage;
    [SerializeField]
    private Animator transition;
    [SerializeField]
    private Animator musicTransition;

    private void Start()
    {
        musicTransition = Camera.main.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& !other.isTrigger)
        {
            audioSource = Camera.main.GetComponent<AudioSource>();
            playerStorage.intialValue = playerPos;
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("StartFade");
        musicTransition.SetTrigger("StartFade");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneToLoad);
       
    }
}
