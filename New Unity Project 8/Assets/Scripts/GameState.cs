using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        mapCreator = GetComponent<MapCreator>();

        

    }
   
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gamePoint = mapCreator.TilesCreated * 10;
        slider.value = healthPoint;
        text.text = gamePoint.ToString();

        if(healthPoint == 0)
        {
            Time.timeScale = 0;
        }

    }
}
