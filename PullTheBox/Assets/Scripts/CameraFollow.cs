using UnityEngine;
/// <summary>
/// 挂主摄像机上
/// </summary>
public class CameraFollow : MonoBehaviour
{
    Transform player; //声明主角
    public float height, distance; //和主角在Y轴及Z轴上的距离
    void Start()
    {
        //根据名字获取主角
        player = GameObject.Find("Player").transform; 
    }
    void Update()
    {
        //摄像机始终看向主角
        Quaternion dir = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, dir, Time.deltaTime);
        //始终在主角身后偏上方位置
        transform.position = player.position + Vector3.up * height + Vector3.back * distance;
    }
}
