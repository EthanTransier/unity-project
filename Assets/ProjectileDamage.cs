using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public float damage = 2500f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().health -= damage;
            Debug.Log("Damage");
        }
    }
}