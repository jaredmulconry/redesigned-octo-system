using UnityEngine;
using System.Collections;

public class TankShooter : MonoBehaviour {

    public Rigidbody ShellPrefab;
    public Transform FiringPoint;
    public float LaunchSpeed = 30.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonUp("Fire1"))
        {
            Rigidbody shell = Instantiate(ShellPrefab,
                                        FiringPoint.position,
                                        FiringPoint.rotation)
                                    as Rigidbody;
            Physics.IgnoreCollision(shell.GetComponent<Collider>(),
                                    GetComponent<Collider>());
            shell.velocity = FiringPoint.forward * LaunchSpeed;
        }
	}
}
