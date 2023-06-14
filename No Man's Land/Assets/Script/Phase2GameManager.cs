using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Phase2GameManager : MonoBehaviour
{
    private float currentTime = 0f;
    private GameObject letherGunCheckObject;

    public GameObject enemy;
    public GameObject tank;
    public GameObject redTank;
    public GameObject letherGun;
    public GameObject deadCredit;
    public GameObject endingCredit;
    public GameObject pausedCredit;

    public TMP_Text time;
    public TMP_Text letherGunTime;

    public float spawnXAxis = 15f;
    public float tankAfterTime;
    public float redtankAfterTime;
    public float letherGunAfterTime = 20f;
    public int letherGunSpawnCount = 0;

    public bool enableSpawnEnemy = false;
    public bool enableSpawnTank = false;
    public bool enableSpawnRedTank = false;
    public bool isTimePaused = false;

    public static int maxTank = 0;
    public static int maxLetherGun = 0;
    public static bool isActivateLetherGun = false;
    public static bool isLetherGunSpawned = false;
    public static bool deadCreditCheck = false;
    public static bool isRedTankDown = false;
    public static float letherGunTimeFloat;

    public AudioSource BackgroundSound;

    // private float spawnTime = Random.Range(1f, 5f);

    // Start is called before the first frame update
    void Start()
    {
        tankAfterTime = Random.Range(45f, 60f);
        redtankAfterTime = Random.Range(90f, 120f);

        Time.timeScale = 1f;
        InvokeRepeating("SpawnEnemy", 3, 3); //3초 후 부터, SpawnEnemy함수를 3초마다 반복해서 실행 시킵니다.
        InvokeRepeating("SpawnTank", tankAfterTime, 15);
        // InvokeRepeating("SpawnLetherGun", letherGunAfterTime, 20);
        Invoke("SpawnRedTank", redtankAfterTime);
        BackgroundSound = GetComponent<AudioSource>();
        BackgroundSound.Play();
    }

    private void Awake()
    {
        letherGunTimeFloat = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        // 시간 표시
        currentTime += Time.deltaTime;
        time.text = "Time " + (string.Format("{0:N1}", currentTime));

        // 배드엔딩 크레딧 표시
        if (deadCreditCheck)
        { 
            deadCredit.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Phase 1");
            }
        }

        // 최종보스 파괴 시
        if (isRedTankDown == true)
        {
            endingCredit.SetActive(true);
            // isRedTankDown = false;
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene("Phase 1");
        }

        // 플레이어 사망 시 배경음악 일시정지 
        if (Phase2PlayerController.isDead)
        {
            BackgroundSound.Pause();
        }

        // LetherGun 쿨타임 표시 
        if (letherGunTimeFloat > 0)
        {
            letherGunTimeFloat -= Time.deltaTime;
        }
        letherGunTime.text = Mathf.Ceil(letherGunTimeFloat).ToString();
        if (letherGunTimeFloat <= 0)
        {
            letherGunTime.text = "Lethar Gun is Spawned!";
            isLetherGunSpawned = true;
            
        }

        if (isLetherGunSpawned && letherGunCheckObject == null)
        {
            StartCoroutine(SpawnLetherGunCoroutine());
            isLetherGunSpawned = false;
        }
    }

    public void Pause()
    {
        if (isTimePaused == false)
        {
            Time.timeScale = 0;
            pausedCredit.SetActive(true);
            isTimePaused = true;
        }
        else
        {
            Time.timeScale = 1;
            pausedCredit.SetActive(false);
            isTimePaused = false;
        }
    }

    IEnumerator SpawnLetherGunCoroutine()
    {
        if(isLetherGunSpawned) // && maxLetherGun == 0)
        {
            SpawnLetherGun();
        }
        isLetherGunSpawned = false;
        Debug.Log(isLetherGunSpawned);
        yield return null;
    }
    
    private void SpawnEnemy()
    {
        if (enableSpawnEnemy && !Phase2PlayerController.isDead && !isRedTankDown)
        {
            float randomY = Random.Range(-5f, 5f);
            GameObject spawnEnemy = (GameObject)Instantiate(enemy, new Vector3(10f, randomY, 0f), Quaternion.identity); 
        }
    }

    private void SpawnTank()
    {
        if (maxTank <= 2)
        {
            if (enableSpawnTank && !Phase2PlayerController.isDead && !isRedTankDown)
            {
                float randomY = Random.Range(-5f, 5f);
                float randomZ = Random.Range(0f, 360f);
                Quaternion rotation = Quaternion.Euler(0, 0, randomZ);
                GameObject spawnTank = (GameObject)Instantiate(tank, new Vector3(gameObject.transform.position.x + spawnXAxis, randomY, 0f), rotation);
                maxTank += 1;
            }
        }
    }

    private void SpawnRedTank()
    {
        if (enableSpawnRedTank && !Phase2PlayerController.isDead)
        {
            float randomY = Random.Range(-1f, 1f);
            float randomZ = Random.Range(0f, 30f);
            Quaternion rotation = Quaternion.Euler(0, 0, randomZ);
            GameObject spawnTank = (GameObject)Instantiate(redTank, new Vector3(gameObject.transform.position.x + spawnXAxis, randomY, 0f), rotation);
        }
    }

    private void SpawnLetherGun()
    {
        isLetherGunSpawned = false;
        float RandomY = Random.Range(-4f, 4f) - Random.Range(-2f, 2f);
        GameObject LetherGun = Instantiate(letherGun, new Vector3(-7.5f, RandomY, 0f), Quaternion.identity);
        letherGunCheckObject = LetherGun;
    }
}
