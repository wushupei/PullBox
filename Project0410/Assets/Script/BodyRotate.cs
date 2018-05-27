using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 上身旋转
/// </summary>
public class BodyRotate : MonoBehaviour
{
    private Ray ray;//声明射线
    private RaycastHit hit;//射线照射点
    public LayerMask layerName;//射线照射层
    public GameObject body; //在编辑器里把上身拖进去
    void Start()
    {
        print("我是git远程安插进来的卧底");
    }
    void Update()
    {
        //当技能正在释放时Player不能移动和旋转
        //找到ReleaseSkill脚本里的pullStar判断是否正在释放技能
        if (GetComponent<ReleaseSkill>().pullStar == false) //如果技能没有释放
            body.transform.LookAt(InputMove()); //身体可以旋转
    }
    private Vector3 InputMove()//获取鼠标在世界中的移动
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);//镜头射出一条射线,跟随鼠标位置
        if (Physics.Raycast(ray, out hit, 100, layerName))//根据层名选择照射层，照射层只照地面
            //获取照射点坐标(也是鼠标所在位置，身体的y值给y轴，让返回的坐标不用停留在地面山)
            return new Vector3(hit.point.x, hit.point.y + body.transform.position.y, hit.point.z);
        else
            return new Vector3(0, 0, 0); //如果照不到地面了就会返回该坐标，尽量吧地面的碰撞器加大点
    }
}
