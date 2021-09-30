using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falloff : MonoBehaviour
{
    // Start is called before the first frame update
private void OnTriggerEnter(Collider other){
    if(other.gameObject.tag == "Player"){
        other.gameObject.transform.GetComponent<PlayerMove>().resetPos(new Vector3(-5,2,-15));
        other.gameObject.GetComponent<Health>().TakeDamage(1);
    
    }
}
}
