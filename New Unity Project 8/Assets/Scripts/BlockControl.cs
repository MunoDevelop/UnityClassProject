using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour {
    
    public IEnumerator lateDestroy(GameObject envTileCube,GameObject skeleton)
    {
        
        GetComponent<Rigidbody>().useGravity = true;
        skeleton.GetComponent<Rigidbody>().useGravity = true;

        yield return new WaitForSeconds(1.3f);
        Destroy(envTileCube);
        Destroy(skeleton);
        Destroy(gameObject);
    }
	
}
