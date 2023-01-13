using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] private RectTransform _windowTransform;

    private float _canvasScale;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_canvasScale == 0) 
            _canvasScale = Screen.ScreenInfo.Scale;

        _windowTransform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _windowTransform.anchoredPosition += eventData.delta / _canvasScale;
    }
}