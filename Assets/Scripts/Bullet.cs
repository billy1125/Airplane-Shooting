using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ExplosionPrefab; // 爆炸預置物件
    public float TiltAngle = 0;        // 子彈角度
    public int Direct = 1;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, TiltAngle);
        Destroy(gameObject, 5); //設定2秒後子彈物件被刪除
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        transform.Translate(0, 5 * Direct * Time.deltaTime, 0, Space.Self);
        //transform.Localposition += new Vector3(0, 5, 0) * Time.deltaTime; //子彈會不斷往上移動
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
