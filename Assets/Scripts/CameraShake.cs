using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeTime = 0.5f;   // �_�ʪ��`�ɶ�����
    float currentTime = 0.0f;          // �{�b���ɶ�
    public bool isShake = false;      // �]�w�O���O�n�_��

    void Update()
    {
        if (isShake)
            currentTime = shakeTime; // �p�GisShake�ܼ��ܦ�true�A�N��{�b���ɶ��]�w���_�ʪ��`�ɶ�����
    }

    void LateUpdate()
    {
        // �H�ۮɶ���֡A�ֳt���������Y��
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
