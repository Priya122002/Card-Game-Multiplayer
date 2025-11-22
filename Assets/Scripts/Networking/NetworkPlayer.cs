using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    public NetworkVariable<FixedString128Bytes> PlayerName = new();

    [ServerRpc(RequireOwnership = false)]
    public void SetPlayerNameServerRpc(string name)
    {
        PlayerName.Value = name;
    }
}
