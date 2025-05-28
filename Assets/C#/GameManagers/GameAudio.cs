using UnityEngine;

public class GameAudio : MonoBehaviour
{
    [Header("Boss Sounds")]
    public AudioClip BossDieSound;
    public AudioClip BossMoveSound;
    public AudioClip BossAttackSound;
    public AudioClip BossRageSound;
    public AudioClip BossGotHurtSound;

    [Header("Player Sounds")]
    public AudioClip DieSound;
    public AudioClip DashSound;
    public AudioClip AttackSound;
    public AudioClip JumpSound;

    [Header("Game Sounds")]
    public AudioClip ButtonClicked;
    public AudioSource asSounds;
    public AudioClip RoundWinSound;
    public AudioClip WinSound;


    public void PlayButtonSound()
    {
        asSounds.PlayOneShot(ButtonClicked);
    }

    public void PlayDieSound()
    {
        asSounds.PlayOneShot(DieSound);
    }
    public void PlayRoundWinSound()
    {
        asSounds.PlayOneShot(RoundWinSound);
    }


    public void PlayScoreSound()
    {
        asSounds.PlayOneShot(WinSound);
    }
    public void PlayWinSound()
    {
        asSounds.PlayOneShot(WinSound);
    }

    public void PlayDashSound()
    {
        asSounds.PlayOneShot(DashSound);
    }

    public void PlayJumpSound()
    {
        asSounds.PlayOneShot(JumpSound);
    }

    public void PlayAttackSound()
    {
        asSounds.PlayOneShot(AttackSound);
    }

    public void PlayBossDeadSound()
    {
        asSounds.PlayOneShot(BossDieSound);
    }

    public void PlayBossMoveSound()
    {
        asSounds.PlayOneShot(BossMoveSound);
    }

    public void PlayBossAttackSound()
    {
        asSounds.PlayOneShot(BossAttackSound);
    }

    public void PlayBossRageSound()
    {
        asSounds.PlayOneShot(BossRageSound);
    }

    public void PlayBossGotHurtSound()
    {
        asSounds.PlayOneShot(BossGotHurtSound);
    }
}
