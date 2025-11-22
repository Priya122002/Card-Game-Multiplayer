using Unity.Netcode;
using UnityEngine;

public class PlayerNameSender : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(SendName), 1f);
    }

    private void SendName()
    {
        if (NetworkManager.Singleton == null) return;

        var playerObj = NetworkManager.Singleton.LocalClient?.PlayerObject;
        if (playerObj == null) return;

        var netPlayer = playerObj.GetComponent<NetworkPlayer>();
        if (netPlayer == null) return;

        string name = LocalPlayerCache.Instance?.PlayerName ?? "Player";

        netPlayer.SetPlayerNameServerRpc(name);
    }
}
