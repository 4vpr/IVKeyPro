using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Text;

namespace IVKeyPro
{
    public partial class KeyChecker
    {
        public KeyboardState keyState_current = Keyboard.GetState();
        public KeyboardState keyState_prev = Keyboard.GetState();
        public KeyChecker()
        {
            keyState_current = Keyboard.GetState();
            keyState_prev = Keyboard.GetState();
        }

        public bool keyPress(Keys key)
        {
            if (keyState_current.IsKeyDown(key) && !keyState_prev.IsKeyDown(key))
            {

                return true;
            }

            return false;
        }


        public bool keyDown(Keys key)
        {
            if (keyState_current.IsKeyDown(key))
            {

                return true;
            }

            return false;
        }
        public bool keyUp(Keys key)
        {
            if (!keyState_current.IsKeyDown(key) && keyState_prev.IsKeyDown(key))
            {

                return true;
            }

            return false;
        }
        public void KeyCheck(bool b)
        {
            //System.Text.StringBuilder sb = new StringBuilder();
            //var keys = keyState.GetPressedKeys();
            //if (keys.Length > 0)
            {
                //Trace.WriteLine("" + keys[0]);
            }
        }

    }
}