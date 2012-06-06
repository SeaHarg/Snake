using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace prjXNAGame
{
    class myPong:theGame 
    {
        text test2;

        text test;
        String key;
        string theText="";
        String previousKey;
        Boolean keysPressed;
        public override void gameIni()
        {
            base.gameIni();
            test = new text(this, "pongText", theText);
        }


        
        public override void gameLogic()
        {
            base.gameLogic();
            
            this.test.changeText(this.getInputtedText ());
           

           
        }
        public override void gameRender()
        {
            test.draw (100,100,Microsoft.Xna.Framework.Graphics.Color.Wheat);
            base.gameRender();
        }

    }
}
