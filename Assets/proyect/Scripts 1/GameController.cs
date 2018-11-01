using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    
    public Text ScoreText;
    public Text FuelText;
    public PlayerController player;
    public GameObject enemy;
    public float enemySpawnInterval = 10f;
    public float horizontalLimit;
    public float fuelDecrease = 5f;
    public GameObject fuelObject;
    public float fuelspaw = 9f;

    float fueltime;
    int score;
 
    float fuel =100f;
    float enemySpawTimer;

	// Use this for initialization
	void Start () {
        horizontalLimit = Screen.width * Camera.main.aspect / 2f / 100f - 2;
        enemySpawTimer = Random.Range(1f, enemySpawnInterval);
        player.OnFuel += OnFuelTaken;
        fueltime = Random.Range(2f, fuelspaw);
    }
	
	// Update is called once per frame
	void Update () {
        enemySpawTimer -= Time.deltaTime;
        
        if(player != null)
        {
            player.OnFuel += OnFuelTaken;
            if (enemySpawTimer <= 0)
            {
         
                enemySpawTimer = Random.Range(1f, enemySpawnInterval);

                GameObject enemyins = Instantiate(enemy);
                enemyins.transform.SetParent(transform);
                enemyins.transform.position = new Vector2(Random.Range(-horizontalLimit, horizontalLimit),player.transform.position.y + Screen.height *2 /100f);
                enemyins.GetComponent<EnemyController>().OnKill += OnEnemyKill;
            }

            fuel -= Time.deltaTime*fuelDecrease;
            FuelText.text = "Fuel: " + (int)fuel;
            if(fuel <= 0)
            {
                FuelText.text = "Fuel: 0";
                Destroy(player.gameObject);
            }
            fueltime -= Time.deltaTime ;
            if(fueltime <= 0)
            {
                fueltime = Random.Range(2f, fuelspaw);
                GameObject fuelins = Instantiate(fuelObject);
                fuelins.transform.SetParent(transform);
                fuelins.transform.position = new Vector2(Random.Range(-horizontalLimit, horizontalLimit), player.transform.position.y + Screen.height * 2 / 100f);
            }
            
        }
        foreach (EnemyController enemy in GetComponentsInChildren<EnemyController>())
        {
            if( Camera.main.transform.position.y- enemy.transform.position.y > Screen.height / 100f)
            {
                Destroy(enemy.gameObject);
            }
        }

        
    }
    void OnEnemyKill()
    {
        score += 25;
        ScoreText.text = "Score: " + score;
    }

    void OnFuelTaken()
    {
        fuel = 100;
    }
}
