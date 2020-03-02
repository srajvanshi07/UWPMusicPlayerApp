using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MyMusicPlayer.Model;
using static MyMusicPlayer.Model.Music;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MyMusicPlayer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayerPage : Page
    {
        private ObservableCollection<MusicCategory> categories;
        private ObservableCollection<Music> newmusic;
        private Music music;
        private MediaPlayer mediaPlayer;
        private string songPathUri;
        private string imageUri;
        private string categorySelected;

        public PlayerPage()
        {
            this.InitializeComponent();
            newmusic = new ObservableCollection<Music>();
            categories = new ObservableCollection<MusicCategory>();
            categories.Add(MusicCategory.Classical);
            categories.Add(MusicCategory.Country);
            categories.Add(MusicCategory.International);
            categories.Add(MusicCategory.Others);
            categories.Add(MusicCategory.Rock);

        }
        
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (drop_category.Text == "Rock")
            {
               MusicManager.UpdateMusic(newmusic, txtSongName.Text, MusicCategory.Rock);
            } 
            else if (drop_category.SelectedItem.ToString() == "Classical")
            {
                MusicManager.UpdateMusic(newmusic, txtSongName.Text, MusicCategory.Classical);
            }
            else if (drop_category.Text == "International")
            {
                MusicManager.UpdateMusic(newmusic, txtSongName.Text, MusicCategory.Rock);
            }
            else if (drop_category.Text == "Country")
            {
                MusicManager.UpdateMusic(newmusic, txtSongName.Text, MusicCategory.Classical);
            }
            else if (drop_category.Text == "Others")
            {
               MusicManager.UpdateMusic(newmusic, txtSongName.Text, MusicCategory.Classical);
            }
            this.Frame.Navigate(typeof(MainPage),newmusic);
        //    await Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder);

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSongName.Text))
            {
                txtSongName.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(txtImageName.Text))
            {
                txtImageName.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(drop_category.Text))
            {
                drop_category.Text = string.Empty;
            }
        }


        private void TxtFilePath_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                TextBox tbPath = sender as TextBox;

                if (tbPath != null)
                { 
                    LoadMediaFromString(tbPath.Text);
                }
            }
        }

        private void txtSongPathName_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                TextBox tbPath = sender as TextBox;

                if (tbPath != null)
                {
                    LoadMediaFromString(tbPath.Text);
                }
            }

        }
        private void LoadMediaFromString(string path)
        {
            try
            {
                mediaPlayer = new MediaPlayer();
                Uri pathUri = new Uri(path);
                mediaPlayer.Source = MediaSource.CreateFromUri(pathUri);
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    // handle exception.
                    // For example: Log error or notify user problem with file
                }
            }
        }

        async private void SongName_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.MusicLibrary;
            picker.FileTypeFilter.Add(".mp3");
            //string path = $"C:\\Repos\\UWPMusicPlayerApp\\MyMusicPlayer\\MyMusicPlayer\\Assets\\Music\\Rock";
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            StorageFolder destFolder = await StorageFolder.GetFolderFromPathAsync(root);
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file                
                this.txtSongName.Text =  file.Name;
             //   IStorageFolder folder = IStorageFolder($"/Assets/Music/{drop_category.Text}/");
     //          await file.CopyAsync(destFolder);
            }
            else
            {
                this.txtSongName.Text = "Operation cancelled.";
            }

        }

        async private void SongLocation_Click(object sender, RoutedEventArgs e)
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");
            Windows.Storage.StorageFolder folderPath = await folderPicker.PickSingleFolderAsync();
            if (folderPath != null)
            {
                // Application now has read/write access to the picked file
                this.txtSongPathName.Text = folderPath.Path;

            }
            else
            {
                this.txtSongPathName.Text = "Operation cancelled.";
            }

        }
        async private void ImageLocation_Click(object sender, RoutedEventArgs e)
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");

            Windows.Storage.StorageFolder folderPath = await folderPicker.PickSingleFolderAsync();
            if (folderPath != null)
            {
                // Application now has read/write access to the picked file
                this.txtImagePath.Text = folderPath.Path;
            }
            else
            {
                this.txtImagePath.Text = "Operation cancelled.";
            }
        }

        async private void ImageName_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                this.txtImageName.Text = file.Name;
            }
            else
            {
                this.txtImageName.Text = "Operation cancelled.";
            }
        }

    }
}
