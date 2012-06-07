using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace prjXNAGame
{
    class Snake:theGame
    {
        sprite[] Border = new sprite[4];
        List<sprite> Body = new List<sprite>();
        sprite BodySprite;

        public void EdgesIni ()
        {
            Border[0] = new sprite(this, "Side", 10, 800, 0, 0);
            Border[1] = new sprite(this, "Side", 800, 10, 0, 0);
            Border[2] = new sprite(this, "Side", 10, 800, 400, 0);
            Border[3] = new sprite(this, "side", 800, 50, 0, 400);

        }
        public void UserIni()
        {
            BodySprite = new sprite(this, "body", 10, 10, 200, 200);
            BodySprite.setVelocity(0, -10);
        }
        public void UserLogic()
        {
            if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
                BodySprite.setVelocity(0, -10);
            else if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
                BodySprite.setVelocity(0, 10);
            else if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
                BodySprite.setVelocity(10, 0);
            else if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
                BodySprite.setVelocity(-10, 0);
        }

        public override void gameIni()
        {
            base.gameIni();
            this.setScreenSize(400, 400);
            this.setScreenColour(Microsoft.Xna.Framework.Graphics.Color.Green);
            EdgesIni();
            UserIni();
        }
        public override void gameLogic()
        {
            base.gameLogic();
            UserLogic();
        }
        public override void gameRender()
        {
            base.gameRender();
            EdgesIni();
            for (int c = 0; c < Border.Length; c++)
            {
                Border[c].draw();
            }
            BodySprite.draw();
            
        }
    }
}
