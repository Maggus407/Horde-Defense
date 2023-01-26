using UnityEngine;

public class AOESlow : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyPathFinder>().speed = other.GetComponent<EnemyPathFinder>().speed / 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyPathFinder>().speed = other.GetComponent<EnemyPathFinder>().enemie.speed;
        }
    }
   
}
