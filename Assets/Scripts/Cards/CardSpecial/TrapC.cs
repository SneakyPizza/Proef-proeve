using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapC : Card
{
    [SerializeField] private GameObject _trapObject;
    protected override void Start()
    {
        base.Start();
    }

    public override void ActivateSelf(Player _player = null, Node node = null)
    {
        ActivateOther(_player, node);
    }

    public override void ActivateOther(Player player = null, Node node = null)
    {
        //base.ActivateOther(player);
        Debug.Log("Should Activate on other" + player + node);
        PlayerRotation rot = GameObject.FindWithTag(Tags.GAMECONTROLLER).GetComponent<PlayerRotation>();

        if (player != null)
        {
            Debug.Log("On Player");
            if (player != rot.Players[rot.CurrentPlayer])
            {
                Debug.Log("Trap used on" + player);
                player.Trapped = true;
                Destroy(this.gameObject);
            }
        }

        if(node != null)
        {
            node.Occupied = true;
        }
    }
}
