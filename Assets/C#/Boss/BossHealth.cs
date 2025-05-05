using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] float Health = 100;

    private void Update()
    {
        BossDefeat();
    }

    public void BossGotHurt()
    {
        Health -= 20;
    }

    void BossDefeat()
    {
        if (Health == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
