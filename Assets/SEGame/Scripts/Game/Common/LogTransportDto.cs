
using Unity.Collections;
using Unity.Netcode;

namespace SEGame
{
    public class LogTransportDto : NetworkBehaviour
    {
        public NetworkVariable<FixedString512Bytes> message;

        [ServerRpc]
        public void SendMessageServerRpc(string message, ServerRpcParams serverRpcParams)
        {
            this.message.Value = $"user {serverRpcParams.Receive.SenderClientId}:> " + message;
        }
        
        public void SendMessage(string message)
        {
            SendMessageServerRpc(message, new ServerRpcParams());
        }
    }
}


