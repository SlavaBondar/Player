using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerAudio
{
    static public class PlayList
    {
        static private List<Track> list = new List<Track>();
        static public void AddSoundToList(Track addingsound)
        {
            list.Add(addingsound);
        }
        static public List<Track> GetPlayList()
        {
            return list;
        }
        static public Track GetTrack(string TrackName)
        {
            Track result = null;
            foreach (Track item in list)
            {
                if (item.TrackName == TrackName)
                {
                    result = item;
                }
            }
            return result;
        }
    }
}
