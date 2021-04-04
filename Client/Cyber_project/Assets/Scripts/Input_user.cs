using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input_user : MonoBehaviour
{

    public string username;
    public string password;
    public string email;

    public GameObject input_U;
    public GameObject input_P;
    public GameObject input_E;

    public GameObject u_display;
    public GameObject p_display;
    public GameObject e_display;

    public void StoreUser()
    {
        username = input_U.GetComponent<Text>().text;
        password = input_U.GetComponent<Text>().text;
        email = input_U.GetComponent<Text>().text;

        VariableStorage.username = username;
        VariableStorage.password = password;
        VariableStorage.email = email;
    }
}
