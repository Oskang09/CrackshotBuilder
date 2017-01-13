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

namespace CrackshotBuilder
{
    public partial class Form1 : Form
    {
        //string filepath = Application.StartupPath + "/CrackshotFiles";
        public string filepath = "C:/Users/Oska/Desktop/CrackshotFiles";
        string doublespacebar = "        ";
        string spacebar = "    ";
        private TTPHelper ttphelper;
        public Form1()
        {
            InitializeComponent();
            TTPHelper frm = new TTPHelper();
            frm.TopMost = true;
            frm.Show();
            ttphelper = frm;
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
            for (int i = 1;; i++)
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
                            if ( cl.SelectedItem != null)
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
                    ttp.SetToolTip(c, ttplist[1].Replace('|','\n'));
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
            if (S_RightClick2Shootlabel.Visible.Equals(true))
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Right_Click_To_Shoot: " + getTrueFalse(S_RightClick2Shootlabel));
            }
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Cancel_Left_Click_Block_Damage: " + getTrueFalse(S_CancelLeftClicklabel));
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Cancel_Right_Click_Interactions: " + getTrueFalse(S_CancelRightInteractlabel));
            if ( S_DelayBox.Text != "")
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
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Projectile_Subtype: " +  S_P_RangeBox.Text + "-" + S_P_Radius.Text + "-" + S_P_Wall.Text + "-" + S_P_Victim.Text);
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
            if ( getTrueFalse(G_S_Enablelabel) == "true")
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
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Price: " + O_WS_Priceids.Text.Replace('-','~') + "-" + O_WS_PriceNum.Text);
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
    }
}
