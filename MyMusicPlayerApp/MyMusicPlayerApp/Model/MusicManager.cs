using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyMusicPlayerApp.Model.Music;

namespace MyMusicPlayerApp.Model
{
    class MusicManager
    {
        private static List<Music> getAudios()
        {
            var audios = new List<Music>();
            audios.Add(new Music("Say Goodnight", MusicCategory.Rock));
            audios.Add(new Music("Storybook", MusicCategory.Rock));
            audios.Add(new Music("Up North Classic", MusicCategory.Rock));

            audios.Add(new Music("Brain ID 1270", MusicCategory.Country));
            audios.Add(new Music("Squirrel Fever", MusicCategory.Country));
            audios.Add(new Music("The Meat Rack Jan 2016 LIVE", MusicCategory.Country));

            audios.Add(new Music("Downfall", MusicCategory.Classical));
            audios.Add(new Music("Lesser Faith", MusicCategory.Classical));
            audios.Add(new Music("Phase2", MusicCategory.Classical));

            audios.Add(new Music("inter1", MusicCategory.International));
            audios.Add(new Music("inter2", MusicCategory.International));
            audios.Add(new Music("inter3", MusicCategory.International));

            audios.Add(new Music("Shipping Lanes", MusicCategory.Others));
            audios.Add(new Music("The Stork", MusicCategory.Others));
            audios.Add(new Music("Towel Defence Sad Ending", MusicCategory.Others));


            return audios;
        }

        public static void GetAllSounds(ObservableCollection<Music> audios)
        {
            var allMusic = getAudios();
            audios.Clear();
            allMusic.ForEach(s => audios.Add(s));
        }

        public static void GetSoundsByCategory(ObservableCollection<Music> audios, MusicCategory category)
        {
            var allMusic = getAudios();
            var filteredSounds = allMusic.Where(s => s.Category == category).ToList();
            audios.Clear();
            filteredSounds.ForEach(s => audios.Add(s));
        }
    }
}
