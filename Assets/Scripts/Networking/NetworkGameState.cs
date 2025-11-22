using Unity.Netcode;

public class NetworkGameState : NetworkBehaviour
{
    public static NetworkGameState Instance;

    public NetworkVariable<int> CurrentTurn = new(1);
    public NetworkVariable<int> Player1Score = new(0);
    public NetworkVariable<int> Player2Score = new(0);
    public NetworkVariable<int> ActivePlayer = new(0);

    private void Awake()
    {
        Instance = this;
    }
}
