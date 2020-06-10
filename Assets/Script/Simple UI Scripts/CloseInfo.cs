using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
* @Author Rasmus Rosenkjær
* @Version 1.0
* @Date 21/5/2020
*/

public class CloseInfo : MonoBehaviour
{
    public GameObject userInfo;
    public void CloseUserInfo() 
    {
        Destroy(userInfo);
    }
}
