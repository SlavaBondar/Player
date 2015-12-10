using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;


namespace PlayerAudio
{
     public partial class MainWindow : Window
    {
         private MediaPlayer audioPlayer = new MediaPlayer();
        public MainWindow()
        {
            InitializeComponent();
                        DispatcherTimer timer = new DispatcherTimer();
                        timer.Interval = TimeSpan.FromSeconds(1);
                        timer.Tick += timer_Tick;
                        timer.Start();
        }

        private void btnPlay(object sender, RoutedEventArgs e)
        {
            Track tempTrask = null;

            if (lstbTrackList.SelectedIndex != -1)
            {
                tempTrask = PlayList.GetTrack(lstbTrackList.SelectedItem.ToString());
                if (tempTrask == null)
                {
                    MessageBox.Show("Помилка відкриття", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    audioPlayer.Open(new Uri(tempTrask.TrackPath));
                    audioPlayer.Play();
                }
            }
            else
            {
                MessageBox.Show("Виберіть файл", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnStop(object sender, RoutedEventArgs e)
        {
            audioPlayer.Stop();
        }

        private void btnPause(object sender, RoutedEventArgs e)
        {
            audioPlayer.Pause();
        }

        private void btnOpenFiles(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                //audioPlayer.Open(new Uri(openFileDialog.FileName));
                //audioPlayer.Play();
                Track tempSound = new Track();
                tempSound.TrackPath = openFileDialog.FileName;
                tempSound.TrackName = openFileDialog.FileName.Split('\\')[openFileDialog.FileName.Split('\\').Length-1];

                MessageBox.Show("Файл \"" + tempSound.TrackName + "\" був добавлений до плейлису.", "Операція успішна", MessageBoxButton.OK, MessageBoxImage.Information);

                PlayList.AddSoundToList(tempSound);

                lstbTrackList.Items.Clear();

                foreach (Track track in PlayList.GetPlayList())
                {
                    lstbTrackList.Items.Add(track.TrackName);
                }
            }

        }
             void timer_Tick(object sender, EventArgs e)
                {
                    if (audioPlayer.HasAudio)                
                        lblStatus.Content = string.Format("{0} / {1}", audioPlayer.Position.ToString(@"mm\:ss"), audioPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                    else
                        lblStatus.Content = "No file selected...";
                }

             private void Help(object sender, RoutedEventArgs e)
             {
                 HelpWPFxaml t = new HelpWPFxaml();
                 t.ShowDialog();

             }

        }
    }

