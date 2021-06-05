using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Attack_range : MonoBehaviour
{
    public GameObject attackrange;
    bool timeactive = false;
    float timestart = 0;
    // Start is called before the first frame update
    void Start()
    {
        VariableStorage.attacking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeactive)
        {
            timestart += Time.deltaTime;
            if (timestart > 1)
            {
                Debug.Log("stoped");
                VariableStorage.attacking = false;
                timeactive = false;
                attackrange.SetActive(false);
                timestart = 0;
            }

        }
    }

    private void OnMouseDown()
    {
        byte[] messageSent = Encoding.ASCII.GetBytes("0");
        int byteSent = VariableStorage.sender.Send(messageSent);
        byte[] messageSent1 = Encoding.ASCII.GetBytes("0");
        int byteSent1 = VariableStorage.sender.Send(messageSent);
        timeactive = true;
    }
}
