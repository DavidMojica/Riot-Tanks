using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace RiotTanks_1._1
{
    class Audio
    {
        SoundPlayer sound;
        public Audio() { }
        public void SeleccionarAudio(int audio)
        {
            switch (audio)
            {
                case 1:
                    sound = new SoundPlayer(Properties.Resources.shot);
                    break;
                case 2:
                    sound = new SoundPlayer(Properties.Resources.player_shot);
                    break;
                case 3:
                    sound = new SoundPlayer(Properties.Resources.player_respawn);
                    break;
                case 4:
                    sound = new SoundPlayer(Properties.Resources.impact);
                    break;
                case 5:
                    sound = new SoundPlayer(Properties.Resources.tank_impact);
                    break;
                case 6:
                    sound = new SoundPlayer(Properties.Resources.armored_impact);
                    break;
                case 7:
                    sound = new SoundPlayer(Properties.Resources.armored_impact);
                    break;
                case 8:
                    sound = new SoundPlayer(Properties.Resources.tank_destruction);
                    break;
                case 9:
                    sound = new SoundPlayer(Properties.Resources.Battle_City_SFX__11_);
                    break;
                case 10:
                    sound = new SoundPlayer(Properties.Resources.level_done);
                    break;
                default:
                    break;
            }
        }
        public void Reproducir()
        {
            sound.Play();
        }
    }
}