using System.Collections;
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
        byte[] messageSent = Encoding.ASCII.GetBytes("2");
        int byteSent = VariableStorage.sender.Send(messageSent);
        SceneManager.LoadScene(1);
    }

    public void Login()
    {
        byte[] messageSent = Encoding.ASCII.GetBytes("1");
        int byteSent = VariableStorage.sender.Send(messageSent);
        SceneManager.LoadScene(2);
        
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
        byte[] messageSent1 = Encoding.ASCII.GetBytes(VariableStorage.username);
        int byteSent1 = VariableStorage.sender.Send(messageSent1);
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
        SceneManager.LoadScene(5);
    }

    public void Rules()
    {
        SceneManager.LoadScene(6);
    }

}
