using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statemanager : MonoBehaviour
{

    State currentstate;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        
    }

    private void runstatemachine()
    {
        State nextstate = currentstate?.runcurrentstate();

        if(nextstate != null)
        {

        }
    }
}
