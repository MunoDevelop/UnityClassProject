using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static float ACCELERATION = 10.0f; // 가속도.
    public static float SPEED_MIN = 4.0f; // 속도의최솟값.
    public static float SPEED_MAX = 8.0f; // 속도의최댓값.
    public static float JUMP_HEIGHT_MAX = 3.0f;// 점프높이.
    public static float JUMP_KEY_RELEASE_REDUCE = 0.5f; // 점프후의감속도.

    public enum STEP
    { // Player의각종상태를나타내는자료형(*열거체)
        NONE = -1, // 상태정보없음.
        RUN = 0, // 달린다.
        JUMP, // 점프.
        MISS, // 실패.
        NUM, // 상태가몇종류있는지보여준다(=3).
    };
    public STEP step = STEP.NONE; // Player의현재상태.
    public STEP next_step = STEP.NONE; // Player의다음상태.
    public float step_timer = 0.0f; // 경과시간.
    private bool is_landed = false; // 착지했는가.
    private bool is_colided = false; // 뭔가와충돌했는가.
    private bool is_key_released = false; // 버튼이떨어졌는가.

    public static float NARAKU_HEIGHT = -5.0f;

    private void check_landed() // 착지했는지조사
    {
        this.is_landed = false; // 일단false로설정.
        do
        {
            Vector3 s = this.transform.position; // Player의현재위치.
            Vector3 e = s + Vector3.down * 1.0f; // s부터아래로1.0f로이동한위치.
            RaycastHit hit;
            if (!Physics.Linecast(s, e, out hit))
            { // s부터e 사이에아무것도없을때.
                break; // 아무것도하지않고do~while루프를빠져나감(탈출구로).
            }
            // s부터e 사이에뭔가있을때아래의처리가실행.
            if (this.step == STEP.JUMP)
            { // 현재, 점프상태라면.
                if (this.step_timer < Time.deltaTime * 3.0f)
                {// 경과시간이3.0f 미만이라면.
                    break; // 아무것도하지않고do~while루프를빠져나감(탈출구로).
                }
            }
            // s부터e 사이에뭔가있고JUMP 직후가아닐때만아래가실행.
            this.is_landed = true;
        } while (false);
        // 루프의탈출구.
    }


    void Start()
    {
        this.next_step = STEP.RUN;
    }

    // Update is called once per frame

    void Update()
    {
        Vector3 velocity = this.GetComponent<Rigidbody>().velocity; // 속도를설정.
        this.check_landed(); // 착지상태인지체크.


        switch (this.step)
        {
            case STEP.RUN:
            case STEP.JUMP:
                // 현재위치가한계치보다아래면.
                if (this.transform.position.y < NARAKU_HEIGHT)
                {
                    this.next_step = STEP.MISS; // '실패' 상태로한다.
                }
                break;
        }

        this.step_timer += Time.deltaTime; // 경과시간을진행한다.
                                           // 다음상태가정해져있지않으면상태의변화를조사한다.
        if(this.next_step == STEP.NONE) {
            switch (this.step)
            { // Player의현재상태로분기.
                case STEP.RUN: // 달리는중일때.
                    if (!this.is_landed)
                    {
                        // 달리는중이고착지하지않은경우아무것도하지않는다.
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            // 달리는중이고착지했고왼쪽버튼이눌렸다면.
                            // 다음상태를점프로변경.
                            this.next_step = STEP.JUMP;
                        }
                    }
                    break;
                case STEP.JUMP: // 점프중일때.
                    if (this.is_landed)
                    {
                        // 점프중이고착지했다면다음상태를주행중으로변경.
                        this.next_step = STEP.RUN;
                    }
                    break;
            }
        }
        // (계속)
        // '다음정보'가'상태정보없음'이아닌동안(상태가변할때만).
        while (this.next_step != STEP.NONE)
        {
            this.step = this.next_step;// '현재상태'를'다음상태'로갱신.
            this.next_step = STEP.NONE;// '다음상태'를'상태없음'으로변경.
            switch (this.step)
            { // 갱신된'현재상태'가.
                case STEP.JUMP: // '점프'일때.
                                // 최고도달점높이(JUMP_HEIGHT_MAX)까지점프할수있는속도를계산.
                    velocity.y = Mathf.Sqrt(2.0f * 9.8f * PlayerControl.JUMP_HEIGHT_MAX);
                    // '버튼이떨어졌음을나타내는플래그'를클리어한다.
                    this.is_key_released = false;
                    break;
            }
            // 상태가변했으므로경과시간을제로로리셋.
            this.step_timer = 0.0f;
        }
        // 상태별로매프레임갱신처리.
        switch (this.step)
        {
            case STEP.RUN:// 달리는중일때.
                          // 속도를높인다.
                velocity.x += PlayerControl.ACCELERATION * Time.deltaTime;
                // 속도가최고속도제한을넘으면.
                if (Mathf.Abs(velocity.x) > PlayerControl.SPEED_MAX)
                {
                    // 최고속도제한이하로유지한다.
                    velocity.x *= PlayerControl.SPEED_MAX / Mathf.Abs(this.GetComponent<Rigidbody>().velocity.x);
                }
                break;
            // (계속)

            case STEP.JUMP: // 점프중일때.
                do
                {
                    // '버튼이떨어진순간'이아니면.
                    if (!Input.GetMouseButtonUp(0))
                    {
                        break; // 아무것도하지않고루프를빠져나간다.
                    }
                    // 이미감속된상태면(두번이상감속하지않도록).
                    if (this.is_key_released)
                    {
                        break;// 아무것도하지않고루프를빠져나간다.
                    }
                    // 상하방향속도가0 이하면(하강중이라면).
                    if (velocity.y <= 0.0f)
                    {
                        break;// 아무것도하지않고루프를빠져나간다.
                    }
                    // 버튼이떨어져있고상승중이라면감속시작.
                    // 점프의상승은여기서끝.
                    velocity.y *= JUMP_KEY_RELEASE_REDUCE;
                    this.is_key_released = true;
                } while (false);
                break;
        }
        // Rigidbody의속도를위에서구한속도로갱신.
        // (이행은상태에관계없이매번실행된다).
        this.GetComponent<Rigidbody>().velocity = velocity;
    }

}
