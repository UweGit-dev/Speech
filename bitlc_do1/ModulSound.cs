using System.Media;

namespace bitlc_do1
{
    class ModulSound
    {
        public Univers univers;
        public ModulSound(Univers univers)
        {
            this.univers = univers;
            this.univers.modSound = this;
        }
        public SoundPlayer sPlay;
        internal void Run()
        {
            Play_open();
        }
        public void Play_dic()
        {
            sPlay = new SoundPlayer(@"data_mp3\dictat.wav");
            sPlay.Play();
        }
        public void Play_open()
        {
            sPlay = new SoundPlayer(@"data_mp3\OpenMedai.wav");
            sPlay.Play();
        }
        public void Play_auf()
        {
            sPlay = new SoundPlayer(@"data_mp3\data_mp3\OpenWeb.wav");
            sPlay.Play();
        }
        public void Play_zu()
        {
            sPlay = new SoundPlayer(@"data_mp3\PanelAus.wav");
            sPlay.Play();
        }
    }
}
