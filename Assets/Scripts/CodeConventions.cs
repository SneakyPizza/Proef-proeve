using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeConventions : MonoBehaviour
{
    public bool TestVar; //public vars with PascalCasing
    protected bool testVar; //protected vars with camelCasing
    private float _testVar; //vars with "_" + CamelCase
    public const float TEST_CONST = 0; //constants full Caps with a underscore spacing

    private delegate void DelegateConvention(); //PascalCasing

    //properties are methods are the only ones allowed to be public
    public object X
    {
        get
        {
            return null;
        }
        set
        {

        }
    }




}
