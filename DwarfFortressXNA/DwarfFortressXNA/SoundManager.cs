using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace DwarfFortressXNA
{
    public class SoundManager
    {
        public Dictionary<string, Song> songList;
        public string currentSong = "NONE";
        public SoundManager()
        {
            songList = new Dictionary<string,Song>();
        }

        public void OnLoad(ContentManager content)
        {
            songList.Add("GAME_SONG", content.Load<Song>("song_game"));
            songList.Add("TITLE_SONG",content.Load<Song>("song_title"));
        }

        public void PlaySong(string songName)
        {
            StopCurrentSong();
            if (!songList.ContainsKey(songName)) throw new Exception("Bad song name requested: " + songName + "!");
            else MediaPlayer.Play(songList[songName]);
            this.currentSong = songName;
        }

        public void StopCurrentSong()
        {
            if(MediaPlayer.PlayPosition != TimeSpan.Zero) MediaPlayer.Stop();
            this.currentSong = "NONE";
        }



        
    }
}
