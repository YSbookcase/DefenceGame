using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public AudioManager Audio { get; private set; }
    public PlayerManager Player { get; private set; }

    public TileManager Tile { get; private set; }

    public UnitPlacer Placer { get; private set;  }

    private void Awake() => Init();

    private void Init()
    {
        base.SingletonInit();
        Audio = GetComponentInChildren<AudioManager>();
        Player = GetComponentInChildren<PlayerManager>();
        Tile = GetComponentInChildren<TileManager>();
        Placer = GetComponentInChildren<UnitPlacer>();

        if (Audio == null) Debug.LogError("AudioManager is missing");
        if (Player == null) Debug.LogError("PlayerManager is missing");
        if (Tile == null) Debug.LogError("TileManager is missing");
        if (Tile == null) Debug.LogError("UnitPlacerr is missing");

        // ��������� PlayerManager ����
        if (Player != null)
            DontDestroyOnLoad(Player.gameObject);

    }
}
