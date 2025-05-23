using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public ZombieBoss ZombieBoss;
    public Boss DemonBoss;

    public PlayerController Player;

    public Image[] hearts;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHearts(int lives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < lives;
        }
    }

    public void PlayerisHurt()
    {
        Player.TakeDamage();
    }

    public void attackDamage()
    {
        if (ZombieBoss != null && ZombieBoss.isWeak == true)
        {
            ZombieBoss.BossGotHurtZombie();
        }
        if (DemonBoss != null)
        {
            DemonBoss.BossGotHurtDemon();
        }
    }
}
