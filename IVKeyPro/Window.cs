using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
using System.IO;

namespace IVKeyPro
{

    public enum State
    {
        Playing,
        SongSelct,
        Result
    }
    public partial class Window : Game
    {
        const float TargetWidth = 1920f;
        const float TargetHeight = 1080f;
        Matrix Scale;


        Beatmap selectedBeatmap = new Beatmap();
        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;
        InGame _inGame;

        public Window()
        {
            Setting.Load();
            
            Trace.WriteLine(Setting.keyEffect);
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Setting.resolution_width;
            _graphics.PreferredBackBufferHeight = Setting.resolution_height;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;
            _graphics.IsFullScreen = Setting.fullscreen;
        }
        public void ApplyResoultion()
        {
            _graphics.PreferredBackBufferWidth = Setting.resolution_height;
            _graphics.PreferredBackBufferWidth = Setting.resolution_width;
            _graphics.SynchronizeWithVerticalRetrace = false;
            float scaleX = _graphics.PreferredBackBufferWidth / TargetWidth;
            float scaleY = _graphics.PreferredBackBufferHeight / TargetHeight;
            Scale = Matrix.CreateScale(new Vector3(scaleX, scaleY, 1));



        }
        protected override void Initialize()
        {
            
            // TODO: Add your initialization logic here
            ApplyResoultion();
            GetBeatmaps();
            Trace.WriteLine(loaded_beatmaps.ElementAt(0).folderName);
            selectedBeatmap = loaded_beatmaps.ElementAt(selectedBeatmapAt);
            base.Initialize();
            UpdateDIff();

        }


        protected override void Update(GameTime gameTime)
        {
            if (Setting.state == State.SongSelct)
            {
                SongSelcetUpdate();
            }
            if (Setting.state == State.Playing)
            {
                _inGame.Update();
            }
            if(Setting.state == State.Result)
            {
                ResultSceenUpdate(_inGame.gameResult);
            }

            base.Update(gameTime);
        }


        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}

