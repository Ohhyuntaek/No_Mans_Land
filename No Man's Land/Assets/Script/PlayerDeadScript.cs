using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadScript : MonoBehaviour
{
    public AudioSource playerDeadSound;

    // Start is called before the first frame update
    void Start()
    {
        playerDeadSound = GetComponent<AudioSource>();
        playerDeadSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
