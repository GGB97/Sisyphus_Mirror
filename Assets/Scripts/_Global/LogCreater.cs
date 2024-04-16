using System; /* DateTime */
using System.IO; /* StreamWriter */
using UnityEngine;

public class LogCreator : SingletoneBase<LogCreator>
{
    DateTime startTime;
    DateTime currentTime;

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
        Debug.Log($"기록 종료 / 플레이 시간 {PlayTime()}\n");

        Application.logMessageReceived -= saveLog;

        writer.Flush();
        writer.Close();
    }

    public string PlayTime()
    {
        currentTime = DateTime.Now;
        TimeSpan playTime = startTime - currentTime;
        int min = Mathf.Abs(playTime.Minutes);
        int sec = Mathf.Abs(playTime.Seconds);
        return $"[{min}분 {sec}초]";
    }
}