using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Character_controller : MonoBehaviour
{
    private int moves = 5;
    private int k = 0;
    public float moveSpeed = 5f;
    public float x1;
    public float x2;
    public float y1;
    public float y2;
    private bool active = false;
    private bool click = false;
    public Transform movepoint;
    private Animator anim;
    private Vector3 change;
    public LayerMask StopMovement;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.gameObject.name);
        if (VariableStorage.player == "1")
        {
            Debug.Log("player1");
            transform.position = new Vector3(x1, y1, -2);
        }
        if (VariableStorage.player == "2")
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
        if (click && moves != 0 && VariableStorage.player == "1")
        {

            change = Vector3.zero;
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, movepoint.position, step);

            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {
                if (Input.GetAxisRaw("Horizontal") == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, StopMovement))
                    {
                        movepoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        change.x = Input.GetAxisRaw("Horizontal");
                        byte[] messageSent1 = Encoding.ASCII.GetBytes(this.gameObject.name + "," + movepoint.position.x + "," + movepoint.position.y);
                        int byteSent = VariableStorage.sender.Send(messageSent1);
                        Debug.Log(this.gameObject.name + " (" + movepoint.position.x + ", " + movepoint.position.y + ")");
                        Debug.Log(moves);
                        moves -= 1;
                        if (moves == 0)
                        {
                            click = false;
                        }
                        Debug.Log(moves);
                    }
                }
            }

            //Debug.Log(Input.GetKeyUp(KeyCode.UpArrow));
            //if (Input.GetKeyUp(KeyCode.UpArrow))
            //{
            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {
                if (Input.GetAxisRaw("Horizontal") == -1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, StopMovement))
                    {
                        movepoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        change.x = Input.GetAxisRaw("Horizontal");
                        byte[] messageSent1 = Encoding.ASCII.GetBytes(this.gameObject.name + "," + movepoint.position.x + "," + movepoint.position.y);
                        int byteSent = VariableStorage.sender.Send(messageSent1);
                        Debug.Log(this.gameObject.name + " (" + movepoint.position.x + ", " + movepoint.position.y + ")");
                        Debug.Log(moves);
                        moves -= 1;
                        if (moves == 0)
                        {
                            click = false;
                        }
                        Debug.Log(moves);
                    }
                }
            }
            //}

            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {
                if (Input.GetAxisRaw("Vertical") == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, StopMovement))
                    {
                        movepoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                        change.y = Input.GetAxisRaw("Vertical");
                        byte[] messageSent1 = Encoding.ASCII.GetBytes(this.gameObject.name + "," + movepoint.position.x + "," + movepoint.position.y);
                        int byteSent = VariableStorage.sender.Send(messageSent1);
                        Debug.Log(this.gameObject.name + " (" + movepoint.position.x + ", " + movepoint.position.y + ")");
                        Debug.Log(moves);
                        moves -= 1;
                        if (moves == 0)
                        {
                            click = false;
                        }
                        Debug.Log(moves);
                    }
                }
            }

            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {
                if (Input.GetAxisRaw("Vertical") == -1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, StopMovement))
                    {
                        movepoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                        change.y = Input.GetAxisRaw("Vertical");
                        byte[] messageSent1 = Encoding.ASCII.GetBytes(this.gameObject.name + "," + movepoint.position.x + "," + movepoint.position.y);
                        int byteSent = VariableStorage.sender.Send(messageSent1);
                        Debug.Log(this.gameObject.name + " (" + movepoint.position.x + ", " + movepoint.position.y + ")");
                        Debug.Log(moves);
                        moves -= 1;
                        if (moves == 0)
                        {
                            click = false;
                        }
                        Debug.Log(moves);
                    }
                }
            }

            if (change != Vector3.zero)
            {
                anim.SetBool("standing", false);
                anim.SetFloat("moveX", change.x);
                anim.SetFloat("moveY", change.y);
                anim.SetBool("moving", true);
                Debug.Log(moves == 0);
            }
            if (change == Vector3.zero)
            {
                anim.SetBool("moving", false);
                anim.SetBool("standing", true);
            }

            if (moves == 0)
            {
                VariableStorage.soldier_clicked = false;
                click = false;
            }

            transform.position = Vector3.MoveTowards(transform.position, movepoint.position, step);
            Thread.Sleep(200);
            anim.SetBool("moving", false);
            anim.SetBool("standing", true);
            if (moves == 0 && VariableStorage.soldiers_clicked == 5)
            {
                moves = 5;
                Thread.Sleep(500);
                byte[] message1 = Encoding.ASCII.GetBytes("2");
                VariableStorage.sender.Send(message1);
                //Thread.Sleep(200);
                VariableStorage.player = "2";
            }

        }
        if (VariableStorage.soldiers_clicked == 5 && moves == 0 && VariableStorage.player == "2")
        {
            Debug.Log("CHANGE");
            moves = 5;
        }
    }


    private void OnMouseDown()
    {
        Debug.Log(VariableStorage.player + moves);
        if (moves != 0 && VariableStorage.player == "1")
        {
            if (VariableStorage.soldier_clicked == false)
            {
                click = true;
                VariableStorage.soldier_clicked = true;
                VariableStorage.soldiers_clicked++;
            }
        }
        Debug.Log(click);
    }
}
