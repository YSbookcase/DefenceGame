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

    private ObjectPool _bulletPool;
    private Coroutine _shootCoroutine;

    private void Awake()
    {
        _bulletPool = new ObjectPool(transform, bulletPrefab, 20);
    }

    private void Update()
    {
        StartShooting();
    }



    public void StartShooting()
    {
        if (_shootCoroutine == null)
        {
            _shootCoroutine = StartCoroutine(FireLoop());
        }
    }

    private IEnumerator FireLoop()
    {
        WaitForSeconds wait = new WaitForSeconds(fireDelay);

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


    public void SetWeaponStats(int attackPower, float bulletSpeed, float fireDelay)
    {
        this.attackPower = attackPower;
        this.bulletSpeed = bulletSpeed;
        this.fireDelay = fireDelay;

        // ���� FireLoop Coroutine �����
        if (_shootCoroutine != null)
            StopCoroutine(_shootCoroutine);

        _shootCoroutine = StartCoroutine(FireLoop());
    }
    private void ReturnBullet(PooledObject bullet)
    {
        _bulletPool.PushPool(bullet);
    }


}
