using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotation : MonoBehaviour
{
    private List<GameObject> enemies;
    public float turnSpeed = 10f;

    void Start()
    {
        enemies = new List<GameObject>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemies.Remove(other.gameObject);
        }
    }

    void Update()
    {
        if (enemies.Count > 0)
        {
            LockOntarget();
        }
    }

    void LockOntarget()
    {
        Vector3 dir = enemies[0].transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
