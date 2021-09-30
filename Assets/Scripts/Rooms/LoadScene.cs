using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public string roomNext;

    // Update is called once per frame
 void OnTriggerEnter(Collider contact)
 {
    if (contact.gameObject.tag == "Player") {
        SceneManager.LoadScene(roomNext);
        Cursor.lockState = CursorLockMode.None;
    }
 }
}
