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

    PlayerControl playerControl;

    // Use this for initialization
    void Start () {
        playerControl = GameObject.Find("unitychan").GetComponent<PlayerControl>();

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
