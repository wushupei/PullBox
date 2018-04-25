using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLamp : MonoBehaviour
{
    public bool lightStar=false; //判断灯是否亮着，一开始都没亮
    public Material defaultMater; //当灯不亮时的默认材质
    public Material selfMater; //在编辑器把相应材质拖进去
    public GameObject selfLight; //在编辑器把自己的灯拖进去
    void OnTriggerEnter(Collider other) //进入触发器时调用一次
    {
        if (other.gameObject.tag == gameObject.tag) //如果是自己相同标签的Cube进来了
        {
            selfLight.GetComponent<MeshRenderer>().material = selfMater; //让灯附加上自己颜色的材质(灯亮)
            lightStar = true; //灯光状态设为开
        }
    }
   void OnTriggerExit(Collider other) //离开触发器时调用一次
   {
       if (other.gameObject.tag == gameObject.tag)//如果是自己相同标签的Cube出去了
        {
          selfLight.GetComponent<MeshRenderer>().material = defaultMater; //附加默认材质(灯灭)
           lightStar = false; //灯光状态设为关
        }
   }
}
