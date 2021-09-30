 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
using UnityEngine.SceneManagement;
 public class PauseMenuManager : MonoBehaviour
 {
     public static bool isPaused = false;
     public GameObject pauseMenuUI;
 

    // hide pause menu at awake
    void Awake(){
        pauseMenuUI.SetActive(false);
    }
     // Update is called once per frame
     void Update()
     {
 
         if (Input.GetKeyDown(KeyCode.Escape))
         {
            
 
             if (isPaused) 
             {
                 Resume();
             }

             else
             { 
                Pause();
             }
         }
     }
 
    public void Resume()
     {
         Cursor.lockState = CursorLockMode.Locked;
         pauseMenuUI.SetActive(false);
         Time.timeScale = 1f;
         isPaused = false;

     }

     // set timeScale to 0 to pause the game
     void Pause()
     {
         Cursor.lockState = CursorLockMode.None;
         pauseMenuUI.SetActive(true);
         Time.timeScale = 0f;
         isPaused = true;
     }


     public void returnToMain(){
         pauseMenuUI.SetActive(false);
         Time.timeScale = 1f;
         isPaused = false;
         SceneManager.LoadScene("MainMenu");
     }
     public void QuitGame()
     {
         Application.Quit();
     }
 }