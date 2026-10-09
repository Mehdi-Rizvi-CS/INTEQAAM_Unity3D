
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public Slider healthSlider;
    public float maxHealth = 1000f;

    [Header("Respawn")]
    public Transform respawnPoint;
    public float respawnDelay = 2f;

    private float currentHealth;
    private bool isDead;

    private CharacterController characterController;
    private Rigidbody rb;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.minValue = 0f;
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        currentHealth = Mathf.Max(0f, currentHealth - damage);

        if (healthSlider != null)
            healthSlider.value = currentHealth;

        if (currentHealth <= 0f)
        {
            StartCoroutine(RespawnPlayer());
        }
    }

    private IEnumerator RespawnPlayer()
    {
        isDead = true;

        // Pause the game for 2 real-time seconds.
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(respawnDelay);

        // Resume temporarily so the player can be moved safely.
        Time.timeScale = 1f;

        if (respawnPoint != null)
        {
            if (characterController != null)
                characterController.enabled = false;

            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;

            if (characterController != null)
                characterController.enabled = true;
        }
        else
        {
            Debug.LogError("Respawn Point is not assigned!");
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        currentHealth = maxHealth;

        if (healthSlider != null)
            healthSlider.value = maxHealth;

        isDead = false;
    }

    private void OnDestroy()
    {
        // Avoid leaving the game paused if this object is destroyed.
        Time.timeScale = 1f;
    }
}