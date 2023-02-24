using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private int _matchTime;
    public int MatchTime { get => _matchTime; private set => _matchTime = value; }

    [SerializeField] private int _enemySpawnTime;
    public int EnemySpawnTime { get => _enemySpawnTime; private set => _enemySpawnTime = value; }

    [SerializeField] private SpriteDisplayer.ShipColor _playerColor;
    public SpriteDisplayer.ShipColor PlayerColor { get => _playerColor; private set => _playerColor = value; }

    private void Awake()
    {
        MatchTime = 90;
        EnemySpawnTime = 6;
        PlayerColor = SpriteDisplayer.ShipColor.GREEN;
    }

    public void ChangeMatchTime(int change)
    {
        MatchTime = change;
    }

    public void ChangeEnemySpawnTime(int change)
    {
        EnemySpawnTime = change;
    }

    public void ChangePlayerColor(int change)
    {
        PlayerColor = (SpriteDisplayer.ShipColor)change;
    }
}