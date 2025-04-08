using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using OpenRouter.Samples.DeepSeek;

public class SendButtonManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private DeepSeekChat deepSeekChat;
    [SerializeField] private STTBridge sttBridge;
    [SerializeField] private TMP_InputField promptInputField;

    [Header("Graphics")]
    [SerializeField] private GameObject sendText;
    [SerializeField] private GameObject micImage;

    [Header("Settings")]
    private bool recording;

    private void Start()
    {
        ShowSendText();
        promptInputField.onValueChanged.AddListener(InputFieldValueChangeCallback);
    }

    public void PointerDownCallback()
    {
        if (promptInputField.text.Length > 0)
        {
            deepSeekChat.SendButtonCallback();
        }
        else
        {
            sttBridge.SetActivation(true);
            recording = true;
            ShowMicImage();
        }
    }

    public void PointerUpCallback()
    {
        sttBridge.SetActivation(false);
        recording = false;

        InputFieldValueChangeCallback(promptInputField.text);
    }

    private void InputFieldValueChangeCallback(string prompt)
    {
        if (recording)
            return;

        if (prompt.Length < 0)
            ShowMicImage();
        else
            ShowSendText();
    }

    private void ShowSendText()
    {
        sendText.SetActive(true);
        micImage.SetActive(false);
    }

    private void ShowMicImage()
    {
        micImage.SetActive(true);
        sendText.SetActive(false);
    }
}
