// took inspirations from https://www.youtube.com/watch?v=KcyO1biSIOw
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour{

    // Start is called before the first frame update
    static bool isShuttingDown = false;


    // tells if this enemy needs navigation. Not actually needed for gameplay but can remove annoying warnings from console
    public bool isNav;
    private Transform playerPosition;

    private GameObject player;
    private NavMeshAgent nav;
    public float speed = 1.0f;

    public float trackingRadius;

    public Vector3 velocity;
    private Rigidbody rd;
    private Vector3 NextPosition;
    private Vector3 originalPosition;
    public GameObject newBuff;

    public GameObject projectile;
    private float nextActionTime = 0.0f;
    public float period = 2.0f;

    public float dropRate = 0.7f;
    

    void Awake(){
        nextActionTime += Time.time;
    }

    void Start(){
        originalPosition = this.transform.position;

        nav = GetComponent<NavMeshAgent>();
    }
    void Update(){
        player = GameObject.Find("Player");{
            if(player != null){
                playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        if(player != null && isNav && Vector3.Distance(transform.position, playerPosition.position) <= trackingRadius){  
            nav.SetDestination(playerPosition.transform.position); 
        }
        if (Time.fixedTime > nextActionTime && playerPosition != null) {
            nextActionTime += period;
            if(Vector3.Distance(transform.position, playerPosition.position) <= trackingRadius) fireProj();
        }
    }
    void OnApplicationQuit(){
        isShuttingDown = true;
    // to prevent buff drops from messing up the editor
    }

    void fireProj(){
        if (projectile != null){
            GameObject proj = Instantiate (projectile, transform.position, Quaternion.identity);
            proj.GetComponent<ProjectileControl>().velocity 
                = (this.playerPosition.transform.position - this.transform.position).normalized * 5.0f;
        }

    }

    void OnDestroy()
    {
        float hasBuff = Random.Range(0, 20);
        //Debug.Log("hasBuff: "+ hasBuff);
        if(hasBuff <= 20 * dropRate)
        {
            Vector3 nextPosition = this.transform.position;
            if(!isShuttingDown && player != null){
                if(nextPosition.y > 5.0f){
                    GameObject Ground = GameObject.Find("Ground");
                    nextPosition = new Vector3(nextPosition.x, Ground.transform.position.y+3.0f, nextPosition.z);
                }
                Instantiate(newBuff, nextPosition, Quaternion.identity);
            }
        }      
    }
}
