// ���� UnitShoot�� WeaponPoint�� ����� �ϳ��� ������Ʈ�� �����ϴ� ���� ����
// ����: ������ ������ �ּ�ȭ, ���� �Ͽ�ȭ, Ŀ�ø� ����

using System.Collections;
using UnityEngine;
using DesignPattern;

public class UnitWeapon : MonoBehaviour
{
    [Header("Bullet Pool �� ���� ����")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireDelay;
    [SerializeField] private int attackPower;


    [Header("Ÿ�� ����")]
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

        // ù ��° �߻� �� ���
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
