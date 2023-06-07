using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int HP = 10;
    public float moveSpeed = 1f;
    public float moveSpeedinZAxis = 1f;
    public GameObject bulletPrefeb;
    public Transform muzzle;
    public float bulletSpeed = 10f;
    public int extraBullet = 30;

    Rigidbody2D rb;

    public BackgroundScrolling backgroundScrolling;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        // backgroundScrolling.StopScrolling();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;

        transform.position = Camera.main.ViewportToWorldPoint(pos);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        rb.velocity = movement * moveSpeed;

        if (horizontalInput < 0f)
        {
            backgroundScrolling.StartScrollingForward();
            backgroundScrolling.scrollSpeed = 1.0f;
            // backgroundScrolling.StopScrollingForward();
        }
        else if (horizontalInput > 0f)
        {
            backgroundScrolling.StartScrollingForward();
            backgroundScrolling.scrollSpeed = 3.0f;
            // backgroundScrolling.StopScrollingReverse();
        }
        else
        {
            backgroundScrolling.StopScrollingForward();
            backgroundScrolling.StopScrollingReverse();
        }
    }

    private void Shoot()
    {
        if (extraBullet > 0)
        {
            GameObject spawnedBullet = Instantiate(bulletPrefeb, muzzle.position, muzzle.rotation);
            Rigidbody2D bulletRb = spawnedBullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(bulletSpeed * muzzle.right, ForceMode2D.Impulse);
            Destroy(spawnedBullet, 1.3f);
            extraBullet -= 1;
            Debug.Log("extraBullet : " + extraBullet);
        }
        else if (extraBullet == 0)
        {
            extraBullet = 0;
            Debug.Log("Extra Bullet is " + extraBullet);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("enemyBullet"))
        {
            Destroy(collision.gameObject);
            HP -= 1;
            Debug.Log(HP);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemyBody"))
        {
            extraBullet = 30;
            Debug.Log("Extra Bullet is " + extraBullet + ". " + "Fire Again!");
            Destroy(collision.gameObject);
        }
    }
}
