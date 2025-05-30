using UnityEngine;
using DesignPattern;

public class Bullet : PooledObject
{
    private Rigidbody _rigidbody;

    [SerializeField] private float maxLifeTime = 3f;
    private float currentLifeTime;

    private int attackPower;
    private System.Action<PooledObject> returnCallback;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
            Debug.LogError("[Bullet] Rigidbody가 없습니다!");
    }

    public void Init(int power, System.Action<PooledObject> returnToPool)
    {
        attackPower = power;
        returnCallback = returnToPool;
        currentLifeTime = maxLifeTime;
    }

    private void Update()
    {
        currentLifeTime -= Time.deltaTime;
        if (currentLifeTime <= 0f)
        {
            ReturnToPool();
        }
    }

    public void Fire(Vector3 direction, float speed)
    {
        if (_rigidbody == null) return;
        _rigidbody.velocity = direction.normalized * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어 유닛은 무시
        if (other.CompareTag("PlayerUnit"))
            return;

        var damagable = other.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(attackPower);
        }

        ReturnToPool();
    }

    private void ReturnToPool()
    {
        returnCallback?.Invoke(this);
    }
}
