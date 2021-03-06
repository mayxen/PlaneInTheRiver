﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public delegate void fuelHandler();
    public event fuelHandler OnFuel;
    public float horSpeed = 5f;
    public float verSpeed = 5f;
    public float missileSpeed = 10f;
    public float horizontalLimit;
    public float verticalLimit;
    public GameObject missile;
    bool isFiring;
    // Use this for initialization
    void Start () {
		horizontalLimit = Screen.width* Camera.main.aspect / 2f / 100f -2;
        verticalLimit = Screen.height * Camera.main.aspect / 2f / 100f -2;
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

        if (Input.GetAxis("Fire1")== 1f)//de esta forma tiene que soltar el fire1 para poder volver a disparar
        {
            if (!isFiring)
            {
                isFiring = true;
                GameObject fire = Instantiate(missile);
                fire.transform.SetParent(transform.parent);
                fire.transform.position = transform.position;
                fire.GetComponent<Rigidbody2D>().velocity = Vector2.up * missileSpeed;
                Destroy(fire, 4f);
            }
            
        }else
        {
            isFiring = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyBullet") || collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("fuel") && OnFuel!=null)
        {
            Destroy(collision.gameObject);
            OnFuel();
        }
    }
}
