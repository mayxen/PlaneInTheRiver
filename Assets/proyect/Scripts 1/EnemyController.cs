using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public delegate void KillHandler();
    public event KillHandler OnKill;
    public GameObject bullet;
    public float speed = 2.5f;
    public float bulletSpeed = -3f;
    public float shootingInterval = 4f;
    float shootingTimer;
	// Use this for initialization
	void Start () {
        shootingTimer = Random.Range(0f,shootingInterval);
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
	}
	
	// Update is called once per frame
	void Update () {
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0f)
        {
            shootingTimer = Random.Range(0f, shootingInterval);
            GameObject fire = Instantiate(bullet);
            fire.transform.SetParent(transform.parent);
            fire.transform.position = transform.position;
            fire.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
            Destroy(fire, 4f);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("playerBullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);

            if (OnKill != null)
            {
                OnKill();
            }
        }
    }
}
