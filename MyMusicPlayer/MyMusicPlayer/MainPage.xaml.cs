using MyMusicPlayer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static MyMusicPlayer.Model.Music;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyMusicPlayer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Music> audios;
        private List<MenuItem> menuItems;
        private MediaPlayer mediaPlayer;
        public MainPage()
        {
            this.InitializeComponent();
            audios = new ObservableCollection<Music>();
            MusicManager.GetAllSounds(audios);
            menuItems = new List<MenuItem>();
            // Load Pane
            menuItems.Add(new MenuItem
            {
                IconFile = "Assets/Icons/Rock.png",
                Category = MusicCategory.Rock
            });
            menuItems.Add(new MenuItem
            {
                IconFile = "Assets/Icons/Classical.png",
                Category = MusicCategory.Classical
            });
            menuItems.Add(new MenuItem
            {
                IconFile = "Assets/Icons/International.png",
                Category = MusicCategory.International
            });
            menuItems.Add(new MenuItem
            {
                IconFile = "Assets/Icons/Country.png",
                Category = MusicCategory.Country
            });
            menuItems.Add(new MenuItem
            {
                IconFile = "Assets/Icons/Others.png",
                Category = MusicCategory.Others
            });
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MusicManager.GetAllSounds(audios);
            CategoryTextBlock.Text = "All Music";
            MenuItemsListView.SelectedItem = null;
            BackButton.Visibility = Visibility.Collapsed;
        }

        private void MusicGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var music = (Music)e.ClickedItem;
            MyMediaElement.Source = MediaSource.CreateFromUri(new Uri(this.BaseUri, music.AudioFile));
        }

        private void MenuItemsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = (MenuItem)e.ClickedItem;
            CategoryTextBlock.Text = menuItem.Category.ToString();
            MusicManager.GetSoundsByCategory(audios, menuItem.Category);
            BackButton.Visibility = Visibility.Visible;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer = new MediaPlayer();
            mediaPlayer = MyMediaElement.MediaPlayer;
            mediaPlayer.Pause();
            this.Frame.Navigate(typeof(PlayerPage));        
        }
    }
}

