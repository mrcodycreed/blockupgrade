using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaptionManager : MonoBehaviour
{
    public GameObject captionPanel;
    public TMP_Text captionText;
    public Image backgroundImage;

    public Vector2 margin = new Vector2(30, 30);
    public Vector2 padding = new Vector2(40, 25);
    public float maxWidth = 900f;

    Coroutine hideRoutine;

    void Start()
    {
        HideCaption();
    }

    public void ShowCaption(string message, string position, float duration)
    {
        if (captionPanel == null || captionText == null)
            return;

        captionPanel.SetActive(true);

        captionText.enableWordWrapping = true;
        captionText.rectTransform.sizeDelta = new Vector2(maxWidth, 0);
        captionText.text = message;
        captionText.ForceMeshUpdate();

        Vector2 preferred = captionText.GetPreferredValues(message, maxWidth, 0);
        RectTransform panel = captionPanel.GetComponent<RectTransform>();
        panel.sizeDelta = new Vector2(
            preferred.x + padding.x,
            preferred.y + padding.y
        );

        captionText.rectTransform.anchorMin = Vector2.zero;
        captionText.rectTransform.anchorMax = Vector2.one;
        captionText.rectTransform.offsetMin = new Vector2(padding.x / 2, padding.y / 2);
        captionText.rectTransform.offsetMax = new Vector2(-padding.x / 2, -padding.y / 2);

        SetPosition(position);

        if (backgroundImage != null)
            backgroundImage.color = new Color(0f, 0f, 0f, 0.65f);

        if (hideRoutine != null)
            StopCoroutine(hideRoutine);

        hideRoutine = StartCoroutine(HideAfterDelay(duration));
    }

    public void HideCaption()
    {
        if (captionPanel != null)
            captionPanel.SetActive(false);
    }

    IEnumerator HideAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        HideCaption();
    }

    void SetPosition(string position)
    {
        RectTransform panel = captionPanel.GetComponent<RectTransform>();
        string p = position.Trim().ToLower();

        if (p == "top left")
            SetAnchor(panel, new Vector2(0, 1), new Vector2(0, 1), new Vector2(margin.x, -margin.y));
        else if (p == "top center")
            SetAnchor(panel, new Vector2(0.5f, 1), new Vector2(0.5f, 1), new Vector2(0, -margin.y));
        else if (p == "top right")
            SetAnchor(panel, new Vector2(1, 1), new Vector2(1, 1), new Vector2(-margin.x, -margin.y));
        else if (p == "bottom left")
            SetAnchor(panel, new Vector2(0, 0), new Vector2(0, 0), new Vector2(margin.x, margin.y));
        else if (p == "bottom center")
            SetAnchor(panel, new Vector2(0.5f, 0), new Vector2(0.5f, 0), new Vector2(0, margin.y));
        else
            SetAnchor(panel, new Vector2(1, 0), new Vector2(1, 0), new Vector2(-margin.x, margin.y));
    }

    void SetAnchor(RectTransform rect, Vector2 anchor, Vector2 pivot, Vector2 position)
    {
        rect.anchorMin = anchor;
        rect.anchorMax = anchor;
        rect.pivot = pivot;
        rect.anchoredPosition = position;
    }
}