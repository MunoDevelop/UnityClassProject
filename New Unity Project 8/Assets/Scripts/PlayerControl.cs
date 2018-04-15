using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerState
{
    Run, Jump, Jab,SamK,ScrewKick,HiKick,SpinKick,RisingP
}

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    GameObject gameRoot;
    [SerializeField]
    private float characterSpeedBase = 5;
    [SerializeField]
    private float characterJumpPower = 6;

    private float characterSpeedByStateRate = 1;

    private MapCreator mapCreator;

    private Animator animator;

    private PlayerState playerState = PlayerState.Run;

    [SerializeField]
    private Transform screwKickPosition;

    List<GameObject> monsterList = new List<GameObject>();
    [SerializeField]
    private Moderator moderator;
    [SerializeField]
    private float skillXRange;


    internal PlayerState PlayerState
    {
        get
        {
            return playerState;
        }

        set
        {
            playerState = value;
        }
    }

    Vector3  NextTileMovingVector(Vector3 thisTilePos,Vector3 nextTilePos)
    {
        return nextTilePos - thisTilePos;
    }

    public void autoMoveRight()
    {
        Tile tile = mapCreator.getPlayerTile(transform.position.x);
        Tile nextTile = mapCreator.getPlayerNextTile(transform.position.x);

        Vector3 moveVec = NextTileMovingVector(new Vector3(tile.XPos, tile.YPos, 0), new Vector3(nextTile.XPos, nextTile.YPos, 0));
        //Debug.Log(moveVec);
        transform.Translate(moveVec*Time.deltaTime*characterSpeedBase* characterSpeedByStateRate);
    }
  

  bool isGrounded(){
     return Physics.Raycast(transform.position, -Vector3.up,  0.3f);
 }
    

    void resetCharaterState(float time,string currentState)
    {
       
        if (time>0.9 )
        {
            playerState = PlayerState.Run;
            
            characterSpeedByStateRate = 1;
        }     


    }

    void monsterListUpdate()
    {
        monsterList.Clear();
        
        foreach (Tile tile in mapCreator.TileList){
            if (tile.Skeleton != null)
            {
                monsterList.Add(tile.Skeleton);
            }
        }
        //Debug.Log(monsterList.Count);
    }

    void sendToModerator(PlayerSkill ps)
    {
        float playerXPosition = transform.position.x;
         
        foreach (GameObject monster in monsterList)
        {
            float skeletonXPosition = monster.transform.GetChild(1).position.x;

            //Debug.Log(skeletonXPosition);
            //if monster in x range 
            if (skeletonXPosition > playerXPosition
                && (playerXPosition + skillXRange > skeletonXPosition))
            {
               // Debug.Log(skeletonXPosition);
                moderator.calculateDamage(ps, monster);
                moderator.calculateEffect(ps, monster);
            }
        }
    }


private void Awake()
    {
        mapCreator = gameRoot.GetComponent<MapCreator>();

        animator = GetComponent<Animator>();
       
    }
    private void Update()
    {
        string currentState = "";

        float time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        time -= Mathf.Floor(time);
        //Debug.Log(time);
        if (animator.GetCurrentAnimatorClipInfo(0).Length > 0)
        {
             currentState = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        }

        

        autoMoveRight();
        resetCharaterState(time,currentState);


        monsterListUpdate();
    }



    private void LateUpdate()
    {
        
        //jump
       if (Input.GetKeyDown(KeyCode.K)&& isGrounded()
            && (playerState==PlayerState.Run)
            ) { 
            playerState = PlayerState.Jump;
            //animator.SetBool("Jump", true);
            animator.Play("Jump");
            //animator.avatar = jumpingUCAvatar;
            GetComponent<Rigidbody>().velocity += new Vector3(0, characterJumpPower, 0);
       }
        //Jab
        if (Input.GetKeyDown(KeyCode.J) && isGrounded()
             && (playerState == PlayerState.Run)
             )
        {
            playerState = PlayerState.Jab;
            animator.Play("Jab");
            characterSpeedByStateRate = 0;
            sendToModerator(PlayerSkill.Jab);
        }
        //Hikick
        if (Input.GetKeyDown(KeyCode.U) && isGrounded()
             && (playerState == PlayerState.Run)
             )
        {
            playerState = PlayerState.HiKick;
            animator.Play("HiKick");
            characterSpeedByStateRate = 0;
            sendToModerator(PlayerSkill.HiKick);
        }
        //SpinKick
        if (Input.GetKeyDown(KeyCode.I) && isGrounded()
             && (playerState == PlayerState.Run)
             )
        {
            playerState = PlayerState.SpinKick;
            animator.Play("SpinKick");
            characterSpeedByStateRate = 0;
            sendToModerator(PlayerSkill.SpinKick);
        }
        //SamK
        if (Input.GetKeyDown(KeyCode.O) && isGrounded()
             && (playerState == PlayerState.Run)
             )
        {
            playerState = PlayerState.SamK;
            animator.Play("SamK");
            characterSpeedByStateRate = 0;
            sendToModerator(PlayerSkill.SamK);
        }
        //ScrewKick
        if (Input.GetKeyDown(KeyCode.Y) && isGrounded()
           && (playerState == PlayerState.Run)
           )
        {
            playerState = PlayerState.ScrewKick;
            animator.Play("ScrewKick");
            characterSpeedByStateRate = 0;
            sendToModerator(PlayerSkill.ScrewKick);
            //GetComponent<Rigidbody>().velocity += new Vector3(4, 0, 0);
        }
        //RisingP
        if (Input.GetKeyDown(KeyCode.H) && isGrounded()
           && (playerState == PlayerState.Run)
           )
        {
            playerState = PlayerState.RisingP;
            animator.Play("RisingP");
            characterSpeedByStateRate = 0;
            sendToModerator(PlayerSkill.RisingP);
        }

    }
}
