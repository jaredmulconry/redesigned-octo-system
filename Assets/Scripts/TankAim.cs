using UnityEngine;
using System.Collections;

public class TankAim : MonoBehaviour {

    public float AimSensitivity = 150;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        float mouseInput = Input.GetAxis("Mouse X");

        transform.rotation *= 
            Quaternion.Euler(0.0f, 
                             mouseInput * AimSensitivity * Time.deltaTime,
                             0.0f);
    }

 //   LayerMask somebodyStopMe;

	//// Use this for initialization
	//void Start () {
 //       somebodyStopMe = LayerMask.GetMask("Ground");
	//}
	
	//// Update is called once per frame
	//void LateUpdate () {
 //       Ray jay = Camera.main.ScreenPointToRay(Input.mousePosition);

 //       RaycastHit hit;

 //       if(Physics.Raycast(jay, out hit, Mathf.Infinity, somebodyStopMe))
 //       {
 //           Vector3 aimPoint = hit.point;
 //           aimPoint.y = transform.position.y;
 //           transform.LookAt(aimPoint);
 //       }
	//}
}
