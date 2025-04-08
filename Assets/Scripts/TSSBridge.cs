using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi.TTS.Utilities;

public class TSSBridge : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TTSSpeaker speaker;

    [Header("Datas")]
    private string[] messages;

    private void Start()
    {
        // DiscussionManager.onChatGPTMessageReceived += Speak;
        DiscussionBubble.onSpeakButtonClicked += SpeakButtonClickedCallback;
    }

    private void OnDestroy()
    {
        // DiscussionManager.onChatGPTMessageReceived -= Speak;
        DiscussionBubble.onSpeakButtonClicked -= SpeakButtonClickedCallback;
    }

    private void Speak(string message)
    {
        messages = message.Split('.');
        StartCoroutine(speaker.SpeakQueuedAsync(messages));
        // speaker.Speak(message);
    }

    private void SpeakButtonClickedCallback(string message)
    {
        if (speaker.IsSpeaking)
        {
            speaker.Stop();
        }
        else
        {
            Speak(message);
        }
    }
}
