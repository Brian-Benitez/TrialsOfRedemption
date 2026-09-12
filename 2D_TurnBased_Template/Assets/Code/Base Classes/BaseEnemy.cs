using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float EnemyHealth;
    public float MaxEnemyHealth;
    public float EnemySpeed;
    public float EnemyDamage;

    [Header("Item that can be dropped")]
    public GameObject BossSoulsObj;

    [Header("Enemy Souls Value")]
    public int EnemySoulsValue;
    [Header("demo stuff delete later")]
    public bool IsHit = false;

    public bool IsDead = false;

    public StunState StunStateRef;

    private void Start()
    {
        MaxEnemyHealth = EnemyHealth;
    }

    [SerializeField]
    public enum TypeOfEnemy
    {
        Swordsman, 
        Archer,
        Wizard,
        Boss,
        Object
    }

    public enum LevelOfEnemy
    {
        LevelOne,
        LevelTwo,
        LevelThree,
        Boss
    }

    public TypeOfEnemy EnemyType;
    public LevelOfEnemy EnemyDifficulty;

    public void TakeDamage(float damage)
    {
        PlayersUltController.Instance.AddUltPoint(damage);
        EnemyHealth -= damage;
        IsHit = true;   
        Debug.Log("enemy took: " + damage);
        StunStateRef.IsEnemyStunned();
        DoesEnemyDie();
    }

    public void HealSelfFully() => EnemyHealth = MaxEnemyHealth;

    public void DoesEnemyDie()
    {
        if (EnemyHealth <= 0)
        {
            Debug.Log("im dead");
            if (EnemyType == TypeOfEnemy.Object)
                Debug.Log("object destroyed");

            else
            {
                SoulsBankController.Instance.SoulsBank += EnemySoulsValue;
                EnemiesSpawner.Instance.EnemiesAlive--;
                EnemiesSpawner.Instance.CheckOnTotalEnemies();
                SoulsBankController.Instance.PayoutToPlayer();
                XPController.Instance.AddXPToPlayer(EnemySoulsValue);
                PlayerAmmoController.Instance.AddAmmo();
                PlayerController.Instance.Player.GetComponent<BaseCharacter>().UpdatePlayersStats();//i dont like how im doing this give ref to SBC
                if(EnemyType != TypeOfEnemy.Boss)
                {
                    EnemyTurnController.Instance.RemoveEnemyFromList(this.gameObject);
                }
                IsDead = true;
            }
            
            if(EnemyType == TypeOfEnemy.Boss)
            {
                Debug.Log("start buffing enemies");
                BuffEnemiesManager.Instance.StartBossDefeatedEvent();
            }
            Destroy(this.gameObject);
        }
    }
}
