using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject ExplosionPrefab; // �z���w�m����

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2); //�]�w2���l�u����Q�R��
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -5, 0) * Time.deltaTime; //�l�u�|���_���U����
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
        if (collision.tag == "Wall") //�p�G�I�������ҬOWall
        {
            Destroy(gameObject);
        }
    }
}
