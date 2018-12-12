using UnityEngine;
/// <summary>
/// 挂灯柱管理器上
/// </summary>
public class LightManage : MonoBehaviour
{
    public Lampstandard[] lights; //声明所有灯
    public GameObject againButton; //在编辑器中将按钮物体拖进去
    void Awake()
    {        
        lights = transform.GetComponentsInChildren<Lampstandard>();
        Time.timeScale = 1; //初始时游戏事件为正常值
    }
    void Update()
    {
        //所有灯都亮了,调用过关方法,并暂停游戏
        if (AllLightIsBright())
        {
            againButton.SetActive(true);
            Time.timeScale = 0;
        }
    }
    //判断是否所有的灯都亮了
    bool AllLightIsBright()
    {
        //发现一个灯没亮,立刻返回false,如果都没发现,返回true
        foreach (var light in lights)
        {
            if (!light.isBright)
                return false;
        }
        return true;
    }
}
