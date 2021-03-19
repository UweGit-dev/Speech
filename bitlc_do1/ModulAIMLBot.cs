using AIMLbot;

namespace bitlc_do1
{
    class ModulAIMLBot
    {
        // klasse an's univers anbinden
        public Univers univers;
        public ModulAIMLBot(Univers univers)
        {
            this.univers = univers;
            this.univers.modABot = this;
        }

        //bot einrichten
        private Bot sonjaBot;
        private User uwe;//user einrichten

        // wird beim start ausgeführt
        public void Run()
        {
            sonjaBot = new Bot();// objekt bot festlegen
            uwe = new User("uWeRockT", sonjaBot);// user objekt festlegen und zuortnen
            // landen von bot settings /aiml/confog/
            sonjaBot.loadSettings();
            sonjaBot.isAcceptingUserInput = false;
            sonjaBot.loadAIMLFromFiles();
            sonjaBot.isAcceptingUserInput = true;
        }
        public bool say = true;

        public void InBot(string inbot)
        {
            univers.form1.botin = inbot;//string übergabe an ModuleMainSynt|zeile99
            Request botRequest = new Request(inbot, uwe, sonjaBot);// bott füttern mit ausgabe von ModulRec
            Result botResult = sonjaBot.Chat(botRequest);//bot ausgabe
            string botcode = (botResult.Output);//botausgabe eine string zuortnen

            if (botcode.IndexOf('*') == 0)
            {
                univers.modAct.ActIn(botcode);
                say = false;//deaktiv setzen | ModulMainSynt_zeile31
            }
            if (botcode.IndexOf('+') == 0)
            {
                univers.modAct.ActIn(botcode);
                say = false;
            }

            if (inbot == null)
            {
                say = false;
            }          
            else if (say == true && inbot != null)//abfrage nach | ModulMainSynt_zeile31
            {
                
                univers.modMainSynt.Sonjasay(botcode);//ausgabe des trings ModulMainSynt| zeile33
            }
            else
            {
                //univers.modSound.Play_open();
            }
      
            //übergabe an string an die form
            say = true;// auf aktiv stellen | ModulMainSynt_zeile31

        }
    }
}
