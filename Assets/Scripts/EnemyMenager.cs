using UnityEngine;
using System.Collections;


public class EnemyMenager : MonoBehaviour
{
    public bool snake;

    [SerializeField] private GameObject explode;

    public int hp;
    private GameObject cloneStorage;
    public float speed;
    [SerializeField] private GameObject redCoat;
    private GameObject player;

    void Start()
    {

        gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(speed, 0);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ammo")
        {
            if (hp > 1)
            {
                hp--;

            }
            else
            {
                gameObject.GetComponent<AudioSource>().Play();
                player.GetComponent<PlayerManage>().score += 20;
                player.GetComponent<PlayerManage>().updateScore();
                StartCoroutine(SelfDestruct());
            }

            cloneStorage = Instantiate(explode, collision.transform.position, gameObject.transform.rotation); // create the explosion effect
            Destroy(cloneStorage, 0.4f);

            Destroy(collision.gameObject); // destroy the bullet

            StartCoroutine(hit());
        }
        else if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerManage>().playerDmg();
            Destroy(gameObject);

        }
        else if (collision.gameObject.name == "HomeZone")
        {
            player.GetComponent<PlayerManage>().playerDmg();
            Destroy(gameObject);

        }
        else if (collision.gameObject.name == "Eraser")
        {
            Destroy(gameObject);

        }
    }


    private IEnumerator hit()
    {

        redCoat.SetActive(true);
        if (snake)
        {
            gameObject.transform.localScale = new Vector3(3, 3, 3);
        }

        gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(speed, 0);
        if (snake)
        {
            gameObject.transform.localScale = new Vector3(6, 6, 6);
        }

        redCoat.SetActive(false);

    }

    private IEnumerator SelfDestruct()
    {

        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);

    }
}