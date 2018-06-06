using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExceptionLogger : MonoBehaviour {

    private StreamWriter SW;

    public string logFileName = "logError.txt";

	void Start () {
        DontDestroyOnLoad(gameObject);
        SW = new StreamWriter(Application.persistentDataPath + "/" + logFileName);
        Debug.Log(Application.persistentDataPath + "/" + logFileName);
	}

    private void OnEnable() {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable() {
        //Application.RegisterLogCallback(null); // Deprecated
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type) {
       if (type == LogType.Exception || type == LogType.Error) {
            SW.WriteLine("Logged at: " + DateTime.Now.ToString() 
                + " - Log Desc: " + logString 
                + " - Trace: " + stackTrace 
                + " - Type " + type.ToString());
        }
    }

    private void OnDestroy() {
        SW.Close();
    }  
	
}
