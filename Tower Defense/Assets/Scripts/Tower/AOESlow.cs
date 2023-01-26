using UnityEngine;

public class AOESlow : MonoBehaviour
{  
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyPathFinder>().enemie.speed = (int)other.GetComponent<EnemyPathFinder>().enemie.speed / 2f;
            Debug.Log("Enemy");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyPathFinder>().enemie.speed = (int)other.GetComponent<EnemyPathFinder>().enemie.speed;
        }
    }
   
}
