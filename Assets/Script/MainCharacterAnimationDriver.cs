using UnityEngine;

public class MainCharacterAnimationDriver  : MonoBehaviour
{
    [Header("VRoidモデル側についているAnimator")]
    [SerializeField] private Animator modelAnimator;

    [Header("既存PlayerのCharacterController")]
    [SerializeField] private CharacterController characterController;

    [Header("確認用：このキーでAttackを再生")]
    [SerializeField] private KeyCode testAttackKey=KeyCode.F;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int AttackHash = Animator.StringToHash("Attack"); 
    private void Awake()
    {
        //Inspectorで指定していない場合、Playerは以下のAnimatorを自動で探す。
        if(modelAnimator == null)
        {
            modelAnimator = GetComponentInChildren<Animator>();

        }

        //PlayerRootにCharacterControllerがある場合、自動で取得する。
        if(characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
        
        if(modelAnimator == null)
        {
            Debug.LogError("VRoidモデルのAnimatorが見つかりません。");
        }
    }

    private void Update()
    {
        if (modelAnimator == null)
        {
            return;
        }

        // 既存CharacterControllerの実際の移動速度を使って、
        // Idle / Walk / Run Blend Treeを切り替える。
        if (characterController != null)
        {
            Vector3 horizontalVelocity = characterController.velocity;
            horizontalVelocity.y = 0f;

            float speed = horizontalVelocity.magnitude;
            modelAnimator.SetFloat(SpeedHash, speed);
        }

        // 動作確認用。FキーでAttack Triggerを送る。
        if (Input.GetKeyDown(testAttackKey))
        {
            PlayAttackAnimation();
        }
    }

    // 既存のPlayerAttack.csなどから呼び出すためのpublicメソッド。
    public void PlayAttackAnimation()
    {
        if (modelAnimator == null)
        {
            return;
        }

        modelAnimator.SetTrigger(AttackHash);
    }

    // CharacterControllerを使わない移動処理の場合、
    // 既存の移動Scriptからこのメソッドを呼んで速度を渡せる。
    public void SetMoveSpeed(float speed)
    {
        if (modelAnimator == null)
        {
            return;
        }

        modelAnimator.SetFloat(SpeedHash, speed);
    }
}