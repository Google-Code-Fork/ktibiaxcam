using System;
using System.Diagnostics;
using Tibia.Memory;
using Tibia.Connection;
using Tibia.Features.Providers;
using Tibia.Connection.Providers;

namespace Tibia.Client.Contracts {
    /// <summary>
    /// Interface used to access the inner properties of the ClientControl Base.
    /// </summary>
    public interface IClient {

        Process Process { get; }
        IntPtr hProcess { get; }
        IntPtr Handle { get; }
        IntPtr MainWindowHandle { get; }

        string Tile { get; }
        bool IsConnected { get; }

        void OpenProcess();
        void CloseHandle();
        void SetActiveWindow();
        void SetWindowText(string Text);

        TibiaMemoryProvider Memory { get; }
        ConnectionProvider Connection { get; }
        ActionProvider Actions { get; }
    }
}
