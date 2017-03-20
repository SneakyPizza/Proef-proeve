using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoC : Card
{

    public override void ActivateOther(Player player)
    {
        base.ActivateOther(player);
        PlayerRotation rot = GameObject.FindWithTag(Tags.GAMECONTROLLER).GetComponent<PlayerRotation>();

        if (player != rot.Players[rot.CurrentPlayer])
        {
            //TODO: set back clicked player 1 turn
            Debug.Log("Lasso used on" + player);
        } 
    }
	

}
