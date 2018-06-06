using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateUsage : MonoBehaviour {

    public delegate void EventHandler(int Param1, int Param2);

    public event EventHandler EH;

    ///<summary>
    /// добавление делегата HandleMyEvent 
    /// </summary>

    private void Awake() {
        // добавить обработчик события в список делегатов
        EH += HandleMyEvent;
    }

    private void Start() {
        EH(0,0);
    }

    ///<summary>
    /// пример обработчика события. Позволяет ссылаться на него как на делегат типа EventHandler
    /// </summary>

    private void HandleMyEvent(int Param1, int Param2) {
        Debug.Log("Event Called");
    }
}
