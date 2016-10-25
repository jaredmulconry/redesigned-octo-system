using UnityEngine;
using System.Collections;

public class EnemyTankShooter : MonoBehaviour
{
    public Rigidbody ShellPrefab;
    public Transform FiringPoint;
    public float LaunchSpeed = 30.0f;
    public float ShootDelay = 0.8f;

    private bool canShoot;
    private float shootTimer;

    void Awake()
    {
        canShoot = false;
        shootTimer = 0.0f;
    }

    void Update()
    {
        if(canShoot)
        {
            shootTimer -= Time.deltaTime;
            if(shootTimer <= 0.0f)
            {
                shootTimer = ShootDelay;
                Fire();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canShoot = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canShoot = false;
        }
    }

    void Fire()
    {
        Rigidbody shellInstance = Instantiate(ShellPrefab,
                                              FiringPoint.position,
                                              FiringPoint.rotation)
                                          as Rigidbody;

        Physics.IgnoreCollision(shellInstance.GetComponent<Collider>(),
                                    GetComponent<Collider>());

        Shell shellScript = shellInstance.GetComponent<Shell>();
        shellScript.TargetTag = "Player";

        shellInstance.velocity = FiringPoint.forward * LaunchSpeed;
    }
}
