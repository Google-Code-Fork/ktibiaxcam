using System.Collections.Generic;

namespace Tibia.Features.Model {
    public class CreatureCollection : List<Creature> {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatureCollection"/> class.
        /// </summary>
        public CreatureCollection() { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatureCollection"/> class.
        /// </summary>
        /// <param name="list">The list.</param>
        public CreatureCollection(List<Creature> list) {
            base.AddRange(list);
        }

        /// <summary>
        /// Gets the <see cref="Tibia.Features.Model.Creature"/> with the specified name.
        /// </summary>
        /// <value></value>
        public Creature this[string name] {
            get {
                for (int i = 0; i < Count; i++) { if (this[i].Name == name) { return this[i]; } }
                return null;
            }
        }

        /// <summary>
        /// Gets the <see cref="Tibia.Features.Model.Creature"/> with the specified name.
        /// </summary>
        /// <value></value>
        public Creature this[string name, bool live] {
            get {
                foreach (var creature in this) {
                    if (creature.Name.ToLower() == name) {
                        if (live && creature.HpBar > 0) {
                            return creature;
                        }
                        else { return creature; }
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Sort this Collection Basead on Name.
        /// </summary>
        public void Sort(Structures.SortDirection direction) {
            switch (direction) {
                case Structures.SortDirection.Ascending: Sort(delegate(Creature R1, Creature R2) { return R1.Name.CompareTo(R2.Name); }); break;
                case Structures.SortDirection.Descending: Sort(delegate(Creature R1, Creature R2) { return R2.Name.CompareTo(R1.Name); }); break;
            }
        }

        /// <summary>
        /// Sort this Collection Basead on Distance from Player.
        /// </summary>
        public void Sort(Structures.SortDirection direction, Player player) {
            switch (direction) {
                case Structures.SortDirection.Ascending: Sort(delegate(Creature R1, Creature R2) { return R1.Distance(player).XY.CompareTo(R2.Distance(player).XY); }); break;
                case Structures.SortDirection.Descending: Sort(delegate(Creature R1, Creature R2) { return R2.Distance(player).XY.CompareTo(R1.Distance(player).XY); }); break;
            }
        }

        /// <summary>
        /// Search the Defined Creature by Id.
        /// </summary>
        public Creature Search(uint id) {
            for (int i = 0; i < Count; i++) {
                if (this[i].Id == id) { return this[i]; }
            }
            return null;
        }

        /// <summary>
        /// Search the Defined Creature by Name.
        /// </summary>
        public Creature Search(string name) {
            for (int i = 0; i < Count; i++) {
                if (this[i].Name.ToLower() == name.ToLower()) { return this[i]; }
            }
            return null;
        }

        /// <summary>
        /// Return Count: {0}.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return string.Format("Count: {0}", Count);
        }
    }
}
