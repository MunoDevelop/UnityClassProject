using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr : MonoBehaviour {
    public Animator animator;

    public Transform flag ;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        time -= Mathf.Floor(time);

        Debug.Log(time);
        string currentState = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        

        if (time > 0.98 && (currentState != "Run"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, flag.position.z);
        }
    }
}
