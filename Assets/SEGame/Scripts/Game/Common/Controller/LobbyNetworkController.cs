
using System.Collections.Generic;
using SkyForge.Extention;
using Unity.Netcode;
using System.Linq;
using System;

namespace SEGame
{
    public class LobbyNetworkController : NetworkBehaviour, IDisposable
    {
        private LobbyService m_lobbyService;
        private IUILogViewModel m_uILogViewModel;

        private ulong m_clientId;
        
#if DEDICATED_SERVER
        
        public void Init(LobbyService lobbyService, IUILogViewModel uILogViewModel, ulong clientId)
        {
            m_lobbyService = lobbyService;
            m_uILogViewModel = uILogViewModel;
            m_clientId = clientId;
        }

        public override void OnNetworkSpawn()
        {
            if (!IsServer)
                return;
            
            foreach (var connectedClientId in NetworkManager.Singleton.ConnectedClients.Keys)
            {
                if(connectedClientId != m_clientId)
                    gameObject.GetComponent<NetworkObject>().NetworkHide(connectedClientId);
            }
        }
#else
        public override void OnNetworkSpawn()
        {
            if (IsServer || !IsOwner)
                return;
            
            var gameplayEntryPoint = UnityExtention.GetEntryPoint<GameplayEntryPoint>() as GameplayEntryPoint;
            var gameplayContainer = gameplayEntryPoint?.GetContainer();
            m_uILogViewModel = gameplayContainer?.Resolve<IUILogViewModel>();
            
            gameplayContainer?.RegisterInstance(this);
            base.OnNetworkSpawn();
        }

#endif

        [ServerRpc]
        public void SearchLobbyServerRpc(ServerRpcParams serverRpcParams)
        {
            var currentClientId = serverRpcParams.Receive.SenderClientId;
            
            m_uILogViewModel.LogMessage("server","server search Lobby");
            
            if (!m_lobbyService.SearchLobby(currentClientId, out var lobbyId))
            {

                var failedClientRpcParams = CreateClientRpcParams(new List<ulong>() { currentClientId });
                
                DontFindLobbyClientRpc(failedClientRpcParams);
                
                m_uILogViewModel.LogMessage("server","failed to search lobby");
                return;
            }

            if (!m_lobbyService.AddPlayerToLobby(lobbyId, currentClientId))
            {
                var notifyClientRpcParams = CreateClientRpcParams(new List<ulong>() { currentClientId });
                
                FailedToAddedPlayerInLobbyClientRpc(notifyClientRpcParams);
                
                m_uILogViewModel.LogMessage("server","search lobby failed");
            }

            if (m_lobbyService.GetFirstClientId(lobbyId, out var firstClientId))
            {
                var successClientRpcParams = CreateClientRpcParams(new List<ulong>() { currentClientId, firstClientId });
            
                FindLobbyClientRpc(firstClientId, currentClientId, successClientRpcParams);
            
                m_uILogViewModel.LogMessage("server",$"search lobby Success users: {currentClientId}, {firstClientId}");
            }
        }

        [ServerRpc]
        public void CreateLobbyServerRpc(ServerRpcParams serverRpcParams)
        {
            if (!m_lobbyService.CreateLobby(serverRpcParams.Receive.SenderClientId))
            {
                var notifyClientRpcParams = CreateClientRpcParams(new List<ulong>() { serverRpcParams.Receive.SenderClientId });
                
                FailedToAddedPlayerInLobbyClientRpc(notifyClientRpcParams);
                return;
            }
            
            var successClientRpcParams = CreateClientRpcParams(new List<ulong>() { serverRpcParams.Receive.SenderClientId });
            
            CreatedLobbyClientRpc(successClientRpcParams);
            m_uILogViewModel.LogMessage("server","server create lobby");
        }

        [ClientRpc]
        public void DontFindLobbyClientRpc(ClientRpcParams clientRpcParams)
        {
            m_uILogViewModel.LogMessage($"user {NetworkManager.Singleton.LocalClientId}","dont-find lobby in server");
            CreateLobbyClient();
        }

        [ClientRpc]
        public void FindLobbyClientRpc(ulong firstClientId, ulong secondClientId, ClientRpcParams clientRpcParams)
        {
            if(firstClientId.Equals(NetworkManager.Singleton.LocalClientId) || secondClientId.Equals(NetworkManager.Singleton.LocalClientId))
                m_uILogViewModel.LogMessage($"user {NetworkManager.Singleton.LocalClientId}","find lobby in server");
        }

        [ClientRpc]
        public void CreatedLobbyClientRpc(ClientRpcParams clientRpcParams)
        {
            m_uILogViewModel.LogMessage($"user {NetworkManager.Singleton.LocalClientId}","server created lobby");
        }

        [ClientRpc]
        public void FailedToAddedPlayerInLobbyClientRpc(ClientRpcParams clientRpcParams)
        {
            m_uILogViewModel.LogMessage($"user {NetworkManager.Singleton.LocalClientId}", "server failed to add player in lobby");
        }
        
        public void SearchLobbyClient()
        {
            SearchLobbyServerRpc(new ServerRpcParams());
        }

        public void CreateLobbyClient()
        {
            CreateLobbyServerRpc(new ServerRpcParams());
        }

        private ClientRpcParams CreateClientRpcParams(List<ulong> clientIds)
        {

            var clientRpcSendParams = new ClientRpcSendParams()
            {
                TargetClientIds = clientIds.ToList()
            };
                
            var clientRpcParams = new ClientRpcParams()
            {
                Send = clientRpcSendParams
            };
            return clientRpcParams;
        }

        public void Dispose()
        {
            
        }
    }
}