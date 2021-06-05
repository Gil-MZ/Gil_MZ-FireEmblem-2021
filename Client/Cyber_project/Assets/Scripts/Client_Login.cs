using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class Client_Login : MonoBehaviour
{

    public void login_data()
    {
        Debug.Log(VariableStorage.username + " " + VariableStorage.password + " " + VariableStorage.email);
        Login(VariableStorage.username, VariableStorage.password, VariableStorage.email);
    }


    public void Login(string username, string password,string email)
    {
        byte[] messageSent = Encoding.ASCII.GetBytes(username);
        int byteSent = VariableStorage.sender.Send(messageSent);

        Debug.Log(byteSent);
        Thread.Sleep(200);
        byte[] messageSent1 = Encoding.ASCII.GetBytes(password);
        byteSent = VariableStorage.sender.Send(messageSent1);

        Debug.Log(byteSent);
        Thread.Sleep(200);
        byte[] messageSent2 = Encoding.ASCII.GetBytes(email);
        byteSent = VariableStorage.sender.Send(messageSent2);

        Debug.Log(byteSent);

        byte[] messageReceived = new byte[1024];

        int byteRecv = VariableStorage.sender.Receive(messageReceived);
        Debug.Log("Message from Server -> " +
                  Encoding.ASCII.GetString(messageReceived,
                                             0, byteRecv));

        if (Encoding.ASCII.GetString(messageReceived,0, byteRecv) == "Not valid")
        {
            VariableStorage.error1 = true;
        }

        else
        {
            VariableStorage.error1 = false;
            Debug.Log(Encoding.ASCII.GetString(messageReceived, 0, byteRecv));
            SceneManager.LoadScene(3);
        }

    }
}
