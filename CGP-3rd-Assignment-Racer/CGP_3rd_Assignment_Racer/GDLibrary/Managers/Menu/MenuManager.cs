using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GDGame;

namespace GDLibrary
{
    public class MenuManager: DrawableGameComponent  
    {
        private Main game;
        private SpriteFont menuItemFont;

        private Texture2D[] menuTextures;
        private Rectangle textureRectangle;
        private Color textureColor;

        private List<MenuItem> menuItemList;
        private MenuItem menuResume, menuExit, menuAudio;
        private MenuItem menuVolumeUp, menuVolumeDown, menuBack;

        private MenuItem menuExitYes, menuExitNo;

        private int currentMenuTextureIndex = 0; //0 = main, 1 = volume
        private bool bPaused = false;

        //temp vars
        MenuItem menuItem = null;
        private Texture2D menuItemTexture;


        public MenuManager(Main game, SpriteFont font, Texture2D[] menuTextures, Color textureColor, 
                                        Texture2D menuItemTexture,  Integer2 textureBorderPadding)
            : base(game)
        {
            this.game = game;
            this.menuItemFont = font;
            this.menuTextures = menuTextures;
            this.menuItemTexture = menuItemTexture;

            this.textureRectangle = game.ScreenManager.ScreenBounds;
            this.textureRectangle.Inflate(textureBorderPadding.X, textureBorderPadding.Y);
            this.textureColor = textureColor;

            //stores all menu item (e.g. Save, Resume, Exit) objects
            this.menuItemList = new List<MenuItem>();
        }
        public override void Initialize()
        {
            this.bPaused = false;                    //unpause menu
            this.game.SpriteManager.Paused = !this.bPaused;  //pause game


            //add the basic items
            InitialiseOptions();

            //show play, audio, exit menu
            ShowMain();

            base.Initialize();
        }

        private void InitialiseOptions()
        {
            //main menu elements
            this.menuResume = new MenuItem(this.menuItemFont, MenuData.MenuTextResume,
                new Vector2(50, 50), MenuData.MenuColorInactiveText, MenuData.MenuColorActiveText);          

            this.menuAudio = (MenuItem)this.menuResume.Clone();
            this.menuAudio.Text = MenuData.MenuTextAudio;
            this.menuAudio.Position = new Vector2(50, 100);

            this.menuExit = (MenuItem)this.menuResume.Clone();
            this.menuExit.Text = MenuData.MenuTextExit;
            this.menuExit.Position = new Vector2(50, 150);

            //audio menu elements
            this.menuVolumeUp = (MenuItem)this.menuResume.Clone();
            this.menuVolumeUp.Text = MenuData.MenuTextAudioUp;
            this.menuVolumeUp.Position = this.menuResume.Position;

            this.menuVolumeDown = (MenuItem)this.menuResume.Clone();
            this.menuVolumeDown.Text = MenuData.MenuTextAudioDown;
            this.menuVolumeDown.Position = this.menuAudio.Position;

            //common elements - back
            this.menuBack = (MenuItem)this.menuResume.Clone();
            this.menuBack.Text = MenuData.MenuTextBackToMain;
            this.menuBack.Position = this.menuExit.Position;

            //yes
            this.menuExitYes = (MenuItem)this.menuResume.Clone();
            this.menuExitYes.Text = MenuData.MenuTextExitYes;
            Vector2 textSize = menuItemFont.MeasureString(this.menuExitYes.Text);
            this.menuExitYes.Position = new Vector2(452, 384);
          
            //no
            this.menuExitNo = (MenuItem)this.menuResume.Clone();
            this.menuExitNo.Text = MenuData.MenuTextExitNo;
            textSize = menuItemFont.MeasureString(this.menuExitNo.Text);
            this.menuExitNo.Position = new Vector2(572, 384);
        }
                                                                                                                                      
        public void Add(MenuItem theMenuItem) 
        {
            this.menuItemList.Add(theMenuItem);
        }

        public void Remove(MenuItem theMenuItem)
        {
            this.menuItemList.Remove(theMenuItem);
        }

        public void Clear()
        {
            if(this.menuItemList.Count > 0)
                this.menuItemList.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            if (!bPaused)
            {
                for (int i = 0; i < menuItemList.Count; i++)
                {
                    menuItem = menuItemList[i];

                    //call the update() to test for collisions with the mouse
                    if (menuItem.Update(gameTime, game.MouseManager))
                        MenuAction(menuItem);
                }

                base.Update(gameTime);
            }
            else
            {
                //if the player presses P during the game AND the menu is not visible, then show the menu
                if (this.game.KeyboardManager.IsFirstKeyPress(MenuData.MenuKeyShowMenu)
                    && this.bPaused)
                {
                    this.bPaused = false;                    //unpause menu
                    this.game.SpriteManager.Paused = !this.bPaused;  //pause game
                }
            }
        }
        Color backColor = Color.White;
        public override void Draw(GameTime gameTime) 
        {
            if (!bPaused)
            {
                this.game.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

                //draw whatever background we expect to see based on what menu or sub-menu we are viewing
                this.game.SpriteBatch.Draw(this.menuTextures[currentMenuTextureIndex], this.textureRectangle, this.textureColor);

                //draw the text on top of the background
                for (int i = 0; i < menuItemList.Count; i++)
                {
                    menuItem = menuItemList[i];

                    if (menuItem.IsMouseOver)
                        backColor = MenuData.MenuColorActiveTexture;
                    else
                        backColor = MenuData.MenuColorInactiveTexture;

                    //menu item background image
                    this.game.SpriteBatch.Draw(this.menuItemTexture,
                                    menuItem.Bounds, backColor);

                    //menu item text
                    this.game.SpriteBatch.DrawString(menuItemFont, menuItem.Text, 
                                                    menuItem.Position, menuItem.Color);
                }

                this.game.SpriteBatch.End();

                base.Draw(gameTime);
            }
        }

        //perform whatever actions are listed on the menu
        private void MenuAction(MenuItem menuItem)
        {
            if(menuItem.Text.Equals(MenuData.MenuTextResume))
                DoResume();
            else if (menuItem.Text.Equals(MenuData.MenuTextExit))
                ShowYesNo();
            else if (menuItem.Text.Equals(MenuData.MenuTextAudio))
                ShowAudio();
            else if (menuItem.Text.Equals(MenuData.MenuTextBackToMain))
                ShowMain();
            else if (menuItem.Text.Equals(MenuData.MenuTextExitYes))
                DoExit();
            else if (menuItem.Text.Equals(MenuData.MenuTextExitNo))
                ShowMain();
            //more will be added based on the options present
        }

        private void ShowYesNo()
        {
            Clear();

            Add(menuExitYes);
            Add(menuExitNo);
            this.currentMenuTextureIndex = MenuData.IndexAudioMenuTexture;
        }

        private void ShowMain()
        {
            Clear();

            Add(menuResume);
            Add(menuAudio);
            Add(menuExit);

            //main screen background texture
            this.currentMenuTextureIndex = MenuData.IndexMainMenuTexture;
        }

    
        private void ShowAudio()
        {
            Clear();

            Add(menuVolumeUp);
            Add(menuVolumeDown);
            Add(menuBack);

            //main screen background texture
            this.currentMenuTextureIndex = MenuData.IndexAudioMenuTexture;
        }

        private void DoResume()
        {
            this.bPaused = true;                    //unpause menu
            this.game.SpriteManager.Paused = !this.bPaused;  //pause game
        }

        private void DoExit()
        {
            this.game.Exit();
        }

    }
}
