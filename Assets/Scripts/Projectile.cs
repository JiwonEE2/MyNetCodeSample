using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	// ����ü ������Ʈ
	// ���巹Ŀ���� ����Ͽ� �ǽð����� ��ġ�� ����ȭ���� �ʰ�,
	// �� Ŭ���̾�Ʈ������ ������ ��� �̵�

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
