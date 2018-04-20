using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour {

    Animator animator;
   

    private GameState gameState;


    public void beAttacked()
    {
        animator.Play("beAttacked");
    }
    public void toDie()
    {
        animator.Play("die");
        StartCoroutine(lateDestroy());
    }
    public IEnumerator  lateDestroy()
    {
        yield return new WaitForSeconds(1.1f);
        Destroy(gameObject);
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
        GameObject gameRoot = GameObject.Find("GameRoot");

        gameState = gameRoot.GetComponent<GameState>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        bool useGravity = GetComponent<Rigidbody>().useGravity;
        if (other.tag == "Player"&& (useGravity==false))
        {
            gameState.HealthPoint--;
        }
       ;
        
    }
}
