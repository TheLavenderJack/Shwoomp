using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenBTM : MonoBehaviour
{

    [SerializeField] private LevelController levelController;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            levelController.QuitToMenu();
        }
    }
}
