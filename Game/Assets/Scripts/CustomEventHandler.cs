using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomEventHandler : MonoBehaviour 
{
    public UnityEvent OnSelectEvent;

    void Awake()
    {
        if (OnSelectEvent == null)
        {
            Debug.LogFormat("No event on {0}", gameObject.name);
            gameObject.SetActive(false);
        }
    }

    void OnSelect()
    {
        Debug.Log("select event from " + gameObject.name);
        OnSelectEvent.Invoke();
    }

    void OnClick()
    {
        Debug.Log("click event from " + gameObject.name);
        OnSelectEvent.Invoke();
    }
}
