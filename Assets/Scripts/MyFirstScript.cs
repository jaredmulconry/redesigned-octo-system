using UnityEngine;
using System.Collections;

public class MyFirstScript : MonoBehaviour {

    int MyInteger;
    public int MyPublicInteger;
    public string MyPublicString = "Hello!";
    Rigidbody MyRigidbody;

	// Use this for initialization
	void Start () {
        MyInteger = 42;
        MyPublicInteger = 10101001;

        MyRigidbody = GetComponent<Rigidbody>();
        if (MyRigidbody != null)
        {
            MyRigidbody.AddForce(new Vector3(0, 1000, 0));
        }
        Debug.Log(MyPublicString);
        Debug.Log(MyInteger);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
