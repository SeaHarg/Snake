using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace prjXNAGame
{
     class text
    {
        public SpriteFont font;
        private theGame game;
        public String data;

        public text(theGame game,String fontName,String data)
        {
            this.data = data;
            this.game = game;
            font = game.Content.Load<SpriteFont>(fontName);

        }

        public void draw(int x, int y,Color theColour)
        {
            game.screen.DrawString(this.font,data,new Vector2 (x,y), theColour);

        }

    }
}
