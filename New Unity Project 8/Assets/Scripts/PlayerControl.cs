using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    GameObject gameRoot;
    private MapCreator mapCreator;

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
        transform.Translate(moveVec*Time.deltaTime*5);
    }
    /*
    public void fixKinematic()
    {
        Tile nextTile = mapCreator.getPlayerNextTile(transform.position.x);
        if (nextTile.Prefeb.GetComponent<Renderer>().isVisible&& isGrounded())
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    */

  bool isGrounded(){
     return Physics.Raycast(transform.position, -Vector3.up,  0.5f + 0.4f);
 }
     /*
    void fakeGravitySimulation()
    {
        if (isGrounded() == false)
        {
            GetComponent<Rigidbody>().velocity += new Vector3(0, -0.1f, 0);
        }
    }
     */
private void Awake()
    {
        mapCreator = gameRoot.GetComponent<MapCreator>();
       //transform.Rotate(0,90,0);
    }


    private void Update()
    {
        autoMoveRight();
       // fakeGravitySimulation();
       //s fixKinematic();
        

       if (Input.GetKeyDown(KeyCode.Mouse0)&& isGrounded())
       {
            GetComponent<Rigidbody>().velocity += new Vector3(0, 8, 0);
       }
    }
}
