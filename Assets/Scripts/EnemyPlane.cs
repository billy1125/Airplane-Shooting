using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    public float MoveSpeed = 0.005f; //�~�P�Ǫ����t�׳]�w��
    public GameObject ExplosionPrefab;
    public GameObject EnemyBulletPrefab; //�ľ��l�u�w�m����
    public int TiltAngle = 0;

    public float span = 0.5f;
    public float delta = 0;

    GameObject GameManager; //�C���ɺt�{��

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5); //�]�w10���~�P�Ǫ�����Q�R��
        GameManager = GameObject.Find("GameManager"); //�����������C���ɺt�{��
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(0, -1, 0) * MoveSpeed * Time.deltaTime; //�~�P�Ǫ��|���_���U����
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if (delta > span)
        {
            delta = 0;
            Vector3 pos = transform.position + new Vector3(0, -0.5f, 0); //�l�u�ͦ�����m�ھھԾ�����m�A�A���W�[0.6f
            for (int i = 1; i > -2; i--)
            {
                int Angle = TiltAngle;
                while (Angle > 0 && i != 0)
                {
                    GameObject Bullet = Instantiate(EnemyBulletPrefab, pos, transform.rotation); //�̾ڤW�z��pos��m�A�ͦ��l�u
                    Bullet.GetComponent<Bullet>().TiltAngle = Angle * i;
                    Angle -= 5;
                }
            }
            Instantiate(EnemyBulletPrefab, pos, transform.rotation); //�̾ڤW�z��pos��m�A�ͦ��l�u
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet") //�p�G�I�������ҬOBullet
        {
            GameManager.GetComponent<GameManager>().IncreaseScore(10); //�p�G����ľ��A�N�[10��
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject); //�R���ľ�����
        }
        else if (collision.tag == "Player") //�p�G�I�������ҬO�Ծ�
        {
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject); //�R���ľ�����
        }
        else if (collision.tag == "Disappear Wall") //�p�G�I�������ҬO���
        {
            Destroy(gameObject); //�R���ľ�����
        }
    }
}
