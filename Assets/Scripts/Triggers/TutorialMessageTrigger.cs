using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessageTrigger : MonoBehaviour
{
    // tute message to be shown on screen
    public GameObject tute;

    void Awake(){
        tute.SetActive(false);
    }

    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Player"){
            tute.SetActive(true);
        }
    }


    // disable tute if cleared
    void OnTriggerStay(Collider col){
        if (TutorialMaster.win){
            tute.SetActive(false);
        }
    }

    void OnTriggerExit(Collider col){
        if (col.gameObject.tag == "Player"){
            tute.SetActive(false);
        }
    }



}
