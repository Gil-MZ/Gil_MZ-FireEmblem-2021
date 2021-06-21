using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class Player_display : MonoBehaviour
{
    private bool active = true;
    public Text user;
    public Text enemy;
    // Start is called before the first frame update
    void Start()
    {
        if (user.text == "")
        {
            if (VariableStorage.player == "1")
            {
                user.text = "Player1: " + VariableStorage.username;
                enemy.text = "Player2: ";
            }
            else
            {
                user.text = "Player2: " + VariableStorage.username;
                enemy.text = "Player1: ";
            }
        }
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
