using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; /* StreamWriter */
using System; /* DateTime */

public class LogCreator : SingletoneBase<LogCreator>
{
    StreamWriter writer;

    void saveLog(string logString, string stackTrace, LogType type)
    {
        string currentTime = DateTime.Now.ToString(("HH:mm:ss"));
        writer.WriteLine($"[{currentTime}] {logString}");
    }

    void Awake()
    {
        writer = new StreamWriter(DBPath.LogPath, append: true);
        Application.logMessageReceived += saveLog;

        DontDestroyOnLoad(this);

        Debug.Log("기록 시작");
    }

    void OnDisable()
    {
        Debug.Log("기록 종료");

        Application.logMessageReceived -= saveLog;

        writer.Flush();
        writer.Close();
    }
}