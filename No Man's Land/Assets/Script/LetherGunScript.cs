using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetherGunScript : MonoBehaviour
{
    public AudioSource gunShotSound;

    // Start is called before the first frame update
    void Start()
    {
        gunShotSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Phase2GameManager.isLetherGunSpawned = false;
            gunShotSound.Play();
            Phase2GameManager.isActivateLetherGun = true;
            Destroy(gameObject, 0.5f);
            Invoke("isActivateLetherGunFalse", 0.1f);
            Phase2GameManager.letherGunTimeFloat = 20f;
        }
    }

    private void isActivateLetherGunFalse()
    {
        Phase2GameManager.isActivateLetherGun = false;
    }
 
}
