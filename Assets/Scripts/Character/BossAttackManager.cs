using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackManager : MonoBehaviour
{
    public GameObject tracker;
    
    private GameObject player;

    public GameObject projectile;
    public GameObject slowField;

    private float speed = 15f;


    int spawning = 20;
    int numberOfProjectiles = 128;


    private float nextActionTime = 0.0f;
    public float firePeriod = 2;

    private float nextFieldTime = 0;
    public float fieldPeriod = 6;


    void Awake(){
        nextActionTime += Time.time;
        nextFieldTime += Time.time + 5;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
                        // self roration
        transform.Rotate(Time.deltaTime * speed, 0, 0, Space.Self);
        transform.Rotate(0, Time.deltaTime * speed,  0, Space.Self);
        transform.Rotate(0, 0, Time.deltaTime * speed, Space.Self);
        
        {
            if(player != null){
                // randomly spreads projectiles in a circle
                if (Time.time > nextActionTime ) {
                    nextActionTime += firePeriod;
                    spreadProjectiles();
                    
                }

                // spawns a damage field around player
                if (Time.time > nextFieldTime){
                    nextFieldTime += fieldPeriod;
                    GameObject field = Instantiate (slowField,GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
                    field.transform.position += new Vector3(0,2,0);
                }

                // phase control of boss
                if (this.gameObject.transform.localScale.x == 3 && this.gameObject.GetComponent<Health>().getCurrentHealth() <= 100){
                    this.gameObject.transform.localScale = new Vector3(2.5f,2.5f,2.5f);
                    spawnMinions();
                    speed += 50f;
                    numberOfProjectiles += 24;
                    firePeriod -= 0.25f;
                }

                if (this.gameObject.transform.localScale.x == 2.5 && this.gameObject.GetComponent<Health>().getCurrentHealth()<= 75){
                    this.gameObject.transform.localScale = new Vector3(2,2,2);
                    spawnMinions();
                    speed += 50f;
                    numberOfProjectiles += 24;
                    firePeriod -= 0.25f;
                }

                if (this.gameObject.transform.localScale.x == 2 && this.gameObject.GetComponent<Health>().getCurrentHealth() <= 25){
                    this.gameObject.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
                    spawnMinions();
                    numberOfProjectiles += 24;
                    speed += 50f;
                    firePeriod -= 0.25f;
                }
            }
        }
    }


    public void spawnMinions(){
        for (int i = 0; i <= spawning; i++){
            GameObject enemy = GameObject.Instantiate<GameObject>(tracker);
            enemy.transform.localPosition = new Vector3(Random.Range(-15, 15), 10, Random.Range(-15,15));
        }
    }


// this piece is modified from https://www.youtube.com/watch?v=NivKaNN7I00
    void spreadProjectiles(){
		float angleStep = 360f / numberOfProjectiles;
		float angle = 0f;
        float radius = 10;
        float moveSpeed = 2f;


        Vector2 origin = this.gameObject.transform.position;
        origin.y = Random.Range(-1,1);

		for (int i = 0; i < numberOfProjectiles; i++) {

			float projectileDirXposition = origin.x + Mathf.Sin ((angle * Mathf.PI) / 180) * radius;
			float projectileDirYposition = origin.y + Mathf.Cos ((angle * Mathf.PI) / 180) * radius;

			Vector2 projectileVector = new Vector2 (projectileDirXposition, projectileDirYposition);
			Vector2 projectileMoveDirection = (projectileVector - origin).normalized * moveSpeed;

            GameObject proj = Instantiate (projectile, origin, Quaternion.identity);
            proj.GetComponent<ProjectileControl>().velocity = new Vector3 (projectileMoveDirection.x,Random.Range(-1,2), projectileMoveDirection.y);


			angle += angleStep;

        }
    }
}
