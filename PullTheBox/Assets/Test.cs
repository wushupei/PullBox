using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        Texture image = Resources.Load<Texture>("Image");
        GetComponent<MeshRenderer>().material.mainTexture = image;
    }
}