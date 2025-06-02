using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _bgmSource;
    private ObjectPool _sfxPool;
    

    [SerializeField] private List<AudioClip> _bgmList = new();
    [SerializeField] private SFXController _sfxPrefab;

    private int _currentBgmIndex = 0;

    private void Awake() => Init();


    private void Start() => PlayCurrentBgm();

    private void Update()
    {
        // 현재 BGM이 끝났으면 다음 트랙으로
        if (!_bgmSource.isPlaying)
        {
            NextBgm();
        }
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


    private void PlayCurrentBgm()
    {
        if (_bgmList.Count == 0) return;

        _bgmSource.clip = _bgmList[_currentBgmIndex];
        _bgmSource.Play();
    }

    private void NextBgm()
    {
        _currentBgmIndex = (_currentBgmIndex + 1) % _bgmList.Count;
        PlayCurrentBgm();
    }

    public void BgmPlay(int index)
    {
        if (0 <= index && index < _bgmList.Count)
        {
            _bgmSource.Stop();
            _currentBgmIndex = index;
            PlayCurrentBgm();
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
