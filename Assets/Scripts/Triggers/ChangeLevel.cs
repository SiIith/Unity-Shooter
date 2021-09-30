using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{

    public Vector3 teleportTo;
    public string roomNext;
    // Start is called before the first frame update
 void OnTriggerEnter(Collider contact)
 {
    if (contact.gameObject.tag == "Player") {
        FindObjectOfType<AudioManager>().Play("Teleport");
        contact.GetComponent<PlayerMove>().resetPos(teleportTo);
        RoomSwitcher.LoadScene(roomNext, contact.gameObject);
    }
 }
}
