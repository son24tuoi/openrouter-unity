using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace OpenRouter.Chat
{
    public sealed class ChatRequest
    {
        [JsonProperty("model")]
        private string model;

        [JsonProperty("messages")]
        private IReadOnlyList<Message> messages;

        public ChatRequest(string model, IEnumerable<Message> messages)
        {
            this.model = model;
            this.messages = messages.ToList();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}