using UnityEngine;
using DesignPattern;


public class WeaponPoint : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private Bullet bulletObject;

    [SerializeField] 
    private ObservableProperty<float> _lifeTime = new();
    public ObservableProperty<float> LifeTime => _lifeTime;
    [SerializeField] Rigidbody rd;

    [Range(10, 30)]
    [SerializeField] private float bulletSpeed;
    private ObjectPool _bulletPool;

    //이팩트 관련 프리팹 향후 추가 필요.
    //[SerializeField] GameObject explosionEffect;
    public int attackPoint; //향후 프러퍼티 사용으로 대체해야함.

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _bulletPool = new ObjectPool(transform, bulletObject, 20);
    }

    void Start()
    {
       
    }


    private void Update()
    {
    
    }

      


    public void Fire()
    {
        Bullet bullet = GetBullet();
        if (bullet != null)
        {
            //bullet.Init(this); //  WeaponPoint 주입
            bullet.transform.position = muzzlePoint.position;
            bullet.transform.rotation = muzzlePoint.rotation;
            bullet.Fire(muzzlePoint.forward, bulletSpeed);
        }
    }

    //
    //
    //public void Fire(float Speed)
    //{
    //    GameObject instance = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
    //    Rigidbody rb = instance.GetComponent<Rigidbody>();
    //    if (rb != null)
    //    {
    //        rb.velocity = muzzlePoint.forward * Speed;
    //    }
    //
    //}


    public Bullet GetBullet()
    {
        // 풀에서 꺼내와서 반환
        PooledObject po = _bulletPool.PopPool();
        return po as Bullet;
    }












}
