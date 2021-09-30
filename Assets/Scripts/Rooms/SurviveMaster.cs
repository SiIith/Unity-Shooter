using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// oversees actions in the survival stage
// see Timer.cs for other related behaviours
public class SurviveMaster : MonoBehaviour
{
    public GameObject meleeTracker;
    public GameObject rangeTracker;

    public int spawn = 5;
    private Vector3 loc;

    private float nextActionTime = 0;
    public float interval = 40.0f;

    bool spawning = false;

    void Awake(){
        nextActionTime += Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        // spawning manager
        spawning = this.gameObject.GetComponent<SurvivalTimer>().spawning;
        if (Time.time > nextActionTime && spawning) {
             nextActionTime += interval;
             spawnMeele();
             spawnRange();
        }
        
    }



    void spawnMeele(){
        for(int i = 0; i < spawn; i++){

            do{
                loc = new Vector3(Random.Range(-15,15), 2, Random.Range(-15,15));
            } while (Vector3.Distance(loc, GameObject.FindGameObjectWithTag("Player").transform.position) <= 10);

            GameObject melee = Instantiate(meleeTracker, loc, Quaternion.identity);
        }
    }

    // spawns 5 less range minions in the air
    void spawnRange(){
        for (int i = 0; i < 2; i++){
            loc = new Vector3(Random.Range(-15,15), 8, Random.Range(-15,15));
            GameObject melee = Instantiate(rangeTracker, loc, Quaternion.identity);
        }
    }
}
