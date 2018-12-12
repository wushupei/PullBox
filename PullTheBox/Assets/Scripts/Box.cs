using UnityEngine;
/// <summary>
/// 挂所有箱子上
/// </summary>
public class Box : MonoBehaviour
{
    [Range(0, 3)] public int para; //颜色判断参数
    Rigidbody rigid; //刚体组件
    LightManage lightManage; //灯管理器
    int index; //索引
    void Start()
    {
        //初始化颜色
        GetComponent<MeshRenderer>().material.color = ColorManage.SetColor(para);
        //获取刚体
        rigid = GetComponent<Rigidbody>();
        //获取脚本管理器
        lightManage = FindObjectOfType<LightManage>();
        //给一个随机索引
        index = Random.Range(0, lightManage.lights.Length);
    }
    Transform parent; //声明父物体
    void Update()
    {
        //获取父物体，判断是否被拿起，为空则寻路，不为空取消物理影响
        parent = transform.parent;
        if (!parent)
        {
            Wayfinding();
            rigid.isKinematic = false;
        }
        else
            rigid.isKinematic = true;
    }
    public float moveSpeed; //寻路速度
    public float rotateSpeed; //转身速度
    void Wayfinding() //寻路
    {
        //获取随机一个灯的位置，拿到方向，面向该方向前进
        Vector3 pos = lightManage.lights[index].transform.position;
        Quaternion dir = Quaternion.LookRotation(pos - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, dir, rotateSpeed * Time.deltaTime);
        transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        //接近目标索引+1，往下一个灯前进，如果索引超出则为0
        if (Vector3.Distance(transform.position, pos) <= 2)
            index++;
        if (index == lightManage.lights.Length)
            index = 0;
    }
}
