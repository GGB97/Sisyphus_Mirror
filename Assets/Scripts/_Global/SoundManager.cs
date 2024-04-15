using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SoundInfo
{
    public string tag;
    public AudioClip clip;
    [Range(0,100)]public float volumePercent;
}
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get => instance; }

    [Header("Background")]
    [SerializeField]
    private AudioClip backgroundClip;//배경 음악
    private AudioSource backgroundAudioSource;//배경 음악 오디오 소스

    [SerializeField]
    [Range(0, 100f)] private float bgmVolumePercent; //브금 0 ~ 100 조절
    [SerializeField]
    [Range(0.0f,1.0f)]private float maxBgmVolume = 0.5f; //최대 브금 0 ~ 1크기
    [SerializeField]
    private bool isPlayingBgm = true;

    [Header("SoundInfo")]
    public List<SoundInfo> soundEffectList; //할당할 효과음 리스트

    private Dictionary<string, SoundInfo> audioDictionary;//해당하는 효과음들을 내부적으로 저장.
    private Queue<GameObject> audioQueue;//오디오 큐

    [SerializeField]
    private GameObject AudioSoundPrefab;
    [SerializeField]
    private float maxQueueCount = 10f;
    private void Awake()
    {
        if (instance == null)
        { 
            instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);    
    }

    // Start is called before the first frame update
    public void Init()
    {
        backgroundAudioSource = GetComponent<AudioSource>();
        Initialize();
    }

    public void Initialize()
    {
        InitializeQueue();
        InitializeDictionary();
    }
    public GameObject CreateSoundObject() //사운드 오브젝트 생성
    {
        GameObject obj = Instantiate(AudioSoundPrefab);
        obj.name = "AudioSourceObject";
        obj.transform.SetParent(transform);//사운드 매니저 하위로 지정
        obj.transform.localPosition = Vector3.zero;
        obj.SetActive(false);
        audioQueue.Enqueue(obj);//큐에 넣음
        return obj;
    }
    public void InitializeQueue()//소리를 낼 객체 초기화
    {
        audioQueue = new Queue<GameObject>();

        for (int i = 0; i < maxQueueCount; i++)
        {
            CreateSoundObject();//객체를 만들고 큐에 넣기
            //Debug.Log("오디오 initilaize");
        }
    }
    public void InitializeDictionary()//드래그 할당한 브금을 내부 사전에 넣는 과정
    {
        audioDictionary = new Dictionary<string, SoundInfo>();

        foreach (var soundInfo in soundEffectList)
        {
            if (soundInfo.tag != "")
            {
                SoundInfo newSoundInfo = new SoundInfo();
                newSoundInfo.tag = soundInfo.tag;
                newSoundInfo.volumePercent = soundInfo.volumePercent;
                newSoundInfo.clip = soundInfo.clip;

                audioDictionary[soundInfo.tag] = newSoundInfo; //사운드 정보로 사전에 tag값에 clip을 저장.                
                //Debug.Log($"{soundInfo.tag}");
            }
        }
    }
    void Start()
    {        
        backgroundAudioSource.clip = backgroundClip;
        backgroundAudioSource.Play();
    }
    public void PlayAudioClip(string tag)//tag값으로 오디오 재생
    {
        if (!audioDictionary.ContainsKey(tag)) //오디오 클립이 존재하는지 확인
        {
            Debug.Log("tag는 존재하지 않는 오디오 입니다.");
            return;
        }

        if (audioQueue.Count > 0)//재생시킬 객체가 있는지 확인
        {
            GameObject obj = audioQueue.Dequeue();//큐에서 꺼낸다.
            AudioSourceObject objAudioSource = obj.GetComponent<AudioSourceObject>();//소리객체의 스크립트 가져옴
            objAudioSource.clip = audioDictionary[tag].clip;//클립 설정
            objAudioSource.volume = PercentToDegree(audioDictionary[tag].volumePercent);//볼륨 설정
            obj.gameObject.SetActive(true);//활성화
            objAudioSource.PlayAudio();
        }
        else//없으면
        {
            GameObject newObj = CreateSoundObject();//새로 생성한다.
            AudioSourceObject objAudioSource = newObj.GetComponent<AudioSourceObject>();
            objAudioSource.clip = audioDictionary[tag].clip;
            objAudioSource.volume = PercentToDegree(audioDictionary[tag].volumePercent);
            newObj.gameObject.SetActive(true);//활성화
            objAudioSource.PlayAudio();
        }
    }
    public void ReturnAudioClip(GameObject audioSourceObject)
    {
        audioQueue.Enqueue(audioSourceObject);
        audioSourceObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        SetBgmVolume();
    }

    public void SetBgmVolume() //브금 볼륨 bgmVolume 값에 따른 설정
    {
        if (isPlayingBgm)
            backgroundAudioSource.volume = PercentToDegree(bgmVolumePercent) * maxBgmVolume;
        else
            backgroundAudioSource.volume = 0.0f;
    }
    public void MuteBgmButtun() //브금 껐다 켰다
    {
        isPlayingBgm = !isPlayingBgm;
    }
    public float PercentToDegree(float percent)
    {
        return percent / 100f;
    }
}

