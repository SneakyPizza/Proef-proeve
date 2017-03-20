using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapC : Card
{
    [SerializeField] private GameObject _trap;
    private Player _player;

    private void Update()
    {
        if (CardSelected && GameObject.FindGameObjectWithTag("Player"))
        {
            ActivateTrap();
        }
    }
 

    private void ActivateTrap()
    {
        

      
       
    }
}
