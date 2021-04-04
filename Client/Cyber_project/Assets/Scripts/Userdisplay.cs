using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Userdisplay : MonoBehaviour
{
    public Text welcome;
    public Text win;
    public Text lose;

    private void Start()
    {
        Display(welcome,win,lose);
    }
    private void Display (Text welcome, Text win, Text lose)
    {
        byte[] messageReceived = new byte[1024];

        int byteRecv = VariableStorage.sender.Receive(messageReceived);
        Debug.Log("Message from Server -> " + Encoding.ASCII.GetString(messageReceived,0, byteRecv));
        byte[] messageReceived1 = new byte[1024];
        int byteRecv1 = VariableStorage.sender.Receive(messageReceived1);
        Debug.Log("Message from Server -> " + Encoding.ASCII.GetString(messageReceived1,0, byteRecv1));

        VariableStorage.wins = Encoding.ASCII.GetString(messageReceived,0, byteRecv);

        VariableStorage.losses = Encoding.ASCII.GetString(messageReceived1, 0, byteRecv1);

        welcome.text = "Welcome to the game ";
        welcome.text += VariableStorage.username;
        Debug.Log(Encoding.ASCII.GetString(messageReceived, 0, byteRecv) + "," + Encoding.ASCII.GetString(messageReceived1, 0, byteRecv1));
        win.text = "Number of Wins: ";
        win.text += Encoding.ASCII.GetString(messageReceived,0, byteRecv);

        lose.text = "Number of Losses: " + Encoding.ASCII.GetString(messageReceived1, 0, byteRecv1);
    }

}
