using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statemanager : MonoBehaviour
{

  public   State currentstate;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        runstatemachine();
    }

    private void runstatemachine()
    {
        State nextstate = currentstate?.runcurrentstate();

        if(nextstate != null)
        {
            //switch to next state
            switchtonextstate(nextstate);
        }
    }
    private void switchtonextstate(State nextstate)
    {
        currentstate = nextstate; 
    }
}
