using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeConventions : MonoBehaviour
{
    private float _testVar; //vars with "_" + CamelCase
    public const float TEST_CONST = 0; //Constants full Caps with a underscore spacing

    private delegate void DelegateConvention(); //CamelCase met eerste letterCaps

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
