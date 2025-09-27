using UnityEngine;

public class TargetHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int points = 1;
    public Collider enemy; 
    private int currentHealth;

    private GameManager gameManager;

    public GameManager GameManager { get { return gameManager; } set { gameManager = value; } }

    // Unity Message
    void OnEnable()
    {
        // Set current health to max health
        currentHealth = maxHealth;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            maxHealth = maxHealth - 1;
        }
    }

    // Function to disable targets
    private void DisableTarget()
    {
        Debug.Log("F");
        if (currentHealth == 0)
        {
            Debug.Log("RU is DUMB");
            if (gameManager != null)
            {
                gameManager.AddScore(points);
            }
            gameObject.SetActive(false);
        }
    }

}