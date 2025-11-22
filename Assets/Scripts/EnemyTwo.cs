using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwo : MonoBehaviour
{
    public GameObject explosionPrefab;
    private GameManager gameManager;

    private float speed;
    private float frequency;
    private float magnitude;
    private Vector3 startPos;
    private float timeElapsed;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        speed = 3f;
        frequency = 2f;
        magnitude = 1.5f;
        startPos = transform.position;
        timeElapsed = 0f;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        // Move down and in a wave pattern
        float yMovement = -speed * Time.deltaTime;
        float xMovement = Mathf.Sin(timeElapsed * frequency) * magnitude * Time.deltaTime;

        transform.Translate(new Vector3(xMovement, yMovement, 0));

        // Destroy if off screen
        if (transform.position.y < -gameManager.verticalScreenSize * 1.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "Player")
        {
            whatDidIHit.GetComponent<PlayerController>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(whatDidIHit.tag == "Weapons")
        {
            Destroy(whatDidIHit.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.AddScore(5);
            Destroy(this.gameObject);
        }
    }
}
