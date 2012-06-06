using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace prjXNAGame
{
    class Snake:theGame
    {
        sprite[] Border = new sprite[4];
        public void Edges ()
        {
            Border[0] = new sprite(this, "Side", 10, 800, 0, 0);
            Border[1] = new sprite(this, "Side", 800, 10, 0, 0);
            Border[2] = new sprite(this, "Side", 10, 800, 400, 0);
            Border[3] = new sprite(this, "side", 800, 10, 0, 400);

            /*for (int c = 0; c < Border.Length; c++)
            {
                Border[c].draw();
            }
             */

        }
        public override void gameIni()
        {
            base.gameIni();
            this.setScreenSize(400, 400);
            Edges();
        }
        public override void gameLogic()
        {
            base.gameLogic();
        }
        public override void gameRender()
        {
            base.gameRender();
            Edges();
            for (int c = 0; c < Border.Length; c++)
            {
                Border[c].draw();
            }
        }
    }
}
