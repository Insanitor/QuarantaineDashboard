using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;


/**
* @Author Dennis Dupont
* @Version 1.3
* @Date 29/5/2020
*/

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] Canvas mainCanvas;
    [SerializeField] GameObject barHolder;
    [SerializeField] List<GameObject> userBars = new List<GameObject>();

    [SerializeField] GameObject userbarPrefab;
    [SerializeField] GameObject detailInfoPrefab;

    [SerializeField] GameObject currentDetailInfo;


    /// <summary>
    /// Makes this object a singleton or destroyes it if another already exists
    /// Afterwards it Queries the server though the Database manage to get the
    /// user information to spawns the bars showing users in the system
    /// </summary>
    /// @Author Dennis Dupont
    /// @Status Done
    /// @Date 29/5/2020
    private void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;

            //Tells unity that this Object, needs to be marked as "DontDestroyOnLoad" which allows it to pass between scenes
            DontDestroyOnLoad(this.gameObject);
        }

        SpawnUserbars();

    }

    /// <summary>
    /// Queries the server though the DatabaseManager to get the
    /// user information to spawns the bars showing users in the system
    /// </summary>
    /// @Author Dennis Dupont
    /// @Status Done
    public void SpawnUserbars()
    {
        GameObject parent = barHolder;
        for (int i = 1; i <= 5; i++)
        {
            //Queries the Database for User Data, to display on the Bars
            CitizenTemplate user = DatabaseManager.QueryDatabaseForUserInfo(i);

            if (user != null)
            {
                //Creates a User Bar
                GameObject currentUserBar = Instantiate(userbarPrefab, parent.transform);

                //Manually sets up this GameObject as a button
                currentUserBar.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => SpawnDetailWindow(user.UserID));

                //Adds the Bar to the list of Bars
                instance.userBars.Add(currentUserBar);

                //Makes the Bar a child of the BarHolder Object
                currentUserBar.transform.SetParent(parent.transform);

                //Gets the text Object in the Bar and updates it according to the user Information
                currentUserBar.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = user.FirstName + " " + user.LastName;

                //Checks the Status of the User Created
                string status = DatabaseManager.GetUserStatus(user.UserID);

                //Updates his Dashboard Status in accordance with his personal Status
                if (status == "Green")
                    currentUserBar.transform.GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0, 255, 0);
                if (status == "Yellow")
                    currentUserBar.transform.GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(255, 255, 0);
                if (status == "Red")
                    currentUserBar.transform.GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(255, 0, 0);
            }
            else
            {
                print("Was null?!");
                break;
            }
        }
    }


    /// <summary>
    /// Queries the server though the DatabaseManager to get the
    /// user information to spawn in the Detail Windows for a specific User
    /// </summary>
    /// @Author Dennis Dupont
    /// @Status Done
    public void SpawnDetailWindow(int barId)
    {
        GameObject parent = GameObject.FindGameObjectWithTag("Canvas");

        CitizenTemplate user = DatabaseManager.QueryDatabaseForUserInfo(barId);

        GameObject detailWindow = Instantiate(detailInfoPrefab, transform.parent);

        //Manually sets up this GameObject as a button
        detailWindow.transform.GetChild(6).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PerformCall(user.UserID));

        //If a details windows is already displayed, it will be removed
        if(instance.currentDetailInfo != null)
        {
            Destroy(currentDetailInfo.gameObject);
        }

        //The Details Windows is set as the Current Details Window
        //And is set to a child of the Canvas object
        instance.currentDetailInfo = detailWindow;
        instance.currentDetailInfo.transform.SetParent(parent.transform, false);

        //Gets the text Object in the Details Window and updates it according to the user Information
        instance.currentDetailInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Name: " + user.FirstName + " " + user.LastName;
        instance.currentDetailInfo.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Zipcode: " + user.ZipCode;

        //Queries the Database for his latest 60 entries
        CurrentLocationTemplate[] userEntries = DatabaseManager.GetUserDataEntries(barId);

        //Clears the Log
        instance.currentDetailInfo.transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";

        //Displays the users Log Entries on the Dashboard
        foreach (CurrentLocationTemplate entry in userEntries)
        {
            instance.currentDetailInfo.transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += "Status: " + entry.Status + "\n" +
               "Latitude: " + entry.Latitude + "\n" +
               "Longitude: " + entry.Longitude + "\n" +
               "Current Time: " + entry.CurrentTime + "\n\n";
        }
    }



    /// <summary>
    /// Used when taking an Action against a Citizen
    /// And updates the Database accordingly
    /// </summary>
    /// @Author Dennis Dupont
    /// @Status Done
    public void PerformCall(int barId)
    {
        Application.OpenURL("tel://+4522131265");
        DatabaseManager.UpdateStatus(barId);
    }
}
