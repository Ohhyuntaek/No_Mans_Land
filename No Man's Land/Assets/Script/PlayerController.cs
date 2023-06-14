using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    public TMP_Text extraBulletText;
    public GameObject noBulletText;
    public Rigidbody2D rb;
    public AudioSource gunShotSound;
    public BackgroundScrolling backgroundScrolling;

    public static bool isDead = false;

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
        gunShotSound = GetComponent<AudioSource>();
        // backgroundScrolling.StopScrolling();

        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        extraBulletText.text = extraBullet + " / " + 30;

        // HP Bar
        // Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x - 0.5f, transform.position.y + height, 0));
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + 1000f, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;

        nowHPBar.fillAmount = (float)nowHP / (float)maxHP;

        // 카메라 밖으로 플레이어가 나가는 것을 방지 
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;

        transform.position = Camera.main.ViewportToWorldPoint(pos);

        // 총알 
        if (extraBullet > 0)
        {
            // 여분의 총알이 있을 때 
            noBulletText.SetActive(false);
            if (Input.GetKeyDown(KeyCode.K))
            {
                Shoot();
            }
        }
        else if (extraBullet <= 0)
        {
            // 총알이 없을 때 
            noBulletText.SetActive(true);
        }

        // 플레이어 사망 
        if (nowHP <= 0)
        {
            EnemyScript.DontFireCheck = true;
            isDead = true;
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
        // 플레이어 움직임 (상 하 좌 우)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        rb.velocity = movement * moveSpeed;

        // Horizontal, Vertical의 Input에 따라 배경 움직임 
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
        // 플레이어 공격 메소드 
        gunShotSound.Play();
        GameObject spawnedBullet = Instantiate(bulletPrefeb, muzzle.position, muzzle.rotation);
        Rigidbody2D bulletRb = spawnedBullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(bulletSpeed * muzzle.right, ForceMode2D.Impulse);
        Destroy(spawnedBullet, 1.3f);
        extraBullet -= 1;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            // 적 총알에 맞았을 때 
            Destroy(collision.gameObject);
            nowHP -= 1;
        }
        if (collision.gameObject.CompareTag("enemyBody"))
        {
            // 적 시체를 밟았을 때 
            extraBullet = 30;
            Destroy(collision.gameObject, 0.3f);
        }
        else if (collision.gameObject.CompareTag("enemyTrench"))
        {
            // 적 참호에 도착했을 때 
            Time.timeScale = 0f;
            EnemyTrenchScript.creditCheck = true;
        }
    }
}
