using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Tibia.Client;
using Tibia.Features.Model;
using Tibia.Features.Structures;
using KTibiaX.Cam.Movie;
using Keyrox.Shared.Objects;
using Keyrox.Shared.Files;
using System.IO;

namespace KTibiaX.Cam {
    public partial class frm_Main : DevExpress.XtraBars.Ribbon.RibbonForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Main"/> class.
        /// </summary>
        public frm_Main() {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>The client.</value>
        public TibiaClient Client { get; set; }

        #region "[rgn] Properties "
        public Player Player { get { return Client.Features.Player; } }
        public MovieFile CurrentMovie { get; set; }
        #endregion

        /// <summary>
        /// Handles the Load event of the frm_Main control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_Main_Load(object sender, EventArgs e) {
            if (Program.AppClientProcess == null) { Close(); }
            this.Client = new TibiaClient(Program.AppClientProcess);
        }

        #region "[rgn] Event Handler"
        private void btnRead_Click(object sender, EventArgs e) {
            CurrentMovie = new MovieFile() {
                Data = DateTime.Now,
                Version = "8.40"
            };
            CurrentMovie.AddChapter(Client);
            var fi = new FileInfo("C:\\teste.mov");
            fi.Write(CurrentMovie.Serialize());
        }
        private void btnWrite_Click(object sender, EventArgs e) {
            ConnectClient();
        }
        #endregion

        /// <summary>
        /// Connects the client.
        /// </summary>
        public void ConnectClient() {

            var c = new Tibia.Objects.Client(Client.Process);
            //c.Login.SetAccountInfo("222222", "tibia"); // Set number account and password in client
            Client.Memory.Writer.String(0x78832C, "testandu", true);
            Client.Memory.Writer.String(0x78834C, "123456", true);
            int ClickX = 90;
            int ClickY = c.Window.Size.Height - 240;
            int Lparm = ((ClickY << 16) | (ClickX & 0xffff));
            c.Input.SendMessage(0x201, 0, Lparm); // Click down in button enter game
            c.Input.SendMessage(0x202, 0, Lparm); // Click up in button enter game
            System.Threading.Thread.Sleep(800);
            c.Input.SendMessage(0x102, 0x0D, 0); // Press enter key

            //var connect = new Callback(delegate() { Client.Connect("127.0.0.1", 6969, true); });
            //connect.BeginInvoke(ConnectionComplete, connect);
        }

        /// <summary>
        /// Connections the complete.
        /// </summary>
        /// <param name="result">The result.</param>
        private void ConnectionComplete(IAsyncResult result) {
                        

            CurrentMovie = new FileInfo("C:\\teste.mov").Read().Deserialize<MovieFile>();
            CurrentMovie.Chapters[0].StartChapter(Client);
        }

        #region "[rgn] Helpers "
        public bool LevelSpyOn(int floor) {
            uint playerZ, tempPtr;

            Client.Memory.Writer.Bytes(Client.Memory.Addresses.SpyLevel.LevelSpy1, Client.Memory.Addresses.SpyLevel.Nops);
            Client.Memory.Writer.Bytes(Client.Memory.Addresses.SpyLevel.LevelSpy2, Client.Memory.Addresses.SpyLevel.Nops);
            Client.Memory.Writer.Bytes(Client.Memory.Addresses.SpyLevel.LevelSpy3, Client.Memory.Addresses.SpyLevel.Nops);

            tempPtr = Client.Memory.Reader.Uint(Client.Memory.Addresses.SpyLevel.LevelSpyPtr);
            tempPtr += Client.Memory.Addresses.SpyLevel.LevelSpyAdd1;
            tempPtr = Client.Memory.Reader.Uint(tempPtr);
            tempPtr += Client.Memory.Addresses.SpyLevel.LevelSpyAdd2;

            playerZ = Client.Memory.Reader.Uint(Client.Memory.Addresses.Player.PlayerZ);

            if (playerZ <= 7) {
                if (playerZ - floor >= 0 && playerZ - floor <= 7) {
                    playerZ = 7 - playerZ;
                    Client.Memory.Writer.Uint(tempPtr, playerZ + (uint)floor);
                    return true;
                }
            }
            else {
                if (floor >= -2 && floor <= 2 && playerZ - floor < 16) {
                    Client.Memory.Writer.Uint(tempPtr, 2 + (uint)floor);
                    return true;
                }
            }

            return false;
        }
        public void LevelSpyOff() {
            Client.Memory.Writer.Bytes(Client.Memory.Addresses.SpyLevel.LevelSpy1, Client.Memory.Addresses.SpyLevel.LevelSpyDefault);
            Client.Memory.Writer.Bytes(Client.Memory.Addresses.SpyLevel.LevelSpy2, Client.Memory.Addresses.SpyLevel.LevelSpyDefault);
            Client.Memory.Writer.Bytes(Client.Memory.Addresses.SpyLevel.LevelSpy3, Client.Memory.Addresses.SpyLevel.LevelSpyDefault);
        }
        #endregion
    }
}