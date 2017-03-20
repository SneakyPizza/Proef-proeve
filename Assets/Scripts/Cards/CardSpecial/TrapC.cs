using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapC : Card
{
    public override void ActivateOther(Player player)
    {
        base.ActivateOther(player);
        PlayerRotation rot = GameObject.FindWithTag(Tags.GAMECONTROLLER).GetComponent<PlayerRotation>();

        if (player != rot.Players[rot.CurrentPlayer])
        {
            //TODO: skip walk
            Debug.Log("Trap used on" + player);

            player.EndTurn();
        }
    }
}
