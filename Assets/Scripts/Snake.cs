using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float step;


    Vector3 up = new Vector3(0, 1, 0);
    Vector3 down = new Vector3(0, -1, 0);
    Vector3 left = new Vector3(-1, 0, 0);
    Vector3 right = new Vector3(1, 0, 0);
    Vector3 now;//ͷ��ʵ��ǰ������

    float timer = 0f;
    float interval = 0.2f;

    public GameObject body;//����Ԥ����
    List<GameObject> snakeBody = new List<GameObject>();//һ����ס���������ĸ�ʽ


    void Awake()
    {
        for (int i = 0; i < 3; ++i)
        {
            GameObject newbodynext = Instantiate<GameObject>(body,
            transform.position - (i + 1) * new Vector3(0, 0.5f, 0),//0.5f����Ϊ��ͷ���ĵ����������ĵ�ľ���Ϊ0.5f;
            Quaternion.identity);
            snakeBody.Add(newbodynext);
        }
    }

    void Start()
    {
        now = up;
    }
    void Update()
    {
        if (now != up && Input.GetKey(KeyCode.W))
        {
            now = up;
        }
        if (now != down && Input.GetKey(KeyCode.S))
        {
            now = down;
        }
        if (now != left && Input.GetKey(KeyCode.A))
        {
            now = left;
        }
        if (now != right && Input.GetKey(KeyCode.D))
        {
            now = right;
        }

        timer += Time.deltaTime;


        if (timer > interval)
        {
            Vector3 tmpPosition = transform.position;    //��¼ͷ���仯ǰ��λ��
            List<Vector3> tmpList = new List<Vector3>(); //��¼����仯ǰ��λ�� 

            for (int i = 0; i < snakeBody.Count; ++i)
            {
                tmpList.Add(snakeBody[i].transform.position);
            }

            //ÿ��interval �� ��ǰ�����ƶ�һ����λ��0.5Ϊһ����ͷ�������С����
            timer = 0;
            transform.position = 0.5f * now + transform.position;
            snakeBody[0].transform.position = tmpPosition;//������0���Ƶ�ͷ��֮ǰ��λ��

            //����ǰ�������λ��
            for (int i = 1; i < snakeBody.Count; ++i)
            {
                snakeBody[i].transform.position = tmpList[i - 1];
            }
        }
    }
}
