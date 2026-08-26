using System;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerStatus
{   
    [SerializeField] public int basicHp=100000;
    [SerializeField] public int basicPower=500;
    [SerializeField] public int basicGuard=1000; 
    [SerializeField] private int maxHp= 100000;
    public int MaxHp => maxHp;
    [SerializeField] private int currentHp= 100000;
    public int CurrentHp => currentHp;
    [SerializeField] private int power= 500;
    public int Power =>power;
    [SerializeField] private int guard= 1000;
    public int Guard =>guard;
    [SerializeField] private int stressStatusTimeLeft=0;
    [SerializeField] private int traumaStatusTimeLeft=0;
    [SerializeField] private int fatigueStatusTimeLeft=0;
    [SerializeField] private int fearStatusTimeLeft=0;
    [SerializeField] private int despairStatusTimeLeft=0;
    [SerializeField] private int connectionFailureStatusTimeLeft=0;
    [SerializeField] private int outputDropStatusTimeLeft=0;
    [SerializeField] private int overheatStatusTimeLeft=0;


    //以下攻撃コマンド用変数
    public int jumpCount=0;
    [SerializeField] public Vector3 jumpPower =new Vector3(0,0,0);


    public void TakeDamage(int damage)
    {
        int finalDamage =Mathf.CeilToInt(damage*basicGuard/Guard);
        currentHp=Mathf.Clamp(currentHp-finalDamage,0,maxHp);
        Debug.Log($"残りHP:{currentHp}");
    }

    
    public void Jump(int jumpCount)
    {
        currentHp=Mathf.Clamp(currentHp-jumpCount*100,0,maxHp);
    }

    public void Turbo()
    {   
        currentHp=Mathf.Clamp(currentHp-10,0,maxHp);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
