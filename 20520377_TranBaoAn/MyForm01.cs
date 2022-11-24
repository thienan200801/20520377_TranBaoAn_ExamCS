using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20520377_TranBaoAn
{
    internal class MyForm01 : Form
    {
        private TrackBar trackBar_color;
        private Button button1;
        private PictureBox mainPic;
        private TrackBar trackBar_brightness;

        Boolean isOpen = false;

        Image originalImage = null;

        public MyForm01()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.trackBar_brightness = new System.Windows.Forms.TrackBar();
            this.trackBar_color = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.mainPic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_brightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_color)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPic)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar_brightness
            // 
            this.trackBar_brightness.Location = new System.Drawing.Point(27, 424);
            this.trackBar_brightness.Maximum = 100;
            this.trackBar_brightness.Name = "trackBar_brightness";
            this.trackBar_brightness.Size = new System.Drawing.Size(879, 56);
            this.trackBar_brightness.TabIndex = 0;
            this.trackBar_brightness.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            this.trackBar_brightness.ValueChanged += new System.EventHandler(this.trackBar_brightness_ValueChanged);
            // 
            // trackBar_color
            // 
            this.trackBar_color.Location = new System.Drawing.Point(27, 496);
            this.trackBar_color.Maximum = 100;
            this.trackBar_color.Name = "trackBar_color";
            this.trackBar_color.Size = new System.Drawing.Size(879, 56);
            this.trackBar_color.TabIndex = 1;
            this.trackBar_color.ValueChanged += new System.EventHandler(this.trackBar_color_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(363, 364);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 38);
            this.button1.TabIndex = 2;
            this.button1.Text = "Upload";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mainPic
            // 
            this.mainPic.Location = new System.Drawing.Point(1, 0);
            this.mainPic.Name = "mainPic";
            this.mainPic.Size = new System.Drawing.Size(960, 332);
            this.mainPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainPic.TabIndex = 3;
            this.mainPic.TabStop = false;
            // 
            // MyForm01
            // 
            this.ClientSize = new System.Drawing.Size(962, 578);
            this.Controls.Add(this.mainPic);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.trackBar_color);
            this.Controls.Add(this.trackBar_brightness);
            this.Name = "MyForm01";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_brightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_color)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult dr = openFileDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                Image file = Image.FromFile(openFileDialog.FileName);
                originalImage = file;
                mainPic.Image = file;
                isOpen = true;
            }
        }
        public static Bitmap AdjustBrightness(Bitmap Image, int Value)
        {
            Bitmap TempBitmap = Image;
            float FinalValue = (float)Value / 255.0f;
            Bitmap NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);
            Graphics NewGraphics = Graphics.FromImage(NewBitmap);
            float[][] FloatColorMatrix = {
                new float[] { 1, 0, 0, 0, 0 },
                new float[] { 0, 1, 0, 0, 0 },
                new float[] { 0, 0, 1, 0, 0 },
                new float[] { 0, 0, 0, 1, 0 },
                new float[] { FinalValue, FinalValue, FinalValue, 1, 1 }
            };
            ColorMatrix NewColorMatrix = new ColorMatrix(FloatColorMatrix);
            ImageAttributes Attributes = new ImageAttributes();
            Attributes.SetColorMatrix(NewColorMatrix);
            NewGraphics.DrawImage(TempBitmap,
              new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height),
              0, 0, TempBitmap.Width, TempBitmap.Height, GraphicsUnit.Pixel, Attributes);
            Attributes.Dispose();
            NewGraphics.Dispose();
            return NewBitmap;
        }

        public static Bitmap AdjustColor(Bitmap Image, int Value)
        {
            Bitmap TempBitmap = Image;
            float FinalValue = (float)Value / 255.0f;
            Bitmap NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);
            Graphics NewGraphics = Graphics.FromImage(NewBitmap);
            float[][] FloatColorMatrix = {
                    new float[]{1+Value, 0, 0, 0, 0},
                    new float[]{0, 1+Value, 0, 0, 0},
                    new float[]{0, 0, 1+Value, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}
            };
            ColorMatrix NewColorMatrix = new ColorMatrix(FloatColorMatrix);
            ImageAttributes Attributes = new ImageAttributes();
            Attributes.SetColorMatrix(NewColorMatrix);
            NewGraphics.DrawImage(TempBitmap,
              new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height),
              0, 0, TempBitmap.Width, TempBitmap.Height, GraphicsUnit.Pixel, Attributes);
            Attributes.Dispose();
            NewGraphics.Dispose();
            return NewBitmap;
        }

        private void trackBar_brightness_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine(trackBar_brightness.Value);
            mainPic.Image = AdjustBrightness((Bitmap)originalImage, trackBar_brightness.Value);
        }

        private void trackBar_color_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine(trackBar_color.Value);
            mainPic.Image = AdjustColor((Bitmap)originalImage, trackBar_color.Value);
        }
    }
}

