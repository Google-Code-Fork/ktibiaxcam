using Tibia.Connection;
using Tibia.Memory;
using System.Collections.Generic;
using Tibia.Connection.Providers;

namespace Tibia.Features.Model.List {
	/// <summary>
	/// Vip List.
	/// </summary>
	public class VipList {
        /// <summary>
        /// Initializes a new instance of the <see cref="VipList"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
		public VipList(ConnectionProvider connection) {
			Connection = connection;
			Initialize();
		}

		#region " Object Properties "
        public ConnectionProvider Connection { get; private set; }
        public TibiaMemoryProvider Memory { get { return Connection.Memory; } }        
        public List<VipUser> AllUsers { get; private set; }
        public List<VipUser> OnlineUsers { get; private set; }
        public List<VipUser> OfflineUsers { get; private set; }
    	#endregion

		/// <summary>
		/// Initialize Vip List.
		/// </summary>
		public void Initialize() {
			AllUsers = GetUsers(null);
		}

		/// <summary>
		/// Get OnLine Users on VipList.
		/// </summary>
		private void GetOnlineUsers() {
			OnlineUsers = GetUsers(true);
		}

		/// <summary>
		/// Get OffLine Users on VipList.
		/// </summary>
		private void GetOffLineUsers() {
			OfflineUsers = GetUsers(false);
		}

        /// <summary>
        /// Get Users on VipList.
        /// </summary>
        /// <param name="online">If online.</param>
        /// <returns></returns>
        private List<VipUser> GetUsers(bool? online) {
            List<VipUser> users = new List<VipUser>();
            uint begin = Memory.Addresses.VipList.VipBegin;
            uint distc = Memory.Addresses.VipList.VipDist;

            for (uint i = 0; i < Memory.Addresses.VipList.VipMaxD; i++) {
                VipUser user = new VipUser(Memory, begin + (i * distc));
                if (user.Id > 0) {
                    if (online.HasValue) {
                        if (online.Value && user.IsOnline) { users.Add(user); }
                        else if (!online.Value && !user.IsOnline) { users.Add(user); }
                    }
                    else { users.Add(user); }
                }
                else { break; }
            }
            return users;
        }
	}
}
