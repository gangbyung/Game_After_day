using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;

    public Transform target; // ���� ��� (�÷��̾�)
    public float smoothSpeed = 0.125f; // ī�޶� �̵��� �ε巯�� �ӵ�
    public Vector3 offset; // ī�޶��� ��ġ ������

    public Vector2 minBounds; // ī�޶� �̵��� �ּ� ��谪
    public Vector2 maxBounds; // ī�޶� �̵��� �ִ� ��谪

    private float fixedZ; // ������ Z ��
    void Start()
    {
        // ���� ī�޶��� Z ���� ������ ������ ����
        fixedZ = transform.position.z;
        if (instance != null )
        {
            Destroy(this.gameObject);
            
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        
    }
    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogError("Camera target is not assigned!");
            return;
        }

        // ��ǥ ��ġ ��� (Ÿ���� ��ġ + ������)
        Vector3 desiredPosition = target.position + offset;

        // �ε巴�� �̵��ϱ� ���� Lerp ���
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // ��踦 ���� �ʵ��� Clamp ó��
        float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);

        // ���� ī�޶� ��ġ ����, Z ���� ������ ������ ����
        transform.position = new Vector3(clampedX, clampedY, fixedZ);
    }
}
