using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour
{
    public Vector3 velocity;

    public int damageAmount = 1;
    public float speed = 8f;
    public float LifeDuration = 2f;

    private float LifeTimer;

    void Start()
    {
        LifeTimer = LifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * speed * Time.deltaTime;
        LifeTimer -= Time.deltaTime;
        if (LifeTimer <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            // Damage object with relevant tag
            FindObjectOfType<AudioManager>().Play("RemoteHitSound");
            Health healthControl = col.gameObject.GetComponent<Health>();
            healthControl.TakeDamage(damageAmount);

        }
        else if(col.gameObject.tag =="Obstaclel") Destroy(this.gameObject);
    }
}

