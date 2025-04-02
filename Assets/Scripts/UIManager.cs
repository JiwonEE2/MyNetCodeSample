using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Button hostButton;
	public Button serverButton;
	public Button clientButton;
	public Button exitButton;

	private void Awake()
	{
		hostButton.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
		serverButton.onClick.AddListener(() => NetworkManager.Singleton.StartServer());
		clientButton.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
		exitButton.onClick.AddListener(() => NetworkManager.Singleton.Shutdown());

		exitButton.gameObject.SetActive(false);
	}

	private bool connectCache = false;

	private void Update()
	{
		bool isConnected = NetworkManager.Singleton?.IsConnectedClient ?? false;

		if (connectCache != isConnected)
		{
			if (isConnected)
			{
				print($"Client Connected. id: {NetworkManager.Singleton.LocalClientId}");
			}
			else
			{
				print("Client Disconnected.");
			}
			SetButtonActive(isConnected);
			connectCache = isConnected;
		}
	}

	public void SetButtonActive(bool isGameStart)
	{
		hostButton.gameObject.SetActive(!isGameStart);
		serverButton.gameObject.SetActive(!isGameStart);
		clientButton.gameObject.SetActive(!isGameStart);

		exitButton.gameObject.SetActive(isGameStart);
	}
}
