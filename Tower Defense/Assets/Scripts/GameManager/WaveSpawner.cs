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

    private void OnEnable()
    {
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

    void CalculateWaveWeight()
    {
        //Calculate the weight of the current wave
        currentMaxWeight = waveRound * 10;
    }

    void FillEnemieList()
    {
        //Fill the list random with enemies
        int current = 0;
        while(current != currentMaxWeight)
        {
            Debug.Log("While");
            //Randomize the enemy
            //Get a random enemie
            int randomEnemie = Random.Range(0, enemies.Count);
            //Check if the enemie is a boss
            //If the enemie is a boss, add it to the list
            if (enemies[randomEnemie].boss == true && waveRound%20==0)
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

            yield return new WaitForSeconds(1);
        }
    }

    void ClearWave()
    {
        enemiesInWave.Clear();
    }

    public void StartGame()
    {
        gameObject.GetComponent<WaveSpawner>().enabled = true;
        Time.timeScale = 1;
    }
}
