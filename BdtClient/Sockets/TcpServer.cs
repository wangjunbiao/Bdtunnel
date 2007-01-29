// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Bdt.Shared.Logs;
using Bdt.Client.Resources;
#endregion

namespace Bdt.Client.Sockets
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Serveur TCP de base
    /// </summary>
    /// -----------------------------------------------------------------------------
    public abstract class TcpServer : LoggedObject
    {

        #region " Constantes "
        public const int ACCEPT_POLLING_TIME = 50;
        #endregion

        #region " Attributs "
        private TcpListener m_listener;
        private ManualResetEvent m_mre = new ManualResetEvent(false);
        private IPAddress m_ip = IPAddress.Any;
        private int m_port;
        #endregion

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// L'ip d'écoute
        /// </summary>
        /// -----------------------------------------------------------------------------
        public IPAddress Ip
        {
            get
            {
                return m_ip;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le port d'écoute
        /// </summary>
        /// -----------------------------------------------------------------------------
        public int Port
        {
            get
            {
                return m_port;
            }
        }
        #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="port">port local côté client</param>
        /// <param name="shared">bind sur toutes les ip/ip locale</param>
        /// -----------------------------------------------------------------------------
        public TcpServer(int port, bool shared)
        {
            m_ip = (IPAddress)(shared ? IPAddress.Any : IPAddress.Loopback);
            m_port = port;

            m_listener = new TcpListener(m_ip, m_port);
            Thread thr = new Thread(new System.Threading.ThreadStart(ServerThread));
            thr.Start();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Callback en cas de nouvelle connexion
        /// </summary>
        /// <param name="client">le socket client</param>
        /// -----------------------------------------------------------------------------
        protected abstract void OnNewConnection(TcpClient client);

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fermeture de l'écoute
        /// </summary>
        /// -----------------------------------------------------------------------------
        public void CloseServer()
        {
            m_mre.Set();
            m_listener.Stop();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Traitement principal du thread
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void ServerThread()
        {
            try
            {
                m_listener.Start();
                while (!m_mre.WaitOne(ACCEPT_POLLING_TIME, false))
                {
                    try
                    {
                        TcpClient client = m_listener.AcceptTcpClient();
                        OnNewConnection(client);
                    }
                    catch (SocketException ex)
                    {
                        if (ex.SocketErrorCode != SocketError.Interrupted)
                        {
                            Log(ex.Message, ESeverity.ERROR);
                            Log(ex.ToString(), ESeverity.DEBUG);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log(ex.Message, ESeverity.ERROR);
                        Log(ex.ToString(), ESeverity.DEBUG);
                    }
                }
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.AddressAlreadyInUse)
                {
                    Log(String.Format(Strings.TCP_SERVER_DISABLED, m_port), ESeverity.WARN);
                }
                else
                {
                    Log(ex.Message, ESeverity.ERROR);
                }
                Log(ex.ToString(), ESeverity.DEBUG);
            }
            catch (Exception ex)
            {
                Log(ex.Message, ESeverity.ERROR);
                Log(ex.ToString(), ESeverity.DEBUG);
            }
        }
        #endregion

    }

}

