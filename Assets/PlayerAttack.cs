using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject meleeAttackArea = default;
    private bool meleeAttacking = false;

    private float timeToAttack = 0.25f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        meleeAttackArea = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            MeleeAttack();
        }

        if(meleeAttacking){
            timer += Time.deltaTime;

            if(timer >= timeToAttack){
                timer = 0;
                meleeAttacking = false;
                meleeAttackArea.SetActive(meleeAttacking);
            }
        }
    }

    private void MeleeAttack(){
        meleeAttacking = true;
        meleeAttackArea.SetActive(meleeAttacking);
    }
}
