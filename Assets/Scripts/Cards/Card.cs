using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private GameObject _selectedCard;
    private CardManager _cardManager;
    private bool _cardClicked = false;
    private bool _cardSelected = false;

    public bool CardSelected
    {
        get
        {
            return _cardSelected;
        }      
    }

    private void OnCardClick()
    {
      
        if (Input.GetMouseButtonDown(0) && GameObject.FindGameObjectWithTag("Card"))
        {
            SelectCard();
            _cardClicked = true;
        }

        if (_cardClicked == true)
        {
            DeactivateCard();
            _cardClicked = false;
        }

    }

    public void DeactivateCard()
    {
        _cardSelected = false;
    }
    public void SelectCard()
    {
        _selectedCard = gameObject;
        _cardSelected = true;
    }
    public virtual void ActivateCard()
    {
       if(_cardSelected == false)
        {
            return;
        } 
    }
}
