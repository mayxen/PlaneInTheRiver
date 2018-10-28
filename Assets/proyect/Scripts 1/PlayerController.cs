using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float horSpeed = 5;
    public float verSpeed = 5;
    public float horizontalLimit;
    public float verticalLimit;
    // Use this for initialization
    void Start () {
		horizontalLimit = Camera.main.pixelWidth / 2 * Camera.main.aspect / 100 -2;
        verticalLimit = Camera.main.pixelHeight / 2 * Camera.main.aspect / 100 - 2;
    }
	
	// Update is called once per frame
	void Update () {
        //move player

        GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal")*horSpeed, verSpeed);

        if (transform.position.x > horizontalLimit)
        {
            transform.position = new Vector2(horizontalLimit,transform.position.y);
        }
        else if (transform.position.x < -horizontalLimit)
        {
            transform.position = new Vector2(-horizontalLimit, transform.position.y);
        }
        
    }
}
