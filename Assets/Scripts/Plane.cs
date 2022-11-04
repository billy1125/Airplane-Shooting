using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public float MoveSpeed = 1;    
    public int TiltAngle = 0;
    public float LifeAmount = 1.0f;

    public GameObject BulletPrefab;
    public GameObject ExplosionPrefab;

    GameObject m_gameManager; //遊戲導演程式
    Animator m_Animator;
    float HorizontalMove, VerticalMove;

    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameObject.Find("GameManager"); //找到場景中的遊戲導演程式
        m_Animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {        
        transform.position += new Vector3(HorizontalMove, VerticalMove, 0) * MoveSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    private void Update()
    {
        //簡單的左右控制
        HorizontalMove = Input.GetAxisRaw("Horizontal");
        VerticalMove = Input.GetAxisRaw("Vertical");

        //增加子彈發射的功能
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 pos = transform.position + new Vector3(0, 0.6f, 0); //子彈生成的位置根據戰機的位置，再往上加0.6f
            for (int i = 1; i > -2; i--)
            {
                int Angle = TiltAngle;
                while (Angle > 0 && i != 0)
                {                   
                    GameObject Bullet = Instantiate(BulletPrefab, pos, transform.rotation); //依據上述的pos位置，生成子彈
                    Bullet.GetComponent<Bullet>().TiltAngle = Angle * i;
                    Angle -= 5;
                }
            }
            Instantiate(BulletPrefab, pos, transform.rotation); //依據上述的pos位置，生成子彈
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy Bullet")
        {
            LifeAmount -= 0.1f;            
        }
        else if (collision.tag == "Enemy")
        {
            LifeAmount -= 0.3f;
        }
        m_Animator.SetTrigger("Make Damage");
        Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        m_gameManager.GetComponent<GameManager>().MakeDamage(LifeAmount);

        if (LifeAmount <= 0)
            Destroy(gameObject);
    }
}
