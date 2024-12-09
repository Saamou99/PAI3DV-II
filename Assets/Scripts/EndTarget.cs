using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTarget : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached the end target! Loading GameSceneBoss...");
            SceneManager.LoadScene("End"); // Change to the boss scene
        }
    }
}