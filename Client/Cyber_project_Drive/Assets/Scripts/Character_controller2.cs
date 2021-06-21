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

public class Character_controller2 : MonoBehaviour
{
    private int moves = 6;
    public static string player;
    public float moveSpeed = 5f;
    public float x1;
    public float x2;
    public float y1;
    public float y2;
    private bool moved = false;
    private bool move = true;
    private bool active = true;
    public Canvas can;
    public GameObject attack;
    public Transform movepoint;
    private Animator anim;
    private Vector3 change;
    public LayerMask StopMovement;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.gameObject.name);
        if (VariableStorage.player2 == "1")
        {
            Debug.Log("player1");
            transform.position = new Vector3(x1, y1, -2);
        }
        if (VariableStorage.player2 == "2")
        {
            Debug.Log("player2");
            transform.position = new Vector3(x2, y2, -2);
        }
        anim = GetComponent<Animator>();
        movepoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (VariableStorage.data != "" && VariableStorage.data != "exitexit32exitexit" && VariableStorage.data != "00")
        {
            if (VariableStorage.data != "2")
            {
                string message = VariableStorage.data;
                if (message.IndexOf('!') != -1)
                {
                    VariableStorage.data = "";
                    string[] b = message.Split('!');
                    VariableStorage.attacker_name = b[0];
                    VariableStorage.getting_attacked = b[1];
                    Debug.Log(VariableStorage.attacker_name + ", " + VariableStorage.getting_attacked);
                    if(VariableStorage.attacker_name != "0" && VariableStorage.getting_attacked != "0" && VariableStorage.attacker_name != "" && VariableStorage.getting_attacked != "")
                    {
                        can.gameObject.SetActive(false);
                        attack.SetActive(true);
                    }
                }
                else
                {
                    string[] a = message.Split(',');
                    //Debug.Log(message.Substring(message.IndexOf(","), message.IndexOf(")")));
                    //if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
                    //{
                    Debug.Log(a);
                    float i = float.Parse(a[1]);
                    float k = float.Parse(a[2]);
                    if (a[0] == this.gameObject.name.Substring(0, this.gameObject.name.Length - 4))
                    {
                        Debug.Log(a[0] == this.gameObject.name.Substring(0, this.gameObject.name.Length - 4) + "heyyyyy");
                        Debug.Log(a[0] + "," + a[1] + "," + a[2] + "heyyyyyy");
                        if (!moved)
                        {

                            if (i > this.gameObject.transform.position.x && move)
                            {
                                move = false;
                                movepoint.position += new Vector3(1f, 0f, 0f);
                                anim.SetBool("standing", false);
                                anim.SetFloat("moveX", 1);
                                anim.SetFloat("moveY", 0);
                                anim.SetBool("moving", true);
                                moved = true;
                            }
                            else if (i < this.gameObject.transform.position.x && move)
                            {
                                movepoint.position += new Vector3(-1f, 0f, 0f);
                                anim.SetBool("standing", false);
                                anim.SetFloat("moveX", -1);
                                anim.SetFloat("moveY", 0);
                                anim.SetBool("moving", true);
                                moved = true;
                            }
                            else if (k > this.gameObject.transform.position.y && move)
                            {
                                movepoint.position += new Vector3(0f, 1f, 0f);
                                anim.SetBool("standing", false);
                                anim.SetFloat("moveX", 0);
                                anim.SetFloat("moveY", 1);
                                anim.SetBool("moving", true);
                                moved = true;
                            }
                            else if (k < this.gameObject.transform.position.y && move)
                            {
                                movepoint.position += new Vector3(0f, -1f, 0f);
                                anim.SetBool("standing", false);
                                anim.SetFloat("moveX", 0);
                                anim.SetFloat("moveY", -1);
                                anim.SetBool("moving", true);
                                moved = true;
                            }
                            else
                            {
                                anim.SetBool("moving", false);
                                anim.SetBool("standing", true);
                            }
                        }
                        Debug.Log(movepoint.position + ", " + transform.position);
                        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, movepoint.position, moveSpeed * Time.deltaTime);

                        if (Vector3.Distance(transform.position, movepoint.position) < 0.000005f)
                        {
                            move = true;
                            anim.SetBool("moving", false);
                            anim.SetBool("standing", true);
                            moved = false;
                            active = false;
                            VariableStorage.data = "";
                        }
                        //if (VariableStorage.data == "2")
                        //{
                        //this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, movepoint.position, moveSpeed * Time.deltaTime);
                        Thread.Sleep(200);
                    }
                }
            }

        }
        if (VariableStorage.data == "2" && Vector3.Distance(transform.position, movepoint.position) < 0.000005f)
        {
            Thread.Sleep(2000);
            VariableStorage.player = "1";
            VariableStorage.soldiers_clicked = 0;
            VariableStorage.data = "";
        }
    }

    private void OnMouseDown()
    {
        ///Debug.Log(VariableStorage.attacking);
        ///Debug.Log(Vector3.Distance(this.gameObject.transform.position, VariableStorage.attacker.transform.position));
        if (VariableStorage.attacking == true)
        {
            VariableStorage.range = false;
            VariableStorage.attacking = false;
            Debug.Log(Vector3.Distance(this.gameObject.transform.position, VariableStorage.attacker.transform.position));
            if (VariableStorage.attacker.name == "Eliwood" || VariableStorage.attacker.name == "Hawkeye" || VariableStorage.attacker.name == "Lyn" || VariableStorage.attacker.name == "Hector"
                || VariableStorage.attacker.name == "Serra" || VariableStorage.attacker.name == "Alfonse" || VariableStorage.attacker.name == "Marth" || VariableStorage.attacker.name == "Roy")

                if (Vector3.Distance(this.gameObject.transform.position, VariableStorage.attacker.transform.position) <= 1000)
                    VariableStorage.getting_attacked = this.gameObject.name.Substring(0, this.gameObject.name.Length - 4);


            if(VariableStorage.attacker.name == "Rebecca" || VariableStorage.attacker.name == "Lucina")
                if (Vector3.Distance(this.gameObject.transform.position, VariableStorage.attacker.transform.position) <= 1000)
                    VariableStorage.getting_attacked = this.gameObject.name.Substring(0, this.gameObject.name.Length - 4);
            Debug.Log(VariableStorage.getting_attacked);
            VariableStorage.attacker_name = VariableStorage.attacker.name;
            if (VariableStorage.getting_attacked != "")
            {
                byte[] messageSent = Encoding.ASCII.GetBytes(VariableStorage.attacker.name);
                int byteSent = VariableStorage.sender.Send(messageSent);
                byte[] messageSent1 = Encoding.ASCII.GetBytes(VariableStorage.getting_attacked);
                int byteSent1 = VariableStorage.sender.Send(messageSent1);
                Debug.Log(VariableStorage.attacker_name + ", " + VariableStorage.getting_attacked);
                can.gameObject.SetActive(false);
                attack.SetActive(true);
            }
            else
            {
                byte[] messageSent = Encoding.ASCII.GetBytes("0");
                int byteSent = VariableStorage.sender.Send(messageSent);
                byte[] messageSent1 = Encoding.ASCII.GetBytes("0");
                int byteSent1 = VariableStorage.sender.Send(messageSent1);
            }
        }
    }
}
