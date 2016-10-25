using UnityEngine;
using System.Collections;

public class EnemyTurret : MonoBehaviour {

    GameObject player;

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
	}
	
	// Update is called once per frame
	void Update () {
        if(player == null)
        {
            enabled = false;
            return;
        }

        Vector3 correctedPosition = player.transform.position;
        correctedPosition.y = transform.position.y;
        transform.LookAt(correctedPosition);
    }
}
