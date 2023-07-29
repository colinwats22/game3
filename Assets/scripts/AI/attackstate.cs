using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackstate : State
{
    // Start is called before the first frame update
    public override State runcurrentstate()
    {
       Debug.Log("attacked"); 
        
        return this;
    }
}
