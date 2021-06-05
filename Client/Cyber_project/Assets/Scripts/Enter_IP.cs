using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enter_IP : MonoBehaviour
{
    public void IP_got(Text ip)
    {
        VariableStorage.IP =  ip.text;
    }
}
