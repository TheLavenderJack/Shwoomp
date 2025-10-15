using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    public int lvlNum;
    [SerializeField] private GameObject player;
    public GameObject spawnPoint;
    public int gravOnStart;
    private bool isPaused = false;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject musicManager;




    // Start is called before the first frame update
    void Start()
    {
        if (lvlNum == 1)
        {
            MusicManager.instance.StartMusic();
            Time.timeScale = 1;
        }
        if (gravOnStart == -1)
        {
            player.GetComponent<CharacterScript>().FlipGravity();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        //Pause Menu Logic
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);

            // Optionally pause the game
            Time.timeScale = isPaused ? 0 : 1;
        }
    }


    public void Restart()
    {
        Rigidbody2D playerRB = player.GetComponent<CharacterScript>().rb;
        playerRB.velocity = new Vector2(0, 0);

        player.transform.position = spawnPoint.transform.position;
        if (playerRB.gravityScale < 0)
        {
            if (gravOnStart == 1)
            {
                player.GetComponent<CharacterScript>().FlipGravity();
            }
        }
        if (playerRB.gravityScale > 0)
        {
            if (gravOnStart == -1)
            {
                player.GetComponent<CharacterScript>().FlipGravity();
            }
        }
    }

    public void QuitToMenu()
    {
        //stop the music
        MusicManager.instance.StopMusic();
        SceneManager.LoadScene("MenuScreen");
        isPaused = false;
        Time.timeScale = isPaused ? 0 : 1;
    }
}
