using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	// 투사체 오브젝트
	// 데드레커닝을 사용하여 실시간으로 위치를 동기화하지 않고,
	// 각 클라이언트에서만 정해진 대로 이동

	public float moveSpeed;

	private void Start()
	{
		Destroy(gameObject, 3f);
	}

	private void Update()
	{
		transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
	}
}
