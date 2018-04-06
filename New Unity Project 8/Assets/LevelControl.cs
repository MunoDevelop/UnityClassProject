using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public struct Range
    { // 범위를표현하는구조체.
        public int min; // 범위의최솟값.
public int max; // 범위의최댓값.
};
    public float end_time; // 종료시간.
    public float player_speed; // 플레이어의속도.
    public Range floor_count; // 발판블록수의범위.
    public Range hole_count; // 구멍의개수범위.
    public Range height_diff; // 발판의높이범위.
    public LevelData()
    {
        this.end_time = 15.0f; // 종료시간초기화.
        this.player_speed = 6.0f; // 플레이어의속도초기화.
        this.floor_count.min = 10; // 발판블록수의최솟값을초기화.
        this.floor_count.max = 10; // 발판블록수의최댓값을초기화.
        this.hole_count.min = 2; // 구멍개수의최솟값을초기화.
        this.hole_count.max = 6; // 구멍개수의최댓값을초기화.
        this.height_diff.min = 0; // 발판높이변화의최솟값을초기화.
        this.height_diff.max = 0; // 발판높이변화의최댓값을초기화.
    }
}

public class LevelControl : MonoBehaviour {

    private List<LevelData> level_datas = new List<LevelData>();
    public int HEIGHT_MAX = 20;
public int HEIGHT_MIN = -4;

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

    public float getPlayerSpeed() { return (this.level_datas[this.level].player_speed); }


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
    public void update(float passage_time)
    {
        this.current_block.current_count++;
        if (this.current_block.current_count >= this.current_block.max_count)
        {
            this.previous_block = this.current_block;
            this.current_block = this.next_block;
            this.clear_next_block(ref this.next_block);
            // this.update_level(ref this.next_block, this.current_block);
            this.update_level(ref this.next_block, this.current_block, passage_time);
        }
        this.block_count++;
    }


    public void loadLevelData(TextAsset level_data_text)
    {
        string level_texts = level_data_text.text;// 텍스트데이터를문자열로가져온다.
        string[] lines = level_texts.Split('\n');// 개행코드'\'마다분할해서문자열배열에넣는다.
                                                 // lines 내의각행에대해서차례로처리해가는루프.
        foreach (var line in lines)
        {
            if (line == "")
            { // 행이빈줄이면.
                continue; // 아래처리는하지않고반복문의처음으로점프한다.
            };
            Debug.Log(line); // 행의내용을디버그출력한다.
            string[] words = line.Split(); // 행내의워드를배열에저장한다.
            int n = 0;
            // LevelData형변수를생성한다.
            // 현재처리하는행의데이터를넣어간다.
            LevelData level_data = new LevelData();
            // words내의각워드에대해서순서대로처리해가는루프.
            foreach (var word in words)
            {
                if(word.StartsWith("#")) { // 워드의시작문자가#이면.
                    break;
                } // 루프탈출.
                if (word == "")
                { // 워드가텅비었으면.
                    continue;
                } // 루프의시작으로점프한다.

                // n 값을0, 1, 2,...7로변화시켜감으로써8항목을처리한다.
                // 각워드를플롯값으로변환하고level_data에저장한다.
                switch (n)
                {
                    case 0: level_data.end_time = float.Parse(word); break;
                    case 1: level_data.player_speed = float.Parse(word); break;
                    case 2: level_data.floor_count.min = int.Parse(word); break;
                    case 3: level_data.floor_count.max = int.Parse(word); break;
                    case 4: level_data.hole_count.min = int.Parse(word); break;
                    case 5: level_data.hole_count.max = int.Parse(word); break;
                    case 6: level_data.height_diff.min = int.Parse(word); break;
                    case 7: level_data.height_diff.max = int.Parse(word); break;
                }
                n++;
            }
            if(n >= 8) { // 8항목(이상)이제대로처리되었다면.
                this.level_datas.Add(level_data);// List 구조의level_datas에level_data를추가한다.
            } else { // 그렇지않다면(오류의가능성이있다).
                if (n == 0)
                {// 1워드도처리하지않은경우는주석이므로.
                 // 문제없다. 아무것도하지않는다.
                }
                else
                { // 그이외이면오류다.
                  // 데이터개수가맞지않다는것을보여주는오류메시지를표시한다.
                    Debug.LogError("[LevelData] Out of parameter.\n");
                }
            }
        }
        if (this.level_datas.Count == 0)
        { // level_datas에데이터가하나도없으면.
            Debug.LogError("[LevelData] Has no data.\n"); // 오류메시지를표시한다.
            this.level_datas.Add(new LevelData()); // level_datas에기본LevelData를하나추가해둔다.
        }
    }

    private void update_level(ref CreationInfo current, CreationInfo previous, float passage_time)
    { // 새인수passage_time으로플레이경과시간을받는다.
      // 레벨1~레벨5를반복한다.
        float local_time = Mathf.Repeat(passage_time, this.level_datas[this.level_datas.Count - 1].end_time);
        // 현재레벨을구한다.
        int i;
        for (i = 0; i < this.level_datas.Count - 1; i++)
        {
            if (local_time <= this.level_datas[i].end_time)
            {
                break;
            }
        }
        this.level = i;
        current.block_type = Block.TYPE.FLOOR;
        current.max_count = 1;
        if (this.block_count >= 10)
        {
            // 현재레벨용레벨데이터를가져온다.
            LevelData level_data;
            level_data = this.level_datas[this.level];
            switch (previous.block_type)
            {
                case Block.TYPE.FLOOR: // 이전블록이바닥인경우.
                    current.block_type = Block.TYPE.HOLE; // 이번엔구멍을만든다.
                                                          // 구멍크기의최솟값~최댓값사이의임의의값.
                    current.max_count = Random.Range(level_data.hole_count.min, level_data.hole_count.max);
                    current.height = previous.height; // 높이를이전과같이한다.
                    break;
                case Block.TYPE.HOLE: // 이전블록이구멍인경우.
                    current.block_type = Block.TYPE.FLOOR;// 이번엔바닥을만든다.
                                                          // 바닥길이의최솟값~최댓값사이의임의의값.
                    current.max_count = Random.Range(level_data.floor_count.min, level_data.floor_count.max);
                    // 바닥높이의최솟값과최댓값을구한다.
                    int height_min = previous.height + level_data.height_diff.min;
                    int height_max = previous.height + level_data.height_diff.max;
                    height_min = Mathf.Clamp(height_min, HEIGHT_MIN, HEIGHT_MAX); // 최솟값과최대값사이의값을강제로넣기위해사용
                    height_max = Mathf.Clamp(height_max, HEIGHT_MIN, HEIGHT_MAX);
                    // 바닥높이의최솟값~최댓값사이의임의의값.
                    current.height = Random.Range(height_min, height_max);
                    break;
            }
        }
    }
}
