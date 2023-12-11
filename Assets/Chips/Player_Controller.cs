using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//public interface GravityController
//{
//    public abstract void Gravity();
//}

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController cc;
    [SerializeField] float Speed = 10f;
    private float? oldSpeed = null;
    [SerializeField] float Sprint = 15f;
    public float stamina = 10f;
    private float timerStamina = 0f;
    [SerializeField] float TimeStart = 2f;
    private bool isRun = true;
    [SerializeField] float LadderSpeed = 5f;
    [SerializeField] float RotateSpeed = 5f;
    public float JumpHeight = 10f;
    [NonSerialized] public float? oldJumpHeight = null;
    private Vector3 moveDirection;
    [SerializeField] GameObject PlayerBody;

    private bool switchd;
    private bool a = false;
    public float gravity = -9.81f;
    [NonSerialized] public float? staticGravity = null;
    public float umbrellaGravity = -0.5f;

    [NonSerialized] public bool umbrellaIsOpen;
    [NonSerialized] public bool umbrellaOnWind;
    public bool LadderEnter;

    [SerializeField] float DeathHeight;

    Vector3 velosity;

    [SerializeField] GroundChecker ground;
    [SerializeField] Umbrella umbrella;
    public static PlayerController Player_Singltone;

    private void Awake()
    {
        Player_Singltone = this;

    }
    private void Start()
    {
        if (staticGravity == null && oldJumpHeight == null && oldSpeed == null)
        {
            staticGravity = gravity;
            oldJumpHeight = JumpHeight;
            oldSpeed = Speed;

        }
        Time.timeScale = 1;

    }


    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift) && timerStamina <= stamina && isRun)
        {
            Speed = Sprint;
            timerStamina += 1f * Time.deltaTime;
            isRun = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || timerStamina >= stamina)
        {
            isRun = false;
        }
        if(!isRun)
        {
            Speed = (float)oldSpeed;
            if (timerStamina >= 0)
            {
                timerStamina-=1f * Time.deltaTime;
            }
            else if(timerStamina <TimeStart)
            {
                isRun = true;
            }

        }
        ground._IsGround();
        if (!LadderEnter)
        {
            Gravity();
        }
        Controller();
        if (umbrellaIsOpen)
        {
            JumpHeight = umbrella.UmbrellaJumpHeight;
        }
        else
        {
            JumpHeight = (float)oldJumpHeight;
        }
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
        Vector3 horMove = new Vector3(horizontal, 0, 0);
        rotatetion.Normalize();
        Vector3 ladder = new Vector3(0, vertical, 0);
        if (rotatetion != Vector3.zero)
        {
            ////Quaternion rotation = Quaternion.LookRotation(moveDirection);

            ////transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Speed);
            PlayerBody.transform.forward = rotatetion * RotateSpeed * Time.deltaTime;
        }

        if (!LadderEnter)
        {
            cc.Move(moveDirection * Speed * Time.fixedDeltaTime);
            
        }else if (LadderEnter)
        {
            cc.Move(horMove *  Speed * Time.fixedDeltaTime);
            cc.Move(ladder * LadderSpeed * Time.fixedDeltaTime); 
        }
    }
   public  void Gravity()
    {
        bool aa = false;
        if (velosity.y <= DeathHeight)
        {
            aa = true;
            
        }else if (velosity.y >= DeathHeight)
        {
            aa = false;
        }
        if (aa && ground._IsGround())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if (ground._IsGround() && velosity.y < 0 &&!umbrellaIsOpen)
        {
            velosity.y = -2f;
        }
       
        if(umbrellaIsOpen && !ground._IsGround() && !umbrellaOnWind)
        {
            if (!a)
            {
                velosity.y = 0;
                a = true;
                
            }            
            velosity.y += umbrellaGravity * Time.deltaTime;
           
        }

        if(ground._IsGround()||!umbrellaIsOpen)
        {
            a = false;
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
    public float UmbrellaJumpHeight;
   

   public void OpenUmbrella()
    {
        umbrella.SetActive(true);

    }
    public void CloseUmbrella()
    {
        umbrella.SetActive(false);
        
    }

   
}

