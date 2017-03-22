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

    protected virtual void Start()
    {
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<Player>();
    }

    public virtual void ActivateSelf(Player _player = null, Node node = null)
    {
        Debug.Log("Activated " + this + " Card");
    }
    public virtual void ActivateOther(Player _player = null, Node node = null)
    {
        Debug.Log("Activated" + this + " Card on : " + _player + " | " + node);
    }

    public void DeactivateCard()
    {
        Debug.Log("Deactivate");
        _cardSelected = false;
    }
    public void SelectCard()
    {
        Debug.Log("Selected");
        _selectedCard = gameObject;
        _cardSelected = true;
    }
    public bool CardSelected
    {
        get
        {
            return _cardSelected;
        }
        set
        {
            _cardSelected = value;
        }
    }
}
