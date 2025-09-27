using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameManager gameManager;
    public NavMeshAgent agent;

    public Transform player;
    public float health = 5f;
    public LayerMask whatIsGround, whatIsPlayer;
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public Animator animator;
    private Rigidbody rb;
    public int points = 1;
    [SerializeField] Transform bulletpoint;


    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();


        gameManager = FindObjectOfType<GameManager>();

        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        //Check for sight attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (gameManager.State == GameManager.GameState.Playing)
        {
            if (!playerInSightRange && !playerInAttackRange) { Patroling(); }
            if (playerInSightRange && !playerInAttackRange) { ChasePlayer(); }
            if (playerInSightRange && playerInAttackRange) { AttackPlayer(); }
        }
    }



    void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoin = transform.position - walkPoint;
        if (distanceToWalkPoin.magnitude < 1f)
        {
            walkPointSet = false;
        }
        animator.SetBool("Running", true);
        animator.SetBool("Attacking", false);
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("Running", true);
        animator.SetBool("Attacking", false);
    }

    void AttackPlayer()
    {

        agent.SetDestination(transform.position);

        animator.SetBool("Running", false);
        animator.SetBool("Attacking", true);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, bulletpoint.position, bulletpoint.rotation).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    void ResetAttack()
    {
        alreadyAttacked = false;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            Debug.Log(health);
            if (health <= 0)
            {
                Debug.Log("Kill enemy");
                DestroyEnemy();
                gameManager.AddScore(points);
            }
        }
    }


    private void DestroyEnemy()
    {
        Debug.Log("Sleep");
        Destroy(gameObject);
    }

}