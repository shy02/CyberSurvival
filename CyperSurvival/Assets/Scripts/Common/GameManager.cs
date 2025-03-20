using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Image HpBarImage;

    public static int MAX_HP = 1000;
    public static int MAX_GAINED_ITEM = 2;
    public static int DEAULT_POWER = 100;

    private int _playerHp = 1000; // 최대 HP 1000
    public int playerHp
    {

        get { return _playerHp; }
        set { 
            _playerHp = Math.Min(value, MAX_HP);
            SetHpBarImage();
        }
    }

    private int _gainedPowerItem = 0; // 아이템 2개까지만 먹을 수 있게, 다 먹었으면 더이상 드랍X
    public int gainedPowerItem
    {

        get { return _gainedPowerItem; }
        set { 
            _gainedPowerItem = Math.Min(value, MAX_GAINED_ITEM);
            SetPowerImage();
        }
    }

    private int _gainedDefenceItem = 0; // 아이템 2개까지만 먹을 수 있게, 다 먹었으면 더이상 드랍X
    public int gainedDefenceItem
    {
        get { return _gainedDefenceItem; }
        set {
            _gainedDefenceItem = Math.Min(value, MAX_GAINED_ITEM);
            SetDefenceImage();
        }
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
                ShowGameOver();
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

    public void SetHp(int hp)
    {
        playerHp = hp;
    }

    public void AddGainedPowerItem()
    {
        if (gainedPowerItem >= MAX_GAINED_ITEM)
        {
            return;
        }
        gainedPowerItem += 1;
    }

    public void AddGainedDefenceItem()
    {
        if (gainedPowerItem >= MAX_GAINED_ITEM)
        {
            return;
        }
        gainedDefenceItem += 1;
    }

    private void SetHpBarImage()
    {
        float hpImage = (float)playerHp / MAX_HP; 
        HpBarImage.fillAmount = hpImage;
        print($"HPBar Image {hpImage}");
    }

    private void SetPowerImage()
    {

    }

    private void SetDefenceImage()
    {

    }

    private void ShowGameOver()
    {

    }
}
