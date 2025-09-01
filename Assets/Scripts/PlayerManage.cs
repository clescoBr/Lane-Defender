using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class PlayerManage : MonoBehaviour
{
    [SerializeField] private GameObject tank;

    [SerializeField] private GameObject turet1;
    [SerializeField] private GameObject turet2;
    [SerializeField] private GameObject turet3;
    [SerializeField] private GameObject turet4;

    public PlayerInput playerInput;
    private InputAction move;
    private InputAction slide;
    private InputAction rotate;

    private float moveDirection;
    private float slideDirection;
    private float rotateDirection;


    public int paddleSpeed;
    private bool isPaddleMoving;
    private bool isPaddleSliding;
    private bool isPaddleRotate;





    void Start()
    {
        playerInput.currentActionMap.Enable();  //Enable action map
        move = playerInput.currentActionMap.FindAction("Move");
        slide = playerInput.currentActionMap.FindAction("Slide");
        rotate = playerInput.currentActionMap.FindAction("Rotate");

        move.started += Move_started;
        move.canceled += Move_canceled;

        slide.started += Slide_started;
        slide.canceled += Slide_canceled;

        rotate.started += Rotate_started;

    }

  

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        isPaddleMoving = false;
    }
    private void Move_started(InputAction.CallbackContext obj)
    {
        isPaddleMoving = true;

        //print("Movement started");
    }


    private void Slide_canceled(InputAction.CallbackContext obj)
    {
        isPaddleSliding = false;
    }
    private void Slide_started(InputAction.CallbackContext obj)
    {
        isPaddleSliding = true;

        //print("Movement started");
    }



    private void Rotate_started(InputAction.CallbackContext obj)
    {
        isPaddleRotate = true;
    }


    private void FixedUpdate()
    {
        if (isPaddleMoving)
        {
            tank.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, moveDirection * paddleSpeed * Time.deltaTime);

          //  tank.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(tank.GetComponent<Rigidbody2D>().linearVelocity.x, moveDirection * paddleSpeed * Time.deltaTime);
           // tank.GetComponent<Rigidbody2D>().angularVelocity = (moveDirection * 60);
        }

        if (isPaddleSliding)
        {
            tank.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(slideDirection * paddleSpeed * Time.deltaTime,0);

           // tank.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(slideDirection * paddleSpeed * Time.deltaTime, tank.GetComponent<Rigidbody2D>().linearVelocity.y);
        }
      
        if (!isPaddleMoving && !isPaddleSliding)
        {
          //   tank.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // new Vector2(0, 0);

        }

    }
    void Update()
    {
        if (isPaddleMoving)
        {
            moveDirection = move.ReadValue<float>();
            
        }

        if (isPaddleSliding)
        {
            slideDirection = slide.ReadValue<float>();

        }

        if (isPaddleRotate)
        {
            rotateDirection = rotate.ReadValue<float>();
            isPaddleRotate = false;
            tank.GetComponent<Rigidbody2D>().angularVelocity = (rotateDirection * 120);

        }



    }


}
