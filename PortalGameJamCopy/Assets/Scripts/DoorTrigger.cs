using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private string sceneToGoTo;
    [SerializeField] Animator animator;
    private CharacterScript characterScript;
    private bool playerInRange = false;
    [SerializeField] private AudioClip doorSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            characterScript = other.GetComponent<CharacterScript>();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.W) || playerInRange && Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetBool("playerInteract", true);
            //play sound
            audioSource.PlayOneShot(doorSound);
            //disable player movement
            characterScript.rb.velocity = new Vector2(0,0);
            characterScript.enabled = false;

            //wait x amount of time, scene transition
            StartCoroutine(DelayedSceneLoad());
        }
    }


    private IEnumerator DelayedSceneLoad()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneToGoTo);
    }
}
