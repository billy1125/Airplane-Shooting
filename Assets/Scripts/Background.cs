using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float Speed = 1;

    void LateUpdate()
    {
        transform.position += new Vector3(0, -1 * Time.deltaTime * Speed, 0);
    }
}
