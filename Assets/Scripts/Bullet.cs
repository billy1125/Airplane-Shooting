using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ExplosionPrefab; // �z���w�m����

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5); //�]�w2���l�u����Q�R��
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 5, 0) * Time.deltaTime; //�l�u�|���_���W����
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
