using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinder : MonoBehaviour
{

    private Vector3 target;
    private int wavepointIndex = 0;
    List<Node> pathfinder;

    public Enemie enemie;
    public int speed;
    
    void Start(){
        pathfinder = WaveFunction.pathForEnemy;
        speed = enemie.speed;
        target = new Vector3(pathfinder[wavepointIndex].Width, 0.6f, pathfinder[wavepointIndex].Height);
        //Ignoriert alle Collisions --> Damit Enemies sich auch überholen können
        for (int i = 0; i < 6; i++)
        {
            if(i == 2)
            {
                continue;
            }
            else
            {
                Physics.IgnoreLayerCollision(0, i, true);                          
            }
        }

    }
    

   void Update(){
        Vector3 dir = target - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if(Vector3.Distance(transform.position, target) <= 0.1f){
            GetNextWaypoint();
        }
   }

    void GetNextWaypoint(){
        if(wavepointIndex >= pathfinder.Count - 1){
            EndPath();
            return;
        }
        wavepointIndex++;
        target = new Vector3(pathfinder[wavepointIndex].Width, 0.6f, pathfinder[wavepointIndex].Height);
   }

    //If the Enemie reached the End of the Path, destroy it
    void EndPath()
    {
        Destroy(gameObject);
    }

}
