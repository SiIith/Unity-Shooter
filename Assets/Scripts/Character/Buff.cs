using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Buff : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Object;
    private GameObject player;
    private float buffSeed;

    public float redSpawn = 0.2f;
    public float yellowSpawn = 0.2f;

    float radian = 0; // 弧度
	float perRadian = 0.03f; // 每次变化的弧度
	float radius = 0.3f; // 半径
	Vector3 oldPos; // 开始时候的坐标

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Tutorial"){
            redSpawn = 0.5f;
            yellowSpawn = 0.5f;
        }
        
        oldPos = transform.position;
        player = GameObject.Find("Player");
        buffSeed = Random.Range(0, 10);
        //Debug.Log("seed: "+buffSeed);
        randomCreateBuff(buffSeed);
    }

    void Update () {
        player = GameObject.Find("Player");{
            if(player == null){
                Destroy(this.gameObject);
            }
        }

		radian += perRadian; // 弧度每次加0.03
		float dy = Mathf.Cos(radian) * radius; // dy定义的是针对y轴的变量，也可以使用sin，找到一个适合的值就可以
		transform.position = oldPos + new Vector3 (0, dy, 0);   
	}
    
    void OnTriggerEnter(Collider contact)
    {
        if (contact.gameObject.tag == "Player") {
            applyBuff(buffSeed);
            Destroy(this.gameObject);
        }
    }

    void randomCreateBuff(float buffSeed)
    {
        // Create blood buff 
        if(buffSeed < 10 * redSpawn){                 
            Object.GetComponent<Renderer>().material.color = Color.red;
        }
        // Create damage buff     
        else if(buffSeed >= 10 * redSpawn && buffSeed < 10 * (yellowSpawn + redSpawn)){                 
            Object.GetComponent<Renderer>().material.color = Color.yellow;
        }
        // Creat rate of fire buff
        else{
            Object.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    void applyBuff(float buffSeed)
    {
        var playerAttack = player.GetComponent<PlayerAttack>();
        var playerHealth = player.GetComponent<Health>();

        // Create blood buff 
        if(buffSeed < 10 * redSpawn){
            playerAttack.Increasdamage();  
        }
        // Create damage buff    
        else if(buffSeed >= 10 * redSpawn && buffSeed <= 10 * (yellowSpawn + redSpawn)){           
            playerAttack.IncreaseFireRate();
        }
        else{
            playerHealth.AddBlood(1);
        }
    }
}
