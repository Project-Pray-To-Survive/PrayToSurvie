using UnityEngine;

public class PlayerStatus:MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private PlayerStatusData initialData;
    
    public Stat<float> moveSpeed = new Stat<float>();
    public Stat<float> jumpSpeed =  new Stat<float>();
    public LimitedStat<float> stamina =  new LimitedStat<float>(0,0);

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
    }
}
