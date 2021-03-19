using System.Speech.Synthesis;

namespace bitlc_do1
{
    class ModulSynt
    {
        public Univers univers;
        public ModulSynt(Univers univers)
        {
            this.univers = univers;
            this.univers.modSynt = this;
        }
        // später zumvorlesen und fehler medlungen
        SpeechSynthesizer speechzwoo = new SpeechSynthesizer();

        public void Run()
        {
            speechzwoo.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior);
            speechzwoo.Rate = -1;
            speechzwoo.Volume = 80;
        }

        public void speechzwoosteuerung (int play)
        {
            if (play == 2)
            {
                speechzwoo.Dispose();
            }
            else
            {
                speechzwoo.Pause();
            }
        }
        public bool sayzwoo = true;
        public void Zwoosay(string ssay)
        {

            // hier muss auch was rein !switsch!
            if (sayzwoo == true)
            {
                speechzwoo.SpeakAsync(ssay);
            }
            else
            {









                // hier muss noch was rein !drow!
            }
        }
    }
}
