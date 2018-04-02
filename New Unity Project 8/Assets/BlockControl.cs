using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour {
    public MapCreator map_creator= null; // MapCreator를보관하는변수.
	// Use this for initialization
	void Start () {
        // MapCreator를가져와서멤버변수map_creator에보관.
        map_creator = GameObject.Find("GameRoot").GetComponent<MapCreator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (this.map_creator.isDelete(this.gameObject))
        {// 카메라에게나안보이냐고물어보고안보인다고대답하면,
            GameObject.Destroy(this.gameObject); // 자기자신을삭제.
        }
    }
}
