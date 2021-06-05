using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class VariableStorage : MonoBehaviour
{
    public static Socket sender;
    public static bool datasend = false;
    public static bool destroyed = false;
    public static string data = "";
    public static bool attacking = false;
    public static GameObject attacker;
    public static string attacker_name = "";
    public static string getting_attacked = "";
    public static string attack = "";
    public static string player = "0";
    public static string player2 = "0";
    public static int Soldiers_count = 0;
    public static bool soldier_clicked = false;
    public static int soldiers_clicked = 0;
    public static bool User = false;
    public static bool error = false;
    public static bool error1 = false;
    public static string IP = "";
    public static string enemy_name = "";
    public static string username;
    public static string password;
    public static string email;
    public static string wins = "0";
    public static string losses = "0";
    public static string c1 = "c1";
    public static string c2 = "c2";
    public static string c3 = "c3";
    public static string c4 = "c4";
    public static string c5 = "c5";
    public static string c6 = "c6";
    public static string c7 = "c7";
    public static string c8 = "c8";
    public static string c9 = "c9";
    public static string c10 = "c10";

}
