using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private NavMeshAgent agent; // NavMeshAgent component for movement

    void Start()
    {
        // Get the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing on this object.");
            return;
        }

        // Ensure the enemy starts on the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 10f, NavMesh.AllAreas))
        {
            transform.position = hit.position; // Snap to the nearest NavMesh point
        }
        else
        {
            Debug.LogError("No valid NavMesh point found near the enemy's starting position.");
            enabled = false; // Disable this script if no valid NavMesh point is found
        }
    }

    void Update()
    {
        // Ensure the player reference is set
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned.");
            return;
        }

        // Continuously update the agent's destination to follow the player
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);

            // Debug: Draw a line to visualize the path
            Debug.DrawLine(transform.position, player.position, Color.red);
        }
        else
        {
            Debug.LogWarning("NavMeshAgent is not active or not on NavMesh.");
        }
    }
}
