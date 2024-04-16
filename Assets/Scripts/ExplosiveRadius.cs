using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ExplosiveRadius : MonoBehaviour
{

    private int force = 2500;

    private AudioSource audioSource;

    public GameObject parent;

    void Start(){
        if (audioSource == null){
        audioSource = GetComponent<AudioSource>();
        }
    }

    public void OnTriggerEnter(Collider col){
        // Debug.Log("Hi"); //
        if (col.gameObject.CompareTag("Player")){

            col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, 0,0);
                
            
    
                audioSource.Play();
            
            Destroy(parent);
            }
        }
    
}
