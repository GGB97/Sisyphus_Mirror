using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class QuestSaveData
{
    public Dictionary<int, int> ongoingQuests;// = new Dictionary<int, int>();//현재 진행중인 퀘스트 집합
    public Dictionary<int, bool> completeQuests; //= new HashSet<int>();//완료한 퀘스트의 id 집합
    public string gameEndTime;
    public QuestSaveData()
    {
        ongoingQuests = new Dictionary<int, int>();
        completeQuests = new Dictionary<int, bool>();
    }
}
public class QuestSaveManager : SingletoneBase<QuestSaveManager>
{
    QuestSaveData saveData = new QuestSaveData();
    public event Action loadDataEvent;
    private string dataKey = "QuestSaveData1";
    private void Awake()
    {
        if(Instance != this)
            Destroy(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }
    void Start()
    {
        // PlayerPrefs에서 목록 불러오기
        LoadData();
        //PlayerPrefs.DeleteKey(dataKey);
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)//로비로 돌아올 때 호출되게
    {
        if (scene.buildIndex == 1 && QuestManager.Instance.GetTotalQuestCount() == true)//퀘스트 로드가 되어 있을 때
        {
            loadDataEvent?.Invoke();//퀘스트 슬롯 다시 생성
        }

    }

    public void SaveData()
    {
        saveData = QuestManager.Instance.SaveData();

        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        bf.Serialize(ms, saveData); string result = Convert.ToBase64String(ms.GetBuffer());
        PlayerPrefs.SetString(dataKey, result);

        //Debug.Log("json 변환 " + result);

        // PlayerPrefs 저장
        PlayerPrefs.Save();
    }

    private void SaveCurrentTime()
    {
        DateTime nowTime = DateTime.Now;
        saveData.gameEndTime = nowTime.ToString();//날짜 저장
    }

    public void LoadData()
    {
        QuestSaveData data = null;
        string save = PlayerPrefs.GetString(dataKey, null);

        // 저장된 문자열이 있을 경우에만 처리
        if (!string.IsNullOrEmpty(save))
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream(Convert.FromBase64String(save));

            data = (QuestSaveData)binaryFormatter.Deserialize(memoryStream);
            saveData = data;
            // JSON 문자열을 목록으로 역직렬화
            QuestManager.Instance.LoadData(saveData);
            Debug.Log("퀘스트 로드 성공");
        }
        else
        {
            QuestManager.Instance.StartQuestSetting();
            saveData = QuestManager.Instance.SaveData();
            SaveData();
            Debug.Log("퀘스트 첫 로드");
        }
        if(ResetQuest() == false)//리셋을 안 하면 
            loadDataEvent?.Invoke();
    }
    protected override void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    protected override void OnApplicationQuit()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SaveCurrentTime();
        SaveData();
    }
    public void InvokeLoadDataEvent()
    {
        loadDataEvent?.Invoke();
    }
    public bool ResetQuest()
    {
        DateTime dateTime = DateTime.Now;//현재 시간

        string savedGameEndTimeString = saveData.gameEndTime;//이전 기록을 string으로 받아옴
        DateTime savedGameEndTime;

        if (!string.IsNullOrEmpty(savedGameEndTimeString))//널인지 체크
        {
            savedGameEndTime = DateTime.Parse(savedGameEndTimeString);//이전 데이터를 데이트 타임으로 변환
            TimeSpan distance = dateTime - savedGameEndTime;//현재에서 이전 시간을 뺀다.
            if (distance.Days >= 1)//하루 이상 차이가 난다면
            {
                //리셋
                Debug.Log("시간 차로 인한 리셋 완료");
                QuestManager.Instance.StartDailyQuest();//리셋
                return true;
            }
            else
                return false;
        }
        else//저장된게 없으면
        {
            return false;
        }
    }
}
