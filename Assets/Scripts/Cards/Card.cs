using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private GameObject _selectedCard;
    private CardManager _cardManager;
    protected Player _player;
    protected PlayerRotation _playerRotation;
    private bool _cardSelected = false;

    public virtual void ActivateSelf(Player _player)
    {
        Debug.Log("Activated " + this + " Card");
    }
    public virtual void ActivateOther(Player _player)
    {
        Debug.Log("Activated" + this + " Card on : \"TARGET\"");
    }

    public void DeactivateCard()
    {
        Debug.Log("Deactivate");
        _cardSelected = false;
    }
    public void SelectCard()
    {
        Debug.Log("Selected");
        //_selectedCard = gameObject;
        _cardSelected = true;
    }
    public bool CardSelected
    {
        get
        {
            return _cardSelected;
        }
    }
}
