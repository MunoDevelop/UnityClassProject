using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour {
    public struct CreationInfo{
public Block.TYPE block_type; // 블록의종류.
public int max_count; // 블록의최대개수.
public int height; // 블록을배치할높이.
public int current_count; // 작성한블록의개수.
};
public CreationInfo previous_block; // 이전에어떤블록을만들었는가.
public CreationInfo current_block; // 지금어떤블록을만들어야하는가.
public CreationInfo next_block; // 다음에어떤블록을만들어야하는가.
public int block_count= 0; // 생성한블록의총수.
public int level = 0; // 난이도.
                      // Use this for initialization
    private void clear_next_block(ref CreationInfo block)
    {
        // 전달받은블록(block)을초기화.
        block.block_type = Block.TYPE.FLOOR;
        block.max_count = 15;
        block.height = 0;
        block.current_count = 0;
    }
    // 프로필노트를초기화한다.
    public void initialize()
    {
        this.block_count = 0; // 블록의총수를초기화.
                              // 이전, 현재, 다음블록을각각.
                              // clear_next_block()에넘겨서초기화한다.
        this.clear_next_block(ref this.previous_block);
        this.clear_next_block(ref this.current_block);
        this.clear_next_block(ref this.next_block);
    }

    private void update_level(ref CreationInfo current, CreationInfo previous)
    {
        switch (previous.block_type)
        {
            case Block.TYPE.FLOOR: // 이번블록이바닥일경우.
                current.block_type = Block.TYPE.HOLE; // 다음번은구멍을만든다.
                current.max_count = 5; // 구멍은5개만든다.
                current.height = previous.height; // 높이를이전과같게한다.
                break;
            case Block.TYPE.HOLE: // 이번블록이구멍일경우.
                current.block_type = Block.TYPE.FLOOR; // 다음은바닥만든다.
                current.max_count = 10; // 바닥은10개만든다.
                break;
        }
    }
    public void update()
    { // *Update()가아님. create_floor_block() 메서드에서호출
        this.current_block.current_count++;// 이번에만든블록개수를증가.
                                           // 이번에만든블록개수가max_count이상이면.
        if(this.current_block.current_count >= this.current_block.max_count) {
            this.previous_block = this.current_block;
            this.current_block = this.next_block;
            this.clear_next_block(ref this.next_block); // 다음에만들블록의내용을초기화.
            this.update_level(ref this.next_block, this.current_block); // 다음에만들블록을설정.
        }
        this.block_count++; // 블록의총수를증가.
    }
}
