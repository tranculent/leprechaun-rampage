using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
{
    private bool isTouchingPlayer = false;

    // Update is called once per frame
    void Update()
    {
        if (player && !isTouchingPlayer)
        {
            agent.SetDestination(player.position);
        }
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" && attackSound != null)
        {
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            isTouchingPlayer = true;
        }
        isGrounded = collision.transform.tag == "Ground";
    }
}
