using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyMusicPlayer.Model.Music;

namespace MyMusicPlayer.Model
{
    static class MusicManager
    {
        public static List<Music> audios;
        static MusicManager()
        {
            audios = new List<Music>();
            audios.Add(new Music("Say Goodnight", MusicCategory.Rock));
            audios.Add(new Music("Storybook", MusicCategory.Rock));
            audios.Add(new Music("Up North Classic", MusicCategory.Rock));

            audios.Add(new Music("Brain ID 1270", MusicCategory.Country));
            audios.Add(new Music("Squirrel Fever", MusicCategory.Country));
            audios.Add(new Music("The Meat Rack Jan 2016 LIVE", MusicCategory.Country));

            audios.Add(new Music("Lesser Faith", MusicCategory.Classical));
            audios.Add(new Music("Phase2", MusicCategory.Classical));

            audios.Add(new Music("inter1", MusicCategory.International));
            audios.Add(new Music("inter2", MusicCategory.International));
            audios.Add(new Music("inter3", MusicCategory.International));

            audios.Add(new Music("Shipping Lanes", MusicCategory.Others));
            audios.Add(new Music("The Stork", MusicCategory.Others));
            audios.Add(new Music("Towel Defence Sad Ending", MusicCategory.Others));
        }
        public static void GetAllSounds(ObservableCollection<Music> a)
        {
            audios.ForEach(s => a.Add(s));
        }

        public static void GetSoundsByCategory(ObservableCollection<Music> a, MusicCategory category)
        {
            var filteredSounds = audios.Where(s => s.Category == category).ToList();
            a.Clear();
            filteredSounds.ForEach(s => a.Add(s));
        }

        public static void UpdateMusic(ObservableCollection<Music> a, String s, MusicCategory b)
        {
            audios.Add(new Music(s, b));
            audios.ForEach(i => a.Add(i));
        }
    }
}
