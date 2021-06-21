﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Scenes_switcher : MonoBehaviour
{
    public void Register()
    {
        if (VariableStorage.User == true)
        {
            byte[] messageSent = Encoding.ASCII.GetBytes("2");
            int byteSent = VariableStorage.sender.Send(messageSent);
            Debug.Log(2);
            SceneManager.LoadScene(1);
        }
    }

    public void Login()
    {
        if (VariableStorage.User == true)
        {
            byte[] messageSent = Encoding.ASCII.GetBytes("1");
            int byteSent = VariableStorage.sender.Send(messageSent);
            Debug.Log(1);
            SceneManager.LoadScene(2);
        }       
    }

    public void Back()
    {
        byte[] messageSent = Encoding.ASCII.GetBytes("              ");
        int byteSent = VariableStorage.sender.Send(messageSent);
        VariableStorage.error = false;
        VariableStorage.error1 = false;
        SceneManager.LoadScene(0);
    }

    public void Simpleback()
    {
        SceneManager.LoadScene(0);
    }

    public void SpecialBack()
    {
        byte[] messageSent = Encoding.ASCII.GetBytes("              ");
        int byteSent = VariableStorage.sender.Send(messageSent);
        VariableStorage.username = "";
        VariableStorage.password = "";
        VariableStorage.email = "";
        VariableStorage.error = false;
        VariableStorage.error1 = false;
        SceneManager.LoadScene(0);
    }

    public void display()
    {
        SceneManager.LoadScene(4);
    }

    public void About()
    {
        if(VariableStorage.User == true)
            SceneManager.LoadScene(5);
    }

    public void Rules()
    {
        if(VariableStorage.User == true)
            SceneManager.LoadScene(6);
    }

}