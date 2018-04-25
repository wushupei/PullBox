using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public GameObject[] point;//找到所有的灯，好判断它们是否亮着
    public GameObject panel1,panel2; //在编辑界面将Panel1和Panel2拖进去

    private void Awake()
    {
        Time.timeScale = 1; //程序运行时设定系统时间为正常
    }
    void Update()
    {
        if (AllStar(point)) //如果灯光都亮
        {
            panel1.SetActive(true); //显示过关panel
            Time.timeScale = 0; //暂停游戏
        }
        if (Input.GetKey(KeyCode.Escape)) //按下Esc键
        {
            if (!panel2.activeInHierarchy&&!panel1.activeInHierarchy) //如果panel2和panel1都没启用
            {
                panel2.SetActive(true); //启用panel2
                Time.timeScale = 0; //暂停游戏
            }
        }
    }
    bool AllStar(GameObject[] point) //找所有的路灯，判断是否都亮着
    {
        for (int i = 0; i < point.Length; i++) 
        {
            if (!point[i].GetComponent<StreetLamp>().lightStar) //只要有一个灯不亮，返回False
                return false; 
        }
        return true; //所有灯都亮的返回True
    }
    public void Continue() //挂该继续游戏按钮
    {   
        panel2.SetActive(false); //按下继续游戏后，禁用panel2
        Time.timeScale = 1;  //游戏解除暂停
    }
    public void Restart() //挂给再来一局按钮
    {
        SceneManager.LoadScene("FootBallRobot"); // //将初始场景保存，命名为"FootBallRobot"，加载该游戏场景   
    }
    public void SignOut() //挂给退出游戏按钮
    {
        Application.Quit(); //退出程序
    }
}
