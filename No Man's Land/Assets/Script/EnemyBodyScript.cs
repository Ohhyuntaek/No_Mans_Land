using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyScript : MonoBehaviour
{
    public float movementSpeedFast = 2f;
    public float movementSpeedSlow = 1f;

    public static bool reloadSoundCheck = false;
    public AudioSource reloadSound;
    public AudioClip reload;

    // Start is called before the first frame update
    void Start()
    {
        reloadSound = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        float playerMovement = Input.GetAxis("Horizontal");
        MoveBody(playerMovement);
    }

    private void MoveBody (float input)
    {
        if (input < 0)
        {
            transform.Translate(Vector2.up * movementSpeedSlow * Time.deltaTime);
        }
        else if (input > 0)
        {
            transform.Translate(Vector2.up * movementSpeedFast * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            reloadSound.PlayOneShot(reload);
        }
    }
}
