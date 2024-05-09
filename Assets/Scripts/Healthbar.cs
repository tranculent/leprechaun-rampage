using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthBarSprite;
    [SerializeField] private float reduceSpeed;
    private float target = 1;
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        target = currentHealth / maxHealth;
    }

    private void LateUpdate()
    {
        // Rotate the health bar to face the camera
        transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
        // Animation
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
}
