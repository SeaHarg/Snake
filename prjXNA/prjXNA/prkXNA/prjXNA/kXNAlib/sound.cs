using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
namespace prjXNAGame
{
    public class sound
    {
        SoundEffect soundEffect;
        theGame reference;
        public sound(theGame reference,String name)
        {
            this.reference =reference ;
            soundEffect = reference.Content.Load<SoundEffect>(name);

        }
        public void play()
        {
            soundEffect.Play();
        }
        
    }
}
