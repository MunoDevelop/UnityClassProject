using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour {
    public float step_timer = 0.0f; // 경과시간을유지한다.
    void Update()
    {
        this.step_timer += Time.deltaTime; // 경과시간을더해간다.
    }
    public float getPlayTime()
    {
        float time;
        time = this.step_timer;
        return (time); // 호출한곳에경과시간을알려준다.
    }
}
