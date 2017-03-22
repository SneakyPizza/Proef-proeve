using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPocketC : Card
{
    public override void ActivateOther(Player player = null, Node node = null)
    {
        base.ActivateOther(player);
        PlayerRotation rot = GameObject.FindWithTag(Tags.GAMECONTROLLER).GetComponent<PlayerRotation>();
        if (player != rot.Players[rot.CurrentPlayer])
        {
            //if player clicked, select 1 card
            Debug.Log("Pickpocket used on" + player);
        }
    }

}
