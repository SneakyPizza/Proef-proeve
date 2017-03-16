using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    public GameObject ReturnCol()
    {
        Ray click = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(click.origin, click.direction);

        if(hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        else{
            return null;
        }
    }

}
