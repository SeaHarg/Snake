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
    class spriteRect:sprite
    {

        public spriteRect(theGame reference, String contentAssetName, String identity, int width, int height, int X, int Y)
            : base(reference, contentAssetName, width, height, X, Y)
        {
            asset = reference.Content.Load<Texture2D>(contentAssetName);
            
            this.X = (int)(X);
            this.Y = (int)(Y);
            this.width = width;
            this.height = height;
            rectangle = new Rectangle((int)this.X, (int)this.Y, width, height);
            this.identity = identity;
            this.reference = reference;

            rectBody = BodyFactory.Instance.CreateRectangleBody(reference.ps, width, height, 1);//PHYSICS
            rectGeom = GeomFactory.Instance.CreateRectangleGeom(rectBody, this.width, this.height);//PHYSICS
            
            rectGeom.CollisionEnabled = true;
            this.rectGeom.CollisionResponseEnabled = true;
            //this.rectGeom.CollisionCategories = CollisionCategory.Cat1;
            //this.rectGeom.CollidesWith = CollisionCategory.All;
            this.rectGeom.CollidesWith = CollisionCategory.All;
            rectBody.Position = new FarseerGames.FarseerPhysics.Mathematics.Vector2(this.X, this.Y);//PHYSICS
            rectBody.Enabled = true;//PHYSICS
            reference.ps.Add(rectBody);
            reference.ps.Add(rectGeom);
        }

    }
}
