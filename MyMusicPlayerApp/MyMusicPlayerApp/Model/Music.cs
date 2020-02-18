using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicPlayerApp.Model
{
    class Music
    {
        public string Name { get; set; }
        public MusicCategory Category { get; set; }
        public string AudioFile { get; set; }
        public string ImageFile { get; set; }
        public string IconFile { get; set; }

        public enum MusicCategory
        {
            Rock,
            Country,
            Classical,
            International,
            Others
        }

        public Music(string name, MusicCategory category)
        {
            Name = name;
            Category = category;
            AudioFile = $"/Assets/Music/{category}/{name}.mp3";
            ImageFile = $"/Assets/Images/{category}/{name}.jpeg";
            IconFile = $"/Assets/Icons/{category}/{name}.png";
        }
    }
}
