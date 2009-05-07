using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace KTibiaX.Cam.Movie {
    [Serializable]
    public class ChapterPacket {

        [XmlAttribute]
        public DateTime Time { get; set; }

        [XmlAttribute]
        public byte[] Data { get; set; }
    }
}
