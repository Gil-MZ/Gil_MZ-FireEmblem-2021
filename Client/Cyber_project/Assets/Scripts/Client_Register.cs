using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client_Register : MonoBehaviour
{
    // Start is called before the first frame update
    public void Send_Data()
    {
        Debug.Log(VariableStorage.username + " " + VariableStorage.password + " " + VariableStorage.email);
        Register(VariableStorage.username,VariableStorage.password,VariableStorage.email);
    }

    public void Register(string username,string password, string email)
    {
        byte[] messageSent = Encoding.ASCII.GetBytes(username);
        int byteSent = VariableStorage.sender.Send(messageSent);

        Debug.Log(byteSent);

        byte [] messageSent1 = Encoding.ASCII.GetBytes(password);
        byteSent = VariableStorage.sender.Send(messageSent1);

        Debug.Log(byteSent);

        byte [] messageSent2 = Encoding.ASCII.GetBytes(email);
        byteSent = VariableStorage.sender.Send(messageSent2);

        Debug.Log(byteSent);

        byte[] messageReceived = new byte[1024];

        int byteRecv = VariableStorage.sender.Receive(messageReceived);
        Debug.Log("Message from Server -> " +
                  Encoding.ASCII.GetString(messageReceived,
                                             0, byteRecv));

        if(Encoding.ASCII.GetString(messageReceived,0, byteRecv) == "Not valid" ||
            Encoding.ASCII.GetString(messageReceived, 0, byteRecv) == null)
        {
            VariableStorage.error = true;
        }

        else
        {
            VariableStorage.error = false;
            Debug.Log(Encoding.ASCII.GetString(messageReceived, 0, byteRecv));
            SceneManager.LoadScene(0);
        }
        

    }

}
