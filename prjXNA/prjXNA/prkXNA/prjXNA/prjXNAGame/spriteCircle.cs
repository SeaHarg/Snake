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
    class spriteCircle:sprite
    {
       
        public spriteCircle(theGame reference, String contentAssetName, String identity, int width, int height, int X, int Y)
            : base(reference, contentAssetName,  width, height, X, Y)
        {

            asset = reference.Content.Load<Texture2D>(contentAssetName);
            spriteOrigin = new Vector2(asset.Width / 2f, asset.Height / 2f);


            this.X = X;
            this.Y = Y;
            this.width = width;
            this.height = height;


            rectangle = new Rectangle((int)this.X, (int)this.Y, (int)width, (int)height);
            this.identity = identity;
            this.reference = reference;

            rectBody = BodyFactory.Instance.CreateEllipseBody (reference.ps, (width/2f), (height/2f), 1);//PHYSICS
            rectBody.Position = new FarseerGames.FarseerPhysics.Mathematics.Vector2(this.X, this.Y);//PHYSICS
            rectGeom = GeomFactory.Instance.CreateEllipseGeom (reference.ps, rectBody, (width/2f), (height/2f),20);//PHYSICS
            rectGeom.SetBody(rectBody);
            rectGeom.CollisionEnabled = true;
            this.rectGeom.CollisionResponseEnabled = true;
            this.rectGeom.CollisionCategories = CollisionCategory.All;
            this.rectGeom.CollidesWith = CollisionCategory.All;

            rectBody.Enabled = true;//PHYSICS
            rectGeom.OnSeparation += OnSeperation;
            rectGeom.OnCollision += OnCollision;


            reference.ps.BroadPhaseCollider.OnBroadPhaseCollision += OnBroadPhaseCollision;
            reference.ps.Add(rectBody);
            reference.ps.Add(rectGeom);
        }

        private bool OnCollision(Geom geom1, Geom geom2, ContactList contactList)
        {
            return true;
        }



        private void OnSeperation(Geom geom1, Geom geom2)
        {

        }



        private bool OnBroadPhaseCollision(Geom geom1, Geom geom2)
        {

            return true;
        }

    }
}
