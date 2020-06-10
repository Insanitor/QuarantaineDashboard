using System;


/**
* @Author Dennis Dupont & Rasmus Rosenkjær
* @Version 1.1
* @Date 29/5/2020
*/

public class CurrentLocationTemplate
{
    private string userID;
    private string status;
    private string latitude;
    private string longitude;
    private string currentTime;
    private string act;

    public string UserID
    {
        get
        {
            return userID;
        }

        set
        {
            userID = value;
        }
    }

    public string Status
    {
        get
        {
            return status;
        }

        set
        {
            status = value;
        }
    }

    public string Latitude
    {
        get
        {
            return latitude;
        }

        set
        {
            latitude = value;
        }
    }

    public string Longitude
    {
        get
        {
            return longitude;
        }

        set
        {
            longitude = value;
        }
    }

    public string CurrentTime
    {
        get
        {
            return currentTime;
        }

        set
        {
            currentTime = value;
        }
    }

    public string Act
    {
        get
        {
            return act;
        }

        set
        {
            act = value;
        }
    }

    public CurrentLocationTemplate()
    {
    }

    public CurrentLocationTemplate(string status, string latitude, string longitude, string currentTime)
    {
        Status = status;
        Latitude = latitude;
        Longitude = longitude;
        CurrentTime = currentTime;
    }


    public CurrentLocationTemplate(string status, string latitude, string longitude, string currentTime, string act)
    {
        Status = status;
        Latitude = latitude;
        Longitude = longitude;
        CurrentTime = currentTime;
        Act = act;
    }

    

}
