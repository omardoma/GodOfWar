using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		Destroy(this.gameObject);
		if(collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<PlayerController>().KratosGotHit();
            // collider.gameObject.GetComponent<HealthPoints>().health -= attackValue;
            //Start player getting hit animation
        }
    }
}
