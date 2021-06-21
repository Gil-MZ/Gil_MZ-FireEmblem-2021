using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Exit_game()
    {
        byte[] messageSent = Encoding.ASCII.GetBytes("exitexit32exitexit");
        int byteSent = VariableStorage.sender.Send(messageSent);
        Application.Quit();
        Debug.Log("Quit!");
    }
}
