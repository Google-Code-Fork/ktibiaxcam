using System;
using System.Collections.Generic;
using System.Diagnostics;
using KTibiaX.Cam.Events;
using Tibia.Client;
using Tibia.Memory;

namespace KTibiaX.Cam.Dialogs {
    public partial class frm_Clients : DevExpress.XtraEditors.XtraForm {
        public frm_Clients() {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the frm_Clients control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_Clients_Load(object sender, EventArgs e) {
            RefreshProcessess();
        }

        #region "[rgn] Properties    "
        protected List<ProcessDTO> ProcessSource { get; set; }
        public Process SelectedProcess { get { return lstProcess.SelectedIndex > -1 ? ProcessSource[lstProcess.SelectedIndex].Process : null; } }
        public ProcessDTO SelectedItem { get { return lstProcess.SelectedIndex > -1 ? ProcessSource[lstProcess.SelectedIndex] : null; } }
        public event EventHandler<ProcessEventArgs> OnClientChoosed;
        #endregion

        #region "[rgn] Event Handler "
        private void btnSelect_Click(object sender, EventArgs e) {
            if (SelectedProcess != null && OnClientChoosed != null) {
                OnClientChoosed(this, new ProcessEventArgs(SelectedProcess));
            }
            Close();
        }
        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }
        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            RefreshProcessess();
        }
        #endregion

        #region "[rgn] Scructures    "
        public class ProcessDTO {

            /// <summary>
            /// Initializes a new instance of the <see cref="ProcessDTO"/> class.
            /// </summary>
            public ProcessDTO() { }

            /// <summary>
            /// Initializes a new instance of the <see cref="ProcessDTO"/> class.
            /// </summary>
            /// <param name="process">The process.</param>
            /// <param name="imageIndex">Index of the image.</param>
            public ProcessDTO(Process process, int imageIndex) {
                this.Process = process;
                this.ProcessName = process.ProcessName;
                this.ImageIndex = imageIndex;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="ProcessDTO"/> class.
            /// </summary>
            /// <param name="process">The process.</param>
            /// <param name="name">The name.</param>
            /// <param name="imageIndex">Index of the image.</param>
            public ProcessDTO(Process process, string name, int imageIndex) {
                this.Process = process;
                this.ProcessName = name;
                this.ImageIndex = imageIndex;
            }

            #region "[rgn] Properties "
            public Process Process { get; set; }
            public int ImageIndex { get; set; }
            public string ProcessName { get; set; }
            public bool Allowed { get; set; }
            public IntPtr ProcessHandle { get { return Process.Handle; } }
            #endregion

            /// <summary>
            /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
            /// </returns>
            public override string ToString() {
                return Process != null ? ProcessName : base.ToString();
            }
        }
        #endregion
        
        /// <summary>
        /// Loads the processess.
        /// </summary>
        protected void RefreshProcessess() {
            ProcessSource = new List<ProcessDTO>();
            var procs = Process.GetProcesses();

            foreach (var proc in procs) {
                if (proc.ProcessName == "Tibia") {

                    var memory = AddressProvider.GetAddress(proc);
                    if (memory != null) {
                        var client = new TibiaClient(proc);
                        if (client.IsConnected) {
                            ProcessSource.Add(new ProcessDTO(proc, client.Features.Player.Name, 4) { Allowed = true });
                        }
                        else { ProcessSource.Add(new ProcessDTO(proc, "Offline Client", 3)); }
                    }
                    else { ProcessSource.Add(new ProcessDTO(proc, "Not Supported Version", 3)); }
                }
            }
            lstProcess.DataSource = ProcessSource;
        }

    }
}