using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ADHDGame
{
    class GuiHUD
    {
        List<GuiButton> actionBtns;
        List<Texture2D> upBtns;
        List<Texture2D> downBtns;
        List<Texture2D> hoverBtns;
        ADHDGame g;
        RobotActions ra = RobotActions.Instance;
        Texture2D sidebar;
        Texture2D energyBtn;
        Texture2D energy;

        public GuiHUD(ADHDGame game, SpriteBatch sb)
        {
            g = game;
            actionBtns = new List<GuiButton>(15);
            upBtns = new List<Texture2D>(15);
            downBtns = new List<Texture2D>(15);
            hoverBtns = new List<Texture2D>(15);
            int i;
            sidebar = g.Content.Load<Texture2D>("GuiButton\\SideMenu");
            energyBtn = g.Content.Load<Texture2D>("GuiButton\\EmptyButton");
            energy = g.Content.Load<Texture2D>("GuiButton\\EnergyBlip");
            for(i = 0; i<9; i++)
            {
                upBtns.Add(g.Content.Load<Texture2D>("GuiButton\\UpBtn\\UpBtn" + i.ToString()));
                downBtns.Add(g.Content.Load<Texture2D>("GuiButton\\DownBtn\\DownBtn" + i.ToString()));
                hoverBtns.Add(g.Content.Load<Texture2D>("GuiButton\\HoverBtn\\HoverBtn" + i.ToString()));

                //actionBtns.Add(new GuiButton(upBtns[i], downBtns[i], hoverBtns[i], new Vector2(0, 48 * i + 24)));
            }
            actionBtns.Add(new GuiButton(upBtns[0], downBtns[0], hoverBtns[0], Keys.W, new Vector2(10, 21)));
            actionBtns.Add(new GuiButton(upBtns[1], downBtns[1], hoverBtns[1], Keys.S, new Vector2(10, 21 + 48)));
            actionBtns.Add(new GuiButton(upBtns[2], downBtns[2], hoverBtns[2], Keys.A, new Vector2(10, 48 * 2 + 21)));
            actionBtns.Add(new GuiButton(upBtns[3], downBtns[3], hoverBtns[3], Keys.D, new Vector2(10, 48 * 3 + 21)));//end move
            actionBtns.Add(new GuiButton(upBtns[4], downBtns[4], hoverBtns[4], Keys.E, new Vector2(10, 48 * 4 + 21)));//
            actionBtns.Add(new GuiButton(upBtns[5], downBtns[5], hoverBtns[5], Keys.Q, new Vector2(10, 48 * 5 + 21)));//
            actionBtns.Add(new GuiButton(upBtns[6], downBtns[6], hoverBtns[6], Keys.R, new Vector2(10, 48 * 6 + 21)));//
            actionBtns.Add(new GuiButton(upBtns[7], downBtns[7], hoverBtns[7], Keys.F, new Vector2(10, 48 * 7 + 21)));//
            actionBtns.Add(new GuiButton(upBtns[8], downBtns[8], hoverBtns[8], Keys.V, new Vector2(10, 48 * 8 + 21)));
           // actionBtns.Add(new GuiButton(upBtns[9], downBtns[9], hoverBtns[9], Keys.Z, new Vector2(10, 48 * 9 + 21)));
           // actionBtns.Add(new GuiButton(upBtns[10], downBtns[10], hoverBtns[10], Keys.X, new Vector2(10, 48 * 10 + 21)));//
           // actionBtns.Add(new GuiButton(upBtns[11], downBtns[11], hoverBtns[11], Keys.C, new Vector2(10, 48 * 11 + 21)));//
           // actionBtns.Add(new GuiButton(upBtns[12], downBtns[12], hoverBtns[12], Keys.T, new Vector2(10, 48 * 12 + 21)));//
            //actionBtns.Add(new GuiButton(upBtns[13], downBtns[13], hoverBtns[13], Keys.G, new Vector2(10, 48 * 13 + 21)));//
           // actionBtns.Add(new GuiButton(upBtns[14], downBtns[14], hoverBtns[14], Keys.B, new Vector2(10, 48 * 14 + 21)));//
        }

        public void Update()
        {
            int i;
            for (i = 0; i < 9; i++)
            {
                actionBtns[i].Update();
                if (actionBtns[i].isClicked())
                {
                    switch (i)
                    {
                        case 0:
                            //Move Forward action
                            ra.MoveForward(1);
                            break;
                        case 1:
                            //back
                            ra.MoveBack(1);
                            break;
                        case 2:
                            //left
                            ra.MoveLeft(1);
                            break;
                        case 3:
                            //right
                            ra.MoveRight(1);
                            break;
                        case 4:
                            //rotate right
                            ra.Banjo();
                            break;
                        case 5:
                            //stop dubstep
                            ra.PieceOfMind();
                            break;
                        case 6:
                            //Activate
                            ra.ActivateObject();
                            break;
                        case 7:
                            //Push
                            ra.PushObject();
                            break;
                        case 8:
                            //Help
                            ra.Help();
                            break;
                        case 9:
                            break;
                        case 10:
                            break;
                        case 11:
                            break;
                        case 12:
                            break;
                        case 13:
                            break;
                        case 14:
                            break;
                    }
                    actionBtns[i].resetClicked();
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(sidebar, new Rectangle(0, 0, sidebar.Width, sidebar.Height), Color.White);
            sb.Draw(energyBtn, new Rectangle(10, 48 * 13 + 21, energyBtn.Width, energyBtn.Height), Color.White);

            
            if(RobotActions.Instance.energy >= 1){sb.Draw(energy, new Rectangle(15 + 10, 48 * 13 + 21 + 15, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 2){sb.Draw(energy, new Rectangle(15 + energy.Width + 10, 48 * 13 + 21 + 15, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 3){sb.Draw(energy, new Rectangle(15 + energy.Width * 2 + 10, 48 * 13 + 21 + 15, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 4){sb.Draw(energy, new Rectangle(15 + energy.Width * 3 + 10, 48 * 13 + 21 + 15, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 5){sb.Draw(energy, new Rectangle(15 + energy.Width * 4 + 10, 48 * 13 + 21 + 15, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 6){sb.Draw(energy, new Rectangle(15 + energy.Width * 5 + 10, 48 * 13 + 21 + 15, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 7){sb.Draw(energy, new Rectangle(15 + energy.Width * 6 + 10, 48 * 13 + 21 + 15, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 8){sb.Draw(energy, new Rectangle(15 + energy.Width * 7 + 10, 48 * 13 + 21 + 15, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 9){sb.Draw(energy, new Rectangle(15 + energy.Width * 8 + 10, 48 * 13 + 21 + 15, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 10){sb.Draw(energy, new Rectangle(15 + energy.Width * 9 + 10, 48 * 13 + 21 + 15, energy.Width, energy.Height), Color.White);}

            if(RobotActions.Instance.energy >= 11){sb.Draw(energy, new Rectangle(15 + energy.Width + 1, 48 * 13 + 21 + 63, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 12){sb.Draw(energy, new Rectangle(15 + energy.Width + 1, 48 * 13 + 21 + 63, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 13){sb.Draw(energy, new Rectangle(15 + energy.Width * 2 + 1, 48 * 13 + 21 + 63, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 14){sb.Draw(energy, new Rectangle(15 + energy.Width * 3 + 1, 48 * 13 + 21 + 63, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 15){sb.Draw(energy, new Rectangle(15 + energy.Width * 4 + 1, 48 * 13 + 21 + 63, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 16){sb.Draw(energy, new Rectangle(15 + energy.Width * 5 + 1, 48 * 13 + 21 + 63, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 17){sb.Draw(energy, new Rectangle(15 + energy.Width * 6 + 1, 48 * 13 + 21 + 63, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 18){sb.Draw(energy, new Rectangle(15 + energy.Width * 7 + 1, 48 * 13 + 21 + 63, energy.Width, energy.Height), Color.White);}
            if(RobotActions.Instance.energy >= 19){sb.Draw(energy, new Rectangle(15 + energy.Width * 8 + 1, 48 * 13 + 21 + 63, energy.Width, energy.Height), Color.White);}
            if (RobotActions.Instance.energy >= 20) { sb.Draw(energy, new Rectangle(15 + energy.Width * 9 + 1, 48 * 13 + 21 + 63, energy.Width, energy.Height), Color.White); }
            sb.End();
            actionBtns.ForEach((b) => { b.draw(sb); });
        }
    }
}
