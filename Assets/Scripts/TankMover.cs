using UnityEngine;
using System.Collections;

public class TankMover : MonoBehaviour {

    public float TankSpeed;
    public float TankTurnSpeed;

    Rigidbody rb;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
	}

    void MoveTank(float movement, Vector3 direction)
    {
        rb.MovePosition(rb.position + direction * TankSpeed 
                        * movement * Time.deltaTime);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        MoveTank(vertical, transform.forward);

        Quaternion turn = Quaternion.AngleAxis(horizontal
                                                * TankTurnSpeed,
                                            Vector3.up);
        
        rb.MoveRotation(rb.rotation * turn);
	}
}
