using System;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int _gainedPowerItem = 0; // 아이템 두개까지만 먹을 수 있게, 다 먹었으면 더이상 드랍X
    public int gainedPowerItem
    {

        get { return _gainedPowerItem; }
        set { _gainedPowerItem = Math.Min(value, 2); }
    }

    private int _gainedDefenceItem = 0; // 아이템 두개까지만 먹을 수 있게, 다 먹었으면 더이상 드랍X
    public int gainedDefenceItem
    {
        get { return _gainedDefenceItem; }
        set { _gainedDefenceItem = Math.Min(value, 2); }
    }

    private bool _isGameRunning = true;
    public bool isGameRunning
    {
        get { return _isGameRunning; }
        set
        {
            _isGameRunning = value;
            if (!_isGameRunning)
            {
                StartCoroutine(ExitGameAfterDelay()); // 3초 후 게임 종료
            }
        }
    }

    public bool isBossGroggy = false;

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

    IEnumerator ExitGameAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }
}
