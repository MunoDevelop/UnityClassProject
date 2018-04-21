using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadLevel : MonoBehaviour {

    public Text text;

    public void OnButtonClick()
    {
        Destroy(GameObject.Find("GameRoot"));
        SceneManager.LoadScene("Intro");

    }
    // Use this for initialization
    void Start () {
        text.text = GameObject.Find("GameRoot").GetComponent<GameState>().GamePoint.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
