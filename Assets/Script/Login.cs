using System;

/**
* @RAuthor asmus Rosenkjær
* @Version 0.9
* @Date 18/5/2020
*/

[Obsolete("Login Class is Deprecated. Use LoginManager instead")]
public class Login
{
    /// <summary>
    /// Validates a spoof users information
    /// </summary>
    /// @Author Rasmus Rosenkjær
    /// @Status Done
    public ResponseMessage ValidateUserLogin(string username, string password)
    {
        if (username == "Rasmus")
        {
            if (password == "1234")
            {
                return ResponseMessage.OK;
            }
            return ResponseMessage.Failed;
        }
        return ResponseMessage.Failed;
    }
}
