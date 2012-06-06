using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using System.Collections;
namespace prjXNAGame
{
    public class networker
    {
        public  NetworkSession session = null; // The game session
        private int maximumGamers = 2; // Only 2 will play
        private int maximumLocalPlayers = 1; // no split-screen, only remote players
        PacketWriter packetWriter = new PacketWriter();
        PacketReader packetReader = new PacketReader();
        public int netState=0;
       
       
        public void SignInGamer()
        {
            if (!Guide.IsVisible)
            {
                Guide.ShowSignIn(1, false);
            }
        }
        public void CreateSession()
        {
            
            if (session == null)
            {
                session = NetworkSession.Create(NetworkSessionType.SystemLink,
                maximumLocalPlayers,
                maximumGamers);
            }
            // If the host goes out, another machine will assume as a new host
            session.AllowHostMigration = true;
            // Allow players to join a game in progress
            session.AllowJoinInProgress = true;
            session.GamerJoined +=
            new EventHandler<GamerJoinedEventArgs>(session_GamerJoined);
            session.GamerLeft +=
            new EventHandler<GamerLeftEventArgs>(session_GamerLeft);
            session.GameStarted +=
            new EventHandler<GameStartedEventArgs>(session_GameStarted);
            session.GameEnded +=
            new EventHandler<GameEndedEventArgs>(session_GameEnded);
            session.SessionEnded +=
            new EventHandler<NetworkSessionEndedEventArgs>(session_SessionEnded);
            session.HostChanged +=
            new EventHandler<HostChangedEventArgs>(session_HostChanged);
        }

        public void SendMessage(string key)
        {
            foreach (LocalNetworkGamer localPlayer in session.LocalGamers)
            {
                packetWriter.Write(key);
                localPlayer.SendData(packetWriter, SendDataOptions.Reliable   );
                message = "Sending message: " + key;
            }
        }


        public int remoteMouseX, remoteMouseY;
        public bool remoteMouseLeftButton, remoteMouseRightButton;
        public String remoteData;
        public ArrayList remoteKeys = new ArrayList();


        public void ReceiveMessage()
        {
            NetworkGamer remotePlayer; // The sender of the message
            foreach (LocalNetworkGamer localPlayer in session.LocalGamers)
            {
                // While there is data available for us, keep reading
                while (localPlayer.IsDataAvailable)
                {
                    localPlayer.ReceiveData(packetReader, out remotePlayer);
                    // Ignore input from local players
                    if (!remotePlayer.IsLocal)
                    {
                        //netKey = packetReader.ReadString();
                                       
                        remoteData = packetReader.ReadString();
                        
                       
                        remoteMouseX =Convert.ToInt16 (remoteData.Substring (0,remoteData.IndexOf ("|")));
                       
                         remoteData = remoteData.Substring(remoteData.IndexOf("|")+1);
                        remoteMouseY = Convert.ToInt16(remoteData.Substring(0, remoteData.IndexOf("|")));
                        remoteData = remoteData.Substring(remoteData.IndexOf("|") + 1);
                        
                        if (remoteData.Substring(0, remoteData.IndexOf("|")) == "Released")
                            remoteMouseLeftButton = false;
                        else
                            remoteMouseLeftButton = true;
                        remoteData = remoteData.Substring(remoteData.IndexOf("|") + 1);
                        if (remoteData.Substring(0, remoteData.IndexOf("|")) == "Released")
                            remoteMouseRightButton = false;
                        else
                            remoteMouseRightButton = true;
                        remoteData = remoteData.Substring(remoteData.IndexOf("|") + 1);
                        remoteKeys.Clear();
                        
                        while (remoteData.IndexOf("|") > 0)
                        {
                            remoteKeys.Add (remoteData.Substring(0, remoteData.IndexOf("|")));
                            if (remoteData.IndexOf("|") < remoteData.Length)
                                remoteData = remoteData.Substring(remoteData.IndexOf("|") + 1);
                            else
                                remoteData = "";
                        }


                        message = "Received message: " + Convert.ToString(remoteData); ;
                        //netKey;

                    }
                }
            }
        }


