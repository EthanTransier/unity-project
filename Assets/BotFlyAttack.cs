using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotFlyAttack : MonoBehaviour
{
    // Start is called before the first frame update\
    public GameObject projectilePrefab;
    public float dropInterval = 3f; // Adjust as needed
    private float lastDropTime;

    // Update is called once per frame
    void Update()
    {
        // Check if it's time to drop a projectile
        if (Time.time - lastDropTime > dropInterval)
        {
            DropProjectile();
            lastDropTime = Time.time;
        }
    }

    void DropProjectile()
    {
        // Instantiate projectile at the enemy's position
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}
