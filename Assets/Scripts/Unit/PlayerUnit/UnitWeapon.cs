// 기존 UnitShoot과 WeaponPoint의 기능을 하나의 컴포넌트로 통합하는 방향 제안
// 목적: 데이터 의존성 최소화, 관리 일원화, 커플링 감소

using System.Collections;
using UnityEngine;
using DesignPattern;

public class UnitWeapon : MonoBehaviour
{
    [Header("Bullet Pool 및 사출 정보")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireDelay;
    [SerializeField] private int attackPower;


    [Header("타겟 감지")]
    [SerializeField] private float detectRange = 10f;
    [SerializeField] private LayerMask targetLayer;




    private ObjectPool _bulletPool;
    private Coroutine _shootCoroutine;

    private void Awake()
    {
        _bulletPool = new ObjectPool(transform, bulletPrefab, 20);
    }

    private void Update()
    {


        CheckAndShoot();
    }



    private void CheckAndShoot()
    {
        if (IsTargetInFront())
        {
            if (_shootCoroutine == null)
                _shootCoroutine = StartCoroutine(FireLoop());
        }
        else
        {
            if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
                _shootCoroutine = null;
            }
        }
    }

    private bool IsTargetInFront()
    {
        Ray ray = new Ray(muzzlePoint.position, muzzlePoint.forward);
        return Physics.Raycast(ray, out RaycastHit hit, detectRange, targetLayer)
               && hit.collider.CompareTag("Monster");
    }


    private IEnumerator FireLoop()
    {
        WaitForSeconds wait = new WaitForSeconds(fireDelay);

        // 첫 번째 발사 전 대기
        yield return wait;

        while (true)
        {
            Fire();
            yield return wait;
        }
    }

    private void Fire()
    {
        Bullet bullet = _bulletPool.PopPool() as Bullet;
        if (bullet == null) return;

        bullet.transform.position = muzzlePoint.position;
        bullet.transform.rotation = muzzlePoint.rotation;
        bullet.Init(attackPower, ReturnBullet);
        bullet.Fire(muzzlePoint.forward, bulletSpeed);
    }


    public void SetWeaponStats(int attackPower, float bulletSpeed, float fireDelay, float range)
    {
        this.attackPower = attackPower;
        this.bulletSpeed = bulletSpeed;
        this.fireDelay = fireDelay;
        this.detectRange = range;

        if (_shootCoroutine != null)
            StopCoroutine(_shootCoroutine);

        _shootCoroutine = StartCoroutine(FireLoop());
    }


    private void ReturnBullet(PooledObject bullet)
    {
        _bulletPool.PushPool(bullet);
    }


}
