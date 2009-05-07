using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Tibia.Client;

namespace KTibiaX.Cam.Movie {
    [Serializable]
    public class MovieFile {

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieFile"/> class.
        /// </summary>
        public MovieFile() {
        }

        #region "[rgn] Properties "
        [XmlElement]
        public DateTime Data { get; set; }

        [XmlAttribute]
        public string Version { get; set; }

        [XmlArray]
        public List<MovieChapter> Chapters { get; set; }
        #endregion

        /// <summary>
        /// Adds the chapter.
        /// </summary>
        /// <param name="client">The client.</param>
        public void AddChapter(TibiaClient client) {
            if (Chapters == null) { Chapters = new List<MovieChapter>(); }
            var chapter = new MovieChapter();
            chapter.KeepChapter(client);
            Chapters.Add(chapter);
        }
    }
}
