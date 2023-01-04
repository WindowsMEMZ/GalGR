using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalGR
{
    public partial class Form1 : Form
    {
        private string encPath = "nil";

        public static bool isLoadingAArch = false;
        public static int loadIndex = -1;

        SoundPlayer sp = new SoundPlayer();

        public Form1()
        {
            //初始化
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //读取并设置基本信息
            string name = INIHelper.Read("Base", "name", "", "./data/mConfig.wggproj");
            this.Text = name;
            string[] wSize = INIHelper.Read("Base", "size", "1920|1080", "./data/mConfig.wggproj").Split('|');
            this.Size = new Size(int.Parse(wSize[0]), int.Parse(wSize[1]));
            bool mainPicStatus = bool.Parse(INIHelper.Read("Base", "isShowMainPic", "true", "./data/mConfig.wggproj"));
            if (!mainPicStatus)
            {
                pictureBox1.Visible = false;
            }
            else
            {
                string mainPicPath = INIHelper.Read("Base", "mainPicPath", "./assets/default.png", "./data/mConfig.wggproj");
                FileStream stream = new FileStream(mainPicPath, FileMode.Open, FileAccess.Read);
                pictureBox1.Image = Image.FromStream(stream);
            }
            bool mainBGMStatus = bool.Parse(INIHelper.Read("Base", "isPlayMainBGM", "false", "./data/mConfig.wggproj"));
            if (mainBGMStatus)
            {
                string mainSoundPath = INIHelper.Read("Base", "mainSoundPath", "", "./data/mConfig.wggproj");
                sp.SoundLocation = mainSoundPath;
                sp.PlayLooping();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameWindow w = new GameWindow(encPath);
            w.Show();
            
            this.Hide();
            sp.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Saves w = new Saves(true);
            if (w.ShowDialog() == DialogResult.OK)
            {
                loadIndex = w.NowIndex;
                button1_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private bool unzipStatus = false;
        private void Form1_Shown(object sender, EventArgs e)
        {
            bool encStatus = bool.Parse(INIHelper.Read("Base", "isEnc", "false", "./data/gConfig.wggproj"));
            if (encStatus)
            {
                Thread t = new Thread(DearchiveAssets);
                t.Start();
                timer1.Enabled = true;
            }
        }
        void DearchiveAssets()
        {
            string path = Path.GetTempPath() + "ggaall/";
            Directory.CreateDirectory(path.TrimEnd('/'));
            R7z archiver = new R7z();
            archiver.Decompression("./assets.gef", path, "GalgameFromGalGM1145141919810%");
            encPath = path.TrimEnd('/') + "_/";
            unzipStatus = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (unzipStatus)
            {
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                label1.Visible = false;
            }
            else
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                label1.Visible = true;
            }
        }
    }
}
