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


using FarseerGames.FarseerPhysics;//PHYSICS
using FarseerGames.FarseerPhysics.Collisions;//PHYSICS
using FarseerGames.FarseerPhysics.Dynamics;//PHYSICS
using FarseerGames.FarseerPhysics.Dynamics.Springs;//PHYSICS
using FarseerGames.FarseerPhysics.Factories;//PHYSICS


namespace prjXNAGame
{
    class spriteOval:sprite
    {

        public spriteOval(theGame reference, String contentAssetName, String identity, int width, int height, int X, int Y)
            : base(reference, contentAssetName, width, height, X, Y)
        {
            rectBody = BodyFactory.Instance.CreateEllipseBody (reference.ps, width/2f,height/2f, 1);//PHYSICS
            rectGeom = GeomFactory.Instance.CreateEllipseGeom  (rectBody, width/2f,height/2f,20);//PHYSICS

            rectGeom.CollisionEnabled = true;
            this.rectGeom.CollisionResponseEnabled = true;
            this.rectGeom.CollidesWith = CollisionCategory.All;
            //this.rectGeom.CollisionCategories = CollisionCategory.Cat1;


            //this.rectGeom.CollidesWith = CollisionCategory.All;
            rectBody.Position = new FarseerGames.FarseerPhysics.Mathematics.Vector2(this.X, this.Y);//PHYSICS
            rectBody.Enabled = true;//PHYSICS
            reference.ps.Add(rectBody);
            reference.ps.Add(rectGeom);
        }

    }
}
