using UnityEngine;
using System.Collections;

public class EnemyTankMover : MonoBehaviour {

    GameObject player;
    Rigidbody rb;
    public float TankSpeed;
    public float TankTurnSpeed;

    void PostCheckpointLoad()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Awake()
    {
        FindObjectOfType<CheckpointManager>().AddPostLoadCallback(PostCheckpointLoad);
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(player == null)
        {
            enabled = false;
            return;
        }

        rb.MovePosition(rb.position + transform.forward * TankSpeed
                        * Time.deltaTime);
        Vector3 currentForward = transform.forward;
        Vector3 directionToPlayer = player.transform.position
                                    - rb.position;
        directionToPlayer.Normalize();

        float direction = Vector3.Dot(transform.right, 
                                        directionToPlayer);
        float rotateAmount = TankTurnSpeed * Time.deltaTime;
        if(direction < 0.0f)
        {
            rotateAmount *= -1.0f;
        }

        float directionFromForward = Vector3.Dot(currentForward,
                                                 directionToPlayer);
        directionFromForward = Mathf.Clamp01(directionFromForward);
        directionFromForward = 1 - directionFromForward;

        Quaternion toPlayer = rb.rotation
                        * Quaternion.Euler(0.0f, 
                                        rotateAmount * directionFromForward, 
                                        0.0f);

        rb.MoveRotation(toPlayer);
	}
}
