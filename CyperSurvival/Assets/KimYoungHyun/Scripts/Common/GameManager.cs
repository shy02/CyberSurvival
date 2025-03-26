using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject GameOver;

    public Image HpBarImage;
    public Image PowerBarImage;
    public Image DefenceBarImage;

    public static int MAX_HP = 1000;
    public static int MAX_GAINED_ITEM = 2;
    public static int DEAULT_POWER = 15;
    public static int DEFAULT_ULTIMATE_POWER = 5;

    private int _playerHp = 1000;
    public int PlayerHp
    {
        get => _playerHp;
        set
        {
            _playerHp = Math.Min(value, MAX_HP);
            SetHpBarImage();
            if (_playerHp <= 0)
            {
                FinishGame();
            }
        }
    }

    private int _playerPower = 0;
    public int PlayerPower
    {
        get => _playerPower;
        set
        {
            _playerPower = Math.Min(value, MAX_GAINED_ITEM);
            SetPowerBarImage();
        }
    }

    private int _playerDefence = 0;
    public int PlayerDefence
    {
        get => _playerDefence;
        set
        {
            _playerDefence = Math.Min(value, 20);
            SetDefenceBarImage();
        }
    }

    private bool _isGameRunning = true;
    public bool IsGameRunning
    {
        get => _isGameRunning;
        set
        {
            _isGameRunning = value;
            if (!_isGameRunning)
            {
                // Game Over
            }
        }
    }

    public bool isBossGroggy = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SetUI();
        SetHpBarImage();
        SetPowerBarImage();
        SetDefenceBarImage();
    }

    private void SetUI()
    {
        GameOver.SetActive(false);
    }

    public void SetHp(int hp)
    {
        PlayerHp += hp;
        print($"Player hp: {PlayerHp} (+{hp})");
    }

    public void AddGainedPowerItem()
    {
        if (PlayerPower >= MAX_GAINED_ITEM)
        {
            return;
        }
        //GainedPowerItem += 1;
        PlayerPower += 1;
        print($"Gained power item! Player power : {PlayerPower}");
    }

    public void AddGainedDefenceItem()
    {
        if (PlayerDefence >= MAX_GAINED_ITEM)
        {
            return;
        }
        //GainedDefenceItem += 1;
        PlayerDefence += 1;
        print($"Gained defence item! Player dfence : {PlayerDefence}");
    }

    private void SetHpBarImage()
    {
        float hpImage = (float) PlayerHp / MAX_HP; 
        HpBarImage.fillAmount = hpImage;
    }

    private void SetPowerBarImage()
    {
        float powerImage = (float) (PlayerPower + 1) / (MAX_GAINED_ITEM + 1);
        PowerBarImage.fillAmount = powerImage;
    }

    private void SetDefenceBarImage()
    {
        float defenceImage = (float)(PlayerDefence + 1) / (MAX_GAINED_ITEM + 1);
        DefenceBarImage.fillAmount = defenceImage;
    }

    public void FinishGame()
    {
        GameOver.SetActive(true);
        IsGameRunning = false;
    }
}
