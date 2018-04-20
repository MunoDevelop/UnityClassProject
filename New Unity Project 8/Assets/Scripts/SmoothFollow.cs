using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {
    [SerializeField]
    private Transform unfollowTransform;
    [SerializeField]
    private Transform followTransform;
    [SerializeField]
    private float followSpeed;
    [SerializeField]
    GameObject bulletPrefeb;

    PlayerControl playerControl;

    // Use this for initialization
    void Start () {
        playerControl = GameObject.Find("unitychan").GetComponent<PlayerControl>();

    }

    public void shoot()
    {
        if (playerControl.FollowerState == FollowerState.UnFollow)
        {
            return;
        }

        GameObject bullet = Instantiate(bulletPrefeb, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        bullet.GetComponent<Rigidbody>().velocity = new Vector3(12, 3, 0);

    }


    // Update is called once per frame
    void LateUpdate () {
        Vector3 followPosition;
       
        if (playerControl.FollowerState == FollowerState.UnFollow)
        {
            followPosition = unfollowTransform.position;
        }
        else
        {
            followPosition = followTransform.position;
        }
    
        transform.position = Vector3.Lerp(transform.position, followPosition, Time.deltaTime * followSpeed);
    }
}
