using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Error : MonoBehaviour
{
    public Text E;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (VariableStorage.error || VariableStorage.error1)
            E.text = "invalid data registered or an error occurred, please enter data again";

    }
}
