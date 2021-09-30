using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalTimer : MonoBehaviour
{
    public GameObject portal;
    public bool spawning = false;

    public float prepRemaining = 10;
    public float surviveRemaining = 90f;
    public bool timerIsRunning = false;
    public Text timeText;
    public Text infoText;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            // display prep time
            if(prepRemaining > 0)
            {
                infoText.text = "Prepare for enemies:";
                prepRemaining -= Time.deltaTime;
                DisplayTime(prepRemaining);
            }

            // display surviving time after prep
            else if (surviveRemaining > 0 && prepRemaining <= 0)
            {
                infoText.text = "Survive the remaining:";
                spawning = true;
                surviveRemaining -= Time.deltaTime;
                DisplayTime(surviveRemaining);
            }

            // if both prep and survive time runs to 0, stop timer and release the portal
            else if (surviveRemaining <= 0 && prepRemaining <= 0)
            {
                if (spawning == true) {
                    FindObjectOfType<AudioManager>().Play("Win");
                }

                GameObject[] enemies =GameObject.FindGameObjectsWithTag ("Enemy");;
     
                for(var i = 0 ; i < enemies.Length ; i ++)
                {
                    Destroy(enemies[i]);
                }

                spawning = false;
                infoText.text = "You've survived! Enter portal for next stage...";
                surviveRemaining = 0;
                timerIsRunning = false;

                portal.transform.position = new Vector3(0,5,22);


            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
