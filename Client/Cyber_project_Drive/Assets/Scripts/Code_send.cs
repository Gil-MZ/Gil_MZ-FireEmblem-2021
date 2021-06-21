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


public class Code_send : MonoBehaviour
{
    private bool gotcode = false;
    private bool active = false;
    private byte[] buffer = new byte[1024];
    private string code = "";
    private bool success_code = false;
    public GameObject error1;
    public GameObject error2;
    // Start is called before the first frame update
    void Start()
    {
        buffer = new byte[1024];
        VariableStorage.sender.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, Get_code, VariableStorage.sender);
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active == true)
        {
            buffer = new byte[1024];
            VariableStorage.sender.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, Get_code, VariableStorage.sender);
        }
    }


    public void Get_code(IAsyncResult code1)
    {
        Socket clientsocket = code1.AsyncState as Socket;
        int buffersize = clientsocket.EndReceive(code1);
        byte[] packet = new byte[buffersize];
        Array.Copy(buffer, packet, packet.Length);
        code = Encoding.ASCII.GetString(packet, 0, packet.Length);
        Debug.Log(code);
        if (code == "000")
        {
            Debug.Log("aaaaaaa");
            error2.SetActive(true);
        }
        else if (code != "")
            active = false;
    }

    public void check_code(Text code_got)
    {
        byte[] message1 = Encoding.ASCII.GetBytes(code_got.text);
        VariableStorage.sender.Send(message1);
        byte[] messageReceived = new byte[1024];
        int recv = VariableStorage.sender.Receive(messageReceived);
        if (Encoding.ASCII.GetString(messageReceived, 0, recv) == "You have successfully registered to the game!")
        {
            Debug.Log(Encoding.ASCII.GetString(messageReceived, 0, recv));
            success_code = true;
        }
        if (Encoding.ASCII.GetString(messageReceived, 0, recv) == "Not valid")
        {
            active = true;
            error2.SetActive(false);
            error1.SetActive(true);
        }
        
    }

    public void enter(GameObject success)
    {
        if (success_code)
        {
            VariableStorage.username = "";
            VariableStorage.password = "";
            VariableStorage.email = "";
            VariableStorage.error = false;
            VariableStorage.error1 = false;
            SceneManager.LoadScene(0);
        }

    }
}
