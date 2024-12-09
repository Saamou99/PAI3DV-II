using UnityEngine;

public class ZombieCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject); // Destroy the player object
            Debug.Log("Player destroyed! Changing to End scene.");
            UnityEngine.SceneManagement.SceneManager.LoadScene("End"); // Change scene to "End"
        }

        // Check if the colliding object is a bullet
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject); // Destroy the bullet
            Destroy(gameObject); // Destroy the zombie
            Debug.Log("Zombie hit by bullet and destroyed!");
        }
    }
}