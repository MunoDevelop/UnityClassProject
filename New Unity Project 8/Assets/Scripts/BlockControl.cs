using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour {
    
    public IEnumerator lateDestroy(GameObject envTileCube)
    {
        
        GetComponent<Rigidbody>().useGravity = true;
       
        yield return new WaitForSeconds(1.3f);
        Destroy(envTileCube);
        Destroy(gameObject);
    }
	
	void Start () {


    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
