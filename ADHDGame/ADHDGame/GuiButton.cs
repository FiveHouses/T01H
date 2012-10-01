using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace ADHDGame
{
    public class GuiButton
    {
        Texture2D button;
        Texture2D btnDown;
        Texture2D btnHover;
        Rectangle btnArea;
        Keys hotkey = Keys.None;
        bool graphics = true, clicked = false;
        bool KeyPress = false;
        int clickCount = 0;
        Button_State state = Button_State.BUTTON_UP;
        String btnTxt = null;
        Text_Alignment txtAlign = Text_Alignment.MIDDLE_CENTER;
        SpriteFont font;
        Color txtCol;

        public GuiButton()
        {
        }
        public GuiButton(Rectangle buttonArea)
        {
            btnArea = buttonArea;
            graphics = false;
        }
        public GuiButton(Rectangle buttonArea, Keys key)
        {
            btnArea = buttonArea;
            hotkey = key;
            graphics = false;
        }
        public GuiButton(Texture2D btn, Vector2 position)
        {
            button = btn;
            btnArea = btn.Bounds;
            btnDown = btn;
            btnHover = btn;
            btnArea.X = (int)position.X;
            btnArea.Y = (int)position.Y;
        }
        public GuiButton(Texture2D btn, Rectangle buttonArea)
        {
            button = btn;
            btnArea = buttonArea;
            btnDown = btn;
            btnHover = btn;
        }
        public GuiButton(Texture2D btn, Keys key, Vector2 position)
        {
            button = btn;
            hotkey = key;
            btnArea = btn.Bounds;
            btnDown = btn;
            btnHover = btn;
            btnArea.X = (int)position.X;
            btnArea.Y = (int)position.Y;
        }
        public GuiButton(Texture2D btn, Rectangle buttonArea, Keys key)
        {
            button = btn;
            hotkey = key;
            btnArea = buttonArea;
            btnDown = btn;
            btnHover = btn;
        }
        public GuiButton(Texture2D btn, Texture2D buttonDown, Texture2D buttonMouseOver, Vector2 position)
        {
            button = btn;
            btnDown = buttonDown;
            btnHover = buttonMouseOver;
            btnArea = btn.Bounds;
            btnArea.X = (int)position.X;
            btnArea.Y = (int)position.Y;
        }
        public GuiButton(Texture2D btn, Texture2D buttonDown, Texture2D buttonMouseOver, Vector2 position, String text, SpriteFont font, Color textCol)
        {
            txtCol = textCol;
            button = btn;
            btnDown = buttonDown;
            btnHover = buttonMouseOver;
            btnArea = btn.Bounds;
            btnArea.X = (int)position.X;
            btnArea.Y = (int)position.Y;
            btnTxt = text;
            this.font = font;
        }
        public GuiButton(Texture2D btn, Texture2D buttonDown, Texture2D buttonMouseOver, Keys key, Vector2 position)
        {
            button = btn;
            btnDown = buttonDown;
            btnHover = buttonMouseOver;
            hotkey = key;
            btnArea = btn.Bounds;
            btnArea.X = (int)position.X;
            btnArea.Y = (int)position.Y;
        }
        public GuiButton(Texture2D btn, Texture2D buttonDown, Texture2D buttonMouseOver, Rectangle buttonArea)
        {
            button = btn;
            btnDown = buttonDown;
            btnHover = buttonMouseOver;
            btnArea = buttonArea;
        }
        public GuiButton(Texture2D btn, Texture2D buttonDown, Texture2D buttonMouseOver, Keys key, Rectangle buttonArea)
        {
            button = btn;
            btnDown = buttonDown;
            btnHover = buttonMouseOver;
            hotkey = key;
            btnArea = buttonArea;
        }

        public GuiButton(Texture2D btn, Texture2D buttonDown, Texture2D buttonMouseOver, Keys key, Rectangle buttonArea, String text, SpriteFont spriteFont, Color textCol)
        {
            txtCol = textCol;
            button = btn;
            btnDown = buttonDown;
            btnHover = buttonMouseOver;
            hotkey = key;
            btnArea = buttonArea;
            btnTxt = text;
            font = spriteFont;
        }

        public GuiButton(Texture2D btn, Texture2D buttonDown, Texture2D buttonMouseOver, Keys key, Rectangle buttonArea, String text, Text_Alignment textAlign, SpriteFont spriteFont, Color textCol)
        {
            txtCol = textCol;
            button = btn;
            btnDown = buttonDown;
            btnHover = buttonMouseOver;
            hotkey = key;
            btnArea = buttonArea;
            btnTxt = text;
            txtAlign = textAlign;
            font = spriteFont;
        }

        public void Update()
        {
            int clicks = clickCount;
            if (btnArea.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                switch(state)
                {
                    case Button_State.BUTTON_UP:
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            state = Button_State.BUTTON_DOWN;
                        }
                        else
                        {
                            state = Button_State.BUTTON_HOVER;
                        }
                        break;
                    case Button_State.BUTTON_HOVER:
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            state = Button_State.BUTTON_DOWN;
                        }
                        break;
                    case Button_State.BUTTON_DOWN:
                        if (Mouse.GetState().LeftButton == ButtonState.Released)
                        {
                            state = Button_State.BUTTON_HOVER;
                            clickCount++;
                        }
                        break;
                }
            }//if mouseover
            else
            {
                if (state == Button_State.BUTTON_HOVER || state == Button_State.BUTTON_DOWN)
                {
                    state = Button_State.BUTTON_UP;
                }
            }
            
            if (hotkey != Keys.None)
            {
                if (Keyboard.GetState().IsKeyDown(hotkey) && state != Button_State.BUTTON_DOWN)
                {
                    KeyPress = true;
                    state = Button_State.BUTTON_DOWN;
                    clickCount++;
                    KeyPress = false;
                }
                /*else if(Keyboard.GetState().IsKeyUp(hotkey) && KeyPress)
                {
                    state = Button_State.BUTTON_UP;
                    clickCount++;
                    KeyPress = false;
                }*/
            }

            if (clickCount > clicks)
            {
                clicked = true;
            }
        }

        public void draw(SpriteBatch sb)
        {
            if (graphics)
            {
                sb.Begin();
                switch (state)
                {
                    case Button_State.BUTTON_UP:
                        sb.Draw(button, btnArea, Color.White);
                        if (btnTxt != null)
                        {
                            switch (txtAlign)
                            {
                                case Text_Alignment.CENTER:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Center.X - (font.MeasureString(btnTxt).X/2), btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.TOP_CENTER:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Center.X - (font.MeasureString(btnTxt).X / 2), btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.BOTTOM_CENTER:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Center.X - (font.MeasureString(btnTxt).X / 2), btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.MIDDLE_CENTER:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Center.X - (font.MeasureString(btnTxt).X / 2), btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.RIGHT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Right - font.MeasureString(btnTxt).X, btnArea.Center.Y), txtCol);
                                    break;
                                case Text_Alignment.TOP_RIGHT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Right - font.MeasureString(btnTxt).X, btnArea.Center.Y), txtCol);
                                    break;
                                case Text_Alignment.BOTTOM_RIGHT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Right - font.MeasureString(btnTxt).X, btnArea.Center.Y), txtCol);
                                    break;
                                case Text_Alignment.MIDDLE_RIGHT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Right - font.MeasureString(btnTxt).X, btnArea.Center.Y), txtCol);
                                    break;
                                case Text_Alignment.LEFT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Left + 3, btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.TOP_LEFT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Left + 3, btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.BOTTOM_LEFT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Left + 3, btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.MIDDLE_LEFT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Left + 3, btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                            }
                        }
                        break;
                    case Button_State.BUTTON_HOVER:
                        sb.Draw(btnHover, btnArea, Color.White);
                        if (btnTxt != null)
                        {
                            switch (txtAlign)
                            {
                                case Text_Alignment.CENTER:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Center.X - (font.MeasureString(btnTxt).X / 2), btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.TOP_CENTER:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Center.X - (font.MeasureString(btnTxt).X / 2), btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.BOTTOM_CENTER:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Center.X - (font.MeasureString(btnTxt).X / 2), btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.MIDDLE_CENTER:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Center.X - (font.MeasureString(btnTxt).X / 2), btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.RIGHT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Right - font.MeasureString(btnTxt).X, btnArea.Center.Y), txtCol);
                                    break;
                                case Text_Alignment.TOP_RIGHT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Right - font.MeasureString(btnTxt).X, btnArea.Center.Y), txtCol);
                                    break;
                                case Text_Alignment.BOTTOM_RIGHT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Right - font.MeasureString(btnTxt).X, btnArea.Center.Y), txtCol);
                                    break;
                                case Text_Alignment.MIDDLE_RIGHT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Right - font.MeasureString(btnTxt).X, btnArea.Center.Y), txtCol);
                                    break;
                                case Text_Alignment.LEFT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Left + 3, btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.TOP_LEFT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Left + 3, btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.BOTTOM_LEFT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Left + 3, btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.MIDDLE_LEFT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Left + 3, btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                            }
                        }
                        break;
                    case Button_State.BUTTON_DOWN:
                        sb.Draw(btnDown, btnArea, Color.White);
                        if (btnTxt != null)
                        {
                            switch (txtAlign)
                            {
                                case Text_Alignment.CENTER:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Center.X - (font.MeasureString(btnTxt).X / 2), btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.TOP_CENTER:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Center.X - (font.MeasureString(btnTxt).X / 2), btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.BOTTOM_CENTER:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Center.X - (font.MeasureString(btnTxt).X / 2), btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.MIDDLE_CENTER:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Center.X - (font.MeasureString(btnTxt).X / 2), btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.RIGHT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Right - font.MeasureString(btnTxt).X, btnArea.Center.Y), txtCol);
                                    break;
                                case Text_Alignment.TOP_RIGHT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Right - font.MeasureString(btnTxt).X, btnArea.Center.Y), txtCol);
                                    break;
                                case Text_Alignment.BOTTOM_RIGHT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Right - font.MeasureString(btnTxt).X, btnArea.Center.Y), txtCol);
                                    break;
                                case Text_Alignment.MIDDLE_RIGHT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Right - font.MeasureString(btnTxt).X, btnArea.Center.Y), txtCol);
                                    break;
                                case Text_Alignment.LEFT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Left + 3, btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.TOP_LEFT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Left + 3, btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.BOTTOM_LEFT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Left + 3, btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                                case Text_Alignment.MIDDLE_LEFT:
                                    sb.DrawString(font, btnTxt, new Vector2(btnArea.Left + 3, btnArea.Center.Y - (font.MeasureString(btnTxt).Y / 2)), txtCol);
                                    break;
                            }
                        }
                        break;
                }
                sb.End();
            }
        }
        public bool contains(int x, int y)
        {
            return btnArea.Contains(new Point(x,y));
        }
        public void setTexture(Texture2D btnTex)
        {
            button = btnTex;
        }
        public void setHoverTexture(Texture2D hoverTex)
        {
            btnHover = hoverTex;
        }
        public void setDownTexture(Texture2D downTex)
        {
            btnDown = downTex;
        }
        public String getText()
        {
            return btnTxt;
        }
        public void setText(String txt)
        {
            //Is not set if null as requisite font is not set.
            if (btnTxt != null)
            {
                btnTxt = txt;
            }
        }
        public bool isClicked()
        {
            return clicked;
        }
        public void resetClicked()
        {
            clicked = false;
        }
        public int getClickCount()
        {
            return clickCount;
        }
        public Button_State getState()
        {
            return state;
        }
        public void resetClickCount()
        {
            clickCount = 0;
        }

        public enum Button_State
        {
            BUTTON_UP,
            BUTTON_HOVER,
            BUTTON_DOWN
        }
    }
}
