using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// similar to the SurvialTimer but this one shows a %/100 progress rather than time
public class KOHTimer : MonoBehaviour
{

    public GameObject portal;
    public Text timeText;
    public Text infoText;
    public static bool spawning = false;
    private bool win;
    private float currCharge = 0f;
    public float maxCharge = 100f;
    public bool charging = false;

    public float chargeRate = 1.0f;


    // tells if this stage is cleared
    private static bool cleared = false;


    // the 2 sets of ifs check 2 conditions seperately: whether bar is charging, and whether minions are spawning
    //      - spawning: begins when player first touches platform, ends when charge is complete
    //      - charging: charges if player is on the platform, reduces if not, and ends when charge is complete
    void Update()
    {
        if (currCharge < maxCharge) infoText.text = "Capture and hold the centre platform to charge portal!";

        // if both prep and survive time runs to 0, stop timer and release the portal
        else if (currCharge >= maxCharge)
        {
            if (win == false) {
                FindObjectOfType<AudioManager>().Play("Win");
            }
            win = true;
            spawning = false;
            infoText.text = "Portal secured.";
            GameObject[] enemies =GameObject.FindGameObjectsWithTag ("Enemy");;
     
                for(var i = 0 ; i < enemies.Length ; i ++)
                {
                    Destroy(enemies[i]);
                }

            portal.transform.position = new Vector3(0,5,22);
        }
    
        DisplayCharge(currCharge);

        // turns on spawning and increment charge bar
        if (charging)
        {
            // start spawning as soon as player touches platform
            spawning = true;
            currCharge += Time.deltaTime * chargeRate;
        }

        // reduces charger until 0
        else if ( !charging && currCharge > 0)  currCharge -= (Time.deltaTime * 0.6f);

        
    }

    void DisplayCharge(float charge)
    {

        if (charge >= maxCharge) charge = maxCharge;
        if (charge <= 0) charge = 0;

        timeText.text = string.Format("{0:0.##}/{1}", charge, maxCharge);

    }

    void OnTriggerEnter(Collider collider){
        if (collider.tag == "Player") charging = true;
        
    }

    void OnTriggerExit(Collider collider){
        if (collider.tag == "Player") charging = false;
    }
        
}
