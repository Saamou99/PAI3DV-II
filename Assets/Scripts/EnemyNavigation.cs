using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public NavMeshAgent agent; // NavMeshAgent for enemy navigation
    public Transform player; // Reference to the player's position
    public LayerMask whatIsGround, whatIsPlayer; // Layers for ground and player

    public float health; // Enemy's health

    // Patroling
    public Vector3 walkPoint; // Current target point for patrol
    bool walkPointSet; // Whether a patrol point is set
    public float walkPointRange; // Range to pick random patrol points

    // States
    public float sightRange, attackRange; // Ranges for detecting the player
    public bool playerInSightRange, playerInAttackRange; // Whether player is in sight/attack range

    private void Awake()
    {
        // Find the player object and initialize the NavMeshAgent
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for player's presence in sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // Handle behavior based on player's range
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
    }

    private void Patroling()
    {
        // Set a new walk point if not already set
        if (!walkPointSet) SearchWalkPoint();

        // Move to the walk point
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        // Reset walk point once reached
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate a random point within range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Validate the point is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        // Set the destination to the player's position
        agent.SetDestination(player.position);
    }
}
