using UnityEngine;
/// <summary>
/// 挂Player身上
/// </summary>
public class PlayerController : MonoBehaviour
{
    PlayerCharacter character; //声明角色功能脚本
    void Start()
    {
        character = GetComponent<PlayerCharacter>(); //获取
    }
    void Update()
    {
        //获取水平和垂直轴输入来进移动
        character.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        character.PosLimit(); //位置限制
        character.BodyCharacter(); //身体控制        
        //鼠标左键激活拉伸技能
        if (Input.GetMouseButtonDown(0))
            character.stretching = true;
        character.Pull(); //拉伸
        character.PickUp(); //拿起
        //鼠标右键放下箱子
        if (Input.GetMouseButtonDown(1))
            character.PutDown();
    }
}
