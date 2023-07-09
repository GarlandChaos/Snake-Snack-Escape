using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteSetter : MonoBehaviour
{
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private Sprite releasedSprite;
    private Image currentSprite;
    private Button button;
    private bool pressed = false;

    private void Awake()
    {
        currentSprite = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        ToggleSprite();
    }

    private void ToggleSprite()
    {
        pressed = !pressed;
        currentSprite.sprite = pressed ? pressedSprite : releasedSprite;
    }
}
