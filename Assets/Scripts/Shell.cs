using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour
{
    public float MaxLifeTime = 2.0f;
    public float MaxDamage = 30.0f;
    public float ExplosionRadius = 5.0f;
    public float ExplosionForce = 50.0f;

    public ParticleSystem ExplosionSystem;
    public string TargetTag = "Enemy";

    void Start()
    {
        Destroy(gameObject, MaxLifeTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag(TargetTag) &&
            other.gameObject.layer != LayerMask.NameToLayer("Ground"))
            return;

        Rigidbody targetRigidbody = other.rigidbody;

        //- Damage target here
        if(targetRigidbody != null)
        {
            targetRigidbody.AddExplosionForce(ExplosionForce,
                                              transform.position,
                                              ExplosionRadius);

            TankHealth targetHealth = targetRigidbody
                                        .GetComponent<TankHealth>();
            if(targetHealth != null)
            {
                float damage = CalculateDamage(targetRigidbody.position);

                targetHealth.TakeDamage(damage);
            }
        }
        //-

        ExplosionSystem.transform.SetParent(null);

        ExplosionSystem.Play();

        Destroy(ExplosionSystem.gameObject, ExplosionSystem.duration);

        Destroy(gameObject);
    }

    float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;
        float explosionDistance = explosionToTarget.magnitude;

        float relativeDistance = (ExplosionRadius - explosionDistance)
                                / ExplosionRadius;

        float damage = relativeDistance * MaxDamage;

        damage = Mathf.Max(0.0f, damage);

        return damage;
    }
}
