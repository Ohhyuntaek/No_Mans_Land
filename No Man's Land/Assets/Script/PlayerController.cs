using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public GameObject bulletPrefeb;
    public Transform muzzle;
    public float bulletSpeed = 10f;

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

        if(Input.GetKeyDown(KeyCode.A))
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
        GameObject spawnedBullet = Instantiate(bulletPrefeb, muzzle.position, muzzle.rotation);
        Rigidbody2D bulletRb = spawnedBullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(bulletSpeed * muzzle.right, ForceMode2D.Impulse);
        Destroy(spawnedBullet, 2);

    }
}
