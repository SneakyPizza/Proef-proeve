using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private GameObject _selectedCard;
    private CardManager _cardManager;
    private bool _cardClicked = false;
    private bool _cardSelected = false;
    [SerializeField]private List<Sprite> _cardSprites = new List<Sprite>();
 
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
        Debug.Log("Deactivate");
        _cardSelected = false;
    }
    public void SelectCard()
    {
        Debug.Log("Selected");
        _selectedCard = gameObject;
        _cardSelected = true;
    }
    public void ActivateCard()
    {
       if(_cardSelected == false)
        {
            return;
        } 
    }

    public bool CardSelected
    {
        get
        {
            return _cardSelected;
        }
    }
}
