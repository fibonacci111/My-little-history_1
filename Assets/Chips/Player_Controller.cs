using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController cc;
    [SerializeField] public float Speed = 10f;
    [SerializeField] public float RotateSpeed = 5f;
    private Vector3 moveDirection;

    public static PlayerController Player_Singltone;
  
    [SerializeField] GroundChecker ground;
    
    public float gravity = -9.81f;
  
  public float JumpHeight = 10f;
  Vector3 velosity;
    private void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        ground._IsGround();
        Controller();
        Gravity();
       
        if (Input.GetButtonDown("Jump") && ground._IsGround())
        {
            Jump();   
        }
        

    }

    private void Controller()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        moveDirection = (vertical * transform.forward + horizontal * transform.right).normalized;

      

        cc.Move(moveDirection * Speed * Time.deltaTime);
    }
   public void Gravity()
    {
       
        if (ground._IsGround() && velosity.y < 0)
        {
            velosity.y = -2f;
        }
        velosity.y += gravity * Time.deltaTime;
        cc.Move(velosity * Time.deltaTime);
    }
    public void Jump()
    {
        if (ground._IsGround())
        {
            velosity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
            Debug.Log("Jump");
        }
    }
}





[Serializable]
public class GroundChecker
{
    public Transform GroundCheck;
    public float GroundDistanse = 0.4f;
    public LayerMask Ground;
    [NonSerialized] public static bool isGround;
    public bool _IsGround()=> isGround = Physics.CheckSphere(GroundCheck.position, GroundDistanse, Ground);
}