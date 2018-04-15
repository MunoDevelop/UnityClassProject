using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSkill
{
    Jab, SamK, ScrewKick, HiKick, SpinKick, RisingP
}

public class Moderator : MonoBehaviour {


    public void calculateDamage(PlayerSkill ps,GameObject go)
    {
        switch (ps)
        {
            case PlayerSkill.Jab:
                break;
            case PlayerSkill.SamK:
                break;
            case PlayerSkill.ScrewKick:
                break;
            case PlayerSkill.HiKick:
                break;
            case PlayerSkill.SpinKick:
                break;
            case PlayerSkill.RisingP:
                break;
        }
    }

    public void calculateEffect(PlayerSkill ps, GameObject go)
    {
        switch (ps)
        {
            case PlayerSkill.Jab:
                go.GetComponent<SkeletonController>().beAttacked();
                break;
            case PlayerSkill.SamK:
                go.GetComponent<Rigidbody>().useGravity = true;
                go.GetComponent<Rigidbody>().velocity = new Vector3(0, 6, 0);

                go.GetComponent<SkeletonController>().toDie();
                break;
            case PlayerSkill.ScrewKick:
                go.GetComponent<SkeletonController>().beAttacked();
                break;
            case PlayerSkill.HiKick:
                go.GetComponent<SkeletonController>().beAttacked();
                break;
            case PlayerSkill.SpinKick:
                go.GetComponent<SkeletonController>().beAttacked();
                break;
            case PlayerSkill.RisingP:
                go.GetComponent<SkeletonController>().beAttacked();
                break;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
