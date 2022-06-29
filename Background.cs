using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Sprite[] _backgrounds;
    private SpriteRenderer _render;

    private void FindRender()
    {
        _render = GetComponentInChildren<SpriteRenderer>();
    }
    public void GetBackground(int number)
    {
        FindRender();
        if (number < 0) number = 0;
        if (number > _backgrounds.Length - 1) number = _backgrounds.Length - 1;
        _render.sprite = _backgrounds[number];
    }
}
