using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject EnemyAirplanePrefab; //�Ծ��w�m����
    public float span = 1.0f;
    public float delta = 0;

    int intScore = 0;
    public Text ScoreText;

    public GameObject AirPlanePrefab; //�Ծ��w�m����
    public GameObject StartPoint; //�Ծ����ͪ��_�I

    public GameObject AirPlane; // �Ծ�����

    bool IsRestarting = false; //�C���O���O���b���Ҥ��HTrue�N��C�����b���ҡAFalse�N��C�����`���椤

    int LifeAmount = 2;
    public GameObject[] LifeImage; //�ŧi�@�Ӥ��}���ͩR�Ȫ���}�C

    public GameObject GameUI; // �C�������e��

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "���ơG0"; //��l�Ƥ��ƪ�
        InitialAirPlane(); //��l�ƾԾ�
    }

    // Update is called once per frame
    void Update()
    {
        //�H�U�ڭ̰ѦҤ��e�����߫}�Ԫ̹C���A�Q�Q�ݦp�󮳽b�Y���ͪ��{���A�ק令���Ծ������͵{��
        this.delta += Time.deltaTime;
        if (this.delta > this.span && IsRestarting == false)
        {
            this.delta = 0;
            float px = Random.Range(-3.0f, 3.0f); // �o���ڭ̲��ͪ��O-3��3�������B�I��
            Instantiate(EnemyAirplanePrefab, new Vector3(px, 7, 0), Quaternion.identity);
        }

        //�ץ��G�ڭ̦h�[�@�ӱ���A�u���b�u�Ծ��Q�R���v�M�u�C�����`���椤�v����ӱ��󳣦��ߤ��U�A�~�}�l�C������
        //�קK�]�����_Update�A�ҥH���_���;Ծ��A�̫�y���C���Y��...
        //�P�ɤ]�]�p�@�ӽw�Įɶ��A���������|���Ǫ����͡A�קK�٦��Ǫ��ɴN���;Ծ�
        if (AirPlane == null && IsRestarting == false && LifeAmount >= 0)
        {
            LifeImage[LifeAmount].SetActive(false); //�H�ͩR�ȷ��}�C��m���СA�N�ͩR�Ȫ���}�C����
            LifeAmount -= 1; //��@�ӥͩR��

            IsRestarting = true; //�]�w�u�C�����b���ҡv�����A
            StartCoroutine(StartGame()); //�}�l���ҹC��
        }

        if (LifeAmount < 0)
            GameUI.SetActive(true);
    }

    // �[������k�]�P�ǭn�Ʋߤ@�UC#�禡�P���]�w�禡�Ѽơ^
    public void IncreaseScore(int _score)
    {
        intScore += _score;
        ScoreText.text = "���ơG" + intScore;
    }

    //��l�ƾԾ�����k
    public void InitialAirPlane()
    {
        intScore = 0; //�����k�s
        IncreaseScore(intScore); //���s��ܤ���
        AirPlane = Instantiate(AirPlanePrefab, StartPoint.transform.position, StartPoint.transform.rotation);
        IsRestarting = false;
    }

    // �@�� IEnumerator �����A�Ψӳ]�w�@�ӭ��ҹC�����p�ɾ�
    IEnumerator StartGame()
    {
        if (AirPlanePrefab == null)
        {
            yield break;
        }

        yield return new WaitForSeconds(5); //�p�ɤ����]�]�N�O�C��������A�n����U������k�^

        if (LifeAmount == -1) //�p�G�{�b�ͩR�Ȥw�g�O-1�A�N�������X�p�ɾ�
        {
            yield break;
        }
        else
        {
            InitialAirPlane(); //�����A�n��l�ƾԾ�
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Intro");
    }
}
