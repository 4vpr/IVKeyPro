using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework.Input;


namespace IVKeyPro
{
    public partial class Window : Game
    {
        Texture2D texture_note;
        Texture2D texture_bg;
        Texture2D texture_noteEffect;
        Texture2D texture_proplus;
        Texture2D texture_pro;
        Texture2D texture_noob;
        Texture2D texture_bad;
        Texture2D texture_miss;
        Texture2D texture_combo;
        Texture2D textrue_songSelected;
        Texture2D textrue_songUI;
        Texture2D texture_longnote;
        Texture2D[] texture_number = new Texture2D[10];
        Texture2D[] texture_rank = new Texture2D[17];
        private SpriteFont font_mid;
        private SpriteFont font_big;
        protected override void LoadContent()
        {
            font_mid = Content.Load<SpriteFont>("mid");
            font_big = Content.Load<SpriteFont>("big");
            texture_note = this.Content.Load<Texture2D>("note");
            texture_bg = this.Content.Load<Texture2D>("panel");
            texture_proplus = this.Content.Load<Texture2D>("proplus");
            texture_pro = this.Content.Load<Texture2D>("pro");
            texture_noob = this.Content.Load<Texture2D>("noob");
            texture_bad = this.Content.Load<Texture2D>("bad");
            texture_miss = this.Content.Load<Texture2D>("miss");
            texture_noteEffect = this.Content.Load<Texture2D>("noteEffect");
            texture_combo = this.Content.Load<Texture2D>("combo");
            textrue_songSelected = this.Content.Load<Texture2D>("songSelected");
            textrue_songUI = this.Content.Load<Texture2D>("songUI");
            texture_longnote = this.Content.Load<Texture2D>("longnote");

            for (int i = 0; i < 17; i++)
            {
                texture_rank[i] = this.Content.Load<Texture2D>("rank" + i);
            }

            for (int i = 0; i < 10; i++)
            {
                texture_number[i] = this.Content.Load<Texture2D>("" + i);
            }
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }
    }
}
