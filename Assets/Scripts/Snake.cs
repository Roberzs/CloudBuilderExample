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
    Vector3 now;//头部实际前进方向

    float timer = 0f;
    float interval = 0.2f;

    public GameObject body;//身体预设体
    List<GameObject> snakeBody = new List<GameObject>();//一定记住设对象数组的格式


    void Awake()
    {
        for (int i = 0; i < 3; ++i)
        {
            GameObject newbodynext = Instantiate<GameObject>(body,
            transform.position - (i + 1) * new Vector3(0, 0.5f, 0),//0.5f是因为蛇头中心点与蛇身中心点的距离为0.5f;
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
            Vector3 tmpPosition = transform.position;    //记录头部变化前的位置
            List<Vector3> tmpList = new List<Vector3>(); //记录身体变化前的位置 

            for (int i = 0; i < snakeBody.Count; ++i)
            {
                tmpList.Add(snakeBody[i].transform.position);
            }

            //每隔interval 秒 向当前方向移动一个单位（0.5为一个蛇头或蛇身大小）。
            timer = 0;
            transform.position = 0.5f * now + transform.position;
            snakeBody[0].transform.position = tmpPosition;//将蛇身【0】移到头部之前的位置

            //依次前移身体的位置
            for (int i = 1; i < snakeBody.Count; ++i)
            {
                snakeBody[i].transform.position = tmpList[i - 1];
            }
        }
    }
}
