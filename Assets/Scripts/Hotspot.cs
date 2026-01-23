using UnityEngine;
using UnityEngine.Events;

public class Hotspot : MonoBehaviour
{
    public UnityEvent onSelect;

    public void Select()
    {
        onSelect?.Invoke();
    }
}
