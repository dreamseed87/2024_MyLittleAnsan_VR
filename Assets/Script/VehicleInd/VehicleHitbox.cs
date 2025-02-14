using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleHitbox : MonoBehaviour
{
    [SerializeField] private GameObject correctEffectPrefab; // ���� ȹ�� ���� ����Ʈ
    [SerializeField] private GameObject wrongEffectPrefab; // ���� ȹ�� ���� ����Ʈ
    public bool isCorrect = false; // ������Ʈ ��ġ�� ���� ȹ�� ������ ��ġ���� üũ
    private GameObject instateEffectObj;
    private VehicleManager vehicleManager;
    private Transform finalPoint;
    public int ObjectHit()
    {
        transform.parent.GetComponent<HitboxMovement>().StopMovement();
        GetComponent<MeshCollider>().enabled = false;
        if (isCorrect) // Hit���� ���� �� hit �� ���� ȹ��
        {
            CorrectEffectPlay();
            vehicleManager.ScorePlus();
            Destroy(transform.parent.gameObject, 0.1f);
            return 0;
        }
        else // ���� ȹ�� ����
        {
            transform.parent.GetComponent<HitboxMovement>().TurnOff();
            WrongEffectPlay();
            Destroy(transform.parent.gameObject, 0.1f);
            return 1;
        }
    }
    private void CorrectEffectPlay() // ���� ȹ�� ���� ����Ʈ
    {
        finalPoint = transform.parent.GetComponent<HitboxMovement>().GetFinalPoint();
        //��ƼŬ������ ����
        instateEffectObj = Instantiate(correctEffectPrefab, transform.position, Quaternion.identity);
        ParticleSystem instantEffect = instateEffectObj.GetComponent<ParticleSystem>();
        //��ƼŬ ���� ��ġ�� ũ�� ����
        instateEffectObj.transform.position = finalPoint.position - new Vector3(0,0,0.012f);
        instateEffectObj.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        instateEffectObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        //��ƼŬ ���
        instantEffect.Play();
        Destroy(instateEffectObj, 1f);
    }
    private void WrongEffectPlay() // ���� ȹ�� ���� ����Ʈ
    {
        //��ƼŬ������ ����
        instateEffectObj = Instantiate(wrongEffectPrefab, transform.position, Quaternion.identity);
        ParticleSystem instantEffect = instateEffectObj.GetComponent<ParticleSystem>();
        //��ƼŬ ���� ��ġ�� ũ�� ����
        instateEffectObj.transform.position = transform.position;
        instateEffectObj.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        instateEffectObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        //��ƼŬ ���
        instantEffect.Play();
        Destroy(instateEffectObj, 1f);
    }
    public void SetVehicleManager(VehicleManager temp)
    {
        vehicleManager = temp;
    }
}
