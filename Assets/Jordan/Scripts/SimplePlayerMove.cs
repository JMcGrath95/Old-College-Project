using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    
    Vector3 velocity;
    public bool OnGround;
    public int Health;
    public Transform ground;
    public float groundDistance;
    public LayerMask groundMask;
    public int cash;
    public float jumpHeight;
    public float fallspeed;
    public Color debugColor = Color.green;
    // Start is called before the first frame update
    void Start()
    {
        Health = 0;
        cash = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        OnGround = Physics.CheckSphere(ground.position, groundDistance, groundMask);
        Move();
    }
    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        

        Vector3 move = transform.right * -h + transform.forward * v;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += fallspeed * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
}