        public Boolean isRemoteKeyPressed(Keys whichKey)
        {

            for (int c = 0; c < remoteKeys.Count;c++ )
            {
                if (Convert.ToString(remoteKeys[c]) == Convert.ToString(whichKey))
                    return true;
            }
            return false;

        }

        public NetworkSessionState SessionState
        {
        get

            {
            if (session == null)
             return NetworkSessionState.Ended;
            else
             return session.SessionState;
            }
        }






        void session_GamerJoined(object sender, GamerJoinedEventArgs e)
        {
            if (e.Gamer.IsHost)
            {
                message = "The Host started the session!";
            }
            else
            {
                message = "Gamer " + e.Gamer.Tag + " joined the session!";
                // Other played joined, start the game!
                session.StartGame();
            }
        }
        void session_GamerLeft(object sender, GamerLeftEventArgs e)
        {
        message = "Gamer " + e.Gamer.Tag + " left the session!";
        }
        void session_GameStarted(object sender, GameStartedEventArgs e)
        {
        message = "Game Started";
        }
        void session_HostChanged(object sender, HostChangedEventArgs e)
        {
        message = "Host changed from " + e.OldHost.Tag + " to " + e.NewHost.Tag;
        }
        void session_SessionEnded(object sender, NetworkSessionEndedEventArgs e)
        {
        message = "The session has ended";
        }
        void session_GameEnded(object sender, GameEndedEventArgs e)
        {
        message = "Game Over";
        }

        public void FindSession()
        {
          // all sessions found
            AvailableNetworkSessionCollection availableSessions;
            // the session we'll join
            AvailableNetworkSession availableSession = null;
            availableSessions = NetworkSession.Find(NetworkSessionType.SystemLink,
            maximumLocalPlayers, null);
            // Get a session with available gamer slots
            foreach (AvailableNetworkSession curSession in availableSessions)
            {
                int TotalSessionSlots = curSession.OpenPublicGamerSlots +
                curSession.OpenPrivateGamerSlots;
                
                if (TotalSessionSlots >= curSession.CurrentGamerCount)
                    availableSession = curSession;
            }
            // if a session was found, connect to it
            if (availableSession != null)
            {
                message = "Found an available session at host " +
                availableSession.HostGamertag;
                session = NetworkSession.Join(availableSession);
            }
            else
                message = "No sessions found!";
        }


        IAsyncResult AsyncSessionFind = null;
        public void AsyncFindSession()
        {
            message = "Asynchronous search started!";
            if (AsyncSessionFind == null)
            {
                AsyncSessionFind = NetworkSession.BeginFind(
                NetworkSessionType.SystemLink, maximumLocalPlayers, null,
                new AsyncCallback(session_SessionFound), null);
            }
        }
        public void session_SessionFound(IAsyncResult result)
        {
            // all sessions found
            AvailableNetworkSessionCollection availableSessions;
            // the session we will join
            AvailableNetworkSession availableSession = null;
            if (AsyncSessionFind.IsCompleted)
            {
                availableSessions = NetworkSession.EndFind(result);
                // Look for a session with available gamer slots
                foreach (AvailableNetworkSession curSession in
                availableSessions)
                {
                    int TotalSessionSlots = curSession.OpenPublicGamerSlots +
                    curSession.OpenPrivateGamerSlots;
                    if (TotalSessionSlots > curSession.CurrentGamerCount)
                        availableSession = curSession;
                }
                // if a session was found, connect to it
                if (availableSession != null)
                {
                    message = "Found an available session at host" +
                    availableSession.HostGamertag;
                    session = NetworkSession.Join(availableSession);
                }
                else
                    message = "No sessions found!";
                // Reset the session finding result
                AsyncSessionFind = null;
            }
        }

        public void SetPlayerReady()
        {
            foreach (LocalNetworkGamer gamer in session.LocalGamers)
                gamer.IsReady = true;
        }



        // Message regarding the session's current state
        private String message = "Waiting for user command...";
        public String Message
        {
            get { return message; }
        }

        public void Update()
        {
            if (session != null)
            {
                session.Update();
                if (session.SessionState == NetworkSessionState.Lobby)
                    netState = 0;
                else if (session.SessionState == NetworkSessionState.Playing)
                    netState = 1;
                else
                    netState = 2;
            }
        }

    }
}
