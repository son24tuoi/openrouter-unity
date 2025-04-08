using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using OpenRouter.Chat;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

using Random = UnityEngine.Random;

namespace OpenRouter.Samples.DeepSeek
{
    public class DeepSeekChat : MonoBehaviour
    {
        [Header("Elements")]
        [SerializeField] private DiscussionBubble bubblePrefab;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Transform bubblesParent;

        private readonly string openRouterApiKey = "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";
        // private readonly string openRouterApiKey = "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";
        private readonly string modelName = "deepseek/deepseek-r1:free";
        // private readonly string modelName = "google/gemini-2.5-pro-exp-03-25:free";
        private readonly string apiUrl = "https://openrouter.ai/api/v1/chat/completions";

        [SerializeField] private List<Chat.Message> messages = new List<Chat.Message>();
        [SerializeField] private ChatResponse chatResponse;

        public static Action onMessageReceived;

        private void Start()
        {
            StartCoroutine(SendChatRequest("Xin chào"));
        }

        private IEnumerator SendChatRequest(string prompt)
        {
            Chat.Message message = new Chat.Message(Role.Assistant, prompt);
            messages.Add(message);
            // Tạo request object
            ChatRequest requestData = new ChatRequest(modelName, messages);

            string jsonPayload = JsonConvert.SerializeObject(requestData);
            Debug.Log(jsonPayload);
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);

            using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
            {
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Authorization", "Bearer " + openRouterApiKey);
                request.SetRequestHeader("Content-Type", "application/json");
                // request.SetRequestHeader("HTTP-Referer", "https://yourwebsite.com"); // Optional
                // request.SetRequestHeader("X-Title", "YourAppName");                  // Optional

                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("API Error: " + request.error + "\n" + request.downloadHandler.text);
                }
                else
                {
                    string responseJson = request.downloadHandler.text;
                    chatResponse = JsonConvert.DeserializeObject<ChatResponse>(responseJson);
                    Debug.Log("Response: " + responseJson);
                    CreateBubble(chatResponse.FirstChoice.ToString(), false);
                }
            }
        }

        public void SendButtonCallback()
        {
            string prompt = inputField.text;
            inputField.text = string.Empty;

            CreateBubble(prompt, true);
            // CreateBubble("Message Received: " + Random.Range(1, 100), false);

            inputField.text = "";

            StartCoroutine(SendChatRequest(prompt));
        }

        private void CreateBubble(string message, bool isUserMessage)
        {
            DiscussionBubble discussionBubble = Instantiate(bubblePrefab, bubblesParent);
            discussionBubble.Configure(message, isUserMessage);

            onMessageReceived?.Invoke();
        }
    }

    [Serializable]
    public class ChatResponse
    {
        public Choice[] choices;

        public string FirstChoice => choices[0].ToString();
    }

    [Serializable]
    public class Choice
    {
        public Message message;

        public override string ToString()
        {
            return message.ToString();
        }
    }

    [Serializable]
    public class Message
    {
        public string content;
    }
}