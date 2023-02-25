using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Diagnostics;

namespace IVKeyPro
{
    partial class Window
    {
        public enum Alignment { Center = 0, Left = 1, Right = 2, Top = 4, Bottom = 8 }

        public void DrawString(SpriteFont font, string text, Rectangle bounds, Alignment align, Color color)
        {
            Vector2 size = font.MeasureString(text);
            Vector2 pos = new Vector2(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
            Vector2 origin = size * 0.5f;

            if (align.HasFlag(Alignment.Left))
                origin.X += bounds.Width / 2 - size.X / 2;

            if (align.HasFlag(Alignment.Right))
                origin.X -= bounds.Width / 2 - size.X / 2;

            if (align.HasFlag(Alignment.Top))
                origin.Y += bounds.Height / 2 - size.Y / 2;

            if (align.HasFlag(Alignment.Bottom))
                origin.Y -= bounds.Height / 2 - size.Y / 2;

            _spriteBatch.DrawString(font, text, pos, color, 0, origin, 1, SpriteEffects.None, 0);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: Scale);
            GraphicsDevice.Clear(Color.Black);
            if (Setting.state == State.Playing)
            {
                Rectangle rct = new Rectangle();
                //_spriteBatch.DrawString(font_mid, "" + _beatmap.combo, new Vector2(900, 100), Color.White);
                // ++BG++ //
                rct.Width = Convert.ToInt32(Math.Round(1920 / 150f * Setting.noteWidth));
                rct.Y = 0;
                rct.Height = 1080;
                rct.X = Convert.ToInt32(Math.Round(1920 / 2f - rct.Width / 2f));
                _spriteBatch.Draw(texture_bg, rct, Color.White);
                // --BG-- //

                // ++NOTE++ //
                try
                {
                    foreach (var item in _inGame.spawnedNote)
                    {

                        rct.Width = Setting.noteWidth;
                        rct.X = rct.Width * item.line + Convert.ToInt32(Math.Round(1920/2 - (rct.Width * 4f /2)));
                        
                        if (item.length == 0)
                        {
                            rct.Height = Setting.noteHeight;
                        }
                        else
                        {
                            rct.Y = Convert.ToInt32(Math.Round((_inGame.songms - item.timing) * Setting.noteVelocity + 1000 - (item.length * Setting.noteVelocity)));
                            rct.Height = Convert.ToInt32(Math.Round(item.length * Setting.noteVelocity));

                            if (rct.Y > 1000 - rct.Height && _inGame.isLongNote[item.line])
                            {
                                rct.Height -= (rct.Y + rct.Height) - 1000;
                            }
                            _spriteBatch.Draw(texture_longnote, rct, Color.White * 1f);

                        }
                        rct.Y = Convert.ToInt32(Math.Round((_inGame.songms - item.timing) * Setting.noteVelocity + 1000 - (Setting.noteHeight)));
                        rct.Height = Setting.noteHeight;
                        if (rct.Y < 1000 - rct.Height || !_inGame.isLongNote[item.line])
                        {
                            _spriteBatch.Draw(texture_note, rct, Color.White * 1f);
                        }
                            
                        //Trace.WriteLine("print");
                    }
                    _inGame.failedLongNote_remove.Clear();
                    foreach (var item in _inGame.failedLongNote)
                    {
                        rct.Width = Setting.noteWidth;
                        rct.X = rct.Width * item.line + Convert.ToInt32(Math.Round(1920 / 2 - (rct.Width * 4f / 2)));
                        rct.Y = Convert.ToInt32(Math.Round((_inGame.songms - item.timing) * Setting.noteVelocity + 1000 - (item.length * Setting.noteVelocity)));
                        rct.Y += Setting.noteHeight;
                        rct.Height = Convert.ToInt32(Math.Round(item.length * Setting.noteVelocity) + Setting.noteHeight);
                        _spriteBatch.Draw(texture_longnote, rct, Color.White * 0.5f);
                        if (rct.Y < 0)
                        {
                            _inGame.failedLongNote_remove.Add(item);
                        }
                        _inGame.failedLongNote.Except(_inGame.failedLongNote_remove);
                        //Trace.WriteLine("print");
                    }
                    _inGame.failedLongNote_remove.Clear();
                }
                catch
                {
                    Trace.WriteLine("ERROR: DRAW NOTE");
                }

                // ++KEYEFFECT++ //
                if (Setting.keyEffect)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        rct.Height = 700;
                        rct.Width = Setting.noteWidth;
                        //rct.X = (i * Setting.noteWidth) + (1920 / 2) - (4 * Setting.noteWidth / 2);
                        rct.X = (i * Setting.noteWidth) + (1920 / 2) - (4 * Setting.noteWidth / 2);
                        rct.Y = 1000 - rct.Height;
                        float _alpha = _inGame.keyEffect[i] / 100f;
                        //Trace.WriteLine(_alpha);
                        _spriteBatch.Draw(texture_noteEffect, rct, Color.CadetBlue * _alpha * 0.7f);
                    }
                }

