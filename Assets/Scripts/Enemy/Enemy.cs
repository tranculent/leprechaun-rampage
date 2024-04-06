using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health = 5;
    public int damage = 4;
    public GameObject ammoPickupPrefab;
    public AudioClip attackSound;
    public AudioClip footstepsSFX;
    public float footstepInterval = 0.5f;
    public float maxFootstepDistance = 10f;

    protected GameObject gameFlowManager;
    protected Transform player;
    protected float lastFootstepTime;
    protected bool isGrounded;
    protected NavMeshAgent agent;
    protected AudioSource audioSource;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        gameFlowManager = GameObject.FindGameObjectWithTag("GameFlowManager");

        if (player == null && GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        HandleFootsteps();
    }

    protected void HandleFootsteps()
    {
        if (isGrounded && agent.velocity.magnitude > 0.1f)
        {
            float timeSinceLastFootstep = Time.time - lastFootstepTime;
            if (timeSinceLastFootstep > footstepInterval)
            {
                PlayFootstepSound(agent.velocity.normalized);
                lastFootstepTime = Time.time;
            }
        }
    }

    protected void PlayFootstepSound(Vector3 moveDirection)
    {
        if (footstepsSFX != null)
        {
            Ray ray = new Ray(transform.position, moveDirection);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxFootstepDistance))
            {
                float distance = hit.distance;
                float volume = Mathf.Clamp01(1f - (distance / maxFootstepDistance));
                audioSource.volume = volume;
                audioSource.PlayOneShot(footstepsSFX);
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        var gameFlowManagerComponent = gameFlowManager.GetComponent<GameFlowManager>();
        gameFlowManagerComponent.IncreaseXp();
        gameFlowManagerComponent.IncreaseEnemyKilled();
        Instantiate(ammoPickupPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}