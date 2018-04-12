using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    [SerializeField]
    private GameObject player = null;
    private Vector3 position_offset = Vector3.zero;

    void Start () {
        position_offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate() {
        // 카메라 현재 위치를 new_position에 할당.
        Vector3 new_position = transform.position;
        // 플레이어의 X좌표에 차이 값을 더해서 new_position의 X에 대입.
        new_position.x = player.transform.position.x + position_offset.x;
        // 카메라 위치를 새로운 위치(new_position)로 갱신.
        transform.position = new_position;

    }
}
