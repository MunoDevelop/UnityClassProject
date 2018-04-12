using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile
{
    private GameObject prefeb;
    private float xPos;
    private float yPos;
    private bool isPassed;
    public Tile(GameObject prf,float xPosition,float yPosition)
    {
        prefeb = prf;
        xPos = xPosition;
        yPos = yPosition;
        IsPassed = false;
    }

    public GameObject Prefeb
    {
        get
        {
            return prefeb;
        }

        set
        {
            prefeb = value;
        }
    }

    public float XPos
    {
        get
        {
            return xPos;
        }

        set
        {
            xPos = value;
        }
    }

    public float YPos
    {
        get
        {
            return yPos;
        }

        set
        {
            yPos = value;
        }
    }

    public bool IsPassed
    {
        get
        {
            return isPassed;
        }

        set
        {
            isPassed = value;
        }
    }
}




public class MapCreator : MonoBehaviour {
    [SerializeField]
    Transform player;


    List<Tile> tileList = new List<Tile>();
    private int tilesCreated = 0;

    [SerializeField]
    private float tileRadious;
    [SerializeField]
    private GameObject tilePrefeb;
    [SerializeField]
    private float noiseSeed;
    [SerializeField]
    private float noiseLevel;



    internal List<Tile> TileList
    {
        get
        {
            return tileList;
        }

        set
        {
            tileList = value;
        }
    }

    void createTile()
    {
        
        float yPos = Perlin.Noise(tilesCreated / noiseSeed) *noiseLevel;
        float xPos = tileRadious * tilesCreated+tileRadious/2.0f;
        GameObject instance = Instantiate(tilePrefeb, new Vector3(xPos, yPos, 0), Quaternion.identity);

        if (yPos < -1.7f)
        {
            instance.GetComponent<BoxCollider>().enabled = false;
            instance.GetComponent<Renderer>().enabled = false;
        }


        Tile tile = new Tile(instance, xPos,yPos);
        tileList.Add(tile);
        tilesCreated++;
        //selectMaterial();
    }

    int getClosestTileIndex(float playerxPos)
    {
        float closestDistance = 1000;

        int tileIndex = 0;

        foreach(Tile tile in TileList)
        {
            if (Mathf.Abs(tile.XPos - playerxPos)<closestDistance)
            {
                closestDistance = Mathf.Abs(tile.XPos - playerxPos);
                tileIndex = tileList.IndexOf(tile);
            }
        }
        return tileIndex;
    }

    public Tile getPlayerTile(float playerxPos)
    {
        int closestTileIndex = getClosestTileIndex(playerxPos);

        TileList[closestTileIndex].IsPassed = true;


        return TileList[closestTileIndex];
    }

    public Tile getPlayerNextTile(float playerxPos)
    {
        int nextTileIndex = TileList.IndexOf(getPlayerTile(playerxPos)) + 1;


        return TileList[nextTileIndex];
    }

    void Awake () {
       for(int i = 0; i < 50; i++)
        {
            createTile();
        }
    }

    void replaceTile(float playerxPos)
    {
        int countPassedTile = 0;

        foreach(Tile tile in tileList)
        {
            if (tile.IsPassed == true)
            {
                countPassedTile++;
            }
        }

        if (countPassedTile <= 3)
        {
            return;
        }
        else
        {
            //Debug.Log(TileList[0].Prefeb);
            StartCoroutine (TileList[0].Prefeb.GetComponent<BlockControl>().lateDestroy());
            TileList.RemoveAt(0);
            createTile();
        }
        

    }
    
    void Update()
    {
        replaceTile(player.position.x);
    }
}
