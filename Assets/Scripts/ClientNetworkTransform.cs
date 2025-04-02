using Unity.Netcode.Components;

public class ClientNetworkTransform : NetworkTransform
{
	// Unity Netcode의 NetworkTransform의 권한은 기본적으로 Server만 가지고 있으므로 Client가 자신의 오브젝트를 움직일 수 있도록 NetworkTransform을 통해 제어할 권한을 제공해야 함.
	protected override bool OnIsServerAuthoritative()
	{
		return false;
	}
}
