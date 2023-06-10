using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int maxHP = 10;
    public int nowHP = 10;
    public float moveSpeed = 1f;
    public float moveSpeedinZAxis = 1f;
    public bool deadCreditCheck = false;
    public Transform muzzle;

    public float bulletSpeed = 10f;
    public int extraBullet = 30;
    public Rigidbody2D rb;
    public BackgroundScrolling backgroundScrolling;

    public GameObject playerBody;
    public GameObject bulletPrefeb;
    public GameObject prfHpBar;
    public GameObject canvas;
    public GameObject deadCredit;
    public float height = 1f;
    RectTransform hpBar;

    public Image nowHPBar;

    // public EnemyTrenchScript enemyTrenchScript = new EnemyTrenchScript();

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        // backgroundScrolling.StopScrolling();

        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x - 0.5f, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;

        nowHPBar.fillAmount = (float)nowHP / (float)maxHP;

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

        if (nowHP <= 0)
        {
            Destroy(gameObject);
            Quaternion rotation = Quaternion.Euler(0, 0, -90f);
            Instantiate(playerBody, transform.position, rotation);
            deadCreditCheck = true;
            if (deadCreditCheck)
            {
                deadCredit.SetActive(true);
                GameManager.deadCreditCheck = true;
            }
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
            nowHP -= 1;
            Debug.Log(nowHP);
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
        else if (collision.gameObject.CompareTag("enemyTrench"))
        {
            Time.timeScale = 0f;
            EnemyTrenchScript.creditCheck = true;
        }
    }
}
