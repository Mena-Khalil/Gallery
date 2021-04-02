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
using System.Collections;

namespace project
{
    public partial class Form1 : Form
    {
        private int Numimag  ;
        ArrayList Files = new ArrayList();
        string Mode;

        public Form1()
        {
            InitializeComponent();
        }
      
        /////////////// openFileDialog to Choose the picture
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please select images ";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            ofd.Filter = "PNG Images |*.png| JPG Images |*.jpg|BMP Images |*.bmp";
            ofd.Multiselect = true;
            DialogResult dr = ofd.ShowDialog();
            if(dr == DialogResult.OK)
            {
                ///// array to safe path photo
                foreach (var element in ofd.FileNames)
                {
                    Files.Add(element);
                }
                //////array to safe name photo
                foreach (var imag in ofd.SafeFileNames)
                {
                  listBox1.Items.Add(imag);
                }

            }
        }

        /////////// choose Multi mode
        private void singlePictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
           listBox1.Enabled = true;
            Mode = "Single";
            timer1.Stop();
        }

        /////////// choose Multi mode
        private void multiPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            listBox1.Enabled = true;
            Mode = "Multi";
            timer1.Stop();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
      ///////////////// Single Mode
            if (Mode == "Single")
            {
                    panel2.Controls.Clear();
                    listBox1.SelectionMode = SelectionMode.One;
                    PictureBox Pict = new PictureBox();
                    Pict.Image = Image.FromFile(Files[listBox1.SelectedIndex].ToString());
                    Pict.SizeMode = PictureBoxSizeMode.StretchImage;
                    Pict.Dock = DockStyle.Fill;
                    this.panel2.Controls.Add(Pict);
                    string pat = Files[listBox1.SelectedIndex].ToString();
                    textBox1.Text = pat;
               

            }
            //////////////// Multi Mode
            else if (Mode == "Multi")
            {
                    panel2.Controls.Clear();
                    listBox1.SelectionMode = SelectionMode.MultiExtended;
                    int x = 15, y = 15;
                    foreach (int img in listBox1.SelectedIndices)
                    {
                        PictureBox pic = new PictureBox();
                        pic.Image = Image.FromFile(Files[img].ToString());
                        pic.Location = new Point(x, y);
                        pic.Size = new Size(125, 125);
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        x += pic.Width + 10;
                        if (x > panel2.Size.Width - 60)
                        {
                            x = 15;
                            y += 138;
                        }
                        textBox1.Text = "";
                  
                    this.panel2.Controls.Add(pic);
                    }
             }
            else
            {

            }
        }

        ///////////////////// Resize form
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (Mode == "Multi")
            {
                listBox1_SelectedIndexChanged(sender, e);
            }
        }

        ///////////////////// Choose Slid Show Mode
        private void slideShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mode = "Show";
            Numimag = 0;
            timer1.Enabled = false;
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("No picture found to show");
            }
            else
            {
                listBox1.ClearSelected();
                timer1.Enabled = true;
                timer1_Tick(sender, e);
            }
        }

        /////////////////// Timer to Slid show
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (slideShoeToolStripMenuItem.CanSelect)
            {
                this.panel2.Controls.Clear();
                PictureBox pictu = new PictureBox();
                if (Numimag >= listBox1.Items.Count )
                {
                    timer1.Stop();
                    Numimag = 0;
                }
                pictu.ImageLocation = Files[Numimag].ToString();
                pictu.SizeMode = PictureBoxSizeMode.StretchImage;
                pictu.Dock = DockStyle.Fill;
                this.panel2.Controls.Add(pictu);
                string patt = Files[Numimag].ToString();
                textBox1.Text = patt;
                statusBar1.Text = Path.GetFileName(patt);
                Numimag++;
                this.panel2.Controls.Add(statusBar1);
            }
            else
            {
                timer1.Enabled = false;
            }
        }

        ///////////////////// Exit Application
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }


    }
}
