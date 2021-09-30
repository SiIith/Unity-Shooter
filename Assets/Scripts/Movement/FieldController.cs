using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldController : MonoBehaviour
{

    GameObject player;
    public int damage = 1;

    // lifespan of field
    public float lifespan = 5;

    // time player may stay in field before taking damage
    float timer = 1;

    // damager ticker
    float nextActionTime = 0;
    float damageTick = 1f;

    void Awake(){
        nextActionTime += Time.time + timer + 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if (lifespan <= 0 ) {
            Destroy(this.gameObject);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().moveSpeed = 6;
        }

    }


    // slows player once entering trigger
    void OnTriggerEnter(Collider collider){
        if (collider.gameObject.tag == "Player"){
            player = collider.gameObject;
            player.GetComponent<PlayerMove>().moveSpeed /= 2;
            timer = 2;
        }
    }

    void OnTriggerStay(Collider collider){
        if (collider.tag == "Player"){
            timer -= Time.deltaTime;
            if (timer <= 0){
                if(Time.time > nextActionTime){
                    nextActionTime += damageTick;
                    dealDamage();
                }
            }
        }
    }

    void dealDamage(){
        Debug.Log("tick");
        player.GetComponent<Health>().TakeDamage(damage);
    }

    void OnTriggerExit(Collider collider){
        if (collider.tag == "Player") collider.gameObject.GetComponent<PlayerMove>().moveSpeed *= 2;
    }
}
