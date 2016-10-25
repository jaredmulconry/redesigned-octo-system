using UnityEngine;
using System.Collections;

/// <summary>
/// Tracks a player or enemy's health,
/// as well as triggering effects upon death.
/// </summary>
public class TankHealth : MonoBehaviour
{
    public float StartingHealth = 100.0f;
    public GameObject ExplosionPrefab;

    float currentHealth;
    bool isDead;

    ParticleSystem explosionParticles;

    void Awake()
    {
        explosionParticles = Instantiate(ExplosionPrefab)
                                .GetComponent<ParticleSystem>();
        explosionParticles.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        currentHealth = StartingHealth;
        isDead = false;
        SetHealthUI();
    }


    void SetHealthUI()
    {
        //TODO: Update the User Interface.

    }

    /// <summary>
    /// Applies an <paramref name="amount"/> of
    /// damage to this tank. Updating the UI and
    /// triggering death once health has ran out.
    /// </summary>
    /// <param name="amount">Controls how much damage should
    /// be applied. +ve amounts will reduce a tank's health.</param>
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        SetHealthUI();

        if(currentHealth <= 0.0f && !isDead)
        {
            OnDeath();
        }
    }

    /// <summary>
    /// Disables the tank that this script is attached to,
    /// as well as produce an explosion effect in-place of
    /// the tank.
    /// </summary>o
    void OnDeath()
    {
        isDead = true;

        explosionParticles.transform.position = transform.position;
        explosionParticles.gameObject.SetActive(true);

        Destroy(explosionParticles.gameObject, explosionParticles.duration);

        explosionParticles.Play();

        gameObject.SetActive(false);
    }
}
