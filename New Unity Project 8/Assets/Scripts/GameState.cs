using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameState : MonoBehaviour {
    private int gamePoint = 0 ;

    //[SerializeField]
    public Text text;

    public Slider slider;

    private MapCreator mapCreator;

    private int healthPoint = 3;

    public int HealthPoint
    {
        get
        {
            return healthPoint;
        }

        set
        {
            healthPoint = value;
        }
    }

    public int GamePoint
    {
        get
        {
            return gamePoint;
        }

        set
        {
            gamePoint = value;
        }
    }

    private void Awake()
    {
        mapCreator = GetComponent<MapCreator>();

        

    }
   
	// Use this for initialization
	void Start () {
		
	}

   

    public IEnumerator loadScene()
    {

        yield return new WaitForSeconds(2.1f);
        

    }
    // Update is called once per frame
    void Update () {
        GamePoint = mapCreator.TilesCreated * 10;
        slider.value = healthPoint;
        text.text = GamePoint.ToString();

        if(healthPoint == 0)
        {
            Time.timeScale = 0;
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene("finalScene");
            //StartCoroutine(loadScene());
            
        }

    }
}
