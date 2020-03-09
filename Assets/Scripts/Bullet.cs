using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
    	destroyTime = 2.5f;
       Destroy(this.gameObject, destroyTime);
    }
    void OnTriggerEnter(Collider other) {
        if(other.transform.tag != "Bullet") Destroy(this.gameObject);
	}

	void OnCollisionEnter(Collision col){
    	Destroy(this.gameObject);
    }	
}
