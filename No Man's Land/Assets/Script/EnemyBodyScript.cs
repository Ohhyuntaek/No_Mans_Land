using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyScript : MonoBehaviour
{
    public float movementSpeedFast = 2f;
    public float movementSpeedSlow = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
