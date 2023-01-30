using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    List<Enemie> enemies;
    public int waveRound = 1;
    //Based on this the wave is calculated
    int currentMaxWeight;

    //Liste für die Enemies in der Wave
    List<Enemie> enemiesInWave;
    List<Node> paths;

    private bool startEndless;

    //Counter for the spawn of the enemies
    //if all Enemies are spawned, start the CountDown for the next Wave
    private int spawnCounter = 0;

    //Wait time between the spawn of the enemies
    public float timeBetweenWaves = 5f;


    void Start()
    {
        startEndless = false;
        paths = WaveFunction.pathForEnemy;
        enemiesInWave = new List<Enemie>();
        if (paths.Count != 0)
        {
            CalculateWaveWeight();
            FillEnemieList();
            StartCoroutine(SpawnWave());
        }
        else
        {
            Debug.Log("NULL");
        }
    }

    void Update()
    {
        if (startEndless) {

            //Check if all enemies are dead
            if (spawnCounter == enemiesInWave.Count)
            {
                //If all enemies are dead, start the CountDown for the next wave
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    //Start a new wave
                    waveRound++;
                    ClearWave();
                    CalculateWaveWeight();
                    FillEnemieList();
                    StartCoroutine(SpawnWave());
                }
            }
        }
    }

    void CalculateWaveWeight()
    {
        //Calculate the weight of the current wave
        //Before the 20th wave, the weight is logarithmic function
        //After the 20th wave, the weight is a exponential function
        if (waveRound <= 20)
        {
            currentMaxWeight = (int)(Mathf.Log(waveRound, 2) * 10);
            Debug.Log(currentMaxWeight);
        }
        else
        {
            currentMaxWeight = (int)(Mathf.Pow(waveRound, 2) / 10);
            Debug.Log(currentMaxWeight);
        }
    }

    public void FillEnemieList()
    {
        //Fill the list random with enemies
        int current = 0;
        while (current <= currentMaxWeight)
        {
                //Randomize the enemy
                //Get a random enemie
                int randomEnemie = Random.Range(0, enemies.Count);
            //Check if the enemie is a boss
            //If the enemie is a boss, add it to the list
            if (enemies[randomEnemie].boss == true && waveRound % 20 == 0)
            {
                enemiesInWave.Add(enemies[randomEnemie]);
                current += enemies[randomEnemie].weight;
            }
            //If the enemie is not a boss, check if the weight is too high
            //If the weight is too high, get another enemie
            else if (enemies[randomEnemie].boss == false && current + enemies[randomEnemie].weight <= currentMaxWeight)
            {
                enemiesInWave.Add(enemies[randomEnemie]);
                current += enemies[randomEnemie].weight;
            }
            //If the weight is not too high, add the enemie to the list
            else
            {
                enemiesInWave.Add(enemies[randomEnemie]);
                current += enemies[randomEnemie].weight;
            }
        }
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesInWave.Count; i++)
        {
            Instantiate(enemiesInWave[i].enemie, new Vector3(paths[0].Width, 0.6f, paths[0].Height), Quaternion.identity);
            enemiesInWave[i].enemie.GetComponent<EnemyPathFinder>().enemie = enemiesInWave[i];
            enemiesInWave[i].enemie.GetComponent<EnemyPathFinder>().enabled = true;
            spawnCounter++;

            yield return new WaitForSeconds(1);
        }
        startEndless = true;
        timeBetweenWaves = 5f;
    }

    void ClearWave()
    {
        spawnCounter = 0;
        enemiesInWave.Clear();
    }

    public void StartGame()
    {
        Time.timeScale = 1; 
        gameObject.GetComponent<WaveSpawner>().enabled = true;
    }
}
