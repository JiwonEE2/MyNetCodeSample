using Unity.Netcode.Components;

public class ClientNetworkTransform : NetworkTransform
{
	// Unity Netcode�� NetworkTransform�� ������ �⺻������ Server�� ������ �����Ƿ� Client�� �ڽ��� ������Ʈ�� ������ �� �ֵ��� NetworkTransform�� ���� ������ ������ �����ؾ� ��.
	protected override bool OnIsServerAuthoritative()
	{
		return false;
	}
}
