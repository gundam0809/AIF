using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject GameObject;
    public float timer = 30;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
            timer = 30;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")|| collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.MinusHealth();
            if (gameObject)
            {
                Destroy(gameObject);
            }
        }
    }
}