using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject _pauseMenu;
    public static bool _isPaused;
    // Start is called before the first frame update
    void Start()
    {
        _pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

        }
        
    }

    public void PauseGame()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }

    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    public void goToMainMenu()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        SceneManager.LoadScene(0);
    }
}
