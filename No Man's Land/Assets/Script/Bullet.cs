using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
        */
    }
}
