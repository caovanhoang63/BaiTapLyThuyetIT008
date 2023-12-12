using System.Collections.Generic;
using System.Windows.Forms;

namespace SpaceShip
{
    public static class Keyboard
    {
        private static readonly HashSet<Keys> keys = new HashSet<Keys>();

        public static void OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
            if (keys.Contains(e.KeyCode) == false)
            {
                keys.Add(e.KeyCode);
            }
        }

        public static void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (keys.Contains(e.KeyCode))
            {
                keys.Remove(e.KeyCode);
            }
        }

        public static bool IsKeyDown(Keys key)
        {
            return keys.Contains(key);
        }
    }
}