using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



/**
* @Rasmus Rosenkjær
* @Version 1.0
* @Date 18/05/2020
*/
public enum ResponseMessage
{
    OK, Failed
}

/**
* @Author Rasmus Rosenkjær
* @Version 1.1
* @Date 18/05/2020
*/
public class LoginManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField username;
    [SerializeField]
    TMP_InputField password;
    [SerializeField]
    TextMeshProUGUI errorText;

    public void Login() 
    {
        SceneLoader sceneLoader = new SceneLoader();

        ResponseMessage reply = DatabaseManager.QueryDatabaseForLogin(username.text, password.text);
        if (reply == ResponseMessage.OK)
        {
            sceneLoader.LoadScene();
        }

        errorText.text = "Wrong Username or Password";
    }

}
