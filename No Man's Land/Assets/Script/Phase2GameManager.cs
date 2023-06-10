using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Phase2GameManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject tank;
    public GameObject deadCredit;
    public float spawnXAxis = 15f;
    public float afterTime = 45f;

    public static int maxTank = 0;

    public bool enableSpawnEnemy = false;
    public bool enableSpawnTank = false;
    public static bool deadCreditCheck = false;

    // private float spawnTime = Random.Range(1f, 5f);

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 3, 3); //3초 후 부터, SpawnEnemy함수를 3초마다 반복해서 실행 시킵니다.
        InvokeRepeating("SpawnTank", afterTime, 15);
    }

    // Update is called once per frame
    void Update()
    {
         if (deadCreditCheck)
        {
            deadCredit.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Phase 1");
            }
        }
    }

    private void SpawnEnemy()
    {
        if (enableSpawnEnemy)
        {
            float randomY = Random.Range(-5f, 5f);
            GameObject spawnEnemy = (GameObject)Instantiate(enemy, new Vector3(10f, randomY, 0f), Quaternion.identity); 
        }
    }

    private void SpawnTank()
    {
        if (maxTank <= 2)
        {
            if (enableSpawnTank)
            {
                float randomY = Random.Range(-5f, 5f);
                float randomZ = Random.Range(0f, 360f);
                Quaternion rotation = Quaternion.Euler(0, 0, randomZ);
                GameObject spawnTank = (GameObject)Instantiate(tank, new Vector3(gameObject.transform.position.x + spawnXAxis, randomY, 0f), rotation);
                maxTank += 1;
            }
        }
        
    }
}
