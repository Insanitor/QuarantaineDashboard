using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
* @Name Rasmus Rosenkjær
* @Version 1.0
* @Date 29/5/2020
*/

public class CitizenTemplate
{
    int userID;
    string firstName, lastName, zipCode;

    public CitizenTemplate()
    {
    }

    public CitizenTemplate(string firstName, string lastName, int userID)
    {
        FirstName = firstName;
        LastName = lastName;
        UserID = userID;
    }

    public string FirstName
    {
        get
        {
            return firstName;
        }

        set
        {
            firstName = value;
        }
    }

    public string LastName
    {
        get
        {
            return lastName;
        }

        set
        {
            lastName = value;
        }
    }

    public string ZipCode
    {
        get
        {
            return zipCode;
        }

        set
        {
            zipCode = value;
        }
    }

    public int UserID
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
}
