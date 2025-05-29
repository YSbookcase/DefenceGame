using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private UnitData[] unitDatas;

    public void SpawnUnit(int index, Vector3 position)
    {
        UnitData data = unitDatas[index];
        GameObject go = Instantiate(data.unitPrefab, position, Quaternion.identity);
        // Unit�� �����ͼ� �ٿ�ĳ����
        Unit unit = go.GetComponent<Unit>();

        if (unit is UnitPeashooter peashooterUnit)
        {
            peashooterUnit.Init(data); // �������̵�� Init() ȣ���
        }
        else
        {
            unit.Init(data); // �⺻ Unit�̸� ���� Init ȣ��
        }




    }
}
