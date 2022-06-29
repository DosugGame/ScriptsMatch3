using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimitionsEyes : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _render;
    public void SwitchSprite(int number)
    {
        _render.sprite = _sprites[number];
    }

}