                // ++JUDGE++ //
                if (_inGame.judgementDisplay[1] > 0)
                {
                    rct.Height = 45 + 0;
                    rct.Width = 100 + 0;
                    rct.X = 1920 / 2 - rct.Width / 2;
                    int n = _inGame.judgementDisplay[1];
                    if (n > 100) { n = 100; }
                    rct.Y = 300 - Convert.ToInt32(n * 0.3f);
                    float _alpha = _inGame.judgementDisplay[1] / 100f;
                    if (_inGame.judgementDisplay[0] == 0)
                    {
                        _spriteBatch.Draw(texture_miss, rct, Color.OrangeRed * _alpha);
                    }
                    if (_inGame.judgementDisplay[0] == 1)
                    {
                        _spriteBatch.Draw(texture_proplus, rct, Color.White * _alpha);
                    }
                    if (_inGame.judgementDisplay[0] == 2)
                    {
                        _spriteBatch.Draw(texture_pro, rct, Color.White * _alpha);
                    }
                    if (_inGame.judgementDisplay[0] == 3)
                    {
                        _spriteBatch.Draw(texture_noob, rct, Color.Green * _alpha);
                    }
                    if (_inGame.judgementDisplay[0] == 4)
                    {
                        _spriteBatch.Draw(texture_bad, rct, Color.Orange * _alpha);
                    }
                }

                // ++ACCURACY++ //
                {
                    rct.Height = 40;
                    rct.Width = 85;
                    rct.X = 1400;
                    rct.Y = 500;
                    DrawString(font_mid, _inGame.gameResult.acc + "%", rct, Alignment.Center,Color.White);

                    rct.Height = 40;
                    rct.Width = 85;
                    rct.X = 1400;
                    rct.Y = 400;
                    _spriteBatch.Draw(texture_rank[_inGame.gameResult.GetRank()], rct, Color.White * 1f) ;
                }

                // ++COMBO++ //
                {
                    List<int> numbers = new List<int>();
                    string theNumber = Convert.ToString(_inGame.combo);
                    for (int i = 0; i < theNumber.Length; i++)
                    {
                        numbers.Add(Convert.ToInt32(theNumber.Substring(i, 1)));
                    }
                    int fi = 0;
                    int whole_PosX = 1920 / 2;
                    foreach (int item in numbers)
                    {
                        rct.Width = 30;
                        rct.X = 0;
                        rct.Y = 600;
                        rct.Y = 600;
                        rct.Height = 45;
                        int w = numbers.Count() * rct.Width;
                        double y = (whole_PosX - (w / 2) + rct.Width * fi);
                        if (_inGame.combo != 0)
                            rct.Height += Convert.ToInt32(_inGame.comboEffect * 0.1f);
                        rct.Y -= (rct.Height / 2);
                        rct.X = Convert.ToInt32(Math.Round(y));
                        _spriteBatch.Draw(texture_number[item], rct, Color.White);
                        fi++;
                    }
                    _spriteBatch.Draw(texture_combo, new Rectangle(1920 / 2 - 75, 500, 150, 45), Color.White);
                }

                if (_inGame.isPause)
                {
                    rct.X = 0;
                    rct.Y = 0;
                    rct.Width = 1920;
                    rct.Height = 1080;
                    _spriteBatch.Draw(texture_note, rct, Color.Black * 0.5f);
                }


