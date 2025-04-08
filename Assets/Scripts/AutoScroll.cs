using System.Collections;
using System.Collections.Generic;
using OpenRouter.Samples.DeepSeek;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private ScrollRect scrollRect;

    [Header("Settings")]
    [SerializeField] [Range(0.1f, 5f)] private float duration;

    private float timeElapsed;
    private WaitForSeconds delay = new WaitForSeconds(0.3f);

    private void Start()
    {
        // DiscussionManager.onMessageReceived += StartScrollDown;
        DeepSeekChat.onMessageReceived += StartScrollDown;
    }

    private void OnDestroy()
    {
        // DiscussionManager.onMessageReceived -= StartScrollDown;
        DeepSeekChat.onMessageReceived -= StartScrollDown;
    }

    private void StartScrollDown()
    {
        StartCoroutine(ScrollDown());
    }

    private IEnumerator ScrollDown()
    {
        yield return delay;

        ScrollRect scrollRect = GetComponent<ScrollRect>();
        timeElapsed = 0f;

        while (scrollRect.normalizedPosition.y > 0f)
        {
            timeElapsed += Time.deltaTime;
            float newY = Mathf.Lerp(scrollRect.normalizedPosition.y, 0f, timeElapsed / duration);
            scrollRect.normalizedPosition = new Vector2(0f, newY);
            yield return null;
        }
    }
}
