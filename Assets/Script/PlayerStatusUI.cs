using TMPro;
using UnityEngine;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TMP_Text statusText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerStatus status=player.Status;
        statusText.text=
            $"HP:{status.CurrentHp}/{status.MaxHp}\n"+
            $"Power:{status.Power}\n"+
            $"Guard:{status.Guard}\n"+
            $"JumpCost:jump count*100\n"+
            $"Turbo:flame*10";
    }
}
