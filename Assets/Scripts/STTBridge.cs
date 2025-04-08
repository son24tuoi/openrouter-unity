using System.Collections;
using System.Collections.Generic;
using Meta.WitAi;
using UnityEngine;
using TMPro;
using Oculus.Voice;
using System;

public class STTBridge : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField promptInputField;

    [Header("Listen Settings")]
    [SerializeField] private AppVoiceExperience _appVoiceExperience;
    [Tooltip("Text color while receiving text")]
    [SerializeField] private Color _transcriptionColor = Color.black;

    [Header("Prompt Settings")]
    [Tooltip("Color to be used for prompt text")]
    [SerializeField] private Color _promptColor = new Color(0.2f, 0.2f, 0.2f);

    [Header("Error Settings")]
    [Tooltip("Color to be used for error text")]
    [SerializeField] private Color _errorColor = new Color(0.8f, 0.2f, 0.2f);

    private bool _active = false;

    // Add service delegates
    private void OnEnable()
    {
        _appVoiceExperience.VoiceEvents.OnStartListening.AddListener(OnStartListening);
        _appVoiceExperience.VoiceEvents.OnPartialTranscription.AddListener(OnTranscriptionChange);
        _appVoiceExperience.VoiceEvents.OnFullTranscription.AddListener(OnTranscriptionChange);
        _appVoiceExperience.VoiceEvents.OnCanceled.AddListener(OnCanceled);
        _appVoiceExperience.VoiceEvents.OnError.AddListener(OnError);
        _appVoiceExperience.VoiceEvents.OnStoppedListening.AddListener(OnStopListenning);
    }

    // Remove service delegates
    private void OnDisable()
    {
        _appVoiceExperience.VoiceEvents.OnStartListening.RemoveListener(OnStartListening);
        _appVoiceExperience.VoiceEvents.OnPartialTranscription.RemoveListener(OnTranscriptionChange);
        _appVoiceExperience.VoiceEvents.OnFullTranscription.RemoveListener(OnTranscriptionChange);
        _appVoiceExperience.VoiceEvents.OnCanceled.RemoveListener(OnCanceled);
        _appVoiceExperience.VoiceEvents.OnError.RemoveListener(OnError);
        _appVoiceExperience.VoiceEvents.OnStoppedListening.RemoveListener(OnStopListenning);
    }

    // Set listening
    private void OnStartListening()
    {
        _active = true;
    }

    private void OnStopListenning()
    {
        _active = false;
    }

    // Set text change
    private void OnTranscriptionChange(string text)
    {
        SetText(text, _transcriptionColor);
    }

    // Reset text if canceled
    private void OnCanceled(string reason)
    {

    }

    // Apply error
    private void OnError(string status, string error)
    {
        SetText($"[{status}] {error}", _errorColor);
    }

    // Refresh text
    private void SetText(string newText, Color newColor)
    {
        // Ignore if same
        if (string.Equals(newText, promptInputField.text) && newColor == promptInputField.textComponent.color)
        {
            return;
        }

        // Apply text & color
        promptInputField.text = newText;
        promptInputField.textComponent.color = newColor;
    }

    private void SetText(string newText)
    {
        // Ignore if same
        if (string.Equals(newText, promptInputField.text))
        {
            return;
        }

        // Apply text
        promptInputField.text = newText;
    }

    public void SetActivation(bool active)
    {
        // if (_active == active)
            // return;

        // _active = active;

        if (active)
        {
            _appVoiceExperience.Activate();
        }
        else
        {
            _appVoiceExperience.Deactivate();
        }
    }
}
