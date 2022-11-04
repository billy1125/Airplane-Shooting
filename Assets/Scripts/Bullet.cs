using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ExplosionPrefab; // �z���w�m����
    public float TiltAngle = 0;        // �l�u����
    public int Direct = 1;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, TiltAngle);
        Destroy(gameObject, 5); //�]�w2���l�u����Q�R��
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        transform.Translate(0, 5 * Direct * Time.deltaTime, 0, Space.Self);
        //transform.Localposition += new Vector3(0, 5, 0) * Time.deltaTime; //�l�u�|���_���W����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
