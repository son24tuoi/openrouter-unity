using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DiscussionBubble : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Image bubbleImage;
    [SerializeField] private GameObject speakButton;

    [Header("Settings")]
    [SerializeField] private Color userBubbleColor;

    [Header("Events")]
    public static Action<string> onSpeakButtonClicked;

    public void Configure(string message, bool isUserMessage)
    {
        if (isUserMessage)
        {
            bubbleImage.color = userBubbleColor;
            speakButton.SetActive(false);
        }

        messageText.alignment = isUserMessage ? TextAlignmentOptions.Right : TextAlignmentOptions.Left;
        messageText.text = message;
    }

    public void SpeakButtonCallback()
    {
        onSpeakButtonClicked?.Invoke(messageText.text);
    }

    public void CopyToClipboardCallback()
    {
        GUIUtility.systemCopyBuffer = messageText.text;
    }
}
