using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ExplosionPrefab; // 爆炸預置物件

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5); //設定2秒後子彈物件被刪除
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 5, 0) * Time.deltaTime; //子彈會不斷往上移動
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);

            Destroy(gameObject);
        }
        else if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
