﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour {
    
    public IEnumerator lateDestroy(GameObject envTileCube,GameObject skeleton,GameObject item)
    {
        
        GetComponent<Rigidbody>().useGravity = true;
        if (skeleton != null)
        {
            skeleton.GetComponent<Rigidbody>().useGravity = true;
        }
        if (item != null)
        {
            item.GetComponent<Rigidbody>().useGravity = true;
        }


        yield return new WaitForSeconds(1.3f);
        Destroy(envTileCube);
        Destroy(skeleton);
        Destroy(item);
        Destroy(gameObject);
    }
	
}
