using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTestSpawner : MonoBehaviour
{
    [SerializeField] private UnitData testUnit;
    [SerializeField] private Vector3 spawnPosition = new Vector3(0, 0, 0);

    private void Start()
    {
        Spawn(testUnit, spawnPosition);
    }

    private void Spawn(UnitData data, Vector3 pos)
    {
        Quaternion rotation;

        if (data is PeashooterData)
            rotation = Quaternion.Euler(0, 90, 0); // �������� ���ϰ� (Peashooter�� ������ ��� �ϱ� ����)
       
        else
            rotation = Quaternion.identity;

        GameObject go = Instantiate(data.unitPrefab, pos, rotation);
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
