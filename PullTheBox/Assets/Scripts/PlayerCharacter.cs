using UnityEngine;
/// <summary>
/// 挂Player身上
/// </summary>
public class PlayerCharacter : MonoBehaviour
{
    Rigidbody rigid; //刚体组件
    Transform body; //身体
    Transform hand; //手臂
    private void Start()
    {
        //获取刚体组件
        rigid = GetComponent<Rigidbody>();
        //加载图片资源设置主角纹理
        Texture image = Resources.Load<Texture>("Image");
        GetComponent<MeshRenderer>().material.mainTexture = image;
        //根据名称获取身体并设置成黑色
        body = transform.Find("Body");
        body.GetComponent<MeshRenderer>().material.color = Color.black;
        //根据名称获取双臂,从而获取两只手臂,都并设置成黑色
        hand = body.Find("Hands");
        BoxCollider[] hands = hand.GetComponentsInChildren<BoxCollider>();
        foreach (var hand in hands)
        {
            hand.GetComponent<MeshRenderer>().material.color = Color.black;
        }
    }
    public float moveSpeed;
    public void Move(float x, float z) //移动方法
    {
        x *= Time.deltaTime * moveSpeed;
        z *= Time.deltaTime * moveSpeed;
        rigid.velocity = new Vector3(x, rigid.velocity.y, z);
    }

    public float limit_x, limit_z; //限制X和Z轴上的最大移动距离                                  
    public void PosLimit() //移动位置限制
    {
        Vector3 pos = transform.position;
        if (pos.x < -limit_x)
            pos.x = -limit_x;
        else if (pos.x > limit_x)
            pos.x = limit_x;
        if (pos.z < -limit_z)
            pos.z = -limit_z;
        else if (pos.z > limit_z)
            pos.z = limit_z;
        transform.position = pos;
    }

    public LayerMask layer; //声明一个射线照射层,让射线只能照射到地形  
    public void BodyCharacter()//上身控制
    {
        //身体始终保持在小球上方并且锁定x和z轴的旋转
        body.position = transform.position + Vector3.up;
        body.rotation = new Quaternion(0, body.rotation.y, 0, body.rotation.w);
        //从摄像机发射射线到地面,身体始终朝向照射点
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; //声明照射点
        if (Physics.Raycast(ray, out hit, 100, layer))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
            //获取照射点正上方的坐标,高度同身体一样,让身体朝向该坐标
            Vector3 lookTarget = new Vector3(hit.point.x, body.position.y, hit.point.z);
            body.LookAt(lookTarget);
        }
    }

    public bool stretching = false; //拉伸技能是否激活
    bool stretch = true; //判断伸缩方向,初始为伸展方向    
    public float maxLength; //最大拉伸距离
    public float pullSpeed; //伸缩速度 
    public void Pull() //手臂拉伸
    {
        if (stretching)
        {
            //激活拉伸技能后,手臂变长,达到最大长度则缩短
            Vector3 scale = hand.localScale;
            if (stretch)
                scale.z += Time.deltaTime * pullSpeed;
            else
                scale.z -= Time.deltaTime * pullSpeed;
            //手臂回缩至原有长度,恢复原样,停止拉伸技能
            if (scale.z <= 1)
            {
                scale.z = 1;
                stretching = false;
            }
            //初始状态往前伸展,达到最大长度往回收缩
            if (scale.z == 1)
                stretch = true;
            else if (scale.z >= maxLength)
                stretch = false;
            hand.localScale = scale;
        }
    }

    Transform box; //声明变量接收箱子
    void OnTriggerEnter(Collider other) //触发第一帧
    {
        //如果空着手施放技能时获取碰到的是箱子，获取它
        //之后我们将创建一个Box的脚本挂在箱子身上
        //所以这里根据物体身上是否有Box组件来判断该物体是否为箱子
        if (!box && stretching && other.GetComponent<Box>())
            box = other.transform;      
    }  
    public void PickUp() //拿起箱子
    {
        //如果获取了箱子,手臂立刻回缩,让箱子身位身体的子物体跟随身体移动
        if (box) 
        {
            stretch = false;
            box.parent = body;
            //让被拿取的箱子来到身体前方,并和自己保持同一方向
            Vector3 pos = body.position + body.forward * 2;          
            box.position = Vector3.MoveTowards(box.position, pos, Time.deltaTime * pullSpeed);
            box.rotation = Quaternion.Lerp(box.rotation, body.rotation, Time.deltaTime * 5);

        }
    }


    //放下箱子
    public void PutDown()
    {
        //如果手上有箱子,解除父子关系,让box为空
        if (box)
        {
            box.SetParent(null);
            box = null;
        }
    }
}

