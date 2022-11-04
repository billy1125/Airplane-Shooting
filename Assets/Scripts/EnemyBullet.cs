using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject ExplosionPrefab; // 爆炸預置物件

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2); //設定2秒後子彈物件被刪除
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -5, 0) * Time.deltaTime; //子彈會不斷往下移動
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall") //如果碰撞的標籤是Wall
        {
            Destroy(gameObject);
        }
    }
}
