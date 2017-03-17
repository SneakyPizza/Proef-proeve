using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRandomizer : MonoBehaviour {

    [SerializeField]private Sprite[] _backgrounds = new Sprite[8];
    private SpriteRenderer _renderer;
    private int r;

    private void Awake()
    {
        r = Random.Range(0, _backgrounds.Length);
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _renderer.sprite = _backgrounds[r];
    }
}
