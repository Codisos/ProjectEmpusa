using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract;
    public GameObject highlight;

    private bool uiVisible = false;

    public void Interact()
    {
        OnInteract.Invoke();
    }

    public void Hover()
    {
        if (!uiVisible) { SetUI(true); }
    }

    public void HoverExit()
    {
        if(uiVisible) { SetUI(false); }
    }

    private void SetUI(bool value)
    {
        uiVisible = value;
        if(highlight != null) highlight.SetActive(value);
    }

    private void Start()
    {
        SetUI(false);
    }
}
