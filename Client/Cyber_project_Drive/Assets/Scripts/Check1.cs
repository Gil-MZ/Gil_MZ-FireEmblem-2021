using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class Check1 : MonoBehaviour
{
    private double time = 0;
    public Camera cam;
    // Start is called before the first frame update

    private void Update()
    {
        time += Time.deltaTime;
        if(time >= 6.8)
        {
            time = 0;
            VariableStorage.active = false;
            Change_Cam1(cam);
        }
        if(VariableStorage.active)
        {
            Change_Cam(cam);
        }
    }

    public void Change_Cam(Camera cam)
    {
        Debug.Log("change cam");
        cam.orthographicSize = 5;
        cam.transform.SetPositionAndRotation(new Vector3(-9.9f, 14.8f, -10f), new Quaternion(0f, 0f, 0f, 0f));
    }

    public void Change_Cam1(Camera cam)
    {
        Debug.Log("change cam 2");
        cam.orthographicSize = 15;
        cam.transform.SetPositionAndRotation(new Vector3(-13.6f, -7.36f, -10f), new Quaternion(0f, 0f, 0f, 0f));
        this.gameObject.SetActive(false);
    }
}

