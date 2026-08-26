using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //PlayerStatusを取得
    private PlayerStatus enemy=new PlayerStatus();
    public PlayerStatus Enemy =>enemy;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.P))
        //{
            //enemy.TakeDamage(100000);
        //}
        //if(enemy.CurrentHp == 0)
        //{
            //Destroy(gameObject);
        //}
    }

    public void TakeDamage(int damage)
{
    enemy.TakeDamage(damage);

    Debug.Log($"残りHP：{enemy.CurrentHp}");

    if (enemy.CurrentHp <= 0)
    {
        Destroy(gameObject);
    }
}


}
