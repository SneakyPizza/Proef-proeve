using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeConventions : MonoBehaviour
{   
    //Code conventions
    //summarise important functions ( Comments )
    public bool _TestVar; //public vars with PascalCasing
    protected bool testVar; //protected vars with camelCasing
    private float _testVar; //vars with "_" + camelCase
    public const float TEST_CONST = 0; //constants full Caps with a underscore spacing
    private List<int> IntList = new List<int>(); //PascalCasing
    private delegate void DelegateConvention(); //PascalCasing

    //properties are methods are the only ones allowed to be public
    public object _X
    {//Brackets below the functions/vars etc. 
        get
        {
            return null;
        }
        set
        {

        }
    }
    //PascalCasing for functions
    private void FunctionCodeCon()
    {
        //No Caps when using temporary vars
        Vector2 tempvar = new Vector2();
    }




}
