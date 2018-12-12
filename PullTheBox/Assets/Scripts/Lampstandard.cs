using UnityEngine;
/// <summary>
/// 挂所有灯上
/// </summary>
public class Lampstandard : MonoBehaviour
{
    [Range(0, 3)] public int para; //颜色判断参数,范围限定在0~3
    Color color; //初始颜色
    Material material; //声明子物体材质组件
    public bool isBright; //判断灯是否亮起
    void Start()
    {
        //获取初始化颜色
        color = ColorManage.SetColor(para); 
        //自身附上初始化颜色
        GetComponent<MeshRenderer>().material.color = color;
        //获取子物体的材质
        material = transform.GetChild(0).GetComponent<MeshRenderer>().material;
    }
    void OnTriggerEnter(Collider other) //触发
    {
        //如果进入的箱子的颜色和自身拥有的颜色相同,亮灯
        if (other.GetComponent<MeshRenderer>().material.color == color)
        {
            isBright = true;
            material.color = color;
        }
    }
    void OnTriggerExit(Collider other) //离开触发
    {
        //如果离开的箱子的颜色和自身拥有的颜色相同,关灯
        if (other.GetComponent<MeshRenderer>().material.color == color)
        {
            isBright = false;
            material.color = Color.white;
        }
    }
}
