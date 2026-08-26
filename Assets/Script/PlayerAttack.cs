using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("攻撃設定")]
    [SerializeField] private int attackDamage=20000;
    private readonly HashSet<EnemyController> touchingEnemies = new HashSet<EnemyController>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AttackTouchingEnemies();
            Debug.Log("攻撃が実行されました。");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemyController = other.GetComponentInParent<EnemyController>();
        if(enemyController != null)
        {
            touchingEnemies.Add(enemyController);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyController enemyController = other.GetComponentInParent<EnemyController>();
        if(enemyController != null)
        {
            touchingEnemies.Remove(enemyController);
        }
    }

    private void AttackTouchingEnemies()
    {   Debug.Log($"攻撃対象の敵の数: {touchingEnemies.Count}");
        EnemyController[] enemies =new EnemyController[touchingEnemies.Count];
        touchingEnemies.CopyTo(enemies);
        foreach(EnemyController enemy in enemies)
        {
            if(enemy == null)
            {   
                Debug.Log("敵がnullです。攻撃をスキップします。");
                touchingEnemies.Remove(enemy);
                continue;
            }
            enemy.TakeDamage(attackDamage);
            Debug.Log($"敵に{attackDamage}のダメージを与えました。");
        }
    }
}
