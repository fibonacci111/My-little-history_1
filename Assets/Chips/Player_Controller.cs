using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//public interface GravityController
//{
//    public abstract void Gravity();
//}

public class PlayerController :  MonoBehaviour
{
    [SerializeField] CharacterController cc;
    [SerializeField] float Speed = 10f;
    [SerializeField] float RotateSpeed = 5f;
    private Vector3 moveDirection;
    [SerializeField] GameObject PlayerBody;
    public static PlayerController Player_Singltone;
    private bool switchd;

    public float gravity = -9.81f;
    public float staticGravity = -9.81f;
    public float umbrellaGravity = -0.5f;

    [NonSerialized] public bool umbrellaIsOpen;
    [NonSerialized] public bool umbrellaOnWind;

    public float JumpHeight = 10f;
    Vector3 velosity; 
    
    [SerializeField] GroundChecker ground;
    [SerializeField] Umbrella umbrella;

    private void Start()
    {
        Time.timeScale = 1;
        Player_Singltone= this;
    }

    
    void Update()
    {
        ground._IsGround();

        Gravity();
        Controller();


       
        if (Input.GetButtonDown("Jump") && ground._IsGround())
        {
            Jump();   
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!switchd)
            {
                
                umbrella.OpenUmbrella();
                switchd = true;
                umbrellaIsOpen = true;
            }else if (switchd)
            {
                umbrella.CloseUmbrella();
                switchd = false;
                umbrellaIsOpen=false;
            }
        }

    }

    private void Controller()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        moveDirection = (vertical * transform.forward + horizontal * transform.right).normalized;
        Vector3 rotatetion = new Vector3(horizontal, 0, vertical);
        rotatetion.Normalize();
        if (rotatetion != Vector3.zero)
        {
            ////Quaternion rotation = Quaternion.LookRotation(moveDirection);

            ////transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Speed);
            PlayerBody.transform.forward = rotatetion*RotateSpeed*Time.deltaTime;
        }


        cc.Move(moveDirection * Speed * Time.fixedDeltaTime);
    }
   public  void Gravity()
    {
       
        if (ground._IsGround() && velosity.y < 0 &&!umbrellaIsOpen)
        {
            velosity.y = -2f;
        }
       
        if(umbrellaIsOpen && !ground._IsGround() && !umbrellaOnWind)
        {
            velosity.y += umbrellaGravity * Time.deltaTime;
        }
        
        if (umbrellaOnWind&& umbrellaIsOpen)
        {
            velosity.y += gravity * Time.deltaTime;
        }

        if (!umbrellaIsOpen)
        {
            velosity.y += gravity * Time.deltaTime;
        }
        cc.Move(velosity * Time.deltaTime);
    }
    public void Jump()
    {
        if (ground._IsGround())
        {
            velosity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
            
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

[Serializable]
public class Umbrella
{
    [SerializeField] GameObject umbrella;
    
   

   public void OpenUmbrella()
    {
        umbrella.SetActive(true);

    }
    public void CloseUmbrella()
    {
        umbrella.SetActive(false);
        
    }

   
}

