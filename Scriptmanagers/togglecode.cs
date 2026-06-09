using UnityEngine;
using UnityEngine.UI;

public class CodeViewToggle : MonoBehaviour
{
    public CanvasGroup blocksPanel;
    public CanvasGroup stagePanel;

    public Image arrowImage;

    public Sprite arrowRight;
    public Sprite arrowLeft;

    bool showingCode = true;

    public void ToggleView()
    {
        showingCode = !showingCode;

        SetGroupVisible(blocksPanel, showingCode);
        SetGroupVisible(stagePanel, showingCode);

        UpdateArrow();
    }

    void UpdateArrow()
    {
        if (arrowImage == null)
            return;

        arrowImage.sprite =
            showingCode ? arrowLeft : arrowRight;
    }

    void SetGroupVisible(CanvasGroup group, bool visible)
    {
        if (group == null)
            return;

        group.alpha = visible ? 1f : 0f;
        group.interactable = visible;
        group.blocksRaycasts = visible;
    }

    void Start()
    {
        UpdateArrow();
    }
}