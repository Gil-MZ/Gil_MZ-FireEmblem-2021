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

public class Client : MonoBehaviour
{
    //public static string IPString { get; set; } = "127.0.0.1";
    public GameObject error;
    public GameObject connection_made;
    // Main Method 
    private void Update()
    {
        
        if(VariableStorage.IP != "")
            ExecuteClient(error,connection_made);
    }

    // ExecuteClient() Method 
    static void ExecuteClient(GameObject error, GameObject connection_made)
    {
        try
        {
            if (VariableStorage.User == false)
            {
                Debug.Log("1");
                // Establish the remote endpoint  
                // for the socket. This example  
                // uses port 4242 on the local  
                // computer. 
                //IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                //IPAddress ipAddr = ipHost.AddressList[0];
                //Debug.Log(ipHost.AddressList[0]);
                //IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 4242);
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                //IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(VariableStorage.IP), 4242);
                VariableStorage.IP = "";
                // Creation TCP/IP Socket using  
                // Socket Class Costructor 
                //VariableStorage.sender = new Socket(ipAddr.AddressFamily,
                //   SocketType.Stream, ProtocolType.Tcp);

                VariableStorage.sender = new Socket(AddressFamily.InterNetwork,
                           SocketType.Stream, ProtocolType.Tcp);

                // Connect Socket to the remote  
                // endpoint using method Connect() 
                VariableStorage.sender.Connect(localEndPoint);
                // Creation of messagge that 
                // we will send to Server 
                // byte[] messageSent = Encoding.ASCII.GetBytes("Test Client<EOF>");
                // int byteSent = sender.Send(messageSent);

                // Data buffer 
                byte[] messageReceived = new byte[1024];

                // We receive the messagge using  
                // the method Receive(). This  
                // method returns number of bytes 
                // received, that we'll use to  
                // convert them to string 
                int byteRecv = VariableStorage.sender.Receive(messageReceived);
                Debug.Log("Message from Server -> " +
                          Encoding.ASCII.GetString(messageReceived,
                                                     0, byteRecv));
                VariableStorage.User = true;
                VariableStorage.IP = "";
                VariableStorage.destroyed = true;
                connection_made.SetActive(true);
            }
            // Close Socket using  
            // the method Close() 
            //sender.Shutdown(SocketShutdown.Both);
            //sender.Close();

        }
        // Manage of Socket's Exceptions 
        catch (ArgumentNullException ane)
        {
            error.SetActive(true);
            Debug.Log("ArgumentNullException : {0}" + ane.ToString());
        }

        catch (SocketException se)
        {
            error.SetActive(true);
            Debug.Log("SocketException : {0}" + se.ToString());
        }

        catch (Exception e)
        {
            error.SetActive(true);
            Debug.Log("Unexpected exception : {0}" + e.ToString());
        }
        
    }
}