                base.Draw(gameTime);
            }
            if (Setting.state == State.SongSelct)
            {
                Rectangle rct = new Rectangle();
                int i = 0;
                int gap = 20;
                foreach (var item in loaded_beatmaps)
                {
                    //Trace.WriteLine(_menu.loaded_beatmaps[0].Title);
                    rct.Height = 150;
                    rct.Width = 800;
                    rct.X = 100;
                    rct.Y = (1080 / 2 - rct.Height / 2) + (rct.Height * (i - selectedBeatmapAt)) + (gap * (i - selectedBeatmapAt));
                    DrawString(font_mid, loaded_beatmaps[i].Title, new Rectangle(rct.X + 80, rct.Y + 60, 100, 25), Alignment.Left, Color.White);
                    if (selectedBeatmap == item)
                    {
                        _spriteBatch.Draw(textrue_songSelected, rct, Color.White * 0.5f);
                        DrawString(font_mid, "Song by: ", new Rectangle(1000, 700, 100, 25), Alignment.Left, Color.White);
                        DrawString(font_mid, "Mapper: : ", new Rectangle(1000, 800, 100, 25),Alignment.Left, Color.White);

                        _spriteBatch.Draw(textrue_songUI, rct, Color.White * 1f);
                        for (int k = 0,c = 0; k < 6; k++)
                        {
                            string s = "";
                            if (item.HasDifficulty[k])
                            {
                                rct.X = 1000 + (c*rct.Height) + 50;
                                rct.Y = 500;
                                switch (k)
                                {
                                    case 0:
                                        s = "easy";
                                        break;
                                    case 1:
                                        s = "normal";
                                        break;
                                    case 2:
                                        s = "hard";
                                        break;
                                    case 3:
                                        s = "expert";
                                        break;
                                    case 4:
                                        s = "insane";
                                        break;
                                    case 5:
                                        s = "master";
                                        break;
                                }
                                float _alpha = 0;
                                if(selectedDifficulty == k)
                                {
                                    _alpha = 1f;
                                }
                                if (selectedDifficulty != k)
                                {
                                    _alpha = 0.5f;
                                }
                                DrawString(font_mid,s, new Rectangle(rct.X, rct.Y, 100, 25), Alignment.Center, Color.White * _alpha);
                                c++;
                            }
                        }
                    }

                   
                    
                    i++;
                }
                
            }

            if (Setting.state == State.Result)
            {
                Rectangle rct = new Rectangle();
                rct.Height = 45;
                rct.Width = 100;
                int gap = 60;
                rct.X = 800;
                int startY = 500;
                rct.Y = startY;
                Rectangle rct2 = new Rectangle();
                rct2.Height = rct.Height;
                rct2.Width = rct.Width;
                rct2.Y = rct.Y;
                rct2.X = 1060;

                _spriteBatch.Draw(texture_proplus, rct, Color.White);
                DrawString(font_mid, _inGame.gameResult.proplus.ToString(), rct2, Alignment.Center, Color.White);
                rct.Y = startY + gap;
                rct2.Y = startY + gap;
                _spriteBatch.Draw(texture_pro, rct, Color.White);
                DrawString(font_mid, _inGame.gameResult.pro.ToString(), rct2, Alignment.Center, Color.White);
                rct.Y = startY + gap * 2;
                rct2.Y = startY + gap * 2;
                _spriteBatch.Draw(texture_noob, rct, Color.White);
                DrawString(font_mid, _inGame.gameResult.noob.ToString(), rct2, Alignment.Center, Color.White);
                rct.Y = startY + gap * 3;
                rct2.Y = startY + gap * 3;
                _spriteBatch.Draw(texture_bad, rct, Color.White);
                DrawString(font_mid, _inGame.gameResult.bad.ToString(), rct2, Alignment.Center, Color.White);
                rct.Y = startY + gap * 4;
                rct2.Y = startY + gap * 4;
                _spriteBatch.Draw(texture_miss, rct, Color.White);
                DrawString(font_mid, _inGame.gameResult.miss.ToString(), rct2, Alignment.Center, Color.White);

                _spriteBatch.DrawString(font_mid, _inGame.gameResult.score.ToString(), new Vector2(800, 300), Color.White);
                _spriteBatch.DrawString(font_mid, (_inGame.gameResult.acc).ToString() + "%", new Vector2(1100, 300), Color.White);


            }
            if (Setting.state == State.SongSelct)
            {

            }

            _spriteBatch.End();
        }
    }
}
