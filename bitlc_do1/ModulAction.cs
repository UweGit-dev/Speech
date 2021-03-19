namespace bitlc_do1
{
    class ModulAction
    {
       
        public Univers univers;
        public ModulAction(Univers univers)
        {
            this.univers = univers;
            this.univers.modAct = this;
        }

        private ModulAction_Wiki modActionWiki = new ModulAction_Wiki();
        
        public void ActIn(string str)
        {
            if (str == null)
            {
                univers.modMainSynt.Sonjasay("das weiss ich nicht");
            }
            else if (str.IndexOf('*') == 0)
            {
                str = (str.Replace('*', ' '));
                str = (str.Replace('.', ' '));
                string x;
                x = modActionWiki.WikiClick(str);
                univers.modSynt.Zwoosay(x);
            }
            else if (str.IndexOf('+') == 0)
            {
                str = (str.Replace('+', ' '));
                switch (str)
                {
                    case "mach ich":
                        univers.form1.anweisung = "start";
                        univers.form1.FaceOpen();
                        break;
                    case "socket":
                        univers.form1.panelSocket.Visible = true;                        
                        break;
                }
            }
            else if(str.IndexOf('$') == 0)
            {
                str = (str.Replace('$', ' '));
            }
       
        }

        internal void Run()
        {

        }
    }
}
