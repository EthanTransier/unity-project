using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackArea : MonoBehaviour
{
    private int damage = 40;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Health>() != null)
        {
            // health.Damage(damage);
        }
    }
}
