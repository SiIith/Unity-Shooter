using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoomMaster : MonoBehaviour
{
    public GameObject portal;
    private bool win;

    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Boss").Length == 0){
            if (win == false) {
                FindObjectOfType<AudioManager>().Play("Win");
            }
            win = true;
            GameObject[] enemies =GameObject.FindGameObjectsWithTag ("Enemy");;
     
                for(var i = 0 ; i < enemies.Length ; i ++)
                {
                    Destroy(enemies[i]);
                }
            portal.transform.position = new Vector3(-5,2,5);
        }
    }
}
