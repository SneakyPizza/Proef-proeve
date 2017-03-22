using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapC : Card
{
    protected override void Start()
    {
        base.Start();
    }

    public override void ActivateOther(Player player)
    {
        base.ActivateOther(player);
        PlayerRotation rot = GameObject.FindWithTag(Tags.GAMECONTROLLER).GetComponent<PlayerRotation>();

        if (player != rot.Players[rot.CurrentPlayer])
        {
            Debug.Log("Trap used on" + player);

            _player.Trapped = true;
            Destroy(this.gameObject);
        }
    }
}
