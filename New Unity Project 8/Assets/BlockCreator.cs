using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour {
    public GameObject[] blockPrefabs; // 블록을저장할배열.
    private int block_count = 0; // 생성한블록의개수.

    public void createBlock(Vector3 block_position)
    {
        // 만들어야할블록의종류(흰색인가빨간색인가)를구한다.
        int next_block_type = this.block_count % this.blockPrefabs.Length;// %: 나머지를구하는연산자.
                                                                         // 블록을생성하고go에보관한다.
        GameObject go = GameObject.Instantiate(this.blockPrefabs[next_block_type]) as GameObject;
        go.transform.position = block_position; // 블록의위치를이동.
        this.block_count++; // 블록의개수를증가.
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
