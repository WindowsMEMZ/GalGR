using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalGR
{
    public partial class Saves : Form
    {
        private bool isReadSaving;
        private bool[] saveStatus;
        private int index;
        private string nPerson;
        private string nDialog;

        public Saves(bool isRead, int nowIndex = -1, string nowPerson = "nil", string nowDialog = "nil")
        {
            isReadSaving = isRead;
            if (nowIndex != -1)
            {
                index = nowIndex;
                nPerson = nowPerson;
                nDialog = nowDialog;
            }
            InitializeComponent();
        }

        public int NowIndex
        {
            get { return index; }
        }

        private void Saves_Load(object sender, EventArgs e)
        {
            saveStatus = Array.ConvertAll(INIHelper.Read("Base", "saveStatus", "false|false|false|false|false|false|false|false", "./data/Save.wggproj").Split('|'), bool.Parse);
            for (int i = 0; i < saveStatus.Length; i++)
            {
                if (saveStatus[i])
                {
                    string p = INIHelper.Read("Data", $"person{i + 1}", "", "./data/Save.wggproj");
                    string d = INIHelper.Read("Data", $"dialog{i + 1}", "", "./data/Save.wggproj");
                    string t = INIHelper.Read("Data", $"time{i + 1}", "", "./data/Save.wggproj");
                    if (d.Length > 12)
                    {
                        d = d.Substring(0, 11) + "...";
                    }
                    switch (i)
                    {
                        case 0:
                            button1.Text = $"存档1\n{p}：{d}\n{t}";
                            break;
                        case 1:
                            button2.Text = $"存档2\n{p}：{d}\n{t}";
                            break;
                        case 2:
                            button3.Text = $"存档3\n{p}：{d}\n{t}";
                            break;
                        case 3:
                            button4.Text = $"存档4\n{p}：{d}\n{t}";
                            break;
                        case 4:
                            button5.Text = $"存档5\n{p}：{d}\n{t}";
                            break;
                        case 5:
                            button6.Text = $"存档6\n{p}：{d}\n{t}";
                            break;
                        case 6:
                            button7.Text = $"存档7\n{p}：{d}\n{t}";
                            break;
                        case 7:
                            button8.Text = $"存档8\n{p}：{d}\n{t}";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (isReadSaving)
                    {
                        switch (i)
                        {
                            case 0:
                                button1.Enabled = false;
                                break;
                            case 1:
                                button2.Enabled = false;
                                break;
                            case 2:
                                button3.Enabled = false;
                                break;
                            case 3:
                                button4.Enabled = false;
                                break;
                            case 4:
                                button5.Enabled = false;
                                break;
                            case 5:
                                button6.Enabled = false;
                                break;
                            case 6:
                                button7.Enabled = false;
                                break;
                            case 7:
                                button8.Enabled = false;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isReadSaving)
            {
                index = int.Parse(INIHelper.Read("Data", "index1", "1", "./data/Save.wggproj"));
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                INIHelper.Write("Data", "index1", index.ToString(), "./data/Save.wggproj");
                saveStatus[0] = true;
                INIHelper.Write("Base", "saveStatus", $"{saveStatus[0]}|{saveStatus[1]}|{saveStatus[2]}|{saveStatus[3]}|{saveStatus[4]}|{saveStatus[5]}|{saveStatus[6]}|{saveStatus[7]}", "./data/Save.wggproj");
                INIHelper.Write("Data", "person1", nPerson, "./data/Save.wggproj");
                INIHelper.Write("Data", "dialog1", nDialog, "./data/Save.wggproj");
                INIHelper.Write("Data", "time1", DateTime.Now.ToString("g"), "./data/Save.wggproj");
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isReadSaving)
            {
                index = int.Parse(INIHelper.Read("Data", "index2", "1", "./data/Save.wggproj"));
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                INIHelper.Write("Data", "index2", index.ToString(), "./data/Save.wggproj");
                saveStatus[0] = true;
                INIHelper.Write("Base", "saveStatus", $"{saveStatus[0]}|{saveStatus[1]}|{saveStatus[2]}|{saveStatus[3]}|{saveStatus[4]}|{saveStatus[5]}|{saveStatus[6]}|{saveStatus[7]}", "./data/Save.wggproj");
                INIHelper.Write("Data", "person2", nPerson, "./data/Save.wggproj");
                INIHelper.Write("Data", "dialog2", nDialog, "./data/Save.wggproj");
                INIHelper.Write("Data", "time2", DateTime.Now.ToString("g"), "./data/Save.wggproj");
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (isReadSaving)
            {
                index = int.Parse(INIHelper.Read("Data", "index3", "1", "./data/Save.wggproj"));
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                INIHelper.Write("Data", "index3", index.ToString(), "./data/Save.wggproj");
                saveStatus[0] = true;
                INIHelper.Write("Base", "saveStatus", $"{saveStatus[0]}|{saveStatus[1]}|{saveStatus[2]}|{saveStatus[3]}|{saveStatus[4]}|{saveStatus[5]}|{saveStatus[6]}|{saveStatus[7]}", "./data/Save.wggproj");
                INIHelper.Write("Data", "person3", nPerson, "./data/Save.wggproj");
                INIHelper.Write("Data", "dialog3", nDialog, "./data/Save.wggproj");
                INIHelper.Write("Data", "time3", DateTime.Now.ToString("g"), "./data/Save.wggproj");
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (isReadSaving)
            {
                index = int.Parse(INIHelper.Read("Data", "index4", "1", "./data/Save.wggproj"));
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                INIHelper.Write("Data", "index4", index.ToString(), "./data/Save.wggproj");
                saveStatus[0] = true;
                INIHelper.Write("Base", "saveStatus", $"{saveStatus[0]}|{saveStatus[1]}|{saveStatus[2]}|{saveStatus[3]}|{saveStatus[4]}|{saveStatus[5]}|{saveStatus[6]}|{saveStatus[7]}", "./data/Save.wggproj");
                INIHelper.Write("Data", "person4", nPerson, "./data/Save.wggproj");
                INIHelper.Write("Data", "dialog4", nDialog, "./data/Save.wggproj");
                INIHelper.Write("Data", "time4", DateTime.Now.ToString("g"), "./data/Save.wggproj");
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (isReadSaving)
            {
                index = int.Parse(INIHelper.Read("Data", "index5", "1", "./data/Save.wggproj"));
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                INIHelper.Write("Data", "index5", index.ToString(), "./data/Save.wggproj");
                saveStatus[0] = true;
                INIHelper.Write("Base", "saveStatus", $"{saveStatus[0]}|{saveStatus[1]}|{saveStatus[2]}|{saveStatus[3]}|{saveStatus[4]}|{saveStatus[5]}|{saveStatus[6]}|{saveStatus[7]}", "./data/Save.wggproj");
                INIHelper.Write("Data", "person5", nPerson, "./data/Save.wggproj");
                INIHelper.Write("Data", "dialog5", nDialog, "./data/Save.wggproj");
                INIHelper.Write("Data", "time5", DateTime.Now.ToString("g"), "./data/Save.wggproj");
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (isReadSaving)
            {
                index = int.Parse(INIHelper.Read("Data", "index6", "1", "./data/Save.wggproj"));
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                INIHelper.Write("Data", "index6", index.ToString(), "./data/Save.wggproj");
                saveStatus[0] = true;
                INIHelper.Write("Base", "saveStatus", $"{saveStatus[0]}|{saveStatus[1]}|{saveStatus[2]}|{saveStatus[3]}|{saveStatus[4]}|{saveStatus[5]}|{saveStatus[6]}|{saveStatus[7]}", "./data/Save.wggproj");
                INIHelper.Write("Data", "person6", nPerson, "./data/Save.wggproj");
                INIHelper.Write("Data", "dialog6", nDialog, "./data/Save.wggproj");
                INIHelper.Write("Data", "time6", DateTime.Now.ToString("g"), "./data/Save.wggproj");
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (isReadSaving)
            {
                index = int.Parse(INIHelper.Read("Data", "index7", "1", "./data/Save.wggproj"));
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                INIHelper.Write("Data", "index7", index.ToString(), "./data/Save.wggproj");
                saveStatus[0] = true;
                INIHelper.Write("Base", "saveStatus", $"{saveStatus[0]}|{saveStatus[1]}|{saveStatus[2]}|{saveStatus[3]}|{saveStatus[4]}|{saveStatus[5]}|{saveStatus[6]}|{saveStatus[7]}", "./data/Save.wggproj");
                INIHelper.Write("Data", "person7", nPerson, "./data/Save.wggproj");
                INIHelper.Write("Data", "dialog7", nDialog, "./data/Save.wggproj");
                INIHelper.Write("Data", "time7", DateTime.Now.ToString("g"), "./data/Save.wggproj");
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (isReadSaving)
            {
                index = int.Parse(INIHelper.Read("Data", "index8", "1", "./data/Save.wggproj"));
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                INIHelper.Write("Data", "index8", index.ToString(), "./data/Save.wggproj");
                saveStatus[0] = true;
                INIHelper.Write("Base", "saveStatus", $"{saveStatus[0]}|{saveStatus[1]}|{saveStatus[2]}|{saveStatus[3]}|{saveStatus[4]}|{saveStatus[5]}|{saveStatus[6]}|{saveStatus[7]}", "./data/Save.wggproj");
                INIHelper.Write("Data", "person8", nPerson, "./data/Save.wggproj");
                INIHelper.Write("Data", "dialog8", nDialog, "./data/Save.wggproj");
                INIHelper.Write("Data", "time8", DateTime.Now.ToString("g"), "./data/Save.wggproj");
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
