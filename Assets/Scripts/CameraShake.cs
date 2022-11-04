using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeTime = 0.5f;   // 震動的總時間長度
    float currentTime = 0.0f;          // 現在的時間
    public bool isShake = false;      // 設定是不是要震動

    void Update()
    {
        if (isShake)
            currentTime = shakeTime; // 如果isShake變數變成true，就把現在的時間設定為震動的總時間長度
    }

    void LateUpdate()
    {
        // 隨著時間減少，快速的移動鏡頭框
        if (currentTime > 0.0f)
        {
            isShake = false;
            currentTime -= Time.deltaTime;
            GetComponent<Camera>().rect = new Rect(0.1f * (Random.value) * Mathf.Pow(currentTime, 2),
                                                        0.1f * (Random.value) * Mathf.Pow(currentTime, 2),
                                                        1.0f, 1.0f);
        }
        else
        {
            currentTime = 0.0f;
        }
    }
}
