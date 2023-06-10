using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    public GameObject explosedTankBullet;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemyTrench"))
        {
            Destroy(gameObject);
            GameObject explosedAnmation = Instantiate(explosedTankBullet, transform.position, transform.rotation);
            Destroy(explosedAnmation, 1.4f);
        }
    }

}
