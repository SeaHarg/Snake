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


//using FarseerGames.FarseerPhysics;
//using FarseerGames.FarseerPhysics.Dynamics;
//using FarseerGames.FarseerPhysics.Factories;
//using FarseerGames.FarseerPhysics.Mathematics;

namespace prjXNAGame
{
    public class sprite : Microsoft.Xna.Framework.Game
    {
        public float X;
        public float Y;
        public int width, height;
        public Texture2D asset;
        //public Vector2 position;
        public Rectangle rectangle;
        public string identity;
        public theGame reference;
       // private Body _rectangleBody;
        //private Vector2 _origin;
       

        public sprite(theGame reference, String contentAssetName, String identity, int width, int height, int X, int Y)
        {
            asset = reference.Content.Load<Texture2D>(contentAssetName);
            //position = new Vector2(X, Y);
            this.X = (int)(X);
            this.Y = (int)(Y);
            this.width = width;
            this.height = height;
            rectangle = new Rectangle((int)this.X, (int)this.Y, width, height);
            this.identity = identity;
            this.reference = reference;
        //    _rectangleBody = BodyFactory.Instance.CreateRectangleBody(new PhysicsSimulator(new FarseerGames.FarseerPhysics.Mathematics.Vector2(0, 0)),width, height, 1);
         //   _rectangleBody.Position = new FarseerGames.FarseerPhysics.Mathematics.Vector2(this.X, this.Y);
        //    _origin = new Vector2(width / 2f, height / 2f);


        }
        public sprite(theGame reference, int width, int height, int X, int Y, String fileName, String identity)
        {

            asset = Texture2D.FromFile(reference.graphics.GraphicsDevice, fileName, width, height); //aLoader.Load<Texture2D>("frog") as Texture2D;
            this.X = (int)(X);
            this.Y = (int)(Y);
            this.width = width;
            this.height = height;
            rectangle = new Rectangle((int)this.X, (int)this.Y, width, height);
            this.identity = identity;
            this.reference = reference;

        }
        public void draw()
        {
            reference.screen.Draw(asset, rectangle, reference.white);
           // reference.screen.Draw(asset,new Rectangle (Convert.ToInt32  (_rectangleBody.Position.X),Convert.ToInt32(_rectangleBody.Position.Y),this.width,this.height) , null, Color.White, _rectangleBody.Rotation, _origin,  SpriteEffects.None, 0);
        }
        public void move(float dx, float dy)
        {
            this.X += dx;
            this.Y += dy;
            this.rectangle = new Rectangle((int)this.X, (int)this.Y, this.width, this.height);
         //   _rectangleBody.Position = new FarseerGames.FarseerPhysics.Mathematics.Vector2(X, Y);


        }

        public bool pixelCollidesWith(sprite b)
        {
            if (Intersects(this.rectangle, b.rectangle))
            {

                uint[] bitsA = new uint[this.asset.Width * this.asset.Height];
                this.asset.GetData<uint>(bitsA);

                uint[] bitsB = new uint[b.asset.Width * b.asset.Height];
                b.asset.GetData<uint>(bitsB);

                int x1 = Math.Max(this.rectangle.X, b.rectangle.X);
                int x2 = Math.Min(this.rectangle.X + this.rectangle.Width, b.rectangle.X + b.rectangle.Width);

                int y1 = Math.Max(this.rectangle.Y, b.rectangle.Y);
                int y2 = Math.Min(this.rectangle.Y + this.rectangle.Height, b.rectangle.Y + b.rectangle.Height);

                for (int y = y1; y < y2; ++y)
                {
                    for (int x = x1; x < x2; ++x)
                    {
                        if (((bitsA[(x - this.rectangle.X) + (y - this.rectangle.Y) * this.asset.Width] & 0xFF000000) >> 24) > 20 &&
                            ((bitsB[(x - b.rectangle.X) + (y - b.rectangle.Y) * b.asset.Width] & 0xFF000000) >> 24) > 20)
                            return true;
                    }
                }
            }

            return false;
        }



        public static bool Intersects(Rectangle a, Rectangle b)
        {
            // check if two Rectangles intersect
            return (a.Right > b.Left && a.Left < b.Right &&
                    a.Bottom > b.Top && a.Top < b.Bottom);
        }

        public static bool Touches(Rectangle a, Rectangle b)
        {
            // check if two Rectangles intersect or touch sides
            return (a.Right >= b.Left && a.Left <= b.Right &&
                    a.Bottom >= b.Top && a.Top <= b.Bottom);
        }

        public  bool simpleCollidesWith(sprite b1)
        {
            Rectangle a = this.rectangle;
            Rectangle b = b1.rectangle;
            // check if two Rectangles intersect
            return (a.Right > b.Left && a.Left < b.Right &&
                    a.Bottom > b.Top && a.Top < b.Bottom);
        }



    }

}