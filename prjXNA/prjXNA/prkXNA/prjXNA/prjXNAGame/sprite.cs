









using System;
using System.Text;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Dynamics.Springs;
using FarseerGames.FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace prjXNAGame
{
    public class sprite : Microsoft.Xna.Framework.Game
    {
        protected float X;
        protected float Y;
        protected float width, height;
        public Texture2D asset;
        public Rectangle rectangle;
        public string identity;
        public theGame reference;
        public  Body rectBody;//PHYSICS
        public  Geom rectGeom;//PHYSICS
        protected  Vector2 spriteOrigin;
      
        public Boolean usePhysics = true;

        //use this constructor when you want to add a sprite from the content pipeline

        //sprites should have a pure magenta background for 
        //transparency
        public sprite(theGame reference, String contentAssetName,  float width, float height, float X, float Y)
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

            rectBody = BodyFactory.Instance.CreateRectangleBody(reference.ps, width, height, 1);//PHYSICS
            rectBody.Position = new FarseerGames.FarseerPhysics.Mathematics.Vector2(this.X, this.Y);//PHYSICS
            rectGeom = GeomFactory.Instance.CreateRectangleGeom(reference.ps,rectBody, this.width, this.height);//PHYSICS
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


        //use the following constructor when wanting to create collisdable groups of sprites
        public sprite(theGame reference, String contentAssetName, float width, float height, float X, float Y, CollisionCategory collCat,CollisionCategory collWith)
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

            rectBody = BodyFactory.Instance.CreateRectangleBody(reference.ps, width, height, 1);//PHYSICS
            rectBody.Position = new FarseerGames.FarseerPhysics.Mathematics.Vector2(this.X, this.Y);//PHYSICS
            rectGeom = GeomFactory.Instance.CreateRectangleGeom(reference.ps, rectBody, this.width, this.height);//PHYSICS
            rectGeom.SetBody(rectBody);
            rectGeom.CollisionEnabled = true;
            this.rectGeom.CollisionResponseEnabled = true;
            this.rectGeom.CollisionCategories = collCat;
            this.rectGeom.CollidesWith = collWith;
            //this.rectGeom.CollisionCategories = CollisionCategory.
            rectBody.Enabled = true;//PHYSICS
            rectGeom.OnSeparation += OnSeperation;
            rectGeom.OnCollision += OnCollision;
        


            reference.ps.BroadPhaseCollider.OnBroadPhaseCollision += OnBroadPhaseCollision;
            reference.ps.Add(rectBody);
            reference.ps.Add(rectGeom);
        }



        //use the following constructor when you don't want to get a sprite
        //from the content pipeline

        //pics should be in the bin\debug folder
        public sprite(theGame reference,int width, int height, int X, int Y, String fileName, String identity)
        {

            asset = Texture2D.FromFile(reference.graphics.GraphicsDevice, fileName, width,height); //aLoader.Load<Texture2D>("frog") as Texture2D;
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
            this.rectGeom.CollidesWith = CollisionCategory.All;
            //this.rectGeom.CollisionCategories = CollisionCategory.Cat1;
          

            //this.rectGeom.CollidesWith = CollisionCategory.All;
            rectBody.Position = new FarseerGames.FarseerPhysics.Mathematics.Vector2(this.X, this.Y);//PHYSICS
            rectBody.Enabled = true;//PHYSICS
            rectGeom.OnSeparation += OnSeperation;
            rectGeom.OnCollision += OnCollision;
            reference.ps.BroadPhaseCollider.OnBroadPhaseCollision += OnBroadPhaseCollision;
            reference.ps.Add(rectBody);
            reference.ps.Add(rectGeom);
            
        }

        public Geom getGeom()
        {
            return this.rectGeom;
        }


        public Body getBody()
        {
            return this.rectBody;
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

        public void setVelocity(float dx, float dy)
        {
            this.rectBody.LinearVelocity.X = dx;
            this.rectBody.LinearVelocity.Y = dy;
        }
        public float getVelocityDX()
        {
            return this.rectBody.LinearVelocity.X;
        }
        public float getVelocityDY()
        {
            return this.rectBody.LinearVelocity.Y;
        }

        public void draw()
        {
            
                this.rectangle = new Rectangle((int)rectGeom.Position.X, (int)rectGeom.Position.Y, (int)this.width, (int)this.height);//PHYSICS
                //?????****the following seems to have a problme with setting the centre as the origin of rotation****?????
                //reference.screen.Draw(asset, this.rectangle, null, reference.white, this.rectBody.Rotation, new Vector2(this.width/2f,this.height/2f), SpriteEffects.None, 0f);
                //reference.screen.Draw(asset, this.rectangle, null, reference.white, this.rectBody.Rotation, new Vector2(0,0), SpriteEffects.None, 0f);
                //reference.screen.Draw(asset, this.rectangle, null, reference.white, this.rectBody.Rotation, new Vector2(0, 0), SpriteEffects.None, 0f);
                //reference.screen.Draw(asset, this.rectangle, reference.white);
                reference.screen.Draw(asset, this.rectangle, null, Color.White, rectGeom.Rotation, spriteOrigin, SpriteEffects.None, 0);
                //reference.screen.Draw(asset, new Microsoft.Xna.Framework.Vector2 (this.rectGeom.Position.X,this.rectGeom.Position.Y ), null, Color.White, rectGeom.Rotation, spriteOrigin,1, SpriteEffects.None, 0);
         }
        public void setPosition(float x, float y)
        {
            rectBody.Position = new FarseerGames.FarseerPhysics.Mathematics.Vector2(x, y);//PHYSICS
            
        }

        public void setBounciness(float value)
        {
            this.rectGeom.RestitutionCoefficient = value;
        }
        public void allowRotation(Boolean rotate)
        {
            if (!rotate)
                this.rectBody.MomentOfInertia = float.PositiveInfinity;
            else
                this.rectBody.MomentOfInertia = 0;

        }

        public float getX()
        {
            return this.rectBody.Position.X;
        }
        public float getY()
        {
            return this.rectBody.Position.Y;
        }
        
        public float getWidth()
        {
            return this.width;
        }
        public float getHeight()
        {
            return this.height;
        }
        public void setMass(float mass)
        {
            this.rectBody.Mass = mass;
        }
        public void applyForce(float dx, float dy)
        {
            this.rectBody.ApplyForce(new FarseerGames.FarseerPhysics.Mathematics.Vector2(dx, dy));//PHYSICS
        }

        public void applyTorque(float t)
        {
            this.rectBody.ApplyTorque(t);
        }
        public void makeStatic(Boolean yesNo)
        {
            this.rectBody.IsStatic = yesNo;
        }
        public void applyImpulse(float dx, float dy)
        {
            this.rectBody.ApplyImpulse(new FarseerGames.FarseerPhysics.Mathematics.Vector2(dx, dy));
        }
        public void clearImpulse()
        {
            this.rectBody.ClearImpulse();
        }
        public void clearTorque()
        {
            this.rectBody.ClearTorque();
        }
        public void clearForce()
        {
            try
            {
                this.clearForce();
            }
            catch (Exception e) { }
        }
        public void enableCollision(Boolean state)
        {
            this.rectGeom.CollisionEnabled = state;
        }

        public bool physicsCollide(sprite withSprite)
        {
            if (this.rectGeom.Collide(withSprite.rectGeom))
            {
                
                return true;
            }
            else
            {
                return false;

            }
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