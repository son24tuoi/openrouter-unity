using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace OpenRouter.Chat
{
    public enum Role
    {
        [JsonProperty("system")]
        System = 1,
        [JsonProperty("assistant")]
        Assistant,
        [JsonProperty("user")]
        User,
        [JsonProperty("function")]
        Function
    }
}