// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using Bdt.Shared.Request;
using Bdt.Shared.Response;
#endregion

namespace Bdt.Shared.Service
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Les services proposés par une instance de Tunnel
    /// </summary>
    /// -----------------------------------------------------------------------------
    public interface ITunnel
    {

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Login d'un utilisateur
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>une réponse LoginResponse avec un sid</returns>
        /// -----------------------------------------------------------------------------
        LoginResponse Login(LoginRequest request);

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Logout d'un utilisateur
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>une réponse générique</returns>
        /// -----------------------------------------------------------------------------
        MinimalResponse Logout(SessionContextRequest request);

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// La version du serveur
        /// </summary>
        /// <returns>La version de l'assembly</returns>
        /// -----------------------------------------------------------------------------
        MinimalResponse Version();

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ping du serveur
        /// </summary>
        /// -----------------------------------------------------------------------------
        void Ping();

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Etablissement d'une nouvelle connexion
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>le cid de connexion</returns>
        /// -----------------------------------------------------------------------------
        ConnectResponse Connect(ConnectRequest request);

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Déconnexion
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>une reponse générique</returns>
        /// -----------------------------------------------------------------------------
        ConnectionContextResponse Disconnect(ConnectionContextRequest request);

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Lecture de données
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>les données lues dans response.Data</returns>
        /// -----------------------------------------------------------------------------
        ReadResponse Read(ConnectionContextRequest request);

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ecriture de données
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>une réponse générique</returns>
        /// -----------------------------------------------------------------------------
        ConnectionContextResponse Write(WriteRequest request);

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Enumeration des session
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>les sessions dans response.Sessions</returns>
        /// -----------------------------------------------------------------------------
        MonitorResponse Monitor(SessionContextRequest request);

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Demande de suppression d'une session
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>une réponse générique</returns>
        /// -----------------------------------------------------------------------------
        MinimalResponse KillSession(KillSessionRequest request);

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Demande de suppression d'une connexion
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>une réponse générique</returns>
        /// -----------------------------------------------------------------------------
        ConnectionContextResponse KillConnection(KillConnectionRequest request);
    }

}


