using System.Drawing;
using Tibia.Connection;
using Tibia.Connection.Providers;
using Tibia.Features.Actions.Pipes;
using Tibia.Features.Structures;

namespace Tibia.Features.Actions.Messages {
    public class Screen {

        /// <summary>
        /// Initializes a new instance of the <see cref="Screen"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public Screen(ConnectionProvider connection) {
            Connection = connection;
        }

        #region "[rgn] Object Properties "
        public ConnectionProvider Connection { get; set; }
        #endregion

        /// <summary>
        /// Draws the screen text.
        /// </summary>
        /// <param name="TextName">Name of the text.</param>
        /// <param name="loc">The loc.</param>
        /// <param name="color">The color.</param>
        /// <param name="font">The font.</param>
        /// <param name="text">The text.</param>
        public void DrawScreenText(string TextName, Location loc, Color color, ClientFont font, string text) {
            Connection.Pipe.Send(DisplayTextPacket.Create(Connection, TextName, loc, color, font, text));
        }

        /// <summary>
        /// Removes the screen text.
        /// </summary>
        /// <param name="TextName">Name of the text.</param>
        public void RemoveScreenText(string TextName) {
            Connection.Pipe.Send(RemoveTextPacket.Create(Connection, TextName));
        }

        /// <summary>
        /// Removes all screen text.
        /// </summary>
        public void RemoveAllScreenText() {
            Connection.Pipe.Send(RemoveAllTextPacket.Create(Connection));
        }

        /// <summary>
        /// Draws the creature text.
        /// </summary>
        /// <param name="creatureName">Name of the creature.</param>
        /// <param name="loc">The loc.</param>
        /// <param name="color">The color.</param>
        /// <param name="font">The font.</param>
        /// <param name="text">The text.</param>
        public void DrawCreatureText(string creatureName, Location loc, Color color, ClientFont font, string text) {
            Connection.Pipe.Send(DisplayCreatureTextPacket.Create(Connection, 0, creatureName, loc, color, font, text));
        }

        /// <summary>
        /// Draws the creature text.
        /// </summary>
        /// <param name="CreatureID">The creature ID.</param>
        /// <param name="loc">The loc.</param>
        /// <param name="color">The color.</param>
        /// <param name="font">The font.</param>
        /// <param name="Text">The text.</param>
        public void DrawCreatureText(int CreatureID, Location loc, Color color, ClientFont font, string Text) {
            Connection.Pipe.Send(DisplayCreatureTextPacket.Create(Connection, CreatureID, "MyChar", loc, color, font, Text));
        }

        /// <summary>
        /// Updates the creature text.
        /// </summary>
        /// <param name="CreatureName">Name of the creature.</param>
        /// <param name="loc">The loc.</param>
        /// <param name="NewText">The new text.</param>
        public void UpdateCreatureText(string CreatureName, Location loc, string NewText) {
            Connection.Pipe.Send(UpdateCreatureTextPacket.Create(Connection, 0, CreatureName, loc, NewText));
        }

        /// <summary>
        /// Updates the creature text.
        /// </summary>
        /// <param name="CreatureID">The creature ID.</param>
        /// <param name="loc">The loc.</param>
        /// <param name="NewText">The new text.</param>
        public void UpdateCreatureText(int CreatureID, Location loc, string NewText) {
            Connection.Pipe.Send(UpdateCreatureTextPacket.Create(Connection, CreatureID, "", loc, NewText));
        }

        /// <summary>
        /// Removes the creature text.
        /// </summary>
        /// <param name="CreatureName">Name of the creature.</param>
        public void RemoveCreatureText(string CreatureName) {
            Connection.Pipe.Send(RemoveCreatureTextPacket.Create(Connection, 0, CreatureName));
        }

        /// <summary>
        /// Removes the creature text.
        /// </summary>
        /// <param name="CreatureID">The creature ID.</param>
        public void RemoveCreatureText(int CreatureID) {
            Connection.Pipe.Send(RemoveCreatureTextPacket.Create(Connection, CreatureID, ""));
        }

    }
}
