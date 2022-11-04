using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    public float MoveSpeed = 0.005f; //外星怪物的速度設定值
    public GameObject ExplosionPrefab;
    public GameObject EnemyBulletPrefab; //敵機子彈預置物件
    public int TiltAngle = 0;

    public float span = 0.5f;
    public float delta = 0;

    GameObject GameManager; //遊戲導演程式

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5); //設定10秒後外星怪物物件被刪除
        GameManager = GameObject.Find("GameManager"); //找到場景中的遊戲導演程式
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(0, -1, 0) * MoveSpeed * Time.deltaTime; //外星怪物會不斷往下移動
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if (delta > span)
        {
            delta = 0;
            Vector3 pos = transform.position + new Vector3(0, -0.5f, 0); //子彈生成的位置根據戰機的位置，再往上加0.6f
            for (int i = 1; i > -2; i--)
            {
                int Angle = TiltAngle;
                while (Angle > 0 && i != 0)
                {
                    GameObject Bullet = Instantiate(EnemyBulletPrefab, pos, transform.rotation); //依據上述的pos位置，生成子彈
                    Bullet.GetComponent<Bullet>().TiltAngle = Angle * i;
                    Angle -= 5;
                }
            }
            Instantiate(EnemyBulletPrefab, pos, transform.rotation); //依據上述的pos位置，生成子彈
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet") //如果碰撞的標籤是Bullet
        {
            GameManager.GetComponent<GameManager>().IncreaseScore(10); //如果打到敵機，就加10分
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject); //刪除敵機物件
        }
        else if (collision.tag == "Player") //如果碰撞的標籤是戰機
        {
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject); //刪除敵機物件
        }
        else if (collision.tag == "Disappear Wall") //如果碰撞的標籤是牆壁
        {
            Destroy(gameObject); //刪除敵機物件
        }
    }
}
