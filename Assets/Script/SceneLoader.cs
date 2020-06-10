using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
* @Author Rasmus Rosenkjær
* @Version 1.0
* @Date 18/5/2020
*/

public class SceneLoader
{

    /// <summary>
    /// Used to Load new Scenes
    /// </summary>
    /// @Author Rasmus Rosenkjær
    /// @Status Done
    /// @Date 18/5/2020
    public void LoadScene() 
    {
        SceneManager.LoadScene(1);
    }
}
