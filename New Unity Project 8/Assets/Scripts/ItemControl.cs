using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour {

    PlayerControl playerControl;
	// Use this for initialization
	void Start () {
        playerControl = GameObject.Find("unitychan").GetComponent<PlayerControl>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerControl.useItem();
            

            Destroy(gameObject);
        }
      

    }
}
