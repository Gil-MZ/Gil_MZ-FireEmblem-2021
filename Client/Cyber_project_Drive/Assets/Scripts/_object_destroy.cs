using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _object_destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (VariableStorage.destroyed == true)
            Destroy(this.gameObject);
    }
}
