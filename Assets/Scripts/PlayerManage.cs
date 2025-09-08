using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManage : MonoBehaviour
{
    [SerializeField] private GameObject tank;

    [SerializeField] private GameObject turet1;
    [SerializeField] private GameObject turet2;
    [SerializeField] private GameObject turet3;
    [SerializeField] private GameObject turet4;

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject eraser;


    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text maxScoreText;






    public PlayerInput playerInput;
    private InputAction move;
    private InputAction slide;
    private InputAction rotate;
    private InputAction space;

    private float moveDirection;
    private float slideDirection;
    private float rotateDirection;
    private float ammo;

    public int paddleSpeed;
    private bool isPaddleMoving;
    private bool isPaddleSliding;
    private bool isPaddleRotate;
    private bool stillShooting;

    public int lives;
    public int score;
    private int maxScore;

    private GameObject cloneStorage;
    void Start()
    {
      //  SceneManager.LoadScene("SceneName");


        playerInput.currentActionMap.Enable();  //Enable action map
        move = playerInput.currentActionMap.FindAction("Move");
        slide = playerInput.currentActionMap.FindAction("Slide");
        rotate = playerInput.currentActionMap.FindAction("Rotate");
        space = playerInput.currentActionMap.FindAction("Atack");

        move.started += Move_started;
        move.canceled += Move_canceled;
        slide.started += Slide_started;
        slide.canceled += Slide_canceled;
        rotate.started += Rotate_started;
        space.started += Atack_started;
        space.canceled += Atack_stoped;
        ammo = 5;
        maxScore = 360;
        StartCoroutine(reloadGun());
        livesText.text = ("Lives: " + lives);
        ammoText.text = ("Ammo: " + ammo + "/5");
        maxScoreText.text = ("High Score: " + maxScore);
        scoreText.text = ("Score: " + score);


        tank.GetComponent<Rigidbody2D>().angularVelocity = ( -120);
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

    private void Atack_started(InputAction.CallbackContext obj)
    {
        stillShooting = true;
        Shoot();
        
    }

    private void Atack_stoped(InputAction.CallbackContext obj)
    { 
        stillShooting = false;
    }

    private void Shoot()
    {
        if (ammo > 0)
        {
            cloneStorage = Instantiate(bullet, turet2.transform.position, gameObject.transform.rotation);
            cloneStorage.GetComponent<Rigidbody2D>().linearVelocity = cloneStorage.transform.up * 5;
            cloneStorage.name = "Ammo";

            cloneStorage = Instantiate(explosion, turet2.transform.position, gameObject.transform.rotation);
            Destroy(cloneStorage, 0.1f);



            cloneStorage = Instantiate(bullet, turet4.transform.position, gameObject.transform.rotation);
            cloneStorage.GetComponent<Rigidbody2D>().linearVelocity = cloneStorage.transform.up * -5;
            cloneStorage.transform.Rotate(new Vector3(0, 0, 180));
            cloneStorage.name = "Ammo";

            cloneStorage = Instantiate(explosion, turet4.transform.position, gameObject.transform.rotation);
            Destroy(cloneStorage, 0.1f);



            cloneStorage = Instantiate(bullet, turet3.transform.position, gameObject.transform.rotation);
            cloneStorage.GetComponent<Rigidbody2D>().linearVelocity = cloneStorage.transform.right * -5;
            cloneStorage.transform.Rotate(new Vector3(0, 0, 90));
            cloneStorage.name = "Ammo";

            cloneStorage = Instantiate(explosion, turet3.transform.position, gameObject.transform.rotation);
            Destroy(cloneStorage, 0.1f);



            cloneStorage = Instantiate(bullet, turet1.transform.position, gameObject.transform.rotation);
            cloneStorage.GetComponent<Rigidbody2D>().linearVelocity = cloneStorage.transform.right * 5;
            cloneStorage.transform.Rotate(new Vector3(0, 0, -90));
            cloneStorage.name = "Ammo";

            cloneStorage = Instantiate(explosion, turet1.transform.position, gameObject.transform.rotation);
            Destroy(cloneStorage, 0.1f);

            ammo--;
            ammoText.text = ("Ammo: " + ammo + "/5");
            StartCoroutine(continueShooting());
            
        }
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
    private IEnumerator continueShooting()
    {
        yield return new WaitForSeconds(0.3f);
        print(stillShooting);
        if (stillShooting)
        {
            Shoot();
        }
    }

    private IEnumerator reloadGun()
    {
        yield return new WaitForSeconds(1);

        if (ammo < 5)
        {
            ammo++;
            ammoText.text = ("Ammo: " + ammo + "/5");
        }
        loopReload();
    }

    private void loopReload()
    {
        StartCoroutine(reloadGun());

    }
    public void playerDmg()
    {
        lives--;
        gameObject.GetComponent<AudioSource>().Play();

        livesText.text = ("Lives: " + lives);
        if (lives < 1)
        {
            print("Dead");
           resetGame();
        }
    }

    public void updateScore()
    {
        scoreText.text = ("Score: " + score);

        if (score > maxScore)
        {
            maxScore = score;
            maxScoreText.text = ("High Score: "+ maxScore);
        }
    }

    private void resetGame()
    {
        lives = 3;
        livesText.text = ("Lives: " + lives);

        score = 0;
        updateScore();

        tank.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        tank.GetComponent<Rigidbody2D>().angularVelocity = (- 120);
        gameObject.transform.position = new Vector3(-9, -2);
        eraser.SetActive(true);
        StartCoroutine(destroyEraser());
    }
    private IEnumerator destroyEraser()
    {
        yield return new WaitForSeconds(0.3f);
        eraser.SetActive(false);

    }
}
