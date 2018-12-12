using UnityEngine;
/// <summary>
/// 颜色工具类,不用挂
/// </summary>
public class ColorManage
{
    public static Color SetColor(int para) //设置自身颜色
    {
        switch (para) //根据不同参数设置不同颜色
        {
            case 0:
                return Color.red;
            case 1:
                return Color.yellow;
            case 2:
                return Color.blue;
            default:
                return Color.green;
        }
    }
}
