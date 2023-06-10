using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosionScript : MonoBehaviour
{
    public AudioSource explosedSound;
    public AudioClip exSound;

    // Start is called before the first frame update
    void Start()
    {
        explosedSound = GetComponent<AudioSource>();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        explosedSound.PlayOneShot(exSound, 0.1f);
    }

}
