using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Character_activation : MonoBehaviour
{
    bool active = false;
    public GameObject[] character;
    private byte[] buffer = new byte[1024];

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(VariableStorage.player + VariableStorage.player2);
        for (int i = 0; i <= 9; i++)
        {
            if (character[i].name == VariableStorage.c1 || character[i].name == VariableStorage.c2 ||
                character[i].name == VariableStorage.c3 || character[i].name == VariableStorage.c4 ||
                character[i].name == VariableStorage.c5)
                character[i].SetActive(true);
        }

        byte[] messageReceived1 = new byte[1024];
        int byteRecv1 = VariableStorage.sender.Receive(messageReceived1);
        byte[] messageReceived2 = new byte[1024];
        int byteRecv2 = VariableStorage.sender.Receive(messageReceived2);
        byte[] messageReceived3 = new byte[1024];
        int byteRecv3 = VariableStorage.sender.Receive(messageReceived3);
        byte[] messageReceived4 = new byte[1024];
        int byteRecv4 = VariableStorage.sender.Receive(messageReceived4);
        byte[] messageReceived5 = new byte[1024];
        int byteRecv5 = VariableStorage.sender.Receive(messageReceived5);

        VariableStorage.c6 = Encoding.ASCII.GetString(messageReceived1, 0, byteRecv1);
        VariableStorage.c7 = Encoding.ASCII.GetString(messageReceived2, 0, byteRecv2);
        VariableStorage.c8 = Encoding.ASCII.GetString(messageReceived3, 0, byteRecv3);
        VariableStorage.c9 = Encoding.ASCII.GetString(messageReceived4, 0, byteRecv4);
        VariableStorage.c10 = Encoding.ASCII.GetString(messageReceived5, 0, byteRecv5);
        if (VariableStorage.player == "1")
            VariableStorage.player2 = "2";
        else
            VariableStorage.player2 = "1";

        for (int y = 10; y <= 19; y++)
        {
            if (character[y].name.Trim(new Char[] { ' ', '(', ')', '1' }) == VariableStorage.c6 ||
                character[y].name.Trim(new Char[] { ' ', '(', ')', '1' }) == VariableStorage.c7 ||
                character[y].name.Trim(new Char[] { ' ', '(', ')', '1' }) == VariableStorage.c8 ||
                character[y].name.Trim(new Char[] { ' ', '(', ')', '1' }) == VariableStorage.c9 ||
                character[y].name.Trim(new Char[] { ' ', '(', ')', '1' }) == VariableStorage.c10)
                character[y].SetActive(true);
        }
        client_Receive();
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
            client_Receive();
    }

    private void client_Receive()
    {
        active = true;
        buffer = new byte[1024];
        VariableStorage.sender.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, RecvCo, VariableStorage.sender);
    }
    private void RecvCo(IAsyncResult result)
    {
        Debug.Log("receive something");
        Socket clientsocket = result.AsyncState as Socket;
        int buffersize = clientsocket.EndReceive(result);
        byte[] packet = new byte[buffersize];
        Array.Copy(buffer, packet, packet.Length);
        Debug.Log(Encoding.ASCII.GetString(packet, 0, packet.Length));
        VariableStorage.data = Encoding.ASCII.GetString(packet, 0, packet.Length);
        Debug.Log(VariableStorage.data);
        active = false;
        //byte[] messageReceived = new byte[1024];
        //int byteRecv = VariableStorage.sender.ReceiveAsync(messageReceived)
        //VariableStorage.data = Encoding.ASCII.GetString(messageReceived, 0, byteRecv);
    }
}
