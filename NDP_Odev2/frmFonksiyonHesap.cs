using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace NDP_Odev2
{
    public partial class FrmFonksiyonHesap : Form
    {
        Form _frmRun;
        public FrmFonksiyonHesap()
        {
            InitializeComponent();
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            var sayi2 = txtSayi.Text;
            if (string.IsNullOrEmpty(sayi2))
            {
                MessageBox.Show(@"Sayı alanını boş geçmeyiniz");
            }
            int sayi = sayi2.ToInt();
            if (sayi < 0)
            {
                MessageBox.Show(@"0'dan büyük bir sayı giriniz");
            }

            string yazi = Yaziyacevir(sayi);
            txtYazi.Text = yazi;
            if (_frmRun != null)
            {
                _frmRun.Controls.Find("txtYazi", true)[0].Text = yazi;
            }
        }

        private void btnRuntimeForm_Click(object sender, EventArgs e)
        {
            if (_frmRun == null || _frmRun.IsDisposed)
            {
                _frmRun = new Form { Width = 435, Height = 184 };

                var font = GetFont("Tahoma", 12.0f);
                var lblSayi = GetLabel("Sayı :", "lblSayi", 25, 30, 49, 19);
                lblSayi.Font = font;
                var lblYazi = GetLabel("Yazı :", "lblYazi", 25, 69, 49, 19);
                lblYazi.Font = font;

                var textBox = GetTextBox("txtSayi", 79, 27, 328, 27);
                textBox.Font = font;
                textBox.MaxLength = 4;
                textBox.TextChanged += txtSayi_TextChanged;

                var yazi = GetTextBox("txtYazi", 79, 66, 328, 27);
                yazi.Font = font;
                yazi.Enabled = false;

                var hesapla = GetButton("HESAPLA", "btnHesapla", 287, 104, 120, 30);
                hesapla.Font = font;

                hesapla.Click += btnHesapla_Click;

                _frmRun.AcceptButton = hesapla;

                _frmRun.Controls.Add(lblSayi);
                _frmRun.Controls.Add(lblYazi);
                _frmRun.Controls.Add(textBox);
                _frmRun.Controls.Add(yazi);
                _frmRun.Controls.Add(hesapla);
            }

            _frmRun.Show();

        }

        void txtSayi_TextChanged(object sender, EventArgs e)
        {
            txtSayi.Text = ((TextBox)sender).Text;
        }

        private Button GetButton(string text, string name, int x, int y, int w, int h)
        {
            var btn = new Button { Text = text, Name = name, Location = new Point(x, y), Width = w, Height = h };
            return btn;
        }

        private TextBox GetTextBox(string name, int x, int y, int w, int h)
        {
            var txt = new TextBox { Name = name, Location = new Point(x, y), Width = w, Height = h };
            return txt;
        }

        public string Yaziyacevir(int deger)
        {
            string[] birler = { "", "BİR", "İKİ", "ÜÇ", "DÖRT", "BEŞ", "ALTI", "YEDİ", "SEKİZ", "DOKUZ" };
            string[] onlar = { "", "ON", "YİRMİ", "OTUZ", "KIRK", "ELLİ", "ALTMIŞ", "YETMİŞ", "SEKSEN", "DOKSAN" };

            /*4 basamaklı sayı elde ediyoruz*/
            var yazi = deger.ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');
            var degerYazi = "";
            var grupYazi = "";

            if (yazi.Substring(0, 1) != "0")
                degerYazi = birler[yazi.Substring(0, 1).ToInt()] + " BİN ";
            if (degerYazi == "BİR BİN ")
                degerYazi = "BİN ";
            if (yazi.Substring(1, 1) != "0")
                grupYazi = birler[yazi.Substring(1, 1).ToInt()] + " YÜZ ";
            if (grupYazi == "BİR YÜZ ")
                grupYazi = "YÜZ ";
            degerYazi += grupYazi;
            if (yazi.Substring(2, 1) != "0")
                degerYazi += onlar[yazi.Substring(2, 1).ToInt()] + " ";
            if (yazi.Substring(3, 1) != "0")
                degerYazi += birler[yazi.Substring(3, 1).ToInt()];
            if (degerYazi.Trim() == "" && yazi.Substring(3, 1) == "0")
                degerYazi = "SIFIR";
            return degerYazi;

        }

        public Font GetFont(string family, float size)
        {
            return new Font(family, size);
        }

        public Label GetLabel(string text, string name, int x, int y, int w, int h)
        {
            var lbl = new Label
            {
                Text = text,
                Name = name,
                Location = new Point(x, y),
                Width = w,
                Height = h
            };
            return lbl;
        }

    }
}
