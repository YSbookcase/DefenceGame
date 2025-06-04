using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private AudioSource _bgmSource;
    private ObjectPool _sfxPool;

    [SerializeField] private List<SceneBgmData> sceneBgmDataList;

    private List<AudioClip> _currentBgmList = new();
    [SerializeField] private SFXController _sfxPrefab;

    private int _currentBgmIndex = 0;
  
    private void Awake() => Init();



    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        string currentScene = SceneManager.GetActiveScene().name;
        SetBgmListForScene(currentScene);
        PlayCurrentBgm();
    }


    private void Update()
    {

        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        // 현재 BGM이 끝났으면 다음 트랙으로
        if (!_bgmSource.isPlaying && _currentBgmList.Count > 0)
        {
            NextBgm();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Init()
    {
        _bgmSource = GetComponent<AudioSource>();
        if (_bgmSource == null)
        {
            Debug.LogError("[AudioManager] AudioSource가 없습니다. 컴포넌트를 추가해주세요.");
            return;
        }

        _bgmSource.volume = 0.5f; // 초기 볼륨 설정

        _sfxPool = new ObjectPool(transform, _sfxPrefab, 10);
    }

    //  씬 로드시 BGM 리스트 갱신
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (this == null || _bgmSource == null)
        {
            Debug.LogWarning("[AudioManager] 씬 로드 시점에 AudioManager 또는 AudioSource가 유효하지 않음");
            return;
        }

        SetBgmListForScene(scene.name);
        PlayCurrentBgm();
    }

    //  Scene 이름 기반으로 BGM 리스트 설정
    private void SetBgmListForScene(string sceneName)
    {
        SceneBgmData data = sceneBgmDataList.Find(d => d.sceneName == sceneName);
        if (data != null)
        {
            _currentBgmList = data.bgmList;
            _currentBgmIndex = 0;
        }
        else
        {
            _currentBgmList.Clear();
            Debug.LogWarning($"[AudioManager] {sceneName}에 해당하는 BGM 리스트가 없습니다.");
        }
    }

    private void PlayCurrentBgm()
    {

        if (_bgmSource == null || _currentBgmList.Count == 0)
        {
            Debug.LogWarning("[AudioManager] BGM 재생 불가 (AudioSource 또는 BGM 리스트 없음)");
            return;
        }

        if (_currentBgmIndex >= _currentBgmList.Count)
        {
            Debug.LogWarning("[AudioManager] 인덱스 초과로 BGM 재생 실패");
            return;
        }

        AudioClip clip = _currentBgmList[_currentBgmIndex];
        if (clip == null)
        {
            Debug.LogWarning("[AudioManager] BGM 클립이 null입니다.");
            return;
        }


        _bgmSource.clip = _currentBgmList[_currentBgmIndex];
        _bgmSource.Play();
    }

    private void NextBgm()
    {

        _currentBgmIndex = (_currentBgmIndex + 1) % _currentBgmList.Count;
        PlayCurrentBgm();
    }

    //  씬에서 직접 인덱스로 BGM 재생 가능
    public void BgmPlay(int index)
    {
        if (0 <= index && index < _currentBgmList.Count)
        {
            _bgmSource.Stop();
            _currentBgmIndex = index;
            PlayCurrentBgm();
        }
    }

    public void StopBgm()
    {
        if (_bgmSource != null && _bgmSource.isPlaying)
        {
            _bgmSource.Stop();
        }
    }

    public float GetBgmVolume()
    {
        return _bgmSource != null ? _bgmSource.volume : 0f;
    }

    public void SetBgmVolume(float volume)
    {
        if (_bgmSource != null)
            _bgmSource.volume = Mathf.Clamp01(volume);
    }



    public SFXController GetSFX()
    {
        // 풀에서 꺼내와서 반환
        PooledObject po = _sfxPool.PopPool();
        return po as SFXController;
    }
}
