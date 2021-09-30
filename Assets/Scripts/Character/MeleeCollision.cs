using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollision : MonoBehaviour
{
    
    private int damage = 1;

    private void OnCollisionEnter(Collision collision){
        if(collision.collider.tag == "Player"){
            var player = GameObject.Find("Player");
            var health = player.GetComponent<Health>();
            if (health != null)
            {
                FindObjectOfType<AudioManager>().Play("MeleeHitSound");
                health.TakeDamage(damage);
            }
        }
    }
}
