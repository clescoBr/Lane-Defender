using Unity.VisualScripting;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Eraser")
        {


            Destroy(gameObject); // destroy the bullet

        }
    }
}
