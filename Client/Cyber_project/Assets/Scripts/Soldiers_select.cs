using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Soldiers_select : MonoBehaviour
{
    public Toggle toggler;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (toggler.isOn == true && VariableStorage.Soldiers_count >= 5)
            toggler.interactable = false;
        else
            toggler.interactable = true;
    }
    public void Members(bool toggle)
    {
        if (VariableStorage.Soldiers_count < 5 && toggle == false || toggle == true)
        {
            if (toggle == false)
            {
                if (VariableStorage.c1 == "c1")
                    VariableStorage.c1 = toggler.name;
                else if (VariableStorage.c2 == "c2")
                    VariableStorage.c2 = toggler.name;
                else if (VariableStorage.c3 == "c3")
                    VariableStorage.c3 = toggler.name;
                else if (VariableStorage.c4 == "c4")
                    VariableStorage.c4 = toggler.name;
                else if (VariableStorage.c5 == "c5")
                    VariableStorage.c5 = toggler.name;
                VariableStorage.Soldiers_count += 1;

            }
            else
            {
                if (VariableStorage.c1 == toggler.name)
                    VariableStorage.c1 = "c1";
                else if (VariableStorage.c2 == toggler.name)
                    VariableStorage.c2 = "c2";
                else if (VariableStorage.c3 == toggler.name)
                    VariableStorage.c3 = "c3";
                else if (VariableStorage.c4 == toggler.name)
                    VariableStorage.c4 = "c4";
                else if (VariableStorage.c5 == toggler.name)
                    VariableStorage.c5 = "c5";
                VariableStorage.Soldiers_count -= 1;
            }
        }
        Debug.Log(VariableStorage.Soldiers_count + "   " + VariableStorage.c1 + "," + VariableStorage.c2 + "," + VariableStorage.c3 + "," + VariableStorage.c4 + "," + VariableStorage.c5);
    }

    public void Soldiers_send(Text error)
    {
        if (VariableStorage.Soldiers_count == 5)
        {
            error.text = "";

            byte[] messageSent1 = Encoding.ASCII.GetBytes(VariableStorage.c1);
            int byteSent1 = VariableStorage.sender.Send(messageSent1);

            byte[] messageSent2 = Encoding.ASCII.GetBytes(VariableStorage.c2);
            int byteSent2 = VariableStorage.sender.Send(messageSent2);

            byte[] messageSent3 = Encoding.ASCII.GetBytes(VariableStorage.c3);
            int byteSent3 = VariableStorage.sender.Send(messageSent3);

            byte[] messageSent4 = Encoding.ASCII.GetBytes(VariableStorage.c4);
            int byteSent4 = VariableStorage.sender.Send(messageSent4);

            byte[] messageSent5 = Encoding.ASCII.GetBytes(VariableStorage.c5);
            int byteSent5 = VariableStorage.sender.Send(messageSent5);

            Debug.Log(byteSent1 + "," + byteSent2 + "," + byteSent3 + "," + byteSent4 + "," + byteSent5);

            byte[] messageReceived = new byte[1024];

            int byteRecv = VariableStorage.sender.Receive(messageReceived);

            VariableStorage.player = Encoding.ASCII.GetString(messageReceived, 0, byteRecv);

            SceneManager.LoadScene(7);
        }

        else
        {
            error.text = "You need to pick 5 characters!";
        }
    }
}
