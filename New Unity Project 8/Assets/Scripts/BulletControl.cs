using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            Destroy(other.gameObject);
            GetComponent<AudioSource>().Play();

           
        }else if(other.tag == "Tile")
        {
            Destroy(gameObject);
        }


    }
}
