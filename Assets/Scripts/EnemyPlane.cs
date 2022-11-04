using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    public float MoveSpeed = 0.005f; //�~�P�Ǫ����t�׳]�w��
    public GameObject ExplosionPrefab;
    public GameObject EnemyBulletPrefab; //�ľ��l�u�w�m����

    public float span = 0.5f;
    public float delta = 0;

    GameObject GameManager; //�C���ɺt�{��

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5); //�]�w10���~�P�Ǫ�����Q�R��
        GameManager = GameObject.Find("GameManager"); //�����������C���ɺt�{��
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -1 * MoveSpeed, 0) * Time.deltaTime; //�~�P�Ǫ��|���_���U����

        delta += Time.deltaTime;
        if (delta > span)
        {
            delta = 0;
            Vector3 pos = gameObject.transform.position + new Vector3(0, -0.5f, 0);
            //�l�u�ͦ�����m�ھڼľ�����m�A�A���U��0.5f
            Instantiate(EnemyBulletPrefab, pos, gameObject.transform.rotation); //�̾ڤW�z��pos��m�A�ͦ��l�u
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" || collision.tag == "Player") //�p�G�I�������ҬOBullet
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
    }
}
