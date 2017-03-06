using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace CrackshotBuilder
{
    public partial class Form1 : Form
    {
        public string filepath = Application.StartupPath + "/CrackshotFiles";
        //
        string doublespacebar = "        ";
        string spacebar = "    ";
        public Form1()
        {

            InitializeComponent();
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
        public void loadActivation()
        {
            string[] all = "head,back,crit,hit,shoot,reload".Split(',');
            foreach (string box in all)
            {
                E2_PE_ActiveBox.Items.Add(box);
            }
            E2_PE_Addlabel.Click += delegate
            {
                E2_PE_Alist.Items.Add(E2_PE_ActiveBox.Text);
            };
            E2_PE_Removelabel.Click += delegate
            {
                if (E2_PE_Alist.SelectedItem != null)
                {
                    E2_PE_Alist.Items.Remove(E2_PE_Alist.SelectedItem);
                }
            };
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
            loadPotion();
            loadS_Box();
            loadG_FA_Box();
            loadsound();
            loadETT();
            loadPartBox();
            loadED();
            loadBT();
            loadPotionEff();
            loadActivation();
            loadFirework();
            loadAbilities();
            loadcmds();

            loadFormState();
        }
        public void loadcmds()
        {
            O2_E_MVRCAlabel.Click += delegate
            {
                O2_E_MVRCls.Items.Add(O2_E_MVRCcmd.Text);
            };
            O2_E_MVRCRlabel.Click += delegate
            {
                if (O2_E_MVRCls.SelectedItem != null)
                {
                    O2_E_MVRCls.Items.Remove(O2_E_MVRCls.SelectedItem);
                }
            };
            O2_E_RCCAlabel.Click += delegate
            {
                O2_E_RCCls.Items.Add(O2_E_RCCcmd.Text);
            };
            O2_E_RCCRlabel.Click += delegate
            {
                if (O2_E_RCCls.SelectedItem != null)
                {
                    O2_E_RCCls.Items.Remove(O2_E_MVRCls.SelectedItem);
                }
            };
            O2_E_RCAlabel.Click += delegate
            {
                if (O2_E_RBBox.Text == "CONSOLE")
                {
                    O2_E_RCls.Items.Add("@" + O2_E_RCcmd.Text);
                }
                else
                {
                    O2_E_RCls.Items.Add(O2_E_RCcmd.Text);
                }
            };
            O2_E_RCRlabel.Click += delegate
            {
                if (O2_E_RCls.SelectedItem != null)
                {
                    O2_E_RCls.Items.Remove(O2_E_RCls.SelectedItem);
                }
            };
        }
        public void loadAbilities()
        {
            O_A_SEAlabel.Click += delegate
            {
                string item = O_A_SEBoxett.Text + "-" + O_E_SEMBox.Text;
                O_A_SEls.Items.Add(item);
            };
            O_A_SERlabel.Click += delegate
            {
                if (O_A_SEls.SelectedItem != null)
                {
                    O_A_SEls.Items.Remove(O_A_SEls.SelectedItem);
                }
            };
            O_A_BDids.SelectedIndexChanged += delegate
            {
                if (O_A_BDids.SelectedItem != null)
                {
                    O_A_BDPics.Image = Image.FromFile(@filepath + "/Resource/ids/" + O_A_BDids.Text + ".png");
                }
            };
            O_A_BDAlabel.Click += delegate
            {
                O_A_BDls.Items.Add(O_A_BDids.Text.Replace('-', '~'));
            };
            O_A_BDRlabel.Click += delegate
            {
                if (O_A_BDls.SelectedItem != null)
                {
                    O_A_BDls.Items.Remove(O_A_BDls.SelectedItem);
                }
            };
            O_A_BBids.SelectedIndexChanged += delegate
            {
                if (O_A_BBids.SelectedItem != null)
                {
                    O_A_BBPics.Image = Image.FromFile(@filepath + "/Resource/ids/" + O_A_BBids.Text + ".png");
                }
            };
            O_A_BBAlabel.Click += delegate
            {
                if (getTrueFalse(O_A_Wlabel) == "true")
                {
                    bool checkitem = false;
                    int index;
                    string whitelist = string.Empty;
                    foreach (string item in O_A_BBls.Items)
                    {
                        if (item.Contains("TRUE"))
                        {
                            checkitem = true;
                            index = O_A_BBls.Items.IndexOf(item);
                            whitelist = item;
                        }
                    }
                    if (checkitem)
                    {
                        string ids = string.Empty;
                        string[] check = O_A_BBids.Text.Split('-');
                        if (check[1] == "0")
                        {
                            ids += check[0];
                        }
                        else
                        {
                            ids += O_A_BBids.Text.Replace('-', '~');
                        }
                        whitelist += "," + ids;
                    }
                    else
                    {
                        O_A_BBls.Items.Clear();
                        string ids = string.Empty;
                        string[] check = O_A_BBids.Text.Split('-');
                        if (check[1] == "0")
                        {
                            ids += check[0];
                        }
                        else
                        {
                            ids += O_A_BBids.Text.Replace('-', '~');
                        }
                        whitelist = "TRUE-" + ids;
                    }
                    O_A_BBls.Items.Add(whitelist);
                }
                else
                {
                    bool checkitem = false;
                    int index;
                    string whitelist = string.Empty;
                    foreach (string item in O_A_BBls.Items)
                    {
                        if (item.Contains("FALSE"))
                        {
                            checkitem = true;
                            index = O_A_BBls.Items.IndexOf(item);
                            whitelist = item;
                        }
                    }
                    if (checkitem)
                    {
                        string ids = string.Empty;
                        string[] check = O_A_BBids.Text.Split('-');
                        if (check[1] == "0")
                        {
                            ids += check[0];
                        }
                        else
                        {
                            ids += O_A_BBids.Text.Replace('-', '~');
                        }
                        whitelist += "," + ids;
                    }
                    else
                    {
                        string ids = string.Empty;
                        string[] check = O_A_BBids.Text.Split('-');
                        if (check[1] == "0")
                        {
                            ids += check[0];
                        }
                        else
                        {
                            ids += O_A_BBids.Text.Replace('-', '~');
                        }
                        O_A_BBls.Items.Clear();
                        whitelist = "FALSE-" + ids;
                    }
                    O_A_BBls.Items.Add(whitelist);
                }
            };
            O_A_BBRlabel.Click += delegate
            {
                if (O_A_BBls.SelectedItem != null)
                {
                    O_A_BBls.Items.Remove(O_A_BBls.SelectedItem);
                }
            };
        }
        public void loadFirework()
        {
            string[] texts = "BALL,BALL_LARGE,BURST,CREEPER,STAR".Split(',');
            foreach (string t in texts)
            {
                E2_FC_TBox.Items.Add(t);
            }
            string[] clist = "E2_F_FPS/E2_F_FE/E2_F_FH/E2_F_FH2/E2_F_FC/E2_F_FB".Split('/');
            foreach (string cl in clist)
            {
                Button add = (Button)Controls.Find(cl + "Alabel", true).FirstOrDefault();
                Button remove = (Button)Controls.Find(cl + "Rlabel", true).FirstOrDefault();
                ListBox lb = (ListBox)Controls.Find(cl + "ls", true).FirstOrDefault();
                add.Click += delegate
                {
                    string item = E2_FC_TBox.Text + "-" + getTrueFalse(E2_F_T2label).ToUpper() + "-" + getTrueFalse(E2_F_Flabel).ToUpper() + "-" + E2_FC_R.Text + "-" + E2_FC_G.Text + "-" + E2_FC_B.Text;
                    lb.Items.Add(item);
                };
                remove.Click += delegate
                {
                    if (lb.SelectedItem != null)
                    {
                        lb.Items.Remove(lb.SelectedItem);
                    }
                };
            }
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
        public void loadPotionEff()
        {
            string[] lines = File.ReadAllLines(filepath + "/Resource/potion/potioneff.yml");
            foreach (string text in lines)
            {
                ComboBox potion = (ComboBox)Controls.Find(text + "peff", true).FirstOrDefault();
                Button add = (Button)Controls.Find(text + "peffAlabel", true).FirstOrDefault();
                Button remove = (Button)Controls.Find(text + "peffRlabel", true).FirstOrDefault();
                ListBox list = (ListBox)Controls.Find(text + "peffls", true).FirstOrDefault();
                TextBox duration = (TextBox)Controls.Find(text + "peffD", true).FirstOrDefault();
                TextBox level = (TextBox)Controls.Find(text + "peffL", true).FirstOrDefault();

                string[] enchants = File.ReadAllLines(filepath + "/potioneff.txt");
                foreach (string enchant in enchants)
                {
                    potion.Items.Add(enchant);
                }
                add.Click += delegate
                {
                    list.Items.Add(potion.Text + "-" + duration.Text + "-" + level.Text);
                };
                remove.Click += delegate
                {
                    if (list.SelectedItem != null)
                    {
                        list.Items.Remove(list.SelectedItem);
                    }
                };
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
                if (G_FA_TypeBox.Text != "")
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
                        string[] check = I_ED_Itemids.Text.Split('-');
                        if (check[1] == "0")
                        {
                            deviceinfo += check[0];
                        }
                        else
                        {
                            deviceinfo += I_ED_Itemids.Text.Replace('-', '~');
                        }
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
                        string[] chec3k = I_ED_b4boxids.Text.Split('-');
                        if (chec3k[1] == "0")
                        {
                            deviceinfo += chec3k[0];
                        }
                        else
                        {
                            deviceinfo += I_ED_b4boxids.Text.Replace('-', '~');
                        }

                        string[] check2 = I_ED_afterboxids.Text.Split('-');
                        if (check2[1] == "0")
                        {
                            deviceinfo += check2[0];
                        }
                        else
                        {
                            deviceinfo += I_ED_afterboxids.Text.Replace('-', '~');
                        }
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
            if (getTrueFalse(I2_CB_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Cluster_Bombs:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(I2_CB_Enablelabel));
                string ids = string.Empty;
                string[] check = I2_CB_bombletids.Text.Split('-');
                if (check[1] == "0")
                {
                    ids += check[0];
                }
                else
                {
                    ids += I2_CB_bombletids.Text.Replace('-', '~');
                }
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Bomblet_Type: " + ids);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Delay_Before_Split: " + I2_CB_DBSBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Number_Of_Splits: " + I2_CB_NOSBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Number_Of_Bomblets: " + I2_CB_NOBBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Speed_Of_Bomblets: " + I2_CB_SOBBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Delay_Before_Detonation: " + I2_CB_DBDBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Detonation_Delay_Variation: " + I2_CB_DDVBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Particle_Release: " + getListItem(I2_CB_PRpartls));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Release: " + getListItem(AS25_Sound));
            }
            if (getTrueFalse(E_S_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Shrapnel:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(E_S_Enablelabel));
                string ids = string.Empty;
                string[] check = E_S_BTids.Text.Split('-');
                if (check[1] == "0")
                {
                    ids += check[0];
                }
                else
                {
                    ids += E_S_BTids.Text.Replace('-', '~');
                }
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Block_Type: " + ids);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Amount: " + E_S_ABox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Speed: " + E_S_SBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Place_Blocks: " + getTrueFalse(E_S_PBlabel));
            }
            if (getTrueFalse(E_E_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Explosions:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(E_E_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Knockback: " + E_E_KBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Ignite_Victims: " + E_E_IVBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Damage_Multiplier: " + E_E_DMBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable_Friendly_Fire: " + getTrueFalse(E_E_EFFlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable_Owner_Immunity: " + getTrueFalse(E_E_EOIlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Explosion_No_Damage: " + getTrueFalse(E_E_ENDlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Explosion_Potion_Effect: " + getListItem(E_E_EPEpeffls));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Explosion_No_Grief: " + getTrueFalse(E_E_ENGlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Explosion_Radius: " + E_E_ERBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Explosion_Incendiary: " + getTrueFalse(E_E_EIlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Explosion_Delay: " + E_E_EDBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "On_Impact_With_Anything: " + getTrueFalse(E_E_EOIlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Projectile_Activation_Time: " + E_E_PATBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Shooter: " + E_E_MSBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Victim: " + E_E_MVBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Shooter: " + getListItem(AS26_Sound));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Victim: " + getListItem(AS27_Sound));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Explode: " + getListItem(AS28_Sound));
            }
            if (E2_PE_Alist.Items.Count > 0)
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Potion_Effects:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Activation: " + getListItem(E2_PE_Alist));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Potion_Effect_Shooter: " + getListItem(E2_PE_PESpeffls));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Potion_Effect_Victim: " + getListItem(E2_PE_PEVpeffls));
            }
            if (getTrueFalse(E2_P_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Particles:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(E2_P_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Particle_Terrain: " + getTrueFalse(E2_P_PTlabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Particle_Player_Shoot: " + getListItem(E2_P_PPSpartls));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Particle_Impact_Anything: " + getListItem(E2_P_PIApartls));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Particle_Hit: " + getListItem(E2_P_PHpartls));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Particle_Headshot: " + getListItem(E2_P_PH2partls));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Particle_Critical: " + getListItem(E2_P_PCpartls));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Particle_Backstab: " + getListItem(E2_P_PBpartls));
            }
            if (getTrueFalse(E2_F_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Fireworks:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(E2_F_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Firework_Player_Shoot: " + getListItem(E2_F_FPSls));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Firework_Explode: " + getListItem(E2_F_FEls));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Firework_Hit: " + getListItem(E2_F_FHls));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Firework_Headshot: " + getListItem(E2_F_FH2ls));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Firework_Critical: " + getListItem(E2_F_FCls));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Firework_Backstab: " + getListItem(E2_F_FBls));
            }
            csyamlbox.AppendText(Environment.NewLine + spacebar + "Abilities:");
            if (getListItem(O_A_SEls) != null)
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Super_Effective: " + getListItem(O_A_SEls));
            }
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Death_No_Drop: " + getTrueFalse(O_A_DNDlabel));
            if (getListItem(O_A_BDls) != null)
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Bonus_Drops: " + getListItem(O_A_BDls));
            }
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Reset_Hit_Cooldown: " + getTrueFalse(O_A_RHClabel));
            if (O_A_KBox.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Knockback: " + O_A_KBox.Text);
            }
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "No_Fall_Damage: " + getTrueFalse(O_A_NFDlabel));
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "No_Vertical_Recoil: " + getTrueFalse(O_A_NVRlabel));
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Hurt_Effect: " + getTrueFalse(O_A_HElabel));
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Jetpack_Mode: " + getTrueFalse(O_A_JMlabel));
            if (getListItem(O_A_BBls) != null)
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Break_Blocks: " + getListItem(O_A_BBls));
            }
            if (getTrueFalse(O_HE_Enablelabel) == "true")
            {
                csyamlbox.AppendText(Environment.NewLine + spacebar + "Hit_Events:");
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Enable: " + getTrueFalse(O_HE_Enablelabel));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Shooter: " + O_HE_MSBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Message_Victim: " + O_HE_MVBox.Text);
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Impact: " + getListItem(E2_F_FHls));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Shooter: " + getListItem(E2_F_FH2ls));
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Sounds_Victim: " + getListItem(E2_F_FCls));
            }
            csyamlbox.AppendText(Environment.NewLine + spacebar + "Extras:");
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "One_Time_Use: " + getTrueFalse(O2_E_OTUlabel));
            csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Disable_Underwater: " + getTrueFalse(O2_E_DUlabel));
            if (O2_E_MVSBox.Text != "")
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Make_Victim_Speak: " + O2_E_MVSBox.Text);
            }
            if (getListItem(O2_E_MVRCls) != null)
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Make_Victim_Run_Commmand: " + getListItem(O2_E_MVRCls).Replace(',', '|'));
            }
            if (getListItem(O2_E_RCCls) != null)
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Run_Console_Command: " + getListItem(O2_E_RCCls).Replace(',', '|'));
            }
            if (getListItem(O2_E_RCls) != null)
            {
                csyamlbox.AppendText(Environment.NewLine + doublespacebar + "Run_Command: " + getTrueFalse(O2_E_OTUlabel));
                foreach (string text in O2_E_RCls.Items)
                {
                    csyamlbox.AppendText(Environment.NewLine + doublespacebar + spacebar + "- '" + text + "'");
                }
            }
        }
        private void II_ItemIDBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            II_IDPics.Image = Image.FromFile(@filepath + "/Resource/ids/" + II_ItemIDids.Text + ".png");
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
                foreach (Control c in G_A_Ammolabel.Controls)
                {
                    if (c.Name != "G_A_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in G_A_Ammolabel.Controls)
                {
                    if (c.Name != "G_A_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
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
                G_FA_GBlabel.Visible = true;
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
                foreach (Control c in G_S_Scopelabel.Controls)
                {
                    if (c.Name != "G_Scope_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in G_S_Scopelabel.Controls)
                {
                    if (c.Name != "G_Scope_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
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
                foreach (Control c in I_RS_riotlabel.Controls)
                {
                    if (c.Name != "I_RS_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in I_RS_riotlabel.Controls)
                {
                    if (c.Name != "I_RS_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
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
                foreach (Control c in O_WS_namelabel.Controls)
                {
                    if (c.Name != "O_WS_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in O_WS_namelabel.Controls)
                {
                    if (c.Name != "O_WS_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
            }
        }

        private void O_CCR_Enablelabe_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(O_CCR_Enablelabel) == "true")
            {
                foreach (Control c in O_CCR_namelabel.Controls)
                {
                    if (c.Name != "O_CCR_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in O_CCR_namelabel.Controls)
                {
                    if (c.Name != "O_CCR_Enablelabel")
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
                foreach (Control c in O_RC_namelabel.Controls)
                {
                    if (c.Name != "O_RC_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in O_RC_namelabel.Controls)
                {
                    if (c.Name != "O_RC_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
            }
        }

        private void G2_H_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(G2_H_Enablelabel) == "true")
            {
                foreach (Control c in G2_H_namelabel.Controls)
                {
                    if (c.Name != "G2_H_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in G2_H_namelabel.Controls)
                {
                    if (c.Name != "G2_H_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
            }
        }

        private void G2_B_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(G2_B_Enablelabel) == "true")
            {
                foreach (Control c in G2_B_namelabel.Controls)
                {
                    if (c.Name != "G2_B_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in G2_B_namelabel.Controls)
                {
                    if (c.Name != "G2_B_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
            }
        }

        private void G2_CH_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(G2_CH_Enablelabel) == "true")
            {
                foreach (Control c in G2_CH_namelabel.Controls)
                {
                    if (c.Name != "G2_CH_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in G2_CH_namelabel.Controls)
                {
                    if (c.Name != "G2_CH_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
            }
        }

        private void E_SEOH_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(E_SEOH_Enablelabel) == "true")
            {
                foreach (Control c in E_SEOH_namelabel.Controls)
                {
                    if (c.Name != "E_SEOH_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in E_SEOH_namelabel.Controls)
                {
                    if (c.Name != "E_SEOH_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
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
                foreach (Control c in G2_DBOFT_titlelabel.Controls)
                {
                    if (c.Name != "G2_DBOFT_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in G2_DBOFT_titlelabel.Controls)
                {
                    if (c.Name != "G2_DBOFT_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
            }
        }

        private void I_A_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(I_A_Enablelabel) == "true")
            {
                foreach (Control c in I_A_GBlabel.Controls)
                {
                    if (c.Name != "I_A_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in I_A_GBlabel.Controls)
                {
                    if (c.Name != "I_A_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
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
                foreach (Control c in I_ED_namelabel.Controls)
                {
                    if (c.Name != "I_ED_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in I_ED_namelabel.Controls)
                {
                    if (c.Name != "I_ED_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
            }
        }

        private void I_A_BTBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            I_A_BTPics.Image = Image.FromFile(@filepath + "/Resource/ids/" + I_A_BTBox.Text + ".png");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveFormState();
        }
        private void loadFormState()
        {
            if (File.Exists(filepath + "/state.txt"))
            {
                string[] cnamelist = File.ReadAllLines(filepath + "/state.txt");
                foreach (string cname in cnamelist)
                {
                    string[] value = cname.Split('=');
                    Control c = Controls.Find(value[0], true).FirstOrDefault();
                    if (c is ListBox)
                    {
                        ListBox lb = (ListBox)c;
                        string[] lbitems = value[1].Split('|');
                        foreach (string item in lbitems)
                        {
                            lb.Items.Add(item);
                        }
                    }
                    if (c is TextBox)
                    {
                        TextBox tb = (TextBox)c;
                        tb.Text = value[1];
                    }
                    if (c is ComboBox)
                    {
                        ComboBox cb = (ComboBox)c;
                        cb.SelectedItem = value[1];
                    }
                    if (c is CheckBox)
                    {
                        CheckBox cb = (CheckBox)c;
                        if (value[1] == "Checked")
                        {
                            cb.Checked = true;
                        }
                    }
                }
            }
        }
        private void saveFormState()
        {
            File.Delete(filepath + "/state.txt");
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
                    if (c2.Text != "")
                    {
                        sb.AppendLine(c2.Name + "=" + c2.Text);
                    }
                }
                if (ctrl is CheckBox)
                {
                    CheckBox c2 = (CheckBox)ctrl;
                    if (c2.CheckState == CheckState.Checked)
                    {
                        sb.AppendLine(c2.Name + "=" + c2.CheckState);
                    }
                }
                if (ctrl is ComboBox)
                {
                    ComboBox c2 = (ComboBox)ctrl;
                    if (c2.Text != "")
                    {
                        sb.AppendLine(c2.Name + "=" + c2.Text);
                    }
                }
                if (ctrl is ListBox)
                {
                    ListBox c2 = (ListBox)ctrl;
                    if (c2.Items.Count > 0)
                    {
                        string c2text = string.Empty;
                        foreach (string text in c2.Items)
                        {
                            c2text += "|" + text;
                        }
                        sb.AppendLine(c2.Name + "=" + c2text);
                    }
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

        private void I2_CS_bombletids_SelectedIndexChanged(object sender, EventArgs e)
        {
            I2_CB_bombPics.Image = Image.FromFile(@filepath + "/Resource/ids/" + I2_CB_bombletids.Text + ".png");
        }

        private void I2_CB_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(I2_CB_Enablelabel) == "true")
            {
                foreach (Control c in I2_CS_clusterbomlabel.Controls)
                {
                    if (c.Name != "I2_CB_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in I2_CS_clusterbomlabel.Controls)
                {
                    if (c.Name != "I2_CB_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
            }
        }

        private void E_S_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(E_S_Enablelabel) == "true")
            {
                foreach (Control c in E_S_namelabel.Controls)
                {
                    if (c.Name != "E_S_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in E_S_namelabel.Controls)
                {
                    if (c.Name != "E_S_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
            }
        }

        private void E_E_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(E_E_Enablelabel) == "true")
            {

                foreach (Control c in E_E_namelabel.Controls)
                {
                    if (c.Name != "E_E_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in E_E_namelabel.Controls)
                {
                    if (c.Name != "E_E_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
            }
        }

        private void E_S_BTids_SelectedIndexChanged(object sender, EventArgs e)
        {
            E_S_BTPics.Image = Image.FromFile(@filepath + "/Resource/ids/" + E_S_BTids.Text + ".png");
        }

        private void E_L_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(E_L_Enablelabel) == "true")
            {

                foreach (Control c in E_L_namelabel.Controls)
                {
                    if (c.Name != "E_L_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in E_L_namelabel.Controls)
                {
                    if (c.Name != "E_L_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
            }
        }

        private void E2_P_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(E2_P_Enablelabel) == "true")
            {

                foreach (Control c in E2_P_namelabel.Controls)
                {
                    if (c.Name != "E2_P_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in E2_P_namelabel.Controls)
                {
                    if (c.Name != "E2_P_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
            }
        }

        private void E2_F_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(E2_F_Enablelabel) == "true")
            {

                foreach (Control c in E2_F_Fnamelabel.Controls)
                {
                    if (c.Name != "E2_F_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in E2_F_Fnamelabel.Controls)
                {
                    if (c.Name != "E2_F_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
            }
        }

        private void O_HE_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(O_HE_Enablelabel) == "true")
            {

                foreach (Control c in O_HE_namelabel.Controls)
                {
                    if (c.Name != "O_HE_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in O_HE_namelabel.Controls)
                {
                    if (c.Name != "O_HE_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
            }
        }
        public void ResetAllControls(Control ctrl)
        {

            foreach (Control control in ctrl.Controls)
            {
                if (control is PictureBox)
                {
                    PictureBox pb = (PictureBox)control;
                    pb.Image = null;
                }
                if (control is GroupBox || control is TabControl || control is TabPage)
                {
                    ResetAllControls(control);
                }
                if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    textBox.Text = null;
                }
                if (control is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)control;
                    comboBox.Text = "";
                }

                if (control is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)control;
                    checkBox.Checked = false;
                }

                if (control is ListBox)
                {
                    ListBox listBox = (ListBox)control;
                    listBox.Items.Clear();
                }
            }
        }
        private void clearBtn_Click(object sender, EventArgs e)
        {
            ResetAllControls(this);
        }

        private void S_PI_Enablelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (getTrueFalse(S_PI_Enablelabel) == "true")
            {

                foreach (Control c in S_PIncendiarylabel.Controls)
                {
                    if (c.Name != "S_PI_Enablelabel")
                    {
                        c.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control c in S_PIncendiarylabel.Controls)
                {
                    if (c.Name != "S_PI_Enablelabel")
                    {
                        c.Visible = false;
                    }
                }
            }
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "YAML Files|*.yml";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                loadYAMLFile(ofd.FileName);
            }
        }
        public string[] getStringFromDictionary(string key, Dictionary<string, string[]> dics)
        {
            List<string> value = new List<string>();
            foreach (string word in dics.Keys)
            {
                if (word.Equals(key))
                {
                    foreach (string a in dics[word])
                    {
                        value.Add(a);
                    }
                }
            }
            return value.ToArray();
        }
        public void ReadYamls(Dictionary<string, string[]> dics)
        {
            clearBtn.PerformClick();
            // GUN ID
            string gunid = getStringFromDictionary("GUN_ID", dics).FirstOrDefault();
            II_GunID.Text = gunid;
            // Item Information
            string[] ii = getStringFromDictionary("Item_Information", dics);
            if (ii != null)
            {
                foreach (string text in ii)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Item_Name")
                    {
                        II_Name.Text = texts[1];
                    }
                    if (texts[0] == "Item_Type")
                    {
                        II_ItemIDids.SelectedText = texts[1].Replace('~', '-');
                    }
                    if (texts[0] == "Item_Lore")
                    {
                        II_Lore.Text = texts[1];
                    }
                    if (texts[0] == "Inventory_Control")
                    {
                        invcbox.Text = texts[1];
                    }
                    if (texts[0] == "Melee_Mode")
                    {
                        II_Meleelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Melee_Attachment")
                    {
                        II_MeleeAttach.Text = texts[1];
                    }
                    if (texts[0] == "Attachments.Type")
                    {
                        II_A_Type.Text = texts[1];
                    }
                    if (texts[0] == "Attachments.Info")
                    {
                        II_A_Info.Text = texts[1];
                    }
                    if (texts[0] == "Attachments.Toggle_Delay")
                    {
                        II_A_Delay.Text = texts[1];
                    }
                    if (texts[0] == "Attachments.Sounds_Toggle")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS2_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Enchantment_To_Check")
                    {
                        string[] ench = texts[1].Split('-');
                        II_Enchantenchant.Text = ench[0];
                        II_enchLevel.Text = ench[1];
                    }
                    if (texts[0] == "Skip_Name_Check")
                    {
                        II_SkipNamelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Sounds_Acquired")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS1_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Remove_Unused_Tag")
                    {
                        II_RemoveTaglabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Hidden_From_List")
                    {
                        II_HideListlabel.Checked = Boolean.Parse(texts[1]);
                    }
                }
            }
            // Shooting
            string[] shot = getStringFromDictionary("Shooting", dics);
            string type = "";
            if (shot != null)
            {
                foreach (string text in shot)
                {
                    string[] texts = text.Split('|');
                    if (texts[0] == "Disable")
                    {
                        S_Disablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Dual_Wield")
                    {
                        S_DualWieldlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Right_Click_To_Shoot")
                    {
                        S_RightClick2Shootlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Cancel_Left_Click_Block_Damage")
                    {
                        S_CancelLeftClicklabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Cancel_Right_Click_Interactions")
                    {
                        S_CancelRightInteractlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Delay_Between_Shots")
                    {
                        S_DelayBox.Text = texts[1];
                    }
                    if (texts[0] == "Recoil_Amount")
                    {
                        S_RecoilBox.Text = texts[1];
                    }
                    if (texts[0] == "Projectile_Amount")
                    {
                        S_ProjectileAmountBox.Text = texts[1];
                    }
                    if (texts[0] == "Projectile_Type")
                    {
                        S_projectileType.SelectedText = texts[1];
                        type = texts[1];
                    }
                    if (texts[0] == "Projectile_SubType")
                    {
                        if (type == "grenade" || type == "flare")
                        {
                            S_Projectileids.SelectedText = texts[1].Replace('~', '-');
                        }
                        if (type == "splash")
                        {
                            S_Potpotion.SelectedText = texts[1].Replace('~', '-');
                        }
                        if (type == "fireball")
                        {
                            S_P_Fireball.Checked = Boolean.Parse(texts[1]);
                        }
                        if (type == "energy")
                        {
                            string[] energy = texts[1].Split('-');
                            S_P_RangeBox.Text = energy[0];
                            S_P_Radius.Text = energy[1];
                            S_P_Victim.Text = energy[2];
                            S_P_Wall.Text = energy[3];
                        }
                    }
                    if (texts[0] == "Remove_Arrows_On_Impact")
                    {
                        S_RemoveArrowlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Remove_Bullet_Drop")
                    {
                        S_RemoveBulletlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Removal_Or_Drag_Delay")
                    {
                        string[] removal = texts[1].Split('-');
                        S_removedragbox.Text = removal[0];
                        S_RemovalDraglabel.Checked = Boolean.Parse(removal[1]);
                    }
                    if (texts[0] == "Projectile_Speed")
                    {
                        S_projectileSpeed.Text = texts[1];
                    }
                    if (texts[0] == "Projectile_Damage")
                    {
                        S_ProjectileDamage.Text = texts[1];
                    }
                    if (texts[0] == "Projectile_Flames")
                    {
                        S_PFlamelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Projectile_Incendiary.Enable")
                    {
                        S_PI_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Projectile_Incendiary.Duration")
                    {
                        S_piduration.Text = texts[1];
                    }
                    if (texts[0] == "Bullet_Spread")
                    {
                        S_BSBox.Text = texts[1];
                    }
                    if (texts[0] == "Reset_Fall_Distance")
                    {
                        S_ResetFallDislabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Sounds_Projectile")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS3_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Sounds_Shoot")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS4_Sound.Items.Add(s);
                        }
                    }
                }
            }
            // Sneak
            string[] sneak = getStringFromDictionary("Shooting", dics);
            if (sneak != null)
            {
                foreach (string text in sneak)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        G_S_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "No_Recoil")
                    {
                        G_S_NoRecoillabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Bullet_Spread")
                    {
                        G_S_BSlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Sneak_Before_Shooting")
                    {
                        G_S_SBSBox.Text = texts[1];
                    }
                }
            }
            // Fully Automatic
            string[] auto = getStringFromDictionary("Fully_Automatic", dics);
            if (auto != null)
            {
                foreach (string text in auto)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        G_F_Automaticlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Fire_Rate")
                    {
                        G_FB_FRBox.Text = texts[1];
                    }
                }
            }
            // Burst Fire
            string[] burst = getStringFromDictionary("Burstfire", dics);
            if (burst != null)
            {
                foreach (string text in burst)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        G_F_Burstlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Shot_Per_Burst")
                    {
                        G_FB_FRBox.Text = texts[1];
                    }
                    if (texts[0] == "Delay_Between_Shots_In_Burst")
                    {
                        G_FB_DBSIBBox.Text = texts[1];
                    }
                }
            }
            // Ammunition
            string[] ammo = getStringFromDictionary("Ammo", dics);
            if (ammo != null)
            {
                foreach (string text in ammo)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        G_A_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Ammo_Item_ID")
                    {
                        G_AmmoBoxids.SelectedText = texts[1].Replace('~', '-');
                    }
                    if (texts[0] == "Ammo_Name_Check")
                    {
                        G_A_ANCbox.Text = texts[1];
                    }
                    if (texts[0] == "Take_Ammo_Per_Shot")
                    {
                        G_A_TAPSlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Sound_Out_Of_Ammo")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS5_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Sounds_Shoot_With_No_Ammo")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS6_Sound.Items.Add(s);
                        }
                    }
                }
            }
            // Reloading
            string[] reload = getStringFromDictionary("Reloading", dics);
            if (reload != null)
            {
                foreach (string text in reload)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        G_R_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Reload_With_Mouse")
                    {
                        G_R_RWMouselabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Starting_Amount")
                    {
                        G_R_Startbox.Text = texts[1];
                    }
                    if (texts[0] == "Reload_Amount")
                    {
                        G_R_RAbox.Text = texts[1];
                    }
                    if (texts[0] == "Reload_Bullets_Individually")
                    {
                        G_R_RBIlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Take_Ammo_On_Reload")
                    {
                        G_R_TAORlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Take_Ammo_As_Magazine")
                    {
                        G_R_TAAMlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Reload_Duration")
                    {
                        G_R_RDBox.Text = texts[1];
                    }
                    if (texts[0] == "Reload_Shoot_Delay")
                    {
                        G_R_RSDBox.Text = texts[1];
                    }
                    if (texts[0] == "Destroy_When_Empty")
                    {
                        G_R_DWElabel.Text = texts[1];
                    }
                    if (texts[0] == "Sounds_Out_Of_Ammo")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS7_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Sounds_Reloading")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS8_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Dual_Wield.Sounds_Single_Reload")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS9_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Dual_Wield.Single_Reload_Duration")
                    {
                        G_R_DWSRDBox.Text = texts[1];
                    }
                }
            }
            // Firearm Actions
            string[] firearm = getStringFromDictionary("Firearm_Action", dics);
            if (firearm != null)
            {
                foreach (string text in firearm)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Type")
                    {
                        G_FA_TypeBox.SelectedText = texts[1];
                    }
                    if (texts[0] == "Open_Duration")
                    {
                        G_FA_ODBox.Text = texts[1];
                    }
                    if (texts[0] == "Close_Duration")
                    {
                        G_FA_CDBox.Text = texts[1];
                    }
                    if (texts[0] == "Close_Shoot_Delay")
                    {
                        G_FA_CSDbox.Text = texts[1];
                    }
                    if (texts[0] == "Reload_Open_Delay")
                    {
                        G_FA_RODBox.Text = texts[1];
                    }
                    if (texts[0] == "Reload_Close_Delay")
                    {
                        G_FA_RCDBox.Text = texts[1];
                    }
                    if (texts[0] == "Sound_Open")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS10_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Sound_Close")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS11_Sound.Items.Add(s);
                        }
                    }
                }
            }
            // Scope
            string[] scope = getStringFromDictionary("Scope", dics);
            if (scope != null)
            {
                foreach (string text in scope)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        G_Scope_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Night_Vision")
                    {
                        G_Scope_NVlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Zoom_Amount")
                    {
                        G_Scope_ZABox.Text = texts[1];
                    }
                    if (texts[0] == "Zoom_Bullet_Spread")
                    {
                        G_Scope_ZBSBox.Text = texts[1];
                    }
                    if (texts[0] == "Zoom_Before_Shooting")
                    {
                        G_Scope_ZBSBox.Text = texts[1];
                    }
                    if (texts[0] == "Sounds_Toggle_Zoom")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS12_Sound.Items.Add(s);
                        }
                    }
                }
            }
            // Riot Sheilds
            string[] riot = getStringFromDictionary("Riot_Sheild", dics);
            if (riot != null)
            {
                foreach (string text in riot)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        I_RS_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Do_Not_Block_Projectiles")
                    {
                        I_RS_DNBPlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Do_Not_Block_Melee_Attacks")
                    {
                        I_RS_DNBMAlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Durability_Based_On_Damage")
                    {
                        I_RS_DBODlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Durability_Loss_Per_Hit")
                    {
                        I_RS_DLPHBox.Text = texts[1];
                    }
                    if (texts[0] == "Forcefield_Mode")
                    {
                        I_RS_FFMlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Sounds_Blocked")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS13_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Sounds_Break")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS14_Sound.Items.Add(s);
                        }
                    }
                }
            }
            // Weapon Stores
            string[] signshop = getStringFromDictionary("SignShops", dics);
            if (signshop != null)
            {
                foreach (string text in signshop)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        O_WS_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Price")
                    {
                        string[] ids = texts[1].Split('-');
                        if (ids.Count() == 2)
                        {
                            O_WS_Priceids.SelectedText = ids[0];
                            O_WS_PriceNum.Text = ids[1];   
                        }
                        else
                        {
                            O_WS_Priceids.SelectedText = ids[0] + "~" + ids[1];
                            O_WS_PriceNum.Text = ids[2];
                        }
                    }
                    if (texts[0] == "Sign_Gun_ID")
                    {
                        O_GUNIDBox.Text = texts[1];
                    }
                }
            }
            // Custom Crafting Recipes
            string[] crafting = getStringFromDictionary("Crafting", dics);
            if (crafting != null)
            {
                foreach (string text in crafting)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        O_CCR_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Quantity")
                    {
                        O_CCR_QBox.Text = texts[1];
                    }
                    if (texts[0] == "Shaped")
                    {
                        O_CCR_Shapedlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Ingredients")
                    {
                        string[] ids = texts[1].Split(',');
                        for (int i = 1; i < ids.Count(); i++)
                        {
                            ComboBox id = (ComboBox)Controls.Find("C_OOR" + i.ToString() + "ids", true).First();
                            id.SelectedText = ids[i].Replace('~','-');
                        }
                    }
                }
            }
            // Region Checks
            string[] region = getStringFromDictionary("Region_Check", dics);
            if (region != null)
            {
                foreach (string text in region)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        O_RC_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "World_And_Coordinates")
                    {
                        string[] world = texts[1].Split(',');
                        O_RC_WorldBox.Text = world[0];
                        O_RC_X1.Text = world[1];
                        O_RC_Y1.Text = world[2];
                        O_RC_Z1.Text = world[3];
                        O_RC_X2.Text = world[4];
                        O_RC_Y2.Text = world[5];
                        O_RC_Z2.Text = world[6];
                        O_RC_Blacklabel.Checked = Boolean.Parse(world[7]);
                    }
                    if (texts[0] == "Message_Of_Denial")
                    {
                        O_RC_Modbox.Text = texts[1];
                    }
                }
            }
            // Custom Death Mesage
            string[] cdm = getStringFromDictionary("Custom_Death_Message", dics);
            if (cdm != null)
            {
                foreach (string text in cdm)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Normal")
                    {
                        O_CDM_MBox.Text = texts[1];
                    }
                }
            }
            // Headshot
            string[] headshot = getStringFromDictionary("Headshot", dics);
            if (headshot != null)
            {
                foreach (string text in headshot)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        G2_H_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Bonus_Damage")
                    {
                        G2_H_BDBox.Text = texts[1];
                    }
                    if (texts[0] == "Message_Shooter")
                    {
                        G2_H_MSBox.Text = texts[1];
                    }
                    if (texts[0] == "Message_Victim")
                    {
                        G2_H_MVBox.Text = texts[1];
                    }
                    if (texts[0] == "Sounds_Shooter")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS15_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Sounds_Victim")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS16_Sound.Items.Add(s);
                        }
                    }
                }
            }
            // Backstab
            string[] backstab = getStringFromDictionary("Backstab", dics);
            if (backstab != null)
            {
                foreach (string text in backstab)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        G2_B_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Bonus_Damage")
                    {
                        G2_B_BDBox.Text = texts[1];
                    }
                    if (texts[0] == "Message_Shooter")
                    {
                        G2_B_MSBox.Text = texts[1];
                    }
                    if (texts[0] == "Message_Victim")
                    {
                        G2_B_MVBox.Text = texts[1];
                    }
                    if (texts[0] == "Sounds_Shooter")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS17_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Sounds_Victim")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS18_Sound.Items.Add(s);
                        }
                    }
                }
            }
            // Summon Entities
            string[] summon = getStringFromDictionary("Spawn_Entity_On_Hit", dics);
            if (summon != null)
            {
                foreach (string text in summon)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        E_SEOH_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Chance")
                    {
                        E_SEOH_ChanceBox.Text = texts[1];
                    }
                    if (texts[0] == "Mob_Name")
                    {
                        E_SEOH_MNBox.Text = texts[1];
                    }
                    if (texts[0] == "EntityType_Baby_Explode_Amount")
                    {
                        string[] ett = texts[1].Split(',');
                        foreach (string e in ett)
                        {
                            E_SEOH_EBox.Items.Add(e);
                        }
                    }
                    if (texts[0] == "Make_Entities_Target_Victim")
                    {
                        E_SEOH_METVlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Timed_Death")
                    {
                        E_SEOH_TDBox.Text = texts[1];
                    }
                    if (texts[0] == "Entity_Disable_Drops")
                    {
                        E_SEOH_EDDlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Message_Shooter")
                    {
                        E_SEOH_MSBox.Text = texts[1];
                    }
                    if (texts[0] == "Message_Victim")
                    {
                        E_SEOH_MVBox.Text = texts[1];
                    }
                }
            }
            // Damage Based On Flight Time
            string[] dboft = getStringFromDictionary("Damage_Based_On_Flight_Time", dics);
            if (dboft != null)
            {
                foreach (string text in dboft)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        G2_DBOFT_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Bonus_Damage_Per_Tick")
                    {
                        G2_DBOFT_BDPTBox.Text = texts[1];
                    }
                    if (texts[0] == "Minimum_Damage")
                    {
                        G2_DBOFT_MDBox.Text = texts[1];
                    }
                    if (texts[0] == "Maximum_Damage")
                    {
                        G2_DBOFT_MXBox.Text = texts[1];
                    }
                }
            }
            // AirStrikes
            string[] airstrike = getStringFromDictionary("Airstrikes", dics);
            if (airstrike != null)
            {
                foreach (string text in airstrike)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        I_A_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Flare_Activation_Delay")
                    {
                        I_A_FADBox.Text = texts[1];
                    }
                    if (texts[0] == "Particle_Call_Airstrike")
                    {
                        string[] particle = texts[1].Split(',');
                        foreach (string p in particle)
                        {
                            I_A_PCABoxpartls.Items.Add(p);
                        }
                    }
                    if (texts[0] == "Message_Call_Airstrike")
                    {
                        I_A_MCABox.Text = texts[1];
                    }
                    if (texts[0] == "Block_Type")
                    {
                        I_A_BTBox.SelectedText = texts[1].Replace('~', '-');
                    }
                    if (texts[0] == "Area")
                    {
                        I_A_ABox.Text = texts[1];
                    }
                    if (texts[0] == "Distance_Between_Bombs")
                    {
                        I_A_DBBBox.Text = texts[1];
                    }
                    if (texts[0] == "Height_Dropped")
                    {
                        I_A_HDBox.Text = texts[1];
                    }
                    if (texts[0] == "Vertical_Variation")
                    {
                        I_A_VVBox.Text = texts[1];
                    }
                    if (texts[0] == "Horizontal_Variation")
                    {
                        I_A_HVBox.Text = texts[1];
                    }
                    if (texts[0] == "Multiple_Strikes.Enable")
                    {
                        I_A_MS_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Multiple_Strikes.Number_Of_Strikes")
                    {
                        I_A_MS_NOSBox.Text = texts[1];
                    }
                    if (texts[0] == "Multiple_Strikes.Delay_Between_Strikes")
                    {
                        I_A_MS_DBSBox.Text = texts[1];
                    }
                    if (texts[0] == "Sounds_Airstrike")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS21_Sound.Items.Add(s);
                        }
                    }
                }
            }
            //Explosive Device
            string[] explodevice = getStringFromDictionary("Explosive_Devices", dics);
            string deviceinfo = "";
            if (explodevice != null)
            {
                foreach (string text in explodevice)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        I_ED_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Device_Type")
                    {
                        I_ED_DeviceTBox.Text = texts[1];
                        deviceinfo = texts[1];
                    }
                    if (texts[0] == "Device_Info")
                    {
                        switch (deviceinfo)
                        {
                            case "landmine":
                                I_ED_Itemids.Text = texts[1].Replace('~', '-');
                                break;
                            case "remote":
                                string[] list = texts[1].Split('-');
                                I_ED_Abox.Text = list[0];
                                I_ED_UIDbox.Text = list[1];
                                I_ED_Headbox.Text = list[2];
                                break;
                            case "trap":
                                string[] boolean = texts[1].Split('-');
                                I_ED_Chestlabel.Checked = Boolean.Parse(boolean[0]);
                                I_ED_Picklabel.Checked = Boolean.Parse(boolean[1]);
                                I_ED_DAPlabel.Checked = Boolean.Parse(boolean[2]);
                                I_ED_Reuselabel.Checked = Boolean.Parse(boolean[3]);
                                I_ED_NIDlabel.Checked = Boolean.Parse(boolean[4]);
                                break;
                            case "itembomb":
                                string[] ib = texts[1].Split(',');
                                I_ED_A2Box.Text = ib[0];
                                I_ED_Sbox.Text = ib[1];
                                I_ED_b4boxids.SelectedText = ib[2].Replace('~', '-');
                                I_ED_afterboxids.SelectedText = ib[3].Replace('~', '-');
                                break;
                        }
                    }
                    if (texts[0] == "Sounds_Deploy")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS22_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Remote_Bypass_Regions")
                    {
                        I_ED_RBRlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Message_Disarm")
                    {
                        I_ED_MDbox.Text = texts[1];
                    }
                    if (texts[0] == "Message_Trigger_Placer")
                    {
                        I_ED_MTPbox.Text = texts[1];
                    }
                    if (texts[0] == "Message_Trigger_Victim")
                    {
                        I_ED_MTVbox.Text = texts[1];
                    }
                    if (texts[0] == "Sounds_Alert_Placer")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS23_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Sounds_Trigger")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach(string s in sound)
                        {
                            AS24_Sound.Items.Add(s);
                        }
                    }
                }
            }
            // Cluster Bombs
            string[] cluster = getStringFromDictionary("Cluster_Bombs", dics);
            if (cluster != null)
            {
                foreach (string text in cluster)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        I2_CB_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Bomblet_Type")
                    {
                        I2_CB_bombletids.SelectedText = texts[1].Replace('~', '-');
                    }
                    if (texts[0] == "Delay_Before_Split")
                    {
                        I2_CB_DBSBox.Text = texts[1];
                    }
                    if (texts[0] == "Number_Of_Splits")
                    {
                        I2_CB_NOSBox.Text = texts[1];
                    }
                    if (texts[0] == "Number_Of_Bomblets")
                    {
                        I2_CB_NOBBox.Text = texts[1];
                    }
                    if (texts[0] == "Speed_Of_Bomblets")
                    {
                        I2_CB_SOBBox.Text = texts[1];
                    }
                    if (texts[0] == "Delay_Before_Detonation")
                    {
                        I2_CB_DBDBox.Text = texts[1];
                    }
                    if (texts[0] == "Detonation_Delay_Variation")
                    {
                        I2_CB_DDVBox.Text = texts[1];
                    }
                    if (texts[0] == "Particle_Release")
                    {
                        string[] particle = texts[1].Split(',');
                        foreach (string p in particle)
                        {
                            I2_CB_PRpartls.Items.Add(p);
                        }
                    }
                    if (texts[0] == "Sounds_Release")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS25_Sound.Items.Add(s);
                        }
                    }
                }
            }
            // Sharnel
            string[] sharpnel = getStringFromDictionary("Shrapnel", dics);
            if (sharpnel != null)
            {
                foreach (string text in sharpnel)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        E_S_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Block_Type")
                    {
                        E_S_BTids.SelectedText = texts[1].Replace('~', '-');
                    }
                    if (texts[0] == "Amount")
                    {
                        E_S_ABox.Text = texts[1];
                    }
                    if (texts[0] == "Speed")
                    {
                        E_S_SBox.Text = texts[1];
                    }
                    if (texts[0] == "Place_Blocks")
                    {
                        E_S_PBlabel.Checked = Boolean.Parse(texts[1]);
                    }
                }
            }
            // Explosions 
            string[] explo = getStringFromDictionary("Explosions", dics);
            if (explo != null)
            {
                foreach (string text in explo)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        E_E_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Knockback")
                    {
                        E_E_KBox.Text = texts[1];
                    }
                    if (texts[0] == "Ignite_Victims")
                    {
                        E_E_IVBox.Text = texts[1];
                    }
                    if (texts[0] == "Damage_Multiplier")
                    {
                        E_E_DMBox.Text = texts[1];
                    }
                    if (texts[0] == "Enable_Friendly_Fire")
                    {
                        E_E_EFFlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Enable_Owner_Immunity")
                    {
                        E_E_EOIlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Explosion_No_Damage")
                    {
                        E_E_ENDlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Explosion_Potion_Effect")
                    {
                        string[] peff = texts[1].Split(',');
                        foreach (string pef in peff)
                        {
                            E_E_EPEpeffls.Items.Add(pef);
                        }
                    }
                    if (texts[0] == "Explosion_No_Grief")
                    {
                        E_E_ENGlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Explosion_Radius")
                    {
                        E_E_ERBox.Text = texts[1];
                    }
                    if (texts[0] == "Explosion_Incendiary")
                    {
                        E_E_EIlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Explosion_Delay")
                    {
                        E_E_EDBox.Text = texts[1];
                    }
                    if (texts[0] == "On_Impact_With_Anything")
                    {
                        E_E_OIWAlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Projectile_Activation_Time")
                    {
                        E_E_PATBox.Text = texts[1];
                    }
                    if (texts[0] == "Message_Shooter")
                    {
                        E_E_MSBox.Text = texts[1];
                    }
                    if (texts[0] == "Message_Victim")
                    {
                        E_E_MVBox.Text = texts[1];
                    }
                    if (texts[0] == "Sounds_Shooter")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS26_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Sounds_Victim")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS27_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Sounds_Explode")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS28_Sound.Items.Add(s);
                        }
                    }
                }
            }
            // Lightning
            string[] lightning = getStringFromDictionary("Lightning", dics);
            if (lightning != null)
            {
                foreach (string text in lightning)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        E_L_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "No_Damage")
                    {
                        E_L_NDlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "On_Impact_With_Anything")
                    {
                        E_L_OIWAlabel.Checked = Boolean.Parse(texts[1]);
                    }
                }
            }
            // Potion effects
            string[] pe = getStringFromDictionary("Potion_Effects", dics);
            if (pe != null)
            {
                foreach (string text in pe)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Activation")
                    {
                        string[] activation = texts[1].Split(',');
                        foreach (string a in activation)
                        {
                            E2_PE_Alist.Items.Add(a);
                        }
                    }
                    if (texts[0] == "Potion_Effect_Shooter")
                    {
                        string[] pot = texts[1].Split(',');
                        foreach (string p in pot)
                        {
                            E2_PE_PESpeffls.Items.Add(p);
                        }
                    }
                    if (texts[0] == "Potion_Effect_Victim")
                    {
                        string[] pot = texts[1].Split(',');
                        foreach (string p in pot)
                        {
                            E2_PE_PEVpeffls.Items.Add(p);
                        }
                    }
                }
            }
            // Particle Effects
            string[] parteff = getStringFromDictionary("Particles", dics);
            if (parteff != null)
            {
                foreach (string text in parteff)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        E2_P_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Particle_Terrain")
                    {
                        E2_P_PTlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Particle_Player_Shoot")
                    {
                        string[] part = texts[1].Split(',');
                        foreach (string p in part)
                        {
                            E2_P_PPSpartls.Items.Add(p);
                        }
                    }
                    if (texts[0] == "Particle_Impact_Anything")
                    {
                        string[] part = texts[1].Split(',');
                        foreach (string p in part)
                        {
                            E2_P_PIApartls.Items.Add(p);
                        }
                    }
                    if (texts[0] == "Particle_Hit")
                    {
                        string[] part = texts[1].Split(',');
                        foreach (string p in part)
                        {
                            E2_P_PHpartls.Items.Add(p);
                        }
                    }
                    if (texts[0] == "Particle_Headshot")
                    {
                        string[] part = texts[1].Split(',');
                        foreach (string p in part)
                        {
                            E2_P_PH2partls.Items.Add(p);
                        }
                    }
                    if (texts[0] == "Particle_Critical")
                    {
                        string[] part = texts[1].Split(',');
                        foreach (string p in part)
                        {
                            E2_P_PCpartls.Items.Add(p);
                        }
                    }
                    if (texts[0] == "Particle_Backstab")
                    {
                        string[] part = texts[1].Split(',');
                        foreach (string p in part)
                        {
                            E2_P_PBpartls.Items.Add(p);
                        }
                    }
                }
            }
            // Fireworks
            string[] firework = getStringFromDictionary("Fireworks", dics);
            if (firework != null)
            {
                foreach (string text in firework)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        E2_F_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Firework_Player_Shoot")
                    {
                        string[] fw = texts[1].Split(',');
                        foreach (string f in fw)
                        {
                            E2_F_FPSls.Items.Add(f);
                        }
                    }
                    if (texts[0] == "Firework_Explode")
                    {
                        string[] fw = texts[1].Split(',');
                        foreach (string f in fw)
                        {
                            E2_F_FEls.Items.Add(f);
                        }
                    }
                    if (texts[0] == "Firework_Hit")
                    {
                        string[] fw = texts[1].Split(',');
                        foreach (string f in fw)
                        {
                            E2_F_FHls.Items.Add(f);
                        }
                    }
                    if (texts[0] == "Firework_Headshot")
                    {
                        string[] fw = texts[1].Split(',');
                        foreach (string f in fw)
                        {
                            E2_F_FH2ls.Items.Add(f);
                        }
                    }
                    if (texts[0] == "Firework_Critical")
                    {
                        string[] fw = texts[1].Split(',');
                        foreach (string f in fw)
                        {
                            E2_F_FCls.Items.Add(f);
                        }
                    }
                    if (texts[0] == "Firework_Backstab")
                    {
                        string[] fw = texts[1].Split(',');
                        foreach (string f in fw)
                        {
                            E2_F_FBls.Items.Add(f);
                        }
                    }
                }
            }
            // Abilities
            string[] ability = getStringFromDictionary("Abilities", dics);
            if (ability != null)
            {
                foreach (string text in ability)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Super_Effective")
                    {
                        string[] se = texts[1].Split(',');
                        foreach (string s in se)
                        {
                            O_A_SEls.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Death_No_Drop")
                    {
                        O_A_DNDlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Bonus_Drops")
                    {
                        string[] bd = texts[1].Split(',');
                        foreach (string b in bd)
                        {
                            O_A_BDls.Items.Add(b);
                        }
                    }
                    if (texts[0] == "Reset_Hit_Cooldown")
                    {
                        O_A_RHClabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Knockback")
                    {
                        O_A_KBox.Text = texts[1];
                    }
                    if (texts[0] == "No_Fall_Damage")
                    {
                        O_A_NFDlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "No_Vertical_Recoil")
                    {
                        O_A_NVRlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Hurt_Effect")
                    {
                        O_A_HElabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Jetpack_Mode")
                    {
                        O_A_JMlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Break_Blocks")
                    {
                        O_A_BBls.Items.Add(texts[1]);
                    }
                }
            }
            // Hit Events
            string[] hit = getStringFromDictionary("Hit_Events", dics);
            if (hit != null)
            {
                foreach (string text in hit)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "Enable")
                    {
                        O_HE_Enablelabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Message_Shooter")
                    {
                        O_HE_MSBox.Text = texts[1];
                    }
                    if (texts[0] == "Message_Victim")
                    {
                        O_HE_MVBox.Text = texts[1];
                    }
                    if (texts[0] == "Sounds_Impact")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS29_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Sounds_Shooter")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS30_Sound.Items.Add(s);
                        }
                    }
                    if (texts[0] == "Sounds_Victim")
                    {
                        string[] sound = texts[1].Split(',');
                        foreach (string s in sound)
                        {
                            AS31_Sound.Items.Add(s);
                        }
                    }
                }
            }
            // Extras
            string[] extra = getStringFromDictionary("Extras", dics);
            if (extra != null)
            {
                foreach (string text in extra)
                {
                    string[] texts = text.Split(':');
                    if (texts[0] == "One_Time_Use")
                    {
                        O2_E_OTUlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Disable_Underwater")
                    {
                        O2_E_DUlabel.Checked = Boolean.Parse(texts[1]);
                    }
                    if (texts[0] == "Make_Victim_Run_Commmand")
                    {
                        string[] cmd = texts[1].Split('|');
                        foreach (string c in cmd)
                        {
                            O2_E_MVRCls.Items.Add(c);
                        }
                    }
                    if (texts[0] == "Make_Victim_Speak")
                    {
                        O2_E_MVSBox.Text = texts[1];
                    }
                    if (texts[0] == "Run_Console_Command")
                    {
                        string[] cmd = texts[1].Split('|');
                        foreach (string c in cmd)
                        {
                            O2_E_RCCls.Items.Add(c);
                        }
                    }
                    if (texts[0] == "Run_Command")
                    {
                        string[] cmd = texts[1].Split('|');
                        foreach (string c in cmd)
                        {
                            O2_E_RCls.Items.Add(c);
                        }
                    }
                }
            }
        }
        public void loadYAMLFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            Dictionary<string, string[]> yml = new Dictionary<string, string[]>();
            string node;
            foreach (string line in lines)
            {
                if (!line.StartsWith(spacebar) && line.EndsWith(":"))
                {
                    yml.Add("GUN_ID", new string[] { line.Replace(":","")});
                    break;
                }
            }
            for (int j = 0;j < lines.Count() - 1;j++)
            {
                List<string> vl = new List<string>();
                if (lines[j].StartsWith(spacebar) && !lines[j].StartsWith(doublespacebar))
                {
                    node = lines[j].Replace(" ", "").Replace(":", "");
                    for (int i = 1; ;i++)
                    {
                        if (i + j > lines.Count() - 1)
                        {
                            break;
                        }
                        if (lines[i + j].StartsWith(spacebar) && lines[i + j].StartsWith(doublespacebar))
                        {
                            if (lines[i + j].StartsWith(doublespacebar + spacebar))
                            {
                                vl.Remove(lines[i + j - 1].Replace(" ", ""));
                                vl.Add(lines[i + j - 1].Replace(" ", "").Replace(":","") + "." + lines[i + j].Replace(" ", ""));
                            }
                            else
                            {
                                if (lines[i + j].StartsWith(doublespacebar + "-"))
                                {
                                    List<string> l2v = new List<string>();
                                    for (int k = 0; ; k++)
                                    {
                                        string empty = "";
                                        if (i + j + k > lines.Count() - 1)
                                        {
                                            foreach (string litem in l2v.ToArray())
                                            {
                                                empty += litem + "|";
                                            }
                                            if (empty.Length > 0)
                                            {
                                                empty = empty.Remove(empty.Length - 1);
                                            }
                                            vl.Remove(lines[i + j - 1].Replace(" ", ""));
                                            vl.Add(lines[i + j - 1].Replace(" ", "") + empty);
                                            i += k;
                                            break;
                                        }
                                        if (lines[i + j + k].StartsWith(doublespacebar + "-"))
                                        {
                                            l2v.Add(lines[i + j + k].Replace(doublespacebar + "- ", ""));
                                        }
                                        else
                                        {
                                            foreach (string litem in l2v.ToArray())
                                            {
                                                empty += litem + "|";
                                            }
                                            if (empty.Length > 0)
                                            {
                                                empty = empty.Remove(empty.Length - 1);
                                            }
                                            vl.Remove(lines[i + j - 1].Replace(" ", ""));
                                            vl.Add(lines[i + j - 1].Replace(" ", "") + empty);
                                            i += k;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    vl.Add(lines[i + j].Replace(" ", ""));
                                }
                            }
                        }
                        else
                        {
                            j += i - 1;
                            break;
                        }
                    }
                    yml.Add(node, vl.ToArray());
                }
            }
            ReadYamls(yml);
        }
    }
}
