  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    //public ProjectileControl projectilePrefab;
    // Start is called before the first frame update

    public Animator animator;
    private CharacterController controller;
    public Transform player;
    public float moveSpeed = 6.0f;
    private float jumpHeight = 3.0f;
    private float gravityValue = -18f;
    private Vector3 playerVelocity;
    private Collision onCollision;
    private Vector3 movement = Vector3.zero;
    private Vector3 moveForward;
    private Vector3 moveBack;
    private Vector3 moveLeft;
    private Vector3 moveRight;

    private Vector3 initPos = new Vector3(-12.0f, -9.5f, -50.0f);


    public float mouseSensitivity = 100f;
    public float xRotation = 0;
    public float yRotation = 0;

    bool isPaused = false;

    void Awake(){
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        resetPos(initPos);
        
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    { 
        isPaused = PauseMenuManager.isPaused;
        //shootProjectile();
        GetMovement();
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    private void GetMovement()
    {
        Vector3 camF = transform.forward;
        camF.y = 0;
        
        // reset the value of each direction
        moveForward = Vector3.zero;
        moveBack = Vector3.zero;
        moveLeft = Vector3.zero;
        moveRight = Vector3.zero;

        float mouseX = Input.GetAxis("Mouse X")*mouseSensitivity*Time.deltaTime;
        yRotation += mouseX;
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        // key input 
        if (Input.GetKey(KeyCode.W)) {
            moveForward = player.forward;
        } else if (Input.GetKey(KeyCode.S)) {
            // slower when walking back
            moveBack = -player.forward * 0.6f;
        }
            
        if (Input.GetKey(KeyCode.A))
            moveLeft = -player.right;

        if (Input.GetKey(KeyCode.D))
            moveRight = player.right;

        if (movement.magnitude == 0) {
            animator.SetFloat("move", 0f);
        }

        movement = moveForward + moveBack + moveLeft + moveRight;


        // animotion of runing;  
        if (moveBack != Vector3.zero && !isPaused)
        {
            FindObjectOfType<AudioManager>().Play("Footstep");
            animator.SetFloat("move", 1f);
        } else if (movement != Vector3.zero && !isPaused) {
            FindObjectOfType<AudioManager>().Play("Footstep");
            animator.SetFloat("move", 0.2f);
        }

        // test if player is on the ground; 
        bool groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Changes the height position of the player..
        if (Input.GetKey(KeyCode.Space) && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        if (playerVelocity != null) {
            if (playerVelocity.y > 0 && playerVelocity.magnitude > 3) {
                animator.SetFloat("move", 1.8f);
            }
        }

        if (onCollision != null) {
            print(onCollision.GetContact(0).normal);
            playerVelocity.x += -200.0f * onCollision.GetContact(0).normal.x;
            playerVelocity.z += -200.0f * onCollision.GetContact(0).normal.z;
        } else {
            playerVelocity.x = 0;
            playerVelocity.z = 0;
        }
        onCollision = null;

        if (!groundedPlayer) {
            playerVelocity.y += gravityValue * Time.deltaTime;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void resetPos(Vector3 pos){
        this.transform.localPosition = pos;
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.collider.tag == "Enemy"){
            onCollision = collision;
        } else {
            onCollision = null;
        }
    }

}