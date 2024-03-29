﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// this is a UI component that can show a message, icon and button
[RequireComponent(typeof(RectXformMover))]
public class MessageWindow : MonoBehaviour 
{
    public Image messageIcon;
    public Text messageText;
    public Text buttonText;

    public void ShowMessage(Sprite sprite = null, string message = "", string buttonMsg = "start")
    {
        if (messageIcon != null) 
        {
            messageIcon.sprite = sprite;
        }

        if (messageText != null)
        {
            messageText.text = message;
        }
			
        if (buttonText != null)
        {
            buttonText.text = buttonMsg;
        }
    }
}