using UnityEngine;

public class PlayerStatus:MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private PlayerStatusData initialData;
    
    public Stat<float> moveSpeed = new Stat<float>();
    public Stat<float> sprintSpeed = new Stat<float>();
    public Stat<float> jumpSpeed =  new Stat<float>();
    public LimitedStat<float> stamina =  new LimitedStat<float>(0,0);
    public Stat<float> staminaDecreaseSpeed = new Stat<float>();
    public Stat<float> staminaFillSpeed = new Stat<float>();
    public LimitedStat<float> staminaRegenRate = new LimitedStat<float>(0,0);
    
    public float BaseMoveSpeed => initialData.moveSpeed;
    public float BaseSprintSpeed => initialData.sprintSpeed;
    public float BaseJumpSpeed => initialData.jumpSpeed;
    public float BaseStaminaDecreaseSpeed => initialData.staminaDecreaseSpeed;
    public float BaseStaminaFillSpeed => initialData.staminaFillSpeed;
    public float BaseMaxStamina => initialData.maxStamina;
    public float BaseStaminaRegenRate => initialData.staminaRegenRate;
    

    private void Awake()
    {
        SetStatus(initialData);
    }
    
    private void SetStatus(PlayerStatusData data)
    {
        moveSpeed.Value = data.moveSpeed;
        jumpSpeed.Value = data.jumpSpeed;
        stamina.MaxValue = data.maxStamina;
        stamina.Value = stamina.MaxValue;
        stamina.MinValue = 0;
        staminaRegenRate.MaxValue = 100;
        staminaRegenRate.Value = data.staminaRegenRate;
        staminaRegenRate.MinValue = 0;
    }
}
