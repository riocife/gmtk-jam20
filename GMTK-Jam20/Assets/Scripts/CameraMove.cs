using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] float awayFromPlayer = 0.25f;

    Transform player;
    Camera mainCamera;

    float initialZ;

	Vector3 target, mousePos, refVel, shakeOffset;

	public float cameraDist = 3.5f;
	public float smoothTime = 0.2f;
	public float shakingSmoothTime = 0.05f;

	bool shaking;
	float shakeTimeEnd;
	Vector3 shakeVector;
	float shakeMag;

	void Start()
    {
        initialZ = transform.position.z;

        mainCamera = GetComponent<Camera>();
        player = FindObjectOfType<PlayerMovement>().transform;    
    }

    void Update()
    {
		mousePos = CaptureMousePos();
		target = UpdateTargetPos();
		UpdateCameraPosition();

	}

	Vector3 CaptureMousePos()
	{
		Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition); //raw mouse pos
		ret *= 2;
		ret -= Vector2.one; //set (0,0) of mouse to middle of screen
		float max = 0.9f;
		if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
		{
			ret = ret.normalized; //helps smooth near edges of screen
		}
		return ret;
	}

	Vector3 UpdateTargetPos()
	{
		Vector3 mouseOffset = mousePos * cameraDist;
		Vector3 ret = player.position + mouseOffset;
		ret += UpdateShake();
		ret.z = initialZ; //make sure camera stays at same Z coord
		return ret;
	}

	Vector3 UpdateShake()
	{
		if (!shaking) return Vector3.zero;

		float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
		Vector3 shakeOffset = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f) * shakeMag;
		return shakeOffset;
	}

	void UpdateCameraPosition()
	{
		Vector3 tempPos;
		// When shaking we don't want to be smooth.
		float actualSmoothTime = (shaking) ? shakingSmoothTime : smoothTime;
		tempPos = Vector3.SmoothDamp(transform.position, target, ref refVel, smoothTime);
		transform.position = tempPos;
	}

	public void Shake(Vector3 direction, float magnitude, float length)
	{ 
		shaking = true;
		shakeVector = direction;
		shakeMag = magnitude;

		StopCoroutine(ShakeCooldown(0f));
		StartCoroutine(ShakeCooldown(length));
	}

	IEnumerator ShakeCooldown(float duration)
	{
		yield return new WaitForSeconds(duration);
		shaking = false;
	}
}
