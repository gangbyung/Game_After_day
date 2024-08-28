using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;

    public Transform target; // 따라갈 대상 (플레이어)
    public float smoothSpeed = 0.125f; // 카메라 이동의 부드러움 속도
    public Vector3 offset; // 카메라의 위치 오프셋

    public Vector2 minBounds; // 카메라 이동의 최소 경계값
    public Vector2 maxBounds; // 카메라 이동의 최대 경계값

    private float fixedZ; // 고정된 Z 값
    void Start()
    {
        // 현재 카메라의 Z 값을 고정된 값으로 설정
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

        // 목표 위치 계산 (타겟의 위치 + 오프셋)
        Vector3 desiredPosition = target.position + offset;

        // 부드럽게 이동하기 위해 Lerp 사용
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 경계를 넘지 않도록 Clamp 처리
        float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);

        // 실제 카메라 위치 적용, Z 값을 고정된 값으로 설정
        transform.position = new Vector3(clampedX, clampedY, fixedZ);
    }
}
