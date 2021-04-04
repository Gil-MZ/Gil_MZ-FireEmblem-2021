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
    private bool active = true;
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
        if (VariableStorage.data != "" && VariableStorage.player == "2")
        {
            if (VariableStorage.data != "2")
            {
                string message = VariableStorage.data;
                string[] a = message.Split(',');
                //Debug.Log(message.Substring(message.IndexOf(","), message.IndexOf(")")));
                //if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
                //{
                float i = float.Parse(a[1]);
                float k = float.Parse(a[2]);
                if (a[0] == this.gameObject.name.Substring(0, this.gameObject.name.Length - 4))
                {
                    Debug.Log(a[0] == this.gameObject.name.Substring(0, this.gameObject.name.Length - 4));
                    Debug.Log(a[0] + "," + a[1] + "," + a[2]);
                    if (!moved)
                    {

                        if (i > this.gameObject.transform.position.x)
                        {
                            movepoint.position += new Vector3(1f, 0f, 0f);
                            anim.SetBool("standing", false);
                            anim.SetFloat("moveX", 1);
                            anim.SetFloat("moveY", 0);
                            anim.SetBool("moving", true);
                            moved = true;
                        }
                        else if (i < this.gameObject.transform.position.x)
                        {
                            movepoint.position += new Vector3(-1f, 0f, 0f);
                            anim.SetBool("standing", false);
                            anim.SetFloat("moveX", -1);
                            anim.SetFloat("moveY", 0);
                            anim.SetBool("moving", true);
                            moved = true;
                        }
                        else if (k > this.gameObject.transform.position.y)
                        {
                            movepoint.position += new Vector3(0f, 1f, 0f);
                            anim.SetBool("standing", false);
                            anim.SetFloat("moveX", 0);
                            anim.SetFloat("moveY", 1);
                            anim.SetBool("moving", true);
                            moved = true;
                        }
                        else if (k < this.gameObject.transform.position.y)
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
        if (VariableStorage.data == "2" && Vector3.Distance(transform.position, movepoint.position) < 0.000005f)
        {
            Thread.Sleep(200);
            VariableStorage.player = "1";
            VariableStorage.soldiers_clicked = 0;
            VariableStorage.data = "";
        }
    }
}
