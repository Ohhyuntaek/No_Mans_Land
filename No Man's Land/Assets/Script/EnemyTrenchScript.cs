using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTrenchScript : MonoBehaviour
{
    public float movementSpeedFast = 2f;
    public float movementSpeedSlow = 1f;
    public float spawnDelay = 45f;
    public static bool creditCheck = false;

    public GameObject enemyTrenchPrefeb;
    public GameObject credit;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemyTrench", spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (creditCheck)
        {
            credit.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Phase 2");
            }
        }
    }

    private void SpawnEnemyTrench()
    {
        Vector3 spawnPosition = new Vector3(11f, transform.position.y,
            transform.position.z);
        Instantiate(enemyTrenchPrefeb, spawnPosition, transform.rotation);
        float playerMovement = Input.GetAxis("Horizontal");
        MoveTrench(playerMovement);
    }

    private void MoveTrench(float input)
    {
        if (input < 0)
        {
            transform.Translate(Vector2.left * movementSpeedSlow * Time.deltaTime);
        }
        else if (input > 0)
        {
            transform.Translate(Vector2.left * movementSpeedFast * Time.deltaTime);
        }
    }


}
