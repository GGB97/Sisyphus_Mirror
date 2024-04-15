using System; /* DateTime */
using System.IO; /* StreamWriter */
using UnityEngine;

public class LogCreator : SingletoneBase<LogCreator>
{
    DateTime startTime;
    DateTime endTime;

    StreamWriter writer;

    void saveLog(string logString, string stackTrace, LogType type)
    {
        string currentTime = DateTime.Now.ToString(("MM-dd HH:mm:ss"));
        string logEntry = $"[{currentTime}] - {logString}";
        if (type == LogType.Error || type == LogType.Exception)
        {
            logEntry += $"\nStackTrace: {stackTrace}";
        }
        writer.WriteLine(logEntry);
    }

    void Awake()
    {
        writer = new StreamWriter(DBPath.LogPath, append: true);
        Application.logMessageReceived += saveLog;

        DontDestroyOnLoad(this);

        startTime = DateTime.Now;
        Debug.Log("기록 시작");
    }

    void OnDisable()
    {
        endTime = DateTime.Now;
        TimeSpan playTime = startTime - endTime;
        int min = Mathf.Abs(playTime.Minutes);
        int sec = Mathf.Abs(playTime.Seconds);
        Debug.Log($"기록 종료 / 플레이 시간 : {min}분 {sec}초");

        Application.logMessageReceived -= saveLog;

        writer.Flush();
        writer.Close();
    }
}