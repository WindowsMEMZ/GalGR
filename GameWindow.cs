using Microsoft.DirectX.DirectSound;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GalGR
{
    public partial class GameWindow : Form
    {
        private string bgPicPath = "";
        private string bgMusicPath = "";
        private Int64[] bgPicPart;
        private string[] bgPicPartPath;
        private Int64[] bgMusicPart;
        private string[] bgMusicPartPath;
        public static Int64 nowIndex = Form1.loadIndex;
        private Int64 lastIndex;
        private string[] dialogTexts;
        private string[] cNameTexts;
        private bool[] voiceStatus;
        private string[] voicePaths;
        private bool isPlaying = true;
        private bool isBgPicChanged = true;
        private string encPath;

        SoundPlayer sp = new SoundPlayer();

        public GameWindow(string encFilePath = "nil")
        {
            //初始化
            this.StartPosition = FormStartPosition.CenterScreen;
            encPath = encFilePath;
            InitializeComponent();
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {
            try
            {
                //读取并设置基本信息
                this.Text = INIHelper.Read("Base", "name", "", "./data/mConfig.wggproj");
                string[] wSize = INIHelper.Read("Base", "size", "1920|1080", "./data/mConfig.wggproj").Split('|');
                this.Size = new Size(int.Parse(wSize[0]), int.Parse(wSize[1]));
                bgPicPart = Array.ConvertAll(INIHelper.Read("Base", "bgPicPart", "0|1", "./data/gConfig.wggproj").Split('|'), Int64.Parse);
                bgPicPartPath = INIHelper.Read("Base", "bgPicPartPath", "./assets/default.png", "./data/gConfig.wggproj").Split('|');
                bgMusicPart = Array.ConvertAll(INIHelper.Read("Base", "bgMusicPart", "0|1", "./data/gConfig.wggproj").Split('|'), Int64.Parse);
                bgMusicPartPath = INIHelper.Read("Base", "bgMusicPartPath", "./assets/default.wav", "./data/gConfig.wggproj").Split('|');
                dialogTexts = INIHelper.Read("Dialog", "texts", "VGVzdDE=|VGVzdDI=", "./data/gConfig.wggproj").Split('|');
                cNameTexts = INIHelper.Read("Character", "names", "TlRlc3Qx|TlRlc3Qy", "./data/gConfig.wggproj").Split('|');
                for (int i = 0; i < dialogTexts.Length; i++)
                {
                    dialogTexts[i] = Base64Convert(dialogTexts[i], false);
                    cNameTexts[i] = Base64Convert(cNameTexts[i], false);
                }
                voicePaths = INIHelper.Read("Voice", "paths", "./assets/default.wav|./assets/default.wav", "./data/gConfig.wggproj").Split('|');
                voiceStatus = Array.ConvertAll(INIHelper.Read("Voice", "status", "false|false", "./data/gConfig.wggproj").Split('|'), bool.Parse);
                lastIndex = Int64.Parse(INIHelper.Read("Base", "lastIndex", "1", "./data/gConfig.wggproj"));
                if (!bool.Parse(INIHelper.Read("Base", "isUsingBGM", "false", "./data/gConfig.wggproj")))
                {
                    bgMusicPath = "nil";
                }
                if (Form1.isLoadingAArch)
                {
                    bool isDone = false;
                    for (int i = 0; i < bgPicPart.Length - 1; i++)
                    {
                        Int64 l = bgPicPart[i];
                        Int64 h = bgPicPart[i + 1];
                        if (nowIndex > l && nowIndex <= h)
                        {
                            bgPicPath = bgPicPartPath[i];
                            isDone = true;
                        }
                    }
                    if (!isDone)
                    {
                        Debug.WriteLine($"Get {nowIndex} Background Picture false");
                    }
                    if (bgMusicPath != "nil")
                    {
                        isDone = false;
                        for (int i = 0; i < bgMusicPart.Length - 1; i++)
                        {
                            Int64 l = bgMusicPart[i];
                            Int64 h = bgMusicPart[i + 1];
                            if (nowIndex > l && nowIndex <= h)
                            {
                                bgMusicPath = bgMusicPartPath[i];
                                isDone = true;
                            }
                        }
                        if (!isDone)
                        {
                            Debug.WriteLine($"Get {nowIndex} Background Music false");
                        }
                    }
                    label1.Text = cNameTexts[nowIndex];
                    label2.Text = dialogTexts[nowIndex];
                }
                else
                {
                    nowIndex = 0;
                    bgPicPath = INIHelper.Read("Base", "firstBgPicPath", "./assets/default.png", "./data/gConfig.wggproj");
                    if (bgMusicPath != "nil")
                    {
                        bgMusicPath = INIHelper.Read("Base", "firstBgMusicPath", "./assets/default.wav", "./data/gConfig.wggproj");
                    }
                }
                //初始化组件
                pictureBox2.Parent = pictureBox1;
                label1.Parent = pictureBox2;
                label1.BackColor = Color.Transparent;
                label1.Location = new Point(100, 14);
                label2.Parent = pictureBox2;
                label2.BackColor = Color.Transparent;
                label2.Location = new Point(100, 70);
                //初始化内容
                if (!Form1.isLoadingAArch)
                {
                    label1.Text = cNameTexts[0];
                    label2.Text = dialogTexts[0];
                }
                timer1.Enabled = true;

                if (encPath != "nil")
                {
                    for (int i = 0; i < bgPicPartPath.Length; i++)
                    {
                        bgPicPartPath[i] = bgPicPartPath[i].Replace("./assets/", encPath);
                    }
                    for (int i = 0; i < bgMusicPartPath.Length; i++)
                    {
                        bgMusicPartPath[i] = bgMusicPartPath[i].Replace("./assets/", encPath);
                    }
                    for (int i = 0; i < voicePaths.Length; i++)
                    {
                        voicePaths[i] = voicePaths[i].Replace("./assets/", encPath);
                    }
                }
            }
            catch (Exception ex)
            {
                ThrowErr(ex);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isBgPicChanged)
            {
                FileStream stream = new FileStream(bgPicPath, FileMode.Open, FileAccess.Read);
                pictureBox1.Image = Image.FromStream(stream);
                isBgPicChanged = false;
            }
            if (bgMusicPath != "nil" && isPlaying == false)
            {
                sp.SoundLocation = bgMusicPath;
                sp.Stop();
                sp.PlayLooping();
                isPlaying = true;
            }
        }

        private void GameWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            NextDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            NextDialog();
        }

        private void NextDialog()
        {
            try
            {
                if (nowIndex != lastIndex)
                {
                    nowIndex++;
                    bool isDone = false;
                    for (int i = 0; i < bgPicPart.Length - 1; i++)
                    {
                        if (nowIndex == bgPicPart[i])
                        {
                            bgPicPath = bgPicPartPath[i];
                            isDone = true;
                            isBgPicChanged = true;
                            break;
                        }
                    }
                    if (!isDone)
                    {
                        Debug.WriteLine($"Get {nowIndex} Background Picture false");
                    }
                    if (bgMusicPath != "nil")
                    {
                        isDone = false;
                        for (int i = 0; i < bgMusicPart.Length - 1; i++)
                        {
                            if (nowIndex == bgMusicPart[i])
                            {
                                bgMusicPath = bgMusicPartPath[i];
                                isDone = true;
                                isPlaying = false;
                                break;
                            }
                        }
                        if (!isDone)
                        {
                            Debug.WriteLine($"Get {nowIndex} Background Music false");
                        }
                    }
                    if (dialogTexts.Length > nowIndex)
                    {
                        label1.Text = cNameTexts[nowIndex];
                        label2.Text = dialogTexts[nowIndex];
                    }
                    else
                    {
                        Debug.WriteLine("End or Error");
                        CloseGame(2);
                        return;
                    }
                    if (voiceStatus[nowIndex])
                    {
                        Device secDev = new Device();
                        secDev.SetCooperativeLevel(this, CooperativeLevel.Normal);
                        SecondaryBuffer secBuffer = new SecondaryBuffer(voicePaths[nowIndex], secDev);
                        secBuffer.Play(0, BufferPlayFlags.Default);
                    }
                }
                else
                {
                    CloseGame(2);
                }
            }
            catch (Exception ex)
            {
                ThrowErr(ex);
            }
        }

        /// <summary>
        /// Base64编解码
        /// </summary>
        /// <param name="text">原文本或Base64文本</param>
        /// <param name="isEncode">转换反向，true为编码到Base64</param>
        /// <returns></returns>
        private string Base64Convert(string text, bool isEncode)
        {
            if (isEncode)
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
            }
            else
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(text));
            }
        }

        /// <summary>
        /// 停止游戏
        /// </summary>
        /// <param name="type">类型，1为关闭整个软件，2为回到主菜单</param>
        private void CloseGame(uint type)
        {
            switch (type)
            {
                case 1:
                    Application.Exit();
                    break;
                case 2:
                    sp.Stop();
                    Form1 w = new Form1();
                    w.Show();
                    this.Hide();
                    break;
                default:
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定退出游戏吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CloseGame(1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定返回主菜单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CloseGame(2);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Saves w = new Saves(true);
            if (w.ShowDialog() == DialogResult.OK)
            {
                nowIndex = w.NowIndex - 1;
                NextDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Saves w = new Saves(false, (int)nowIndex, label1.Text, label2.Text);
            w.ShowDialog();
        }

        private void ThrowErr(Exception ex)
        {
            if (bool.Parse(INIHelper.Read("Base", "status", "false", "./GalGR.gds")))
            {
                INIHelper.Write("Base", "exitCode", "1", "./GalGR.gds");
                INIHelper.Write("Error", "detail", ex.ToString(), "./GalGR.gds");
                INIHelper.Write("Error", "index", nowIndex.ToString(), "./GalGR.gds");
                Application.Exit();
            }
        }
    }
}
