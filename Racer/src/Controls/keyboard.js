/*
class Keyboard
    {
        setOfPressedKeys = new HashSet<Keys>();

        [Flags]
        private enum KeyStates
        {
            None = 0,
            Down = 1,
            Toggled = 2
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        private static KeyStates GetKeyState(Keys key)
        {
            KeyStates state = KeyStates.None;

            short retVal = GetKeyState((int)key);

            if ((retVal & 0x8000) == 0x8000)
                state |= KeyStates.Down;

            if ((retVal & 1) == 1)
                state |= KeyStates.Toggled;

            return state;
        }

        isKeyDown(key)
        {
            return isKeyPressed &&  KeyStates.Down == (GetKeyState(key) & KeyStates.Down);
        }

        isKeyToggled(key)
        {
            if (setOfPressedKeys.Contains(key))
            {
                if (!IsKeyDown(key))
                    pressed.Remove(key);
            }
            else
            {
                if (IsKeyDown(key))
                {
                    pressed.Add(key);
                    return true;
                }
            }

            return false;
        }
    }
    */