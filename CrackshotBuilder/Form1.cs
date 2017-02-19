using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.IO.Compression;

namespace CrackshotBuilder
{
    public partial class Form1 : Form
    {
        public string filepath = Application.StartupPath + "/CrackshotFiles";
        //public string filepath = "C:/Users/Oska/Desktop/CrackshotBuilder Setup/CrackshotFiles";
        public string currentversion = Properties.Settings.Default.Version;
        //
        string doublespacebar = "        ";
        string spacebar = "    ";
        private TTPHelper ttphelper;
        public Form1()
        {
            InitializeComponent();
            // Version
            S_U_CVersion.Text = currentversion;
            // TTP Helper POPUP
            if (Properties.Settings.Default.TTP == true)
            {
                ttp.CheckState = CheckState.Checked;
                TTPHelper frm = new TTPHelper();
                frm.TopMost = true;
                frm.Show();
                ttphelper = frm;
            }
        }
        public void AddSound(string title, string sound)
        {
            Control[] tbxs = this.Controls.Find(title, true);
            foreach (ListBox c in tbxs)
            {
                c.Items.Add(sound);
            }
        }
        public void OpenSounds(string title)
        {
            Sounds sound = new Sounds(this);
            sound.Text = title;
            sound.ShowDialog();
        }
        private void Loadpics()
        {
            var pics = Directory.EnumerateFiles(filepath + "/Resource/uipics/", "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".png") || s.EndsWith(".jpg") || s.EndsWith(".jpeg"));
            foreach (string picfile in pics)
            {
                string picfilename = Path.GetFileNameWithoutExtension(filepath + "/Resource/uipics/" + picfile);
                string picfileextension = Path.GetExtension(filepath + "/Resource/uipics/" + picfile);
                Control[] clist = Controls.Find(picfilename + "pic", true);
                foreach (PictureBox c in clist)
                {
                    c.Image = Image.FromFile(filepath + "/Resource/uipics/" + picfilename + picfileextension);
                    c.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }
        private void loadETT()
        {
            string[] lines = File.ReadAllLines(filepath + "/Resource/entity/entity.yml");
            foreach (string text in lines)
            {
                Control[] clist = Controls.Find(text + "ett", true);
                foreach (ComboBox c in clist)
                {
                    string[] entities = File.ReadAllLines(filepath + "/entity.txt");
                    foreach (string entity in entities)
                    {
                        c.Items.Add(entity);
                    }
                }
            }
        }
        private void loadsound()
        {
            string[] lang = File.ReadAllLines(filepath + "/Resource/lang/sounds.yml");
            for (int j = 1; ; j++)
            {
                Control[] clist = Controls.Find("AS" + j.ToString(), true);
                if (clist.Length < 1)
                {
                    break;
                }
                foreach (Control c in clist)
                {
                    string[] newtext = lang[4].Split(':');
                    c.Text = newtext[1];
                    c.Click += delegate
                    {
                        OpenSounds(c.Name + "_Sound");
                    };
                }
            }
            for (int i = 1; ; i++)
            {
                Control[] clist = Controls.Find("RS" + i.ToString(), true);
                if (clist.Length < 1)
                {
                    break;
                }
                foreach (Control c in clist)
                {
                    string[] newtext = lang[5].Split(':');
                    c.Text = newtext[1];
                    c.Click += delegate
                    {
                        Control[] con = Controls.Find(c.Name.Replace('R', 'A') + "_Sound", true);
                        foreach (ListBox cl in con)
                        {
                            if (cl.SelectedItem != null)
                            {
                                cl.Items.Remove(cl.SelectedItem);
                            }
                        }
                    };
                }
            }
        }
        private void LoadToolTip()
        {
            string[] lines = File.ReadAllLines(filepath + "/Resource/lang/tooltip.yml");
            foreach (string text in lines)
            {
                string[] texts = text.Split('.');
                Control[] clist = Controls.Find(texts[0] + "label", true);
                foreach (Control c in clist)
                {
                    string[] ttplist = texts[1].Split(':');
                    ToolTip ttp = new ToolTip();
                    ttp.IsBalloon = true;
                    ttp.ToolTipTitle = c.Text;
                    ttp.ToolTipIcon = ToolTipIcon.Info;
                    ttp.Popup += delegate
                    {
                        TTPHelper fr = new TTPHelper();
                        if (Application.OpenForms.OfType<TTPHelper>().FirstOrDefault() == null)
                        {
                            fr.TopMost = true;
                            fr.Show();
                            ttphelper = fr;
                        }
                        ttphelper.TTP_LabelSet = c.Text;
                        ttphelper.TTP_TextSet = ttplist[1].Replace('|', '\n');
                    };
                    ttp.SetToolTip(c, ttplist[1].Replace('|', '\n'));
                }
            }

        }
        private void LoadLangFile()
        {
            string[] lines = File.ReadAllLines(filepath + "/Resource/lang/label.yml");
            foreach (string text in lines)
            {
                string[] texts = text.Split(':');
                Control[] clist = Controls.Find(texts[0] + "label", true);
                foreach (Control c in clist)
                {
                    c.Text = texts[1];
                }
            }
        }
        private void LoadIds()
        {
            string[] lines = File.ReadAllLines(filepath + "/Resource/idbox/id.yml");
            foreach (string text in lines)
            {
                Control[] clist = Controls.Find(text + "ids", true);
                foreach (ComboBox c in clist)
                {
                    string[] idslist = File.ReadAllLines(filepath + "/ids.txt");
                    foreach (string ids in idslist)
                    {
                        c.Items.Add(ids);
                    }
                }
            }
        }
        private void LoadEnchantment()
        {
            string[] lines = File.ReadAllLines(filepath + "/Resource/enchantment/enchantment.yml");
            foreach (string text in lines)
            {
                Control[] clist = Controls.Find(text + "enchant", true);
                foreach (ComboBox c in clist)
                {
                    string[] enchants = File.ReadAllLines(filepath + "/enchantment.txt");
                    foreach (string enchant in enchants)
                    {
                        c.Items.Add(enchant);
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadLangFile();
            LoadEnchantment();
            LoadIds();
            Loadpics();
            LoadToolTip();
            loadPotion();
            loadS_Box();
            loadG_FA_Box();
            loadsound();
            loadETT();
            loadPartBox();
            loadED();
            loadBT();
        }
        private void loadBT()
        {
            string[] idlist = File.ReadAllLines(filepath + "/ids.txt");
            foreach (string block in idlist)
            {
                if (block.Contains("256"))
                {
                    break;
                }
                I_A_BTBox.Items.Add(block);
            }
        }
        private void loadED()
        {
            string[] texts = "landmine/remote/trap/itembomb".Split('/');
            foreach (string t in texts)
            {
                I_ED_DeviceTBox.Items.Add(t);
            }
        }
        public void loadPotion()
        {
            string[] lines = File.ReadAllLines(filepath + "/Resource/potion/potion.yml");
            foreach (string text in lines)
            {
                Control[] clist = Controls.Find(text + "potion", true);
                foreach (ComboBox c in clist)
                {
                    string[] enchants = File.ReadAllLines(filepath + "/potion.txt");
                    foreach (string enchant in enchants)
                    {
                        c.Items.Add(enchant);
                    }
                }
            }
        }
        public void loadPartBox()
        {
            string[] lines = File.ReadAllLines(filepath + "/Resource/particle/partbox.yml");
            foreach (string text in lines)
            {
                Control[] clist = Controls.Find(text + "part", true);
                foreach (ComboBox c in clist)
                {
                    string[] particles = "smoke/lightning/explosion/potion_splash/block_break/flames".Split('/');
                    foreach (string part in particles)
                    {
                        c.Items.Add(part);
                    }
                    Button add = (Button)Controls.Find(text + "Alabel", true).First();
                    Button remove = (Button)Controls.Find(text + "Rlabel", true).First();
                    ComboBox c2l = (ComboBox)Controls.Find(text + "part2", true).First();
                    TextBox c3l = (TextBox)Controls.Find(text + "part3", true).First();
                    ListBox lb = (ListBox)Controls.Find(text + "partls", true).First();
                    add.Click += delegate
                    {
                        if (c.Text != "")
                        {
                            if (c3l.Visible == false && c2l.Visible == false)
                            {
                                lb.Items.Add(c.Text);
                            }
                            if (c2l.Visible == true)
                            {
                                lb.Items.Add(c.Text + "-" + c2l.Text);
                            }
                            if (c3l.Visible == true)
                            {
                                lb.Items.Add(c.Text + "-" + c3l.Text);
                            }
                        }
                    };
                    remove.Click += delegate
                    {
                        if (lb.SelectedItem != null)
                        {
                            lb.Items.Remove(lb.SelectedItem);
                        }
                    };
                    c.SelectedIndexChanged += delegate
                    {
                        c2l.Visible = false;
                        c3l.Visible = false;
                        c2l.Items.Clear();
                        switch (c.Text)
                        {
                            case "potion_splash":
                                c2l.Visible = true;
                                string[] potionnumlist = File.ReadAllLines(filepath + "/potion.txt");
                                foreach (string pot in potionnumlist)
                                {
                                    c2l.Items.Add(pot.Split(':').Last());
                                }
                                break;
                            case "block_break":
                                c2l.Visible = true;
                                string[] idlist = File.ReadAllLines(filepath + "/ids.txt");
                                foreach (string block in idlist)
                                {
                                    if (block.Split('-').First() != "256")
                                    {
                                        if (!c2l.Items.Contains(block.Split('-').First()))
                                        {
                                            c2l.Items.Add(block.Split('-').First());
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                break;
                            case "flames":
                                c3l.Visible = true;
                                break;
                        }
                    };
                }
            }
        }
        public void loadG_FA_Box()
        {
            string[] all = "slide/bolt/lever/pump/break/revolver".Split('/');
            foreach (string box in all)
            {
                G_FA_TypeBox.Items.Add(box);
            }
        }
        public void loadS_Box()
        {
            string[] all = "snowball/arrow/egg/grenade/flare/fireball/witherskull/energy/splash".Split('/');
            foreach (string box in all)
            {
                S_projectileType.Items.Add(box);
            }
        }
        public string getListItem(ListBox c)
        {
            string items = "";
            foreach (string text in c.Items)
            {
                items += text + ",";
            }
            if (items.Length > 0)
            {
                items = items.Remove(items.Length - 1);
            }
            return items;
        }
        public string getTrueFalse(CheckBox c)
        {
            if (c.CheckState.ToString() == "Checked")
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
        private void buildBtn_Click(object sender, EventArgs e)
        {
            csyamlbox.ResetText();
            // Weapon Title
            csyamlbox.AppendText(II_GunID.Text + ":");
            // Item Information Setting
            csyamlbox.AppendText(Environment.NewLine + spacebar + "Item_Information:");
            if (II_Name.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Item_Name: '" + II_Name.Text + "'");
            }
            if (II_ItemIDids.Text != "")
            {
                string[] check = II_ItemIDids.Text.Split('-');
                if (check[1] == "0")
                {
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Item_Type: " + II_ItemIDids.Text.Replace("-0", ""));
                }
                else
                {
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Item_Type: " + II_ItemIDids.Text.Replace("-", "~"));
                }
            }
            if (II_Lore.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Item_Lore: '" + II_Lore.Text + "'");
            }
            if (invcbox.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Inventory_Control: " + invcbox.Text);
            }
            if (II_MeleeAttach.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Melee_Attachment: " + II_MeleeAttach.Text + "");
            }
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Melee_Mode: " + getTrueFalse(II_Meleelabel));
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Skip_Name_Check: " + getTrueFalse(II_SkipNamelabel));
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Remove_Unused_Tag: " + getTrueFalse(II_RemoveTaglabel));
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Hidden_From_List: " + getTrueFalse(II_HideListlabel));
            if (getListItem(AS1_Sound).Length > 1)
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Acquired: " + getListItem(AS1_Sound));
            }
            if (II_Enchantenchant.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enchantment_To_Check: " + II_Enchantenchant.Text + "-" + II_enchLevel.Text);
            }
            if (II_A_Type.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Attachments:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + spacebar + "Type: " + II_A_Type.Text);
            }
            if (II_A_Info.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + spacebar + "Info: " + II_A_Info.Text);
            }
            if (II_A_Delay.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + spacebar + "Toggle_Delay: " + II_A_Delay.Text);
            }
            if (getListItem(AS2_Sound).Length > 1)
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + spacebar + "Sounds_Toggle: " + getListItem(AS2_Sound));
            }
            // Shooting Setting
            csyamlbox.AppendText(Environment.NewLine + spacebar + "Shooting:");
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Disable: " + getTrueFalse(S_Disablelabel));
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Dual_Wield: " + getTrueFalse(S_DualWieldlabel));
            if (getTrueFalse(S_DualWieldlabel) == "false")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Right_Click_To_Shoot: " + getTrueFalse(S_RightClick2Shootlabel));
            }
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Cancel_Left_Click_Block_Damage: " + getTrueFalse(S_CancelLeftClicklabel));
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Cancel_Right_Click_Interactions: " + getTrueFalse(S_CancelRightInteractlabel));
            if (S_DelayBox.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Delay_Between_Shots: " + S_DelayBox.Text);
            }
            if (S_RecoilBox.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Recoil_Amount: " + S_RecoilBox.Text);
            }
            if (S_removedragbox.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Removal_Or_Drag_Delay: " + S_removedragbox.Text + "-" + getTrueFalse(S_RemovalDraglabel));
            }
            if (S_projectileType.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Projectile_Type: " + S_projectileType.Text);

                if (S_projectileType.Text == "energy")
                {
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Projectile_Subtype: " + S_P_RangeBox.Text + "-" + S_P_Radius.Text + "-" + S_P_Wall.Text + "-" + S_P_Victim.Text);
                }
                if (S_projectileType.Text == "fireball")
                {
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Projectile_Subtype: " + getTrueFalse(S_P_Fireball));
                }
                if (S_projectileType.Text == "splash")
                {
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Projectile_Subtype: " + S_Potpotion.Text.Replace(":", "~"));
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Projectile_Flames: " + getTrueFalse(S_PFlamelabel));
                }
                if (S_projectileType.Text == "grenade" || S_projectileType.Text == "flare")
                {
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Projectile_Subtype: " + S_Projectileids.Text.Replace("-", "~"));
                }
                if (S_projectileType.Text == "arrow" || S_projectileType.Text == "snowball" || S_projectileType.Text == "egg")
                {
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Remove_Bullet_Drop: " + getTrueFalse(S_RemoveBulletlabel));
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Projectile_Flames: " + getTrueFalse(S_PFlamelabel));
                    if (S_projectileType.Text == "arrow")
                    {
                        csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Remove_Arrows_On_Impact: " + getTrueFalse(S_RemoveArrowlabel));
                    }
                }
            }
            if (S_BSBox.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Bullet_Spread: " + S_BSBox.Text);
            }
            if (S_ProjectileAmountBox.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Projectile_Amount: " + S_ProjectileAmountBox.Text);
            }
            if (S_ProjectileDamage.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Projectile_Damage: " + S_ProjectileDamage.Text);
            }
            if (S_projectileSpeed.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Projectile_Speed: " + S_projectileSpeed.Text);
            }
            if (getTrueFalse(S_PI_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Projectile_Incendiary:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + spacebar + "Enable: " + getTrueFalse(S_PI_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + spacebar + "Duration: " + S_piduration.Text);
            }
            if (getListItem(AS3_Sound).Length > 1)
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Projectile: " + getListItem(AS3_Sound));
            }
            if (getListItem(AS4_Sound).Length > 1)
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Shoot: " + getListItem(AS4_Sound));
            }
            if (getTrueFalse(G_S_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Sneak:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(G_S_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "No_Recoil: " + getTrueFalse(G_S_NoRecoillabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Bullet_Spread: " + getTrueFalse(G_S_BSlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sneak_Before_Shooting: " + G_S_SBSBox.Text);
            }
            if (getTrueFalse(G_F_Automaticlabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Fully_Automatic:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(G_F_Automaticlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Fire_Rate: " + G_FB_FRBox.Text);
            }
            if (getTrueFalse(G_F_Burstlabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Burstfire:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(G_F_Burstlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Shots_Per_Burst: " + G_FB_SPBBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Delay_Between_Shots_In_Burst: " + G_FB_DBSIBBox.Text);
            }
            if (getTrueFalse(G_A_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Ammo:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(G_A_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Ammo_Item_ID: " + G_AmmoBoxids.Text.Replace("-", "~"));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Ammo_Name_Check: " + G_A_ANCbox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Take_Ammo_Per_Shot: " + getTrueFalse(G_A_TAPSlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Out_Of_Ammo: " + getListItem(AS5_Sound));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Shoot_With_No_Ammo: " + getListItem(AS6_Sound));
            }
            if (getTrueFalse(G_R_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Reload:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(G_R_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Reload_With_Mouse: " + getTrueFalse(G_R_RWMouselabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Starting_Amount: " + G_R_Startbox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Reload_Amount: " + G_R_RAbox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Reload_Bullets_Individually: " + getTrueFalse(G_R_RBIlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Take_Ammo_On_Reload: " + getTrueFalse(G_R_TAORlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Take_Ammo_As_Magazine: " + getTrueFalse(G_R_TAAMlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Reload_Duration: " + G_R_RDBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Reload_Shoot_Delay: " + G_R_RSDBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Destroy_When_Empty: " + getTrueFalse(G_R_DWElabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Out_Of_Ammo: " + getListItem(AS7_Sound));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Reloading: " + getListItem(AS8_Sound));
                if (getTrueFalse(S_DualWieldlabel) == "true")
                {
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Dual_Wield:");
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + spacebar + "Sounds_Single_Reload: " + getListItem(AS9_Sound));
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + spacebar + "Single_Reload_Duration: " + G_R_DWSRDBox.Text);
                }
                if (G_FA_Typelabel.Visible.Equals(true))
                {
                    csyamlbox.AppendText(Environment.NewLine + spacebar + "Firearm_Action:");
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Type: " + G_FA_TypeBox.Text);
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Open_Duration: " + G_FA_ODBox.Text);
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Close_Duration: " + G_FA_CDBox.Text);
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Close_Shoot_Delay: " + G_FA_CSDbox.Text);
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Reload_Open_Delay: " + G_FA_RODBox.Text);
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Reload_Close_Delay: " + G_FA_RCDBox.Text);
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sound_Open: " + getListItem(AS10_Sound));
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sound_Close: " + getListItem(AS11_Sound));
                }
            }
            if (getTrueFalse(G_Scope_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Scope:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(G_Scope_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Night_Vision: " + getTrueFalse(G_Scope_NVlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Zoom_Amount: " + G_Scope_ZABox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Zoom_Bullet_Spread: " + G_Scope_ZBSBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Zoom_Before_Shooting: " + getTrueFalse(G_Scope_Zoomlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Toggle_Zoom: " + getListItem(AS12_Sound));
            }
            if (getTrueFalse(I_RS_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Riot_Shield:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(I_RS_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Do_Not_Block_Projectiles: " + getTrueFalse(I_RS_DNBPlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Do_Not_Block_Melee_Attacks: " + getTrueFalse(I_RS_DNBMAlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Durability_Based_On_Damage: " + getTrueFalse(I_RS_DBODlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Durability_Loss_Per_Hit: " + I_RS_DLPHBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Forcefield_Mode: " + getTrueFalse(I_RS_FFMlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Blocked: " + getListItem(AS13_Sound));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Break: " + getListItem(AS14_Sound));
            }
            if (getTrueFalse(O_WS_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "SignShops:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(O_WS_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Price: " + O_WS_Priceids.Text.Replace('-', '~') + "-" + O_WS_PriceNum.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sign_Gun_ID: " + O_GUNIDBox.Text);
            }
            if (getTrueFalse(O_CCR_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Crafting:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(O_CCR_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Quantity: " + O_CCR_QBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Shaped: " + getTrueFalse(O_CCR_Shapedlabel));
                string ingredients = "";
                for (int i = 1; i < 10; i++)
                {
                    Control[] clist = Controls.Find("C_OOR_" + i.ToString() + "ids", true);
                    foreach (ComboBox c in clist)
                    {
                        if (c.Text != "")
                        {
                            string[] check = c.Text.Split('-');
                            if (check[1] == "0")
                            {
                                ingredients += check[0] + ",";
                            }
                            else
                            {
                                ingredients += c.Text.Replace('-', '~') + ",";
                            }
                        }
                        else
                        {
                            ingredients += "0,";
                        }
                    }
                }
                if (ingredients.Length > 0)
                {
                    ingredients = ingredients.Remove(ingredients.Length - 1);
                }
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Ingredients: " + ingredients);
            }
            if (getTrueFalse(O_RC_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Region_Check:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(O_RC_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Of_Denial: " + O_RC_Modbox.Text);
                string worldcoordinate = O_RC_WorldBox.Text + "," + O_RC_X1.Text + "," + O_RC_Y1.Text + "," + O_RC_Z1.Text + "," + O_RC_X2.Text + "," + O_RC_Y2.Text + "," + O_RC_Z2.Text + "," + getTrueFalse(O_RC_Blacklabel);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "World_And_Coordinates: " + worldcoordinate);
            }
            if (O_CDM_MBox.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Custom_Death_Message:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Normal: " + O_CDM_MBox.Text);
            }
            if (getTrueFalse(G2_H_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Headshot:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(G2_H_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Bonus_Damage: " + G2_H_BDBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Shooter: " + G2_H_MSBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Victim: " + G2_H_MVBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Shooter: " + getListItem(AS15_Sound));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Victim: " + getListItem(AS16_Sound));
            }
            if (getTrueFalse(G2_B_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Backstab:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(G2_B_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Bonus_Damage: " + G2_B_BDBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Shooter: " + G2_B_MSBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Victim: " + G2_B_MVBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Shooter: " + getListItem(AS17_Sound));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Victim: " + getListItem(AS18_Sound));
            }
            if (getTrueFalse(G2_CH_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Critical_Hits:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(G2_CH_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Bonus_Damage: " + G2_CH_BDBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Chance: " + G2_CH_ChanceBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Shooter: " + G2_CH_MSBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Victim: " + G2_CH_MVBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Shooter: " + getListItem(AS19_Sound));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Victim: " + getListItem(AS20_Sound));
            }
            if (getTrueFalse(E_SEOH_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Spawn_Entity_On_Hit:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(E_SEOH_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Chance: " + E_SEOH_ChanceBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Mob_Name: " + E_SEOH_MNBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "EntityType_Baby_Explode_Amount: " + getListItem(E_SEOH_EBox));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Make_Entities_Target_Victim: " + getTrueFalse(E_SEOH_METVlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Timed_Death: " + E_SEOH_TDBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Entity_Disable_Drops: " + getTrueFalse(E_SEOH_EDDlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Shooter: " + E_SEOH_MSBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Victim: " + E_SEOH_MVBox.Text);
            }
            if (getTrueFalse(G2_DBOFT_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Damage_Based_On_Flight_Time:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(G2_DBOFT_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Bonus_Damage_Per_Tick: " + G2_DBOFT_BDPTBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Minimum_Damage: " + G2_DBOFT_MDBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Maximum_Damage: " + G2_DBOFT_MXBox.Text);
            }
            if (getTrueFalse(I_A_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Airstrikes:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(I_A_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Flare_Activation_Delay: " + I_A_FADBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Particle_Call_Airstrike: " + getListItem(I_A_PCABoxpartls));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Call_Airstrike: " + I_A_MCABox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Block_Type: " + I_A_BTBox.Text.Replace('-', '~'));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Area: " + I_A_ABox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Distance_Between_Bombs: " + I_A_DBBBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Height_Dropped: " + I_A_HDBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Vertical_Variation: " + I_A_VVBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Horizontal_Variation: " + I_A_HVBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Airstrike: " + getListItem(AS21_Sound));
                if (getTrueFalse(I_A_MS_Enablelabel) == "true")
                {
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Multiple_Strikes:");
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + spacebar + "Enable: " + getTrueFalse(I_A_MS_Enablelabel));
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + spacebar + "Number_Of_Strikes: " + I_A_MS_NOSBox.Text);
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + spacebar + "Delay_Between_Strikes: " + I_A_MS_DBSBox.Text);
                }
            }
            if (getTrueFalse(I_ED_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Explosive_Devices:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(I_A_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Device_Type: " + I_ED_DeviceTBox.Text);
                string deviceinfo = "";
                switch (I_ED_DeviceTBox.Text)
                {
                    case "landmine":
                        deviceinfo += I_ED_Itemids.Text.Replace('-', '~');
                        deviceinfo += "-" + I_ED_mcart.Text;
                        break;
                    case "remote":
                        deviceinfo += I_ED_Abox.Text;
                        deviceinfo += "-" + I_ED_UIDbox.Text;
                        deviceinfo += "-" + I_ED_Headbox.Text;
                        break;
                    case "trap":
                        deviceinfo += getTrueFalse(I_ED_Chestlabel);
                        deviceinfo += "-" + getTrueFalse(I_ED_Picklabel);
                        deviceinfo += "-" + getTrueFalse(I_ED_DAPlabel);
                        deviceinfo += "-" + getTrueFalse(I_ED_Reuselabel);
                        deviceinfo += "-" + getTrueFalse(I_ED_NIDlabel);
                        break;
                    case "itembomb":
                        deviceinfo += I_ED_A2Box.Text;
                        deviceinfo += "," + I_ED_Sbox.Text;
                        deviceinfo += "," + I_ED_b4boxids.Text.Replace('-', '~');
                        deviceinfo += "," + I_ED_afterboxids.Text.Replace('-', '~');
                        break;
                }
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Device_Info: " + deviceinfo);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Deploy: " + getListItem(AS22_Sound));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Remote_Bypass_Regions: " + getTrueFalse(I_ED_RBRlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Disarm: " + I_ED_MDbox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Trigger_Placer: " + I_ED_MTPbox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Trigger_Victim: " + I_ED_MTVbox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Alert_Placer: " + getListItem(AS23_Sound));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Trigger: " + getListItem(AS24_Sound));
            }
        } 
        private void II_ItemIDBox_SelectedIndexChanged(object sender, EventArgs e)
        { 
            II_IDPics.Image = Image.FromFile(@filepath + "/Resource/ids/" +  II_ItemIDids.Text + ".png");
        }
        private void S_projectileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (S_projectileType.Text == "arrow" || S_projectileType.Text == "snowball" || S_projectileType.Text == "egg")
            {
                S_RemoveArrowlabel.Visible = false;
                if (S_projectileType.Text == "arrow")
                {
                    S_RemoveArrowlabel.Visible = true;
                }
                S_RemoveBulletlabel.Visible = true;
                S_PFlamelabel.Visible = true;
                S_P_RangeBox.Visible = false;
                S_P_Rangelabel.Visible = false;
                S_P_Radius.Visible = false;
                S_P_Radiuslabel.Visible = false;
                S_P_Victimlabel.Visible = false;
                S_P_Victim.Visible = false;
                S_P_Walllabel.Visible = false;
                S_P_Wall.Visible = false;
                S_Projectileids.Visible = false;
                S_idbox.Visible = false;
                S_Potpotion.Visible = false;
                S_P_Fireball.Visible = false;
            }
                if (S_projectileType.Text == "grenade" || S_projectileType.Text == "flare")
            {
                S_RemoveArrowlabel.Visible = false;
                S_RemoveBulletlabel.Visible = false;
                S_PFlamelabel.Visible = false;
                S_P_RangeBox.Visible = false;
                S_P_Rangelabel.Visible = false;
                S_P_Radius.Visible = false;
                S_P_Radiuslabel.Visible = false;
                S_P_Victimlabel.Visible = false;
                S_P_Victim.Visible = false;
                S_P_Walllabel.Visible = false;
                S_P_Wall.Visible = false;
                S_Projectileids.Visible = true;
                S_idbox.Visible = true;
                S_Potpotion.Visible = false;
                S_P_Fireball.Visible = false;
            }
            if (S_projectileType.Text == "witherskull")
            {
                S_RemoveArrowlabel.Visible = false;
                S_RemoveBulletlabel.Visible = false;
                S_PFlamelabel.Visible = false;
                S_P_RangeBox.Visible = false;
                S_P_Rangelabel.Visible = false;
                S_P_Radius.Visible = false;
                S_P_Radiuslabel.Visible = false;
                S_P_Victimlabel.Visible = false;
                S_P_Victim.Visible = false;
                S_P_Walllabel.Visible = false;
                S_P_Wall.Visible = false;
                S_Projectileids.Visible = false;
                S_idbox.Visible = false;
                S_Potpotion.Visible = false;
                S_P_Fireball.Visible = false;
            }
            if (S_projectileType.Text == "splash")
            {
                S_RemoveArrowlabel.Visible = false;
                S_RemoveBulletlabel.Visible = false;
                S_PFlamelabel.Visible = true;
                S_P_RangeBox.Visible = false;
                S_P_Rangelabel.Visible = false;
                S_P_Radius.Visible = false;
                S_P_Radiuslabel.Visible = false;
                S_P_Victimlabel.Visible = false;
                S_P_Victim.Visible = false;
                S_P_Walllabel.Visible = false;
                S_P_Wall.Visible = false;
                S_Projectileids.Visible = false;
                S_idbox.Visible = false;
                S_Potpotion.Visible = true;
                S_P_Fireball.Visible = false;
            }
            if (S_projectileType.Text == "fireball")
            {
                S_RemoveArrowlabel.Visible = false;
                S_RemoveBulletlabel.Visible = false;
                S_PFlamelabel.Visible = false;
                S_P_RangeBox.Visible = false;
                S_P_Rangelabel.Visible = false;
                S_P_Radius.Visible = false;
                S_P_Radiuslabel.Visible = false;
                S_P_Victimlabel.Visible = false;
                S_P_Victim.Visible = false;
                S_P_Walllabel.Visible = false;
                S_P_Wall.Visible = false;
                S_Projectileids.Visible = false;
                S_idbox.Visible = false;
                S_Potpotion.Visible = false;
                S_P_Fireball.Visible = true;
            }
            if (S_projectileType.Text == "energy")
            {
                S_RemoveArrowlabel.Visible = false;
                S_RemoveBulletlabel.Visible = false;
                S_PFlamelabel.Visible = false;
                S_P_RangeBox.Visible = true;
                S_P_Rangelabel.Visible = true;
                S_P_Radius.Visible = true;
                S_P_Radiuslabel.Visible = true;
                S_P_Victimlabel.Visible = true;
                S_P_Victim.Visible = true;
                S_P_Walllabel.Visible = true;
                S_P_Wall.Visible = true;
                S_Projectileids.Visible = false;
                S_idbox.Visible = false;
                S_Potpotion.Visible = false;
                S_P_Fireball.Visible = false;
            } 
        }

        private void S_Projectileids_SelectedIndexChanged(object sender, EventArgs e)
        {
            S_idbox.Image = Image.FromFile(@filepath + "/Resource/ids/" + S_Projectileids.Text + ".png");
        }

        private void G_S_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(G_S_Enablelabel) == "true")
            {
                G_S_NoRecoillabel.Visible = true;
                G_S_BSlabel.Visible = true;
                G_S_SBSlabel.Visible = true;
                G_S_SBSBox.Visible = true;
            }
            else
            {
                G_S_NoRecoillabel.Visible = false;
                G_S_BSlabel.Visible = false;
                G_S_SBSlabel.Visible = false;
                G_S_SBSBox.Visible = false;
            }
        }

        private void G_F_Automaticlabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(G_F_Automaticlabel) == "true")
            {
                G_F_Automaticlabel.Visible = true;
                G_F_Burstlabel.Visible = false;
                G_FB_FRlabel.Visible = true;
                G_FB_FRBox.Visible = true;
                G_FB_SPBlabel.Visible = false;
                G_FB_DBSIBlabel.Visible = false;
                G_FB_SPBBox.Visible = false;
                G_FB_DBSIBBox.Visible = false;

            }
            else
            {
                G_F_Automaticlabel.Visible = true;
                G_F_Burstlabel.Visible = true;
                G_FB_FRlabel.Visible = false;
                G_FB_FRBox.Visible = false;
                G_FB_SPBlabel.Visible = false;
                G_FB_DBSIBlabel.Visible = false;
                G_FB_SPBBox.Visible = false;
                G_FB_DBSIBBox.Visible = false;
            }
        }
        private void G_F_Burstlabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(G_F_Burstlabel) == "true")
            {
                G_F_Automaticlabel.Visible = false;
                G_F_Burstlabel.Visible = true;
                G_FB_FRlabel.Visible = false;
                G_FB_FRBox.Visible = false;
                G_FB_SPBlabel.Visible = true;
                G_FB_DBSIBlabel.Visible = true;
                G_FB_SPBBox.Visible = true;
                G_FB_DBSIBBox.Visible = true;
            }
            else
            {
                G_F_Automaticlabel.Visible = true;
                G_F_Burstlabel.Visible = true;
                G_FB_FRlabel.Visible = false;
                G_FB_FRBox.Visible = false;
                G_FB_SPBlabel.Visible = false;
                G_FB_DBSIBlabel.Visible = false;
                G_FB_SPBBox.Visible = false;
                G_FB_DBSIBBox.Visible = false;
            }
        }

        private void G_AmmoBoxids_SelectedIndexChanged(object sender, EventArgs e)
        {
            G_AmmoBoxidpic.Image = Image.FromFile(@filepath + "/Resource/ids/" + II_ItemIDids.Text + ".png");
        }

        private void G_A_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(G_A_Enablelabel) == "true")
            {
                G_A_ItemIDlabel.Visible = true;
                G_AmmoBoxids.Visible = true;
                G_AmmoBoxidpic.Visible = true;
                G_A_NameClabel.Visible = true;
                G_A_ANCbox.Visible = true;
                G_A_TAPSlabel.Visible = true;
                G_A_SoundOOAlabel.Visible = true;
                AS5_Sound.Visible = true;
                AS5.Visible = true;
                RS5.Visible = true;
                G_A_SSWNAlabel.Visible = true;
                AS6_Sound.Visible = true;
                AS6.Visible = true;
                RS6.Visible = true;
            }
            else
            {
                G_A_ItemIDlabel.Visible = false;
                G_AmmoBoxids.Visible = false;
                G_AmmoBoxidpic.Visible = false;
                G_A_NameClabel.Visible = false;
                G_A_ANCbox.Visible = false;
                G_A_TAPSlabel.Visible = false;
                G_A_SoundOOAlabel.Visible = false;
                AS5_Sound.Visible = false;
                AS5.Visible = false;
                RS5.Visible = false;
                G_A_SSWNAlabel.Visible = false;
                AS6_Sound.Visible = false;
                AS6.Visible = false;
                RS6.Visible = false;
            }
        }

        private void G_R_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(G_R_Enablelabel) == "true")
            {
                G_R_RWMouselabel.Visible = true;
                G_R_RBIlabel.Visible = true;
                G_R_TAORlabel.Visible = true;
                G_R_TAAMlabel.Visible = true;
                G_R_DWElabel.Visible = true;
                G_R_StartAmountlabel.Visible = true;
                G_R_Startbox.Visible = true;
                G_R_ReloadAmountlabel.Visible = true;
                G_R_RAbox.Visible = true;
                G_R_ReloadDurlabel.Visible = true;
                G_R_RDBox.Visible = true;
                G_R_RSDlabel.Visible = true;
                G_R_RSDBox.Visible = true;
                G_R_SOOAlabel.Visible = true;
                G_R_SRlabel.Visible = true;
                AS7_Sound.Visible = true;
                AS8_Sound.Visible = true;
                AS7.Visible = true;
                RS7.Visible = true;
                AS8.Visible = true;
                RS8.Visible = true;
                if (getTrueFalse(S_DualWieldlabel) == "true")
                {
                    G_R_SRDlabel.Visible = true;
                    G_R_SSRlabel.Visible = true;
                    AS9_Sound.Visible = true;
                    G_R_DWSRDBox.Visible = true;
                    AS9.Visible = true;
                    RS9.Visible = true;
                }
            }
            else
            {
                G_R_SRDlabel.Visible = false;
                G_R_RWMouselabel.Visible = false;
                G_R_RBIlabel.Visible = false;
                G_R_TAORlabel.Visible = false;
                G_R_TAAMlabel.Visible = false;
                G_R_DWElabel.Visible = false;
                G_R_StartAmountlabel.Visible = false;
                G_R_Startbox.Visible = false;
                G_R_ReloadAmountlabel.Visible = false;
                G_R_RAbox.Visible = false;
                G_R_ReloadDurlabel.Visible = false;
                G_R_RDBox.Visible = false;
                G_R_RSDlabel.Visible = false;
                G_R_RSDBox.Visible = false;
                G_R_SOOAlabel.Visible = false;
                G_R_SRlabel.Visible = false;
                AS7_Sound.Visible = false;
                AS8_Sound.Visible = false;
                AS7.Visible = false;
                RS7.Visible = false;
                AS8.Visible = false;
                RS8.Visible = false;
                G_R_SSRlabel.Visible = false;
                AS9_Sound.Visible = false;
                G_R_DWSRDBox.Visible = false;
                AS9.Visible = false;
                RS9.Visible = false;
            }
        }

        private void S_DualWieldlabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(S_DualWieldlabel) == "true")
            {
                // Firearm Actions
                G_FA_Typelabel.Visible = false;
                S_RightClick2Shootlabel.Visible = false;
                G_FA_TypeBox.Visible = false;
                G_FA_CSDlabel.Visible = false;
                G_FA_CSDbox.Visible = false;
                G_FA_CDlabel.Visible = false;
                G_FA_CDBox.Visible = false;
                G_FA_ODlabel.Visible = false;
                G_FA_ODBox.Visible = false;
                G_FA_RCDlabel.Visible = false;
                G_FA_RCDBox.Visible = false;
                G_FA_RODlabel.Visible = false;
                G_FA_RODBox.Visible = false;
                G_FA_SOlabel.Visible = false;
                AS10_Sound.Visible = false;
                AS10.Visible = false;
                RS10.Visible = false;
                G_FA_SClabel.Visible = false;
                AS11_Sound.Visible = false;
                AS11.Visible = false;
                RS11.Visible = false;
                // Scope
                G_Scope_Enablelabel.Visible = false;
                G_Scope_NVlabel.Visible = false;
                G_Scope_Zoomlabel.Visible = false;
                G_Scope_ZAlabel.Visible = false;
                G_Scope_ZABox.Visible = false;
                G_Scope_ZBSlabel.Visible = false;
                G_Scope_ZBSBox.Visible = false;
                G_FA_GBlabel.Visible = false;
                AS12_Sound.Visible = false;
                AS12.Visible = false;
                RS12.Visible = false;
                if (getTrueFalse(G_R_Enablelabel) == "true")
                {
                    G_R_SRDlabel.Visible = true;
                    G_R_SSRlabel.Visible = true;
                    AS9_Sound.Visible = true;
                    G_R_DWSRDBox.Visible = true;
                    AS9.Visible = true;
                    RS9.Visible = true;
                }
            }
            else
            {
                G_R_SRDlabel.Visible = false;
                G_R_SSRlabel.Visible = false;
                AS9_Sound.Visible = false;
                G_R_DWSRDBox.Visible = false;
                AS9.Visible = false;
                RS9.Visible = false;

                // Firearm 
                G_FA_Typelabel.Visible = true;
                S_RightClick2Shootlabel.Visible = true;
                G_FA_TypeBox.Visible = true;
                G_FA_CSDlabel.Visible = true;
                G_FA_CSDbox.Visible = true;
                G_FA_CDlabel.Visible = true;
                G_FA_CDBox.Visible = true;
                G_FA_ODlabel.Visible = true;
                G_FA_ODBox.Visible = true;
                G_FA_RCDlabel.Visible = true;
                G_FA_RCDBox.Visible = true;
                G_FA_RODlabel.Visible = true;
                G_FA_RODBox.Visible = true;
                G_FA_SOlabel.Visible = true;
                AS10_Sound.Visible = true;
                AS10.Visible = true;
                RS10.Visible = true;
                G_FA_SClabel.Visible = true;
                AS11_Sound.Visible = true;
                AS11.Visible = true;
                RS11.Visible = true;
                // Scope
                G_Scope_Enablelabel.Visible = true;
            }
        }
        private void II_Meleelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(II_Meleelabel) == "true")
            {
                // Critical Hit
                G2_CH_Enablelabel.Visible = true;
                // Backstab
                G2_B_Enablelabel.Visible = true;
                // Firearm
                G_FA_Typelabel.Visible = false;
                S_RightClick2Shootlabel.Visible = false;
                G_FA_TypeBox.Visible = false;
                G_FA_CSDlabel.Visible = false;
                G_FA_CSDbox.Visible = false;
                G_FA_CDlabel.Visible = false;
                G_FA_CDBox.Visible = false;
                G_FA_ODlabel.Visible = false;
                G_FA_ODBox.Visible = false;
                G_FA_RCDlabel.Visible = false;
                G_FA_RCDBox.Visible = false;
                G_FA_RODlabel.Visible = false;
                G_FA_RODBox.Visible = false;
                G_FA_SOlabel.Visible = false;
                AS10_Sound.Visible = false;
                AS10.Visible = false;
                RS10.Visible = false;
                G_FA_SClabel.Visible = false;
                AS11_Sound.Visible = false;
                AS11.Visible = false;
                RS11.Visible = false;
            }
            else
            {
                // Critical Hit
                G2_CH_Enablelabel.CheckState = CheckState.Unchecked;
                G2_CH_Enablelabel.Visible = false;
                // Backstab
                G2_B_Enablelabel.CheckState = CheckState.Unchecked;
                G2_B_Enablelabel.Visible = false;
                // Firearm
                G_FA_Typelabel.Visible = true;
                S_RightClick2Shootlabel.Visible = true;
                G_FA_TypeBox.Visible = true;
                G_FA_CSDlabel.Visible = true;
                G_FA_CSDbox.Visible = true;
                G_FA_CDlabel.Visible = true;
                G_FA_CDBox.Visible = true;
                G_FA_ODlabel.Visible = true;
                G_FA_ODBox.Visible = true;
                G_FA_RCDlabel.Visible = true;
                G_FA_RCDBox.Visible = true;
                G_FA_RODlabel.Visible = true;
                G_FA_RODBox.Visible = true;
                G_FA_SOlabel.Visible = true;
                AS10_Sound.Visible = true;
                AS10.Visible = true;
                RS10.Visible = true;
                G_FA_SClabel.Visible = true;
                AS11_Sound.Visible = true;
                AS11.Visible = true;
                RS11.Visible = true;
            }
        }

        private void G_Scope_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(G_Scope_Enablelabel) == "true")
            {
                G_Scope_NVlabel.Visible = true;
                G_Scope_Zoomlabel.Visible = true;
                G_Scope_ZAlabel.Visible = true;
                G_Scope_ZABox.Visible = true;
                G_Scope_ZBSlabel.Visible = true;
                G_Scope_ZBSBox.Visible = true;
                G_FA_GBlabel.Visible = true;
                AS12_Sound.Visible = true;
                AS12.Visible = true;
                RS12.Visible = true;
            }
            else
            {
                G_Scope_NVlabel.Visible = false;
                G_Scope_Zoomlabel.Visible = false;
                G_Scope_ZAlabel.Visible = false;
                G_Scope_ZABox.Visible = false;
                G_Scope_ZBSlabel.Visible = false;
                G_Scope_ZBSBox.Visible = false;
                G_FA_GBlabel.Visible = false;
                AS12_Sound.Visible = false;
                AS12.Visible = false;
                RS12.Visible = false;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "*.yml";
            sfd.Filter = "YAML files (*.yml)|*.yml";
            if (sfd.ShowDialog() == DialogResult.OK && sfd.FileName.Length > 0)
            {
                csyamlbox.SaveFile(sfd.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void I_RS_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(I_RS_Enablelabel) == "true")
            {
                I_RS_DNBPlabel.Visible = true;
                I_RS_DNBMAlabel.Visible = true;
                I_RS_DBODlabel.Visible = true;
                I_RS_FFMlabel.Visible = true;
                I_RS_DLPHlabel.Visible = true;
                I_RS_DLPHBox.Visible = true;
                I_RS_SBlabel.Visible = true;
                I_FA_SBrlabel.Visible = true;
                AS13_Sound.Visible = true;
                AS13.Visible = true;
                RS14.Visible = true;
                AS14.Visible = true;
                AS14_Sound.Visible = true;
                RS13.Visible = true;
            }
            else
            {
                I_RS_DNBPlabel.Visible = false;
                I_RS_DNBMAlabel.Visible = false;
                I_RS_DBODlabel.Visible = false;
                I_RS_FFMlabel.Visible = false;
                I_RS_DLPHlabel.Visible = false;
                I_RS_DLPHBox.Visible = false;
                I_RS_SBlabel.Visible = false;
                I_FA_SBrlabel.Visible = false;
                AS13_Sound.Visible = false;
                AS13.Visible = false;
                RS13.Visible = false;
                RS14.Visible = false;
                AS14.Visible = false;
                AS14_Sound.Visible = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            O_WS_IDSign.Image = Image.FromFile(filepath + "/Resource/uipics/sign.jpg");
            Graphics g = Graphics.FromImage(O_WS_IDSign.Image);
            int num = O_GUNIDBox.Text.Length;
            g.DrawString("[CS]" + O_GUNIDBox.Text, new Font("Arial", 12), Brushes.Black, 50 - (num * 2), 0);
        }

        private void O_WS_PriceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            O_WS_PricePic.Image = Image.FromFile(@filepath + "/Resource/ids/" + O_WS_Priceids.Text + ".png");
        }

        private void O_WS_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(O_WS_Enablelabel) == "true")
            {
                O_WS_Pricelabel.Visible = true;
                O_WS_Priceids.Visible = true;
                O_WS_PriceNum.Visible = true;
                O_WS_PricePic.Visible = true;
                O_WS_GUNIDlabel.Visible = true;
                O_GUNIDBox.Visible = true;
                O_WS_IDSign.Visible = true;
            }
            else
            {
                O_WS_Pricelabel.Visible = false;
                O_WS_Priceids.Visible = false;
                O_WS_PriceNum.Visible = false;
                O_WS_PricePic.Visible = false;
                O_WS_GUNIDlabel.Visible = false;
                O_GUNIDBox.Visible = false;
                O_WS_IDSign.Visible = false;
            }
        }

        private void O_CCR_Enablelabe_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(O_CCR_Enablelabel) == "true")
            {
                O_CCR_Shapedlabel.Visible = true;
                O_CCR_Ingrelabel.Visible = true;
                for (int i = 1; i < 10; i++)
                {
                    Control[] c3list = Controls.Find("C_OOR_p" + i.ToString(), true);
                    foreach (PictureBox p in c3list)
                    {
                        p.Visible = true;
                    }
                }
                for (int i = 1; i < 10; i++)
                {
                    Control[] clist = Controls.Find("C_OOR_" + i.ToString() + "ids", true);
                    foreach (ComboBox c in clist)
                    {
                        c.Visible = true;
                        c.SelectedIndexChanged += delegate
                        {
                            string num = c.Name.Replace("C_OOR_", "").Replace("ids","");
                            Control[] c2list = Controls.Find("C_OOR_p" + num, true);
                            foreach (PictureBox p in c2list)
                            {
                                p.Image = Image.FromFile(filepath + "/Resource/ids/" + c.SelectedItem.ToString() + ".png");
                            }
                        };
                    }
                }
            }
            else
            {
                O_CCR_Shapedlabel.Visible = false;
                O_CCR_Ingrelabel.Visible = false;
                for (int i = 1; i < 10; i++)
                {
                    Control[] c3list = Controls.Find("C_OOR_p" + i.ToString(), true);
                    foreach (PictureBox p in c3list)
                    {
                        p.Visible = false;
                    }
                }
                for (int i = 1; i < 10; i++)
                {
                    Control[] clist = Controls.Find("C_OOR_" + i.ToString() + "ids", true);
                    foreach (ComboBox c in clist)
                    {
                        c.Visible = false;

                    }
                }
            }
        }
        private void O_RC_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(O_RC_Enablelabel) == "true")
            {
                O_RC_Worldlabel.Visible = true;
                O_RC_WorldBox.Visible = true;
                O_RC_X1.Visible = true;
                O_RC_Y1.Visible = true;
                O_RC_Z1.Visible = true;
                O_RC_X2.Visible = true;
                O_RC_Y2.Visible = true;
                O_RC_Z2.Visible = true;
                x1.Visible = true;
                y1.Visible = true;
                z1.Visible = true;
                x2.Visible = true;
                y2.Visible = true;
                z2.Visible = true;
                O_RC_Modlabel.Visible = true;
                O_RC_Blacklabel.Visible = true;
                O_RC_Modbox.Visible = true;
            }
            else
            {
                O_RC_Worldlabel.Visible = false;
                O_RC_WorldBox.Visible = false;
                O_RC_X1.Visible = false;
                O_RC_Y1.Visible = false;
                O_RC_Z1.Visible = false;
                O_RC_X2.Visible = false;
                O_RC_Y2.Visible = false;
                O_RC_Z2.Visible = false;
                x1.Visible = false;
                y1.Visible = false;
                z1.Visible = false;
                x2.Visible = false;
                y2.Visible = false;
                z2.Visible = false;
                O_RC_Modlabel.Visible = false;
                O_RC_Blacklabel.Visible = false;
                O_RC_Modbox.Visible = false;
            }
        }

        private void G2_H_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(G2_H_Enablelabel) == "true")
            {
                G2_H_BDlabel.Visible = true;
                G2_H_BDBox.Visible = true;
                G2_H_MSlabel.Visible = true;
                G2_H_MVlabel.Visible = true;
                G2_H_MSBox.Visible = true;
                G2_H_MVBox.Visible = true;
                G2_H_SSlabel.Visible = true;
                G2_H_SVlabel.Visible = true;
                AS15.Visible = true;
                AS15_Sound.Visible = true;
                RS15.Visible = true;
                AS16.Visible = true;
                AS16_Sound.Visible = true;
                RS16.Visible = true;
            }
            else
            {
                G2_H_BDlabel.Visible = false;
                G2_H_BDBox.Visible = false;
                G2_H_MSlabel.Visible = false;
                G2_H_MVlabel.Visible = false;
                G2_H_MSBox.Visible = false;
                G2_H_MVBox.Visible = false;
                G2_H_SSlabel.Visible = false;
                G2_H_SVlabel.Visible = false;
                AS15.Visible = false;
                AS15_Sound.Visible = false;
                RS15.Visible = false;
                AS16.Visible = false;
                AS16_Sound.Visible = false;
                RS16.Visible = false;
            }
        }

        private void G2_B_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(G2_B_Enablelabel) == "true")
            {
                G2_B_BDlabel.Visible = true;
                G2_B_BDBox.Visible = true;
                G2_B_MSlabel.Visible = true;
                G2_B_MVlabel.Visible = true;
                G2_B_MSBox.Visible = true;
                G2_B_MVBox.Visible = true;
                G2_B_SSlabel.Visible = true;
                G2_B_SVlabel.Visible = true;
                AS17.Visible = true;
                AS17_Sound.Visible = true;
                RS17.Visible = true;
                AS18.Visible = true;
                AS18_Sound.Visible = true;
                RS18.Visible = true;
            }
            else
            {
                G2_B_BDlabel.Visible = false;
                G2_B_BDBox.Visible = false;
                G2_B_MSlabel.Visible = false;
                G2_B_MVlabel.Visible = false;
                G2_B_MSBox.Visible = false;
                G2_B_MVBox.Visible = false;
                G2_B_SSlabel.Visible = false;
                G2_B_SVlabel.Visible = false;
                AS17.Visible = false;
                AS17_Sound.Visible = false;
                RS17.Visible = false;
                AS18.Visible = false;
                AS18_Sound.Visible = false;
                RS18.Visible = false;
            }
        }

        private void G2_CH_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(G2_CH_Enablelabel) == "true")
            {
                G2_CH_BDlabel.Visible = true;
                G2_CH_BDBox.Visible = true;
                G2_CH_Chancelabel.Visible = true;
                G2_CH_ChanceBox.Visible = true;
                G2_CH_MSlabel.Visible = true;
                G2_CH_MSBox.Visible = true;
                G2_CH_MVlabel.Visible = true;
                G2_CH_MVBox.Visible = true;
                G2_CH_SSlabel.Visible = true;
                AS19_Sound.Visible = true;
                AS19.Visible = true;
                RS19.Visible = true;
                G2_CH_SVlabel.Visible = true;
                AS20_Sound.Visible = true;
                AS20.Visible = true;
                RS20.Visible = true;
            }
            else
            {
                G2_CH_BDlabel.Visible = false;
                G2_CH_BDBox.Visible = false;
                G2_CH_Chancelabel.Visible = false;
                G2_CH_ChanceBox.Visible = false;
                G2_CH_MSlabel.Visible = false;
                G2_CH_MSBox.Visible = false;
                G2_CH_MVlabel.Visible = false;
                G2_CH_MVBox.Visible = false;
                G2_CH_SSlabel.Visible = false;
                AS19_Sound.Visible = false;
                AS19.Visible = false;
                RS19.Visible = false;
                G2_CH_SVlabel.Visible = false;
                AS20_Sound.Visible = false;
                AS20.Visible = false;
                RS20.Visible = false;
            }
        }

        private void E_SEOH_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(E_SEOH_Enablelabel) == "true")
            {
                E_SEOH_TDlabel.Visible = true;
                E_SEOH_TDBox.Visible = true;
                E_SEOH_METVlabel.Visible = true;
                E_SEOH_EDDlabel.Visible = true;
                E_SEOH_Chancelabel.Visible = true;
                E_SEOH_ChanceBox.Visible = true;
                E_SEOH_MNlabel.Visible = true;
                E_SEOH_MNBox.Visible = true;
                E_SEOH_EBox.Visible = true;
                E_SEOH_Elabel.Visible = true;
                E_SEOH_Entityett.Visible = true;
                E_SEOH_Babylabel.Visible = true;
                E_SEOH_Explolabel.Visible = true;
                E_SEOH_Amlabel.Visible = true;
                E_SEOH_AmBox.Visible = true;
                E_SEOH_AElabel.Visible = true;
                E_SEOH_RElabel.Visible = true;
                E_SEOH_MSlabel.Visible = true;
                E_SEOH_MSBox.Visible = true;
                E_SEOH_MVlabel.Visible = true;
                E_SEOH_MVBox.Visible = true;
            }
            else
            {
                E_SEOH_TDlabel.Visible = true;
                E_SEOH_TDBox.Visible = true;
                E_SEOH_METVlabel.Visible = false;
                E_SEOH_EDDlabel.Visible = false;
                E_SEOH_Chancelabel.Visible = false;
                E_SEOH_ChanceBox.Visible = false;
                E_SEOH_MNlabel.Visible = false;
                E_SEOH_MNBox.Visible = false;
                E_SEOH_EBox.Visible = false;
                E_SEOH_Elabel.Visible = false;
                E_SEOH_Entityett.Visible = false;
                E_SEOH_Babylabel.Visible = false;
                E_SEOH_Explolabel.Visible = false;
                E_SEOH_Amlabel.Visible = false;
                E_SEOH_AmBox.Visible = false;
                E_SEOH_AElabel.Visible = false;
                E_SEOH_RElabel.Visible = false;
                E_SEOH_MSlabel.Visible = false;
                E_SEOH_MSBox.Visible = false;
                E_SEOH_MVlabel.Visible = false;
                E_SEOH_MVBox.Visible = false;
            }
        }
        private void E_SEOH_AElabel_Click(object sender, EventArgs e)
        {
            if (E_SEOH_Entityett.Text != "")
            {
                string entity = E_SEOH_Entityett.Text + "-" + getTrueFalse(E_SEOH_Babylabel) + "-" + getTrueFalse(E_SEOH_Explolabel) + "-" + E_SEOH_AmBox.Text;
                E_SEOH_EBox.Items.Add(entity);
            }
        }

        private void E_SEOH_RElabel_Click(object sender, EventArgs e)
        {
            if (E_SEOH_EBox.SelectedItem != null)
            {
                E_SEOH_EBox.Items.Remove(E_SEOH_EBox.SelectedItem);
            }
        }

        private void G2_DBOFT_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(G2_DBOFT_Enablelabel) == "true")
            {
                G2_DBOFT_BDPTlabel.Visible = true;
                G2_DBOFT_BDPTBox.Visible = true;
                G2_DBOFT_MDlabel.Visible = true;
                G2_DBOFT_MDBox.Visible = true;
                G2_DBOFT_MXlabel.Visible = true;
                G2_DBOFT_MXBox.Visible = true;
            }
            else
            {
                G2_DBOFT_BDPTlabel.Visible = false;
                G2_DBOFT_BDPTBox.Visible = false;
                G2_DBOFT_MDlabel.Visible = false;
                G2_DBOFT_MDBox.Visible = false;
                G2_DBOFT_MXlabel.Visible = false;
                G2_DBOFT_MXBox.Visible = false;
            }
        }

        private void I_A_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(I_A_Enablelabel) == "true")
            {
                I_A_FADlabel.Visible = true;
                I_A_FADBox.Visible = true;
                I_A_PCABoxpart.Visible = true;
                I_A_PCAlabel.Visible = true;
                I_A_MCABox.Visible = true;
                I_A_MCAlabel.Visible = true;
                I_A_BTBox.Visible = true;
                I_A_BTlabel.Visible = true;
                I_A_ABox.Visible = true;
                I_A_Alabel.Visible = true;
                I_A_DBBlabel.Visible = true;
                I_A_DBBlabel.Visible = true;
                I_A_HDlabel.Visible = true;
                I_A_HDBox.Visible = true;
                I_A_VVBox.Visible = true;
                I_A_VVlabel.Visible = true;
                I_A_HVBox.Visible = true;
                I_A_HVlabel.Visible = true;
                I_A_SAlabel.Visible = true;
                AS21.Visible = true;
                AS21_Sound.Visible = true;
                RS21.Visible = true;
                I_A_MS_Enablelabel.Visible = true;
                I_A_PCABoxAlabel.Visible = true;
                I_A_PCABoxRlabel.Visible = true;
                I_A_PCABoxpartls.Visible = true;
                I_A_DBBBox.Visible = true;
            }
            else
            {
                I_A_DBBBox.Visible = false;
                I_A_FADlabel.Visible = false;
                I_A_FADBox.Visible = false;
                I_A_PCABoxpart.Visible = false;
                I_A_PCAlabel.Visible = false;
                I_A_MCABox.Visible = false;
                I_A_MCAlabel.Visible = false;
                I_A_BTBox.Visible = false;
                I_A_BTlabel.Visible = false;
                I_A_ABox.Visible = false;
                I_A_Alabel.Visible = false;
                I_A_DBBlabel.Visible = false;
                I_A_DBBlabel.Visible = false;
                I_A_HDlabel.Visible = false;
                I_A_HDBox.Visible = false;
                I_A_VVBox.Visible = false;
                I_A_VVlabel.Visible = false;
                I_A_HVBox.Visible = false;
                I_A_HVlabel.Visible = false;
                I_A_SAlabel.Visible = false;
                AS21.Visible = false;
                AS21_Sound.Visible = false;
                RS21.Visible = false;
                I_A_MS_Enablelabel.Visible = false;
                I_A_MS_Enablelabel.CheckState = CheckState.Unchecked;
                I_A_PCABoxAlabel.Visible = false;
                I_A_PCABoxRlabel.Visible = false;
                I_A_PCABoxpartls.Visible = false;
            }
        }

        private void I_A_MS_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(I_A_MS_Enablelabel) == "true")
            {
                I_A_MS_NOSBox.Visible = true;
                I_A_MS_NOSlabel.Visible = true;
                I_A_MS_DBSBox.Visible = true;
                I_A_MS_DBSlabel.Visible = true;
            }
            else
            {
                I_A_MS_NOSBox.Visible = false;
                I_A_MS_NOSlabel.Visible = false;
                I_A_MS_DBSBox.Visible = false;
                I_A_MS_DBSlabel.Visible = false;
            }
        }
        private void S_U_CFUlabel_Click(object sender, EventArgs e)
        {
            System.Net.WebClient request = new System.Net.WebClient();
            string newver = request.DownloadString("http://pastebin.com/raw/ddUGMbJy");
            string version = System.Text.RegularExpressions.Regex.Replace(newver.Split('\n').First(), @"\t|\n|\r", "");
            S_U_NVersion.Text = version;
        }

        private void S_U_Updatelabel_Click(object sender, EventArgs e)
        {
            if (S_U_NVersion.Text == "")
            {
                S_U_CFUlabel_Click(sender, e);
            }
            if (S_U_CVersion.Text != S_U_NVersion.Text)
            {
                Process proc = Process.Start(Application.StartupPath + "/updater.exe");
                Application.Exit();
            }
        }

        private void I_ED_DeviceTBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            I_ED_itemIdlabel.Visible = false;
            I_ED_Itemids.Visible = false;
            I_ED_IDPic.Visible = false;
            I_ED_mcart.Visible = false;
            I_ED_mcarttlabel.Visible = false;
            I_ED_UIDlabel.Visible = false;
            I_ED_Alabel.Visible = false;
            I_ED_Headlabel.Visible = false;
            I_ED_Headbox.Visible = false;
            I_ED_Abox.Visible = false;
            I_ED_UIDbox.Visible = false;
            I_ED_Chestlabel.Visible = false;
            I_ED_Picklabel.Visible = false;
            I_ED_Reuselabel.Visible = false;
            I_ED_DAPlabel.Visible = false;
            I_ED_NIDlabel.Visible = false;
            I_ED_A2Box.Visible = false;
            I_ED_A2label.Visible = false;
            I_ED_Afterlabel.Visible = false;
            I_ED_afterboxids.Visible = false;
            I_ED_b4boxids.Visible = false;
            I_ED_B4label.Visible = false;
            I_ED_Sbox.Visible = false;
            I_ED_Slabel.Visible = false;
            if (I_ED_DeviceTBox.Text == "itembomb")
            {
                I_ED_A2Box.Visible = true;
                I_ED_A2label.Visible = true;
                I_ED_Afterlabel.Visible = true;
                I_ED_afterboxids.Visible = true;
                I_ED_b4boxids.Visible = true;
                I_ED_B4label.Visible = true;
                I_ED_Sbox.Visible = true;
                I_ED_Slabel.Visible = true;
            }
            if (I_ED_DeviceTBox.Text == "trap")
            {
                I_ED_Chestlabel.Visible = true;
                I_ED_Picklabel.Visible = true;
                I_ED_Reuselabel.Visible = true;
                I_ED_DAPlabel.Visible = true;
                I_ED_NIDlabel.Visible = true;
            }
            if (I_ED_DeviceTBox.Text == "remote")
            {
                I_ED_UIDlabel.Visible = true;
                I_ED_Alabel.Visible = true;
                I_ED_Headlabel.Visible = true;
                I_ED_Headbox.Visible = true;
                I_ED_Abox.Visible = true;
                I_ED_UIDbox.Visible = true;
            }
            if (I_ED_DeviceTBox.Text == "landmine")
            {
                I_ED_itemIdlabel.Visible = true;
                I_ED_Itemids.Visible = true;
                I_ED_IDPic.Visible = true;
                I_ED_mcart.Visible = true;
                I_ED_mcarttlabel.Visible = true;
                string[] items = "MINECART/MINECART_CHEST/MINECART_FURNACE/MINECART_HOPPER/MINECART_MOB_SPAWNER/MINECART_TNT".Split('/');
                foreach (string mc in items)
                {
                    I_ED_mcart.Items.Add(mc);
                }
            }
        }

        private void I_ED_Itemids_SelectedIndexChanged(object sender, EventArgs e)
        {
            I_ED_IDPic.Image = Image.FromFile(@filepath + "/Resource/ids/" + I_ED_Itemids.Text + ".png");
        }

        private void I_ED_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(I_ED_Enablelabel) == "true")
            {
                I_ED_RBRlabel.Visible = true;
                I_ED_DeviceTlabel.Visible = true;
                I_ED_DeviceTBox.Visible = true;
                I_ED_DeviceIlabel.Visible = true;
                AS22.Visible = true;
                RS22.Visible = true;
                AS22_Sound.Visible = true;
                AS23.Visible = true;
                RS23.Visible = true;
                AS23_Sound.Visible = true;
                AS24.Visible = true;
                RS24.Visible = true;
                AS24_Sound.Visible = true;
                I_ED_MDbox.Visible = true;
                I_ED_MDlabel.Visible = true;
                I_ED_MTPbox.Visible = true;
                I_ED_MTPlabel.Visible = true;
                I_ED_MTVbox.Visible = true;
                I_ED_MTVlabel.Visible = true;
            }
            else
            {
                I_ED_RBRlabel.Visible = false;
                I_ED_DeviceTlabel.Visible = false;
                I_ED_DeviceTBox.Visible = false;
                I_ED_DeviceIlabel.Visible = false;
                I_ED_itemIdlabel.Visible = false;
                I_ED_Itemids.Visible = false;
                I_ED_IDPic.Visible = false;
                I_ED_mcart.Visible = false;
                I_ED_mcarttlabel.Visible = false;
                I_ED_UIDlabel.Visible = false;
                I_ED_Alabel.Visible = false;
                I_ED_Headlabel.Visible = false;
                I_ED_Headbox.Visible = false;
                I_ED_Abox.Visible = false;
                I_ED_UIDbox.Visible = false;
                I_ED_Chestlabel.Visible = false;
                I_ED_Picklabel.Visible = false;
                I_ED_Reuselabel.Visible = false;
                I_ED_DAPlabel.Visible = false;
                I_ED_NIDlabel.Visible = false;
                I_ED_A2Box.Visible = false;
                I_ED_A2label.Visible = false;
                I_ED_Afterlabel.Visible = false;
                I_ED_afterboxids.Visible = false;
                I_ED_b4boxids.Visible = false;
                I_ED_B4label.Visible = false;
                I_ED_Sbox.Visible = false;
                I_ED_Slabel.Visible = false;
                I_ED_MDbox.Visible = false;
                I_ED_MDlabel.Visible = false;
                I_ED_MTPbox.Visible = false;
                I_ED_MTPlabel.Visible = false;
                I_ED_MTVbox.Visible = false;
                I_ED_MTVlabel.Visible = false;
                I_ED_SDlabel.Visible = false;
                I_ED_SAPlabel.Visible = false;
                I_ED_STlabel.Visible = false;
                AS22.Visible = false;
                RS22.Visible = false;
                AS22_Sound.Visible = false;
                AS23.Visible = false;
                RS23.Visible = false;
                AS23_Sound.Visible = false;
                AS24.Visible = false;
                RS24.Visible = false;
                AS24_Sound.Visible = false;
            }
        }

        private void I_A_BTBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            I_A_BTPics.Image = Image.FromFile(@filepath + "/Resource/ids/" + I_A_BTBox.Text + ".png");
        }

        private void savettp_Click(object sender, EventArgs e)
        {
            if (ttp.CheckState == CheckState.Checked)
            {
                Properties.Settings.Default.TTP = true;
                return;
            }
            Properties.Settings.Default.TTP = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveFormState();
        }
        private void saveFormState()
        {
            checkControl(this);
        }
        private void checkControl(Control c)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Control ctrl in c.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox c2 = (TextBox)ctrl;
                    sb.AppendLine(c2.Name + "=" + c2.Text);
                }
                if (ctrl is CheckBox)
                {
                    CheckBox c2 = (CheckBox)ctrl;
                    sb.AppendLine(c2.Name + "=" + c2.CheckState);
                }
                if (ctrl is ComboBox)
                {
                    ComboBox c2 = (ComboBox)ctrl;
                    sb.AppendLine(c2.Name + "=" + c2.Text);
                }
                if (ctrl is ListBox)
                {
                    ListBox c2 = (ListBox)ctrl;
                    sb.AppendLine(c2.Name + "=" + c2.Text);
                }
                if (ctrl is GroupBox)
                {
                    GroupBox c2 = (GroupBox)ctrl;
                    checkControl(c2);
                }
                if (ctrl is TabControl)
                {
                    TabControl c2 = (TabControl)ctrl;
                    checkControl(c2);
                }
                if (ctrl is TabPage)
                {
                    TabPage c2 = (TabPage)ctrl;
                    checkControl(c2);
                }
            }
            File.AppendAllText(filepath + "/state.txt", sb.ToString());
        }
    }
}
