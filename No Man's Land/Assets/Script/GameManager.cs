using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    public bool enableSpawn = false;
    public static bool deadCreditCheck = false;

    // private float spawnTime = Random.Range(1f, 5f);

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 3, 3); //3초 후 부터, SpawnEnemy함수를 3초마다 반복해서 실행 시킵니다.
    }

    // Update is called once per frame
    void Update()
    {
        if (deadCreditCheck)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Phase 1");
            }
        }    
    }

    private void SpawnEnemy()
    {
        float randomY = Random.Range(-5f, 5f);
        if (enableSpawn)
        {
            GameObject spawnEnemy = (GameObject)Instantiate(enemy, new Vector3(10f, randomY, 0f), Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
        }
    }
}
