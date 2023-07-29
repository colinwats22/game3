using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasingstate : State
{

    public attackstate attackstate;
    public bool isinattackingrange; 
    public searchingstate searchingstate; 
    public override State runcurrentstate()
    {
        if (isinattackingrange)
        {
            return attackstate;
        }
        else
        {
            return this;
        }
    }
}
