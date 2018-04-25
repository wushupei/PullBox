using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GOMove : MonoBehaviour
{
    public Transform[] target; //把所有寻路点拖进去
    public float speed = 0; //寻路速度
    private void Start()
    {
        //运行时初始化位置（随机）
        transform.position = new Vector3(Random.Range(-9, 9), 0.75f, Random.Range(-9, 9));
        StartCoroutine(MoveToPath()); //启动协程
    }
    private IEnumerator MoveToPath() //循环寻路
    {
        while (true) //循环得对所有寻路点寻路
        {
            for (int i = 0; i < target.Length; i++)
            { //嵌套协程，依次向寻路点寻路
                yield return StartCoroutine(MoveToTarget(target[i].position));
            }
        }
    }
    IEnumerator MoveToTarget(Vector3 target) //寻路方法
    {
        while ((transform.position-target).magnitude>0.1f)
        {   //当和目标距离大于0.1f时向目标寻路
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return new WaitForFixedUpdate(); //每帧执行一下
        }  
    }
}
