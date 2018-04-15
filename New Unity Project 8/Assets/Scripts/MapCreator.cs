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
    private GameObject environmentCube;

    private GameObject skeleton;

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

    public GameObject EnvironmentCube
    {
        get
        {
            return environmentCube;
        }

        set
        {
            environmentCube = value;
        }
    }

    public GameObject Skeleton
    {
        get
        {
            return skeleton;
        }

        set
        {
            skeleton = value;
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
    private float environmentTileRate;
    [SerializeField]
    private GameObject skeletonPrefeb;
    [SerializeField]
    private float skeletonRate;

    [SerializeField]
    private GameObject environmentTilePrefeb;

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
        float yPos = Mathf.PerlinNoise(tilesCreated/noiseSeed,0.0f) * noiseLevel;
        
        float xPos = tileRadious * tilesCreated+tileRadious/2.0f;
        GameObject instance = Instantiate(tilePrefeb, new Vector3(xPos, yPos, 0), Quaternion.identity);

        Tile tile = new Tile(instance, xPos, yPos);

        if (yPos < 1.8f)
        {
            instance.GetComponent<BoxCollider>().enabled = false;
            instance.GetComponent<Renderer>().enabled = false;
        }else if(Random.Range(0, 100) < skeletonRate)
        {
            tile.Skeleton = createSkeleton(xPos, yPos);
        }

        
        //----------environmentTile
        if (Random.Range(0, 100) < environmentTileRate)
        {
            tile.EnvironmentCube = createEnvironmentTile(xPos);
        }


        //----------
        


        tileList.Add(tile);
        tilesCreated++;
        //selectMaterial();
    }

    GameObject createSkeleton(float xPos,float yPos)
    {
        
          return  Instantiate(skeletonPrefeb, new Vector3(xPos, yPos+0.4f, 0), Quaternion.Euler(new Vector3(0,-90,0)));
    }

    GameObject createEnvironmentTile(float xPos)
    {
        float envX = Random.Range(xPos - 3, xPos + 3);
        float envY = 0;
        float envZ = 0;
        //50 % rate random
        if (Random.Range(0,100) < 50)
        {
            envZ = Random.Range(3, 6);
        }
        else
        {
            envZ = Random.Range(-3, -6);
        }

        envY = Random.Range(3, 7);

        return Instantiate(environmentTilePrefeb, new Vector3(envX, envY, envZ), Quaternion.Euler(new Vector3(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360))));
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

        noiseSeed = Random.Range(14.5f, 17.3f);

       for (int i = 0; i < 50; i++)
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
            StartCoroutine (TileList[0].Prefeb.GetComponent<BlockControl>().lateDestroy(TileList[0].EnvironmentCube,tileList[0].Skeleton));
            TileList.RemoveAt(0);
            createTile();
        }
        

    }
    
    void Update()
    {
        replaceTile(player.position.x);
    }
}
