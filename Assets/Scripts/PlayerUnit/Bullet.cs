using System.Collections;
using UnityEngine;
using DesignPattern;

public class Bullet : PooledObject
{
    private WeaponPoint _weaponPoint;
    private Rigidbody _rigidbody;
    [SerializeField] private float _MaxlifeTime = 3f; // 독립적인 수명 설정
    private float _currentLiftTime;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
            Debug.LogError("[Bullet] Rigidbody가 없습니다!");
    }

    //  외부에서 WeaponPoint를 반드시 설정
    public void Init(WeaponPoint weaponPoint)
    {
        _weaponPoint = weaponPoint;
        _currentLiftTime = _MaxlifeTime;


    }

    private void Update()
    {
   
        
        _currentLiftTime -= Time.deltaTime;
        if (_currentLiftTime <= 0f)
        {
            _currentLiftTime = _MaxlifeTime;
            ReturnPool();
        }
    }

    public void Fire(Vector3 direction, float speed)
    {
        if (_rigidbody == null) return;
        _rigidbody.velocity = direction.normalized * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //이팩트 관련 향후 추가 필요.
        //if (explosionEffect != null)
        //{
        //    Instantiate(explosionEffect, transform.position, Quaternion.identity);
        //
        //}


        IInteraction damagable = collision.gameObject.GetComponent<IInteraction>();
        if (damagable != null)
        {
            Debug.Log($"{collision.gameObject.name} 에서 총알이 데미지 받을 수 있는 컴포넌트를 가져옴.");
            Attack(damagable);

        }
        else
        {
            Debug.Log($"{collision.gameObject.name} 에서 해당 게임 오브젝트에는 데미지 받을 수 있는 컴포넌트가 없음");
        }
        ReturnPool();

    }

    private void Attack(IInteraction damagable)
    {
        damagable.IInteraction(gameObject, _weaponPoint.attackPoint);
    }

}

