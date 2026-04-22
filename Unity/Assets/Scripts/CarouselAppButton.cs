using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CarouselAppButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ScrollRect scrollRect;
    private string url;
    private float dragThreshold = 15f;

    private Vector2 pointerDownPosition;
    private bool isDragging;

    public void Initialize(ScrollRect targetScrollRect, string targetUrl, float threshold)
    {
        scrollRect = targetScrollRect;
        url = targetUrl;
        dragThreshold = threshold;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownPosition = eventData.position;
        isDragging = false;

        if (scrollRect != null)
            scrollRect.OnBeginDrag(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (scrollRect != null)
            scrollRect.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float dragDistance = Vector2.Distance(pointerDownPosition, eventData.position);

        if (dragDistance > dragThreshold)
        {
            isDragging = true;
        }

        if (scrollRect != null)
            scrollRect.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (scrollRect != null)
            scrollRect.OnEndDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float totalDistance = Vector2.Distance(pointerDownPosition, eventData.position);

        if (!isDragging && totalDistance <= dragThreshold)
        {
            OpenURL();
        }
    }

    private void OpenURL()
    {
        if (!string.IsNullOrWhiteSpace(url))
        {
            Application.OpenURL(url);
        }
        else
        {
            Debug.LogWarning("No URL assigned for " + gameObject.name);
        }
    }
}