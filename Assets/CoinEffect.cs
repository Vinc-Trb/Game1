using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public GameObject confettiPrefab;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(confettiPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Debug.Log("🎉 Münze eingesammelt!");
        }
    }
}

