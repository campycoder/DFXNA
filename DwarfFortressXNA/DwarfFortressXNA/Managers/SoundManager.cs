﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace DwarfFortressXNA.Managers
{
    public class SoundManager
    {
        public Dictionary<string, Song> SongList;
        public string CurrentSong = "NONE";
        public bool SoundEnabled = true;
        public SoundManager(bool sound)
        {
            SongList = new Dictionary<string,Song>();
            SoundEnabled = sound;
        }

        public void OnLoad(ContentManager content)
        {
            SongList.Add("GAME_SONG", content.Load<Song>("song_game"));
            SongList.Add("TITLE_SONG",content.Load<Song>("song_title"));
        }

        public void PlaySong(string songName)
        {
            if (!SoundEnabled) return;
            StopCurrentSong();
            if (!SongList.ContainsKey(songName)) throw new Exception("Bad song name requested: " + songName + "!");
            MediaPlayer.Play(SongList[songName]);
            CurrentSong = songName;
        }

        public void StopCurrentSong()
        {
            if(MediaPlayer.PlayPosition != TimeSpan.Zero) MediaPlayer.Stop();
            CurrentSong = "NONE";
        }



        
    }
}
