using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject target;
    public Vector2 offset;
	// Use this for initialization
	void Start () {
		
	}
    private void FixedUpdate()
    {
        transform.position = new Vector3(0,target.transform.position.y+offset.y,transform.position.z);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
