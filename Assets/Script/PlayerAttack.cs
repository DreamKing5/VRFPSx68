using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("攻撃設定")]//攻撃ダメージのデフォ値
    [SerializeField] private int attackDamage=20000;
    private readonly HashSet<EnemyController> touchingEnemies = new HashSet<EnemyController>();

    [Header("攻撃範囲")]
    [SerializeField] private SphereCollider attackContactZone;
    [SerializeField,Min(0f)] private float attackRadius=1.5f;

    [Header("デバッグ表示")]
    [SerializeField] private bool debugMode =true;
    [SerializeField,Min(1)]private int debugVisibleFrames=5;
    [SerializeField] private Color debugColor=new Color(1f,0f,0f,0.25f);
    private int debugFramesRemaining;
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

        //デバッグ機能（Update内）
        if (Input.GetKeyDown(KeyCode.P))
        {
            AttackTouchingEnemies();
            if (debugMode)
            {
                debugFramesRemaining=debugVisibleFrames;
            }
        }else if (debugFramesRemaining > 0)
        {
            debugFramesRemaining--;
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

    //デバッグ機能に関するメソッド
    private void Awake()//未設定時にCliderを自動取得する
    {
        attackContactZone=GetComponent<SphereCollider>();
        ApplyAttackRadius();
    }

    private void ApplyAttackRadius()//攻撃半径をColiderに反映する
    {
        if(attackContactZone == null)
        {
            return;
        }
        attackRadius=Mathf.Max(0f,attackRadius);
        attackContactZone.radius=attackRadius;
    }

    private void OnDrawGizmos()
    {
        if (!debugMode)
        {
            return;
        }
        if (debugFramesRemaining <= 0)
        {
            return;
        }
        if(attackContactZone == null)
        {
            attackContactZone=GetComponent<SphereCollider>();
        }
        Vector3 center =attackContactZone.transform.TransformPoint(attackContactZone.center);
        Vector3 scale =attackContactZone.transform.lossyScale;
        float largestScale=Mathf.Max(Mathf.Abs(scale.x),Mathf.Abs(scale.y),Mathf.Abs(scale.z));
        float worldRadius=attackContactZone.radius*largestScale;
        Gizmos.color=debugColor;
        Gizmos.DrawSphere(center,worldRadius);
        Gizmos.color=new Color(debugColor.r,debugColor.g,debugColor.b,1f);
        Gizmos.DrawWireSphere(center,worldRadius);
    }
}
