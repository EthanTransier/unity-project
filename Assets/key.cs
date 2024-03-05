using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{
    public float keyNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Key") || other.gameObject.CompareTag("Respawn")){
        if(SceneManager.GetActiveScene().buildIndex == 1){

        transform.position = new Vector2(186.53f, 51.83f);

        }else if(SceneManager.GetActiveScene().buildIndex == 2){

        transform.position = new Vector2(-8.18f, -2.19f);

        }else if(SceneManager.GetActiveScene().buildIndex == 3){

        transform.position = new Vector2(-28.59f, 5.82f);

        }
        if(other.gameObject.CompareTag("Key")) keyNumber += 1;
        Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Door") && keyNumber >= 3) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if(other.gameObject.CompareTag("Door") && SceneManager.GetActiveScene().buildIndex == 4) SceneManager.LoadScene(0);
    }
}