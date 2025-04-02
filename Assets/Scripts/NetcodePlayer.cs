using Unity.Netcode;
using UnityEngine;

public class NetcodePlayer : NetworkBehaviour
{
	public float moveSpeed;
	private Renderer rend;
	private NetworkVariable<Color> colorVar = new NetworkVariable<Color>(writePerm: NetworkVariableWritePermission.Owner);

	private void Awake()
	{
		rend = GetComponent<Renderer>();
	}

	private void Start()
	{
		if (IsOwner)
		{
			colorVar.Value = new Color(Random.value, Random.value, Random.value);
		}
		rend.material.color = colorVar.Value;
		//rend.material.color = new Color(Random.value, Random.value, Random.value);
	}

	public override void OnNetworkSpawn()
	{
		colorVar.OnValueChanged += OnColorChange;
	}

	private void OnColorChange(Color prev, Color next)
	{
		rend.material.color = next;
	}

	private void Update()
	{
		// IsOwner : NetworkBehaviour�� ����.
		// ���� ������ ������Ʈ�� ������
		// photonView.isMine return; �� ���� ��.
		if (!IsOwner) return;
		Move();
		Fire();
	}

	private void Move()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		transform.Translate(new Vector3(x, y) * moveSpeed * Time.deltaTime);
	}

	// Unity Netcode�� RPC�� ServerRpc�� ClientRpc�� ������ ��.
	// ServerRpc : Ŭ���̾�Ʈ�� ȣ���Ͽ� �������� �޴� RPC (Client -> Server)
	// ClientRpc : ������ ȣ���Ͽ� Ŭ���̾�Ʈ���� �޴� RPC (Server -> Client)
	private void Fire()
	{
		if (Input.GetButtonDown("Jump"))
		{
			// ������ RPC�� ȣ��
			FireServerRpc(new ServerRpcParams());
		}
	}

	// ServerRpc�� �����ϴ� ��� : ServerRpcAttribute�� ���̰�,
	// �Լ� �̸� �ڿ� �� "ServerRpc"�� ����
	[ServerRpc]
	private void FireServerRpc(ServerRpcParams param)
	{
		print($"rpc call by client: {param.Receive.SenderClientId}");
		// Ŭ��� Rpc ȣ��
		FireClientRpc(new ClientRpcParams());
	}

	public NetworkObject projectilePrefab;

	// ClientRpc�� ���������� ClientRpcAttribute�� ���̰�,
	// �Լ� �̸� �ڿ� ClientRpc�� ����
	[ClientRpc]
	private void FireClientRpc(ClientRpcParams param)
	{
		// ����ü ����
		Instantiate(projectilePrefab, transform.position, Quaternion.identity);
	}
}
