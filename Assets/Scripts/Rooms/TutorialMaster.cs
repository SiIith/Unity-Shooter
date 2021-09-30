using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMaster : MonoBehaviour
{
    public GameObject goal;

    public GameObject tute;

    public static bool win = false;

    void Awake(){
        tute.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
            if (win == false) {
                FindObjectOfType<AudioManager>().Play("Win");
            }
            win = true;
            goal.transform.position = new Vector3(0,4,20);
            tute.SetActive(true);
            
        }
        
    }
}
