using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 挂EventSystem上
/// </summary>
public class ButtonEvent: MonoBehaviour
{
    //再来一次
    public void Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
