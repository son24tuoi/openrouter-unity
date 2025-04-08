using System;
using Newtonsoft.Json;
using UnityEngine;

namespace OpenRouter.Chat
{
    [Serializable]
    public sealed class Message
    {
        [SerializeField]
        [JsonProperty("role")]
        public string role;

        [SerializeField]
        [TextArea(1, 30)]
        [JsonProperty("content")]
        public string content;

        [JsonConstructor]
        public Message([JsonProperty("role")] Role role, [JsonProperty("content")] string content)
        {
            this.role = role.ToString().ToLower();
            this.content = content;
        }
    }
}