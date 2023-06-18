using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float movementSpeed = 3f;
    public float rotationSpeed = 3f;
    public float stoppingDistance = 5f;
    public float fireRate = 1f;
    public float bulletSpeed = 7f;

    public static bool DontFireCheck = false;

    public GameObject explosedBody;
    public GameObject bulletPrefeb;

    private Rigidbody2D rb;
    public AudioSource gunShotSound;

    public Transform muzzle;
    private Transform player;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gunShotSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;

        transform.position = Camera.main.ViewportToWorldPoint(pos);

        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            Vector2 direction = new Vector2(transform.position.x - player.position.x,
                transform.position.y - player.position.y);

            // 플레이어를 향해 회전
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis,
                rotationSpeed * Time.deltaTime);
            transform.rotation = rotation;

            if(distance > stoppingDistance)
            {
                // 특정 거리(stoppingDistance)보다 멀리 있을 경우 이동
                transform.position = Vector2.MoveTowards(transform.position,
                    player.position, movementSpeed * Time.deltaTime);
                // rb.MoveRotation(Quaternion.Euler(rotation));
            }
        }

        timer += Time.deltaTime;

        if(timer >= fireRate)
        {
            SpawnBullet();
            timer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("bullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            Instantiate(explosedBody, transform.position, transform.rotation);
        }
    }

    private void SpawnBullet()
    {
        if (!DontFireCheck)
        {
            gunShotSound.Play();
            GameObject enemyBullet = Instantiate(bulletPrefeb, muzzle.position,
                muzzle.rotation);
            Rigidbody2D bulletRb = enemyBullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(bulletSpeed * muzzle.right, ForceMode2D.Impulse);
            Destroy(enemyBullet, 1.3f);
        }
        
    }
}
