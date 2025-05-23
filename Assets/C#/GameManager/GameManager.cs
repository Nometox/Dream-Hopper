using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public ZombieBoss ZombieBoss;
    public Boss DemonBoss;

    public PlayerController Player;

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

    public void PlayerisHurt()
    {
        Player.isHurt();
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
