using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int _gainedPowerItem = 0;
    public int gainedPowerItem
    {

        get { return _gainedPowerItem; }
        set { _gainedPowerItem = Math.Min(value, 2); }
    }

    private int _gainedDefenceItem = 0;
    public int gainedDefenceItem
    {
        get { return _gainedDefenceItem; }
        set { _gainedDefenceItem = Math.Min(value, 2); }
    }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddGainedPowerItem()
    {
        gainedPowerItem += 1;
    }

    public void AddGainedDefenceItem()
    {
        gainedDefenceItem += 1;
    }
}
