using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class User : MonoBehaviour
{

    public void User_name (Text name)
    {
        Debug.Log(name.text);
        VariableStorage.username = name.text;
    }

    public void User_password(Text name)
    {
        Debug.Log(name.text);
        VariableStorage.password = name.text;
    }

    public void User_email(Text name)
    {
        Debug.Log(name.text);
        VariableStorage.email = name.text;
    }



}
