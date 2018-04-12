using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour {
    
    public IEnumerator lateDestroy()
    {
        
        GetComponent<Rigidbody>().useGravity = true;
       
        yield return new WaitForSeconds(1.3f);
        Destroy(gameObject);
    }
	
	void Start () {


    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
