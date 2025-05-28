using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DesignPattern;

public class UnitShoot : MonoBehaviour
{

    [SerializeField] private WeaponPoint canon;
    [SerializeField] private float delay = 1f;
    //[SerializeField] private float minChargeTime = 0.5f;  // 최소 차징 필요 시간


   
     
    private Coroutine _myCoroutien;
    //private bool isChargingFromUI = false;
    //private float uiHoldTime = 0f;



    private void Update()
    {

        if (_myCoroutien == null)
        {
            _myCoroutien = StartCoroutine(myRoutain());
        }


    }

    //public void FireButtonPressed()
    //{
    //    if (_myCoroutien == null)
    //    { 
    //        _myCoroutien = StartCoroutine(myRoutain());
    //        //_myCoroutien = StartCoroutine(ChargingShot());
    //    }
    //}


    private IEnumerator myRoutain()
    {

        WaitForSeconds delayIN = new WaitForSeconds(delay);
     

        while (true)
        {

            canon.Fire();
            yield return delayIN;
        }


        //_myCoroutien = null;

    }




    //private IEnumerator ChargingShot()
    //{
    //
    //    float holdTime = 0;
    //
    //    while (Input.GetKey(KeyCode.Space))
    //    {
    //        holdTime += Time.deltaTime;
    //        yield return null;
    //    }
    //
    //    if (holdTime >= minChargeTime)
    //    {
    //        float bulletSpeed = Mathf.Clamp(holdTime * 30f, 5f, 30f);
    //        canon.Fire(bulletSpeed);
    //
    //    }
    //
    //    yield return new WaitForSeconds(delay);
    //
    //    _myCoroutien = null;
    //}




    private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"[Tank] Collided with {other.gameObject.name} (tag={other.gameObject.tag})");
        if (other.gameObject.CompareTag("Monster"))
        {
            
            gameObject.SetActive(false);
        }
    }

    //public void StartChargingFromUI()
    //{
    //    if (_myCoroutien == null)
    //        _myCoroutien = StartCoroutine(UIChargingRoutine());
    //}
    //
    //public void ReleaseFireFromUI()
    //{
    //    isChargingFromUI = false;
    //}

    //private IEnumerator UIChargingRoutine()
    //{
    //    isChargingFromUI = true;
    //    uiHoldTime = 0f;
    //
    //    while (isChargingFromUI)
    //    {
    //        uiHoldTime += Time.deltaTime;
    //        yield return null;
    //    }
    //
    //    if (uiHoldTime >= minChargeTime)
    //    {
    //        float bulletSpeed = Mathf.Clamp(uiHoldTime * 30f, 5f, 30f);
    //        canon.Fire(bulletSpeed);
    //    }
    //
    //    yield return new WaitForSeconds(delay);
    //    _myCoroutien = null;
    //}



}
