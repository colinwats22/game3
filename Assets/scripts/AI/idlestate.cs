using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idlestate : State
{

    public bool canseetheplayer;
    public chasingstate chasingstate;
    
    public override State runcurrentstate()
    {
        if(canseetheplayer)
        {
            return chasingstate;
        }
        else
        {
            return this; 
        }
      
    }
}
