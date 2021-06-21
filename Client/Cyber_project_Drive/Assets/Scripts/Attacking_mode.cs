using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attacking_mode : MonoBehaviour
{
    public GameObject[] characters;
    private float time_passed = 0;
    private int attacker = 3;
    private int attacked = 11;
    public GameObject cam;
    public Canvas can;
    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("start");
        if (VariableStorage.attacker_name != "" && VariableStorage.getting_attacked != "")
        {
            Debug.Log(VariableStorage.attacker_name + "," + VariableStorage.getting_attacked);
            string a;
            string b;
            for (int i = 10; i <= 19; i++)
            {
                if (attacker < 10)
                {
                    a = VariableStorage.attacker_name + "_attack";
                    Debug.Log(characters[i].name + ", " + a);
                    if (a == characters[i].name)
                        attacker = i;
                }
            }
            for (int i = 0; i <= 10; i++)
            {
                if (attacked >= 10)
                {
                    b = VariableStorage.getting_attacked + "_dummy";
                    Debug.Log(b);
                    if (b == characters[i].name)
                        attacked = i;
                }
            }
            Debug.Log(attacker + ", " + attacked);
            characters[attacker].SetActive(true);
            characters[attacked].SetActive(true);
            VariableStorage.attacker_name = "";
            VariableStorage.getting_attacked = "";
            cam.SetActive(true);
            Debug.Log("mini finish");
        }
    }

    private void Update()
    {
        if (VariableStorage.attacker_name != "" && VariableStorage.getting_attacked != "")
        {
            Debug.Log(VariableStorage.attacker_name + "," + VariableStorage.getting_attacked);
            string a;
            string b;
            for (int i = 10; i <= 19; i++)
            {
                if (attacker < 10)
                {
                    a = VariableStorage.attacker_name + "_attack";
                    Debug.Log(i);
                    if (a == characters[i].name)
                        attacker = i;
                }
            }
            for (int i = 0; i <= 10; i++)
            {
                if (attacked >= 10)
                {
                    b = VariableStorage.getting_attacked + "_dummy";
                    Debug.Log(i);
                    if (b == characters[i].name)
                        attacked = i;
                }
            }
            
            Debug.Log(attacker + ", " + attacked);
            characters[attacker].SetActive(true);
            characters[attacked].SetActive(true);
            VariableStorage.attacker_name = "";
            VariableStorage.getting_attacked = "";
            cam.SetActive(true);
            Debug.Log("mini finish");
        }
        time_passed += Time.deltaTime;
        Check_time();
    }
    void Check_time()
    {
        if (time_passed >= 7.0)
        {
            cam.SetActive(false);
            can.gameObject.SetActive(true);
            characters[attacker].SetActive(false);
            characters[attacked].SetActive(false);
            Debug.Log(VariableStorage.attacker_name + ", " + VariableStorage.getting_attacked);
            Debug.Log("FINISH!");
            attacker = 3;
            attacked = 11;
            time_passed = 0;
            VariableStorage.active = true;
            this.gameObject.SetActive(false);
        }
    }

    
}
