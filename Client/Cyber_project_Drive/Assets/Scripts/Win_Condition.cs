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

public class Win_Condition : MonoBehaviour
{
    public GameObject win;
    private double time = 0;
    // Update is called once per frame
    void Update()
    {
        if(VariableStorage.data == "exitexit32exitexit")
        {
            win.SetActive(true);
            Thread.Sleep(2500);
            Application.Quit();
            Debug.Log("QUIT!");
        }
        time += Time.deltaTime;
        if (time > 0.5  && VariableStorage.player == "2")
        {
            time = 0;
            Debug.Log("Sent!");
            byte[] message = Encoding.ASCII.GetBytes("-1");
            VariableStorage.sender.Send(message);
        }
    }
}
