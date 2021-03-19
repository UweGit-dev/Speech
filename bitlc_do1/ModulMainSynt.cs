using System;
using System.Speech.Synthesis;

namespace bitlc_do1
{
    class ModulMainSynt
    {
        // klasse an's univers anbinden
        public Univers univers;
        public ModulMainSynt(Univers univers)
        {
            this.univers = univers;
            this.univers.modMainSynt = this;
        }

        // objekt sprachausgabe
        SpeechSynthesizer speechSynt = new SpeechSynthesizer();

        // wird beim starten ausgeführt
        public void Run()
        {
            speechSynt.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);// stimme wird ausgesucht
            speechSynt.Rate = -1;//schnelligkeit der stimme
            speechSynt.Volume = 80;//lautstärke der stimme
            speechSynt.SpeakStarted += new EventHandler<SpeakStartedEventArgs>(Start_speak);
            speechSynt.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(Stop_speak);
            speechSynt.SpeakProgress += new EventHandler<SpeakProgressEventArgs>(Pro_speek);
        }
        private void Pro_speek(object sender, SpeakProgressEventArgs e)
        {
            univers.form1.FaceNext();
        }
        private void Stop_speak(object sender, SpeakCompletedEventArgs e)
        {
            univers.form1.sonja = false;
            univers.modRec.LoardGrammars();
        }
        private void Start_speak(object sender, SpeakStartedEventArgs e)
        {
            univers.form1.sonja = true;
            univers.modRec.uLoardGrammars();
        }
        private void Begrüsungaussage()
        {
            System.DateTime timenow = System.DateTime.Now;
            if (timenow.Hour >= 5 && timenow.Hour < 12)
            {
                speechSynt.SpeakAsync("guten morgen");
            }
            if (timenow.Hour >= 12 && timenow.Hour < 18)
            {
                speechSynt.SpeakAsync("guten tag");
            }
            if (timenow.Hour >= 18 && timenow.Hour < 24)
            {
                speechSynt.SpeakAsync("guten abend");
            }
            if (timenow.Hour >= 1 && timenow.Hour < 5)
            {
                speechSynt.SpeakAsync("Nacht Bonus Multi Gain");
            }
        }

        public void Sonjasay(string ssay)
        {
            speechSynt.SpeakAsync(ssay);// SystemSpeechSynt. ausgabe von string

            univers.form1.botout = ssay;
        }
    }
}
