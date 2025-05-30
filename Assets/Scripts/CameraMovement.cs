using System.Collections;
using Unity.Cinemachine;

using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    bool isTurning = false;
    float turnDuration = 0.3f; // 回転にかける時間（秒）


    private void Start()
    {
        isTurning = false;
    }
    public void TurnCamera()
    {
        if (!isTurning)
        {
            StartCoroutine(TurnCameraCoroutine());
        }
    }

    private IEnumerator TurnCameraCoroutine()
    {
        isTurning = true; // コルーチンの実行を開始

        CinemachineOrbitalFollow orbitalFollow = GetComponent<CinemachineOrbitalFollow>();
        float elapsed = 0.0f;
        float initialValue = orbitalFollow.HorizontalAxis.Value;

        while (elapsed < turnDuration)
        {
            float increment = (180.0f * Time.deltaTime) / turnDuration;
            orbitalFollow.HorizontalAxis.Value += increment;
            elapsed += Time.deltaTime;
            yield return null;
        }

        isTurning = false; // コルーチンの実行が終了
    }

    private IEnumerator BackCameraCoroutine()
    {
        isTurning = true; // コルーチンの実行を開始

        CinemachineOrbitalFollow orbitalFollow = GetComponent<CinemachineOrbitalFollow>();

        float oldValue = orbitalFollow.HorizontalAxis.Value;
        orbitalFollow.HorizontalAxis.Value += 180.0f; // 180度回転
        float targetValue = orbitalFollow.HorizontalAxis.Value;


        while (isTurning)
        {
            orbitalFollow.HorizontalAxis.Value = targetValue;
            yield return null;
        }

        orbitalFollow.HorizontalAxis.Value = oldValue; // 元の値に戻す
    }

    public void BackCamera()
    {
        if (!isTurning)
        {
            StartCoroutine(BackCameraCoroutine());
        }
    }

    public void StopBackCamera()
    {
        isTurning = false;
        StopCoroutine(BackCameraCoroutine());   
    }

    public bool GetIsTurning()
    {
        return isTurning;
    }
}
