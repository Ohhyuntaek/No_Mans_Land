using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurTrenchScript : MonoBehaviour
{
    public float movementSpeedFast = 2f;
    public float movementSpeedSlow = 1f;
    public float destroyDelay = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float playerMovement = Input.GetAxis("Horizontal");
        MoveTrench(playerMovement);
        DestroyTrench();
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

    private void DestroyTrench()
    {
        Destroy(gameObject, destroyDelay);
    }
}
