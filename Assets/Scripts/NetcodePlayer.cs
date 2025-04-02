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
		// IsOwner : NetworkBehaviour에 있음.
		// 내가 소유한 오브젝트만 움직임
		// photonView.isMine return; 과 같은 듯.
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

	// Unity Netcode의 RPC는 ServerRpc와 ClientRpc로 나뉘어 짐.
	// ServerRpc : 클라이언트가 호출하여 서버에서 받는 RPC (Client -> Server)
	// ClientRpc : 서버가 호출하여 클라이언트에서 받는 RPC (Server -> Client)
	private void Fire()
	{
		if (Input.GetButtonDown("Jump"))
		{
			// 서버로 RPC를 호출
			FireServerRpc(new ServerRpcParams());
		}
	}

	// ServerRpc를 정의하는 방법 : ServerRpcAttribute를 붙이고,
	// 함수 이름 뒤에 꼭 "ServerRpc"를 붙임
	[ServerRpc]
	private void FireServerRpc(ServerRpcParams param)
	{
		print($"rpc call by client: {param.Receive.SenderClientId}");
		// 클라로 Rpc 호출
		FireClientRpc(new ClientRpcParams());
	}

	public NetworkObject projectilePrefab;

	// ClientRpc도 마찬가지로 ClientRpcAttribute를 붙이고,
	// 함수 이름 뒤에 ClientRpc를 붙임
	[ClientRpc]
	private void FireClientRpc(ClientRpcParams param)
	{
		// 투사체 생성
		Instantiate(projectilePrefab, transform.position, Quaternion.identity);
	}
}
