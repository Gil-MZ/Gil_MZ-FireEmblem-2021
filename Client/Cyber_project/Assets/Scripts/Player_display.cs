using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_display : MonoBehaviour
{
    public Text user;
    public Text enemy;
    // Start is called before the first frame update
    void Start()
    {
        if (VariableStorage.player == "1")
        {
            user.text = "Player1: " + VariableStorage.username;
            enemy.text = "Player2: " + VariableStorage.enemy_name;
        }
        else
        {
            user.text = "Player2: " + VariableStorage.username;
            enemy.text = "Player1: " + VariableStorage.enemy_name;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
