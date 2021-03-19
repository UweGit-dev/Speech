using System;
using System.Speech.Recognition;
using System.Data.SqlClient;
using System.Configuration;

namespace bitlc_do1
{
    class ModulRec
    {
        public Univers univers;
        public ModulRec(Univers univers)
        {
            this.univers = univers;
            this.univers.modRec = this;
        }

        SpeechRecognitionEngine speechRec = new SpeechRecognitionEngine();

        public void Run()
        {
            Grammer();
            speechRec.SetInputToDefaultAudioDevice();
            speechRec.RecognizeAsync(RecognizeMode.Multiple);

            speechRec.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Ausgabe_audioout);
        }

        private void Grammer()
        {
            //anbindungen an sql
            string constring = ConfigurationManager.ConnectionStrings["MyDataBase"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand sc = new SqlCommand();
            sc.Connection = con;
            sc.CommandText = "select * FROM MyTable";
            SqlDataReader sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                var Loadcmd = sdr["Commands"].ToString();
                Grammar commandgrammar = new Grammar(new GrammarBuilder(new Choices(Loadcmd)));
                speechRec.LoadGrammarAsync(commandgrammar);
                commandgrammar.Priority = 3;
                commandgrammar.Weight = 1f;
            }
            sdr.Close();
            con.Close();

            //aus einer text datei lesen
            Grammar xmlG = new Grammar(@"SpeakEck\XMLFile.xml");
            xmlG.Name = "SRGS File Command Grammar";
            speechRec.LoadGrammarAsync(xmlG);
            xmlG.Priority = 2;
            xmlG.Weight = 0.6f;

            //anbindungen intern Speech
            DictationGrammar dictation = new DictationGrammar();
            speechRec.LoadGrammar(dictation);
            dictation.Weight = 0.4f;
        }
        public void uLoardGrammars()
        {
            speechRec.UnloadAllGrammars();
            speechRec.RequestRecognizerUpdate();
        }
        public void LoardGrammars()
        {
            Grammer();
            speechRec.RequestRecognizerUpdate();
        }

        bool bot = true;
        private void Ausgabe_audioout(object sender, SpeechRecognizedEventArgs e)
        {
            // umwaldung vonSpeechRecognizedEventArgs in string
            string output = e.Result.Text;
            
            string constring = ConfigurationManager.ConnectionStrings["MyDataBase"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand sc = new SqlCommand();
            sc.Connection = con;
            sc.CommandText = "select * FROM MyTable";
            SqlDataReader sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                var commands = sdr["Commands"].ToString();
                var response = sdr["Response"].ToString();
                if (commands == output)
                {
                    univers.modABot.InBot(output);
                    bot = false;
                }
            }

            sdr.Close();
            con.Close();

            if (bot == false)
            {
                bot = true;
            }
            else
            {
                univers.modABot.InBot(output);
            }
           
        }
    }
}
