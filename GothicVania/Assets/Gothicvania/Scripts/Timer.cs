using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]

public class BoolEvent : UnityEvent<bool> { }

public class Timer : MonoBehaviour
{
    public float startTIme = 5;
    public bool fireBool;

    public BoolEvent OnBoolEvent;
    public UnityEvent OnVoidEvent;

    void Start()
    {
        StartCoroutine(TImerProcess(startTIme, fireBool));
    }

    IEnumerator TImerProcess(float startTime, bool fireBool)
    {
        yield return new WaitForSeconds(startTime);

        OnBoolEvent.Invoke(fireBool);
        OnVoidEvent.Invoke();
    }
}
