using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseSkill : MonoBehaviour
{
    public GameObject doubleHand;//在编辑器页面把双手拖进去
    public float pullSpeed = 0; //拉伸速度
    public float maxPull = 20; //最大拉伸距离
    Vector3 handPos; //双手初始位置
    public bool pullStar = false; //技能是否正在释放
    bool holdGO = false; //是否拿着东西 
    private RaycastHit hit;//声明射线照射到的点
    public LayerMask mask;//声明射线能打到的层
    Vector3 hitPos; //获取照射点坐标   

    void Update()
    {
        if (pullStar == false && holdGO == false) //如果技能没有释放，也没拿着东西
        {
            if (Input.GetMouseButtonDown(0)) //可以点击鼠标左键释放技能
            {
                Release(); //释放技能
            }
        }
        else if (holdGO == true) //如果手里拿着东西
        {
            if (Input.GetMouseButtonDown(1)) //按下鼠标右键
            {
                hit.transform.SetParent(null); //放下Cube（解除Cube和双手掌的父子关系）
                holdGO = false; //空手状态           
            }
        }
        Pull(); //拉伸
        KeepPos(hit.transform); //拿稳Cube
    }
    void Release() //释放技能
    {
        HandTrail(1); //显示轨迹
        handPos = doubleHand.transform.position; //获取此刻双手的当前坐标
        pullSpeed = 50; //拉伸速度，方向往前
        pullStar = true; //将技能设为释放状态 
        //从双手位置发射射线
        if (Physics.Raycast(handPos, doubleHand.transform.forward, out hit, maxPull, mask))
        {
            hitPos = hit.point; //如果射线打到东西，获取照射点坐标
        }
    }
    void Pull() //拉伸
    {
        if (pullStar == true) //当技能处于释放状态
        {
            //双手在自身坐标z轴移动，速度方向待定
            doubleHand.transform.Translate(0, 0, Time.deltaTime * pullSpeed);
            //如果射线没照到东西，双手到达最长距离后返回
            if (hit.transform == null && (doubleHand.transform.position - handPos).magnitude > maxPull)
            {
                HandTrail(0); //轨迹显示时间为0（不显示）
                pullSpeed = -50; //双手往后移动
            }
            // 如果照到东西,双手接近该东西,抓住它返回
            else if (hit.transform != null && (doubleHand.transform.position - hitPos).magnitude < 0.5f)
            {
                //抓住射线打到的Cube(设为双手的子物体，跟随双手移动)
                hit.transform.parent = doubleHand.transform;       
                holdGO = true; //表示正拿着Cube           
                HandTrail(0); //隐轨迹显示时间为0（不显示）
                pullSpeed = -50; //双手往后移动
            }
            //如果双手返回时接近出发时位置，则双手位置复位，停止移动
            if (pullSpeed < 0 && (doubleHand.transform.position - handPos).magnitude < 0.5f)
            {
                pullSpeed = 0; //移动停止
                doubleHand.transform.position = handPos; //位置复位
                pullStar = false; //技能处于未放出状态
            }
        }
    }
    void KeepPos(Transform Cube) //保持位置
    {
        if (Cube != null && Cube.parent != null)//如果Cube不为空，且有父物体（被抓住了）
        {
            Cube.position = doubleHand.transform.position; //位置固定
            Cube.rotation = doubleHand.transform.rotation; //旋转固定       
        }
    }
    private void HandTrail(float trailTime) //手掌的运动轨迹
    {
        //找到双手自身和它的子物体（两个手掌）
        Transform[] a = doubleHand.GetComponentsInChildren<Transform>();
        for (int i = 1; i < a.Length; i++)
        {
            //设定两个手掌运动轨迹存留时间，当为0时轨迹不显示
            if (a[i].GetComponent<TrailRenderer>() != null)
                a[i].GetComponent<TrailRenderer>().time = trailTime;
        }
    }
}
