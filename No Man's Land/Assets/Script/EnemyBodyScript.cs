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

    public float fadeDuration = 8f; // 투명도가 사라지는 데 걸리는 시간

    private Material material;
    private float fadeStartTime;

    // Start is called before the first frame update
    void Start()
    {
        reloadSound = GetComponent<AudioSource>();
        material = GetComponent<Renderer>().material;
        fadeStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float playerMovement = Input.GetAxis("Horizontal");
        MoveBody(playerMovement);

        float timeSinceFadeStart = Time.time - fadeStartTime;
        float alpha = 1f - (timeSinceFadeStart / fadeDuration); // 점점 투명해지는 효과를 위한 alpha 계산
        alpha = Mathf.Clamp01(alpha); // alpha 값을 0과 1 사이로 제한

        // Material의 투명도 조절
        Color color = material.color;
        color.a = alpha;
        material.color = color;

        // 투명도가 0이 되면 오브젝트를 Destroy
        if (alpha <= 0.25f)
        {
            Destroy(gameObject);
        }
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
