using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {

    public static float BLOCK_WIDTH = 1.0f; // 블록의폭.
    public static float BLOCK_HEIGHT = 0.2f; // 블록의높이.
    public static int BLOCK_NUM_IN_SCREEN = 24; // 화면내에들어가는블록의개수.

    private struct FloorBlock
    { // 블록에관한정보를모아서관리하는구조체(여러개의정보를하나로묶을때사용).
        public bool is_created; // 블록이만들어졌는가.
        public Vector3 position; // 블록의위치.
    };

    private FloorBlock last_block; // 마지막에생성한블록.
    private PlayerControl player = null; // 씬상의Player를보관.
    private BlockCreator block_creator; // BlockCreator를보관.


    // Use this for initialization
    void Start () {
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        this.last_block.is_created = false;
        this.block_creator = this.gameObject.GetComponent<BlockCreator>();
    }

    private void create_floor_block()
    {
        Vector3 block_position; // 이제부터만들블록의위치.
        if(!this.last_block.is_created) { // last_block이생성되지않은경우.
                                          // 블록의위치를일단Player와같게한다.
            block_position = this.player.transform.position;
            // 그러고나서블록의X 위치를화면절반만큼왼쪽으로이동.
            block_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
            // 블록의Y위치는0으로.
            block_position.y = 0.0f;
        } else { // last_block이생성된경우
          // 이번에만들블록의위치를직전에만든블록과같게.
            block_position = this.last_block.position;
        }
        block_position.x += BLOCK_WIDTH;// 블록을1블럭만큼오른쪽으로이동.
                                        // BlockCreator스크립트의createBlock()메소드에생성을지시!.
        this.block_creator.createBlock(block_position);// 이제까지의코드에서설정한block_position을건네준다.
        this.last_block.position = block_position;// last_block의위치를이번위치로갱신.
        this.last_block.is_created = true;// 블록이생성되었으므로last_block의is_created를true로설정.
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어의X위치를가져온다.
        float block_generate_x = this.player.transform.position.x;
        // 그리고대략반화면만큼오른쪽으로이동.
        // 이위치가블록을생성하는문턱값이된다.
        block_generate_x += BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN + 1) / 2.0f;
        // 마지막에만든블록의위치가문턱값보다작으면.
        while (this.last_block.position.x < block_generate_x)
        {
            // 블록을만든다.
            this.create_floor_block();
        }
    }
}
