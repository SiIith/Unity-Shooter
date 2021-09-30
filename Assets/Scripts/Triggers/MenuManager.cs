using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    
    public void Tutorial()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Tutorial");
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
     {
         Application.Quit();
     }
}
