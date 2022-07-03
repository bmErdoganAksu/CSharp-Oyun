namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        Random random = new Random();
        string[] words = { "isim", "sehir", "bilim" };//Bulunacak kelimenin kategorisi
        string word = "";//yukarýdaki kategorilerden herhangi birinden rastgele seçilen kelime
        List<string> secilenKelimeler = new List<string>();//seçilen kategorideki tüm kelimeleri bir listede tutabilemek için
        int hataliTahminSayisi = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            PictureBox1.Load("images/" + hataliTahminSayisi + ".jpg");//Her hatada resmi deðiþtirmek için
            lbl1.Text = words[random.Next(words.Length)];//seçilen kelimeyi görebilmek için
            //kelimeler klasöründe rastgele seçilen baþlýktaki txt dosyasýný okuma modunda açmak için
            FileStream fs = new FileStream("kelimeler/"+lbl1.Text+".txt", FileMode.Open, FileAccess.Read);
            StreamReader sr= new StreamReader(fs);
            string kelime = sr.ReadLine();//txt dosyasýndaki kelimeyi okumak için

            while (kelime!=null)//kelimelerin tümünü okuyana dek
            {
                secilenKelimeler.Add(kelime.ToUpper());//txt dosyasýndaki kelimeleri secilenKelimeler listesine eklemek için
                kelime = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
            word = secilenKelimeler[random.Next(secilenKelimeler.Count)];//seçilen kelimeler listesinden random bir þekilde bir kelime seçilip word(tahmin edilecek kelime) olarak belirlenir.
            lblGoster.Text = word;//tahmn edilen kelimeyi görebilmek için
            
            lbl2.Text = "";//lbl2 boþ olarak belirlenir.
            for (int i = 0; i < word.Length; i++)
            {
                lbl2.Text+= "_ ";//tahmin edilecek kelimeyi önce _ 'ler þeklinde göstermek için
            }
           
        }
        // harflerin click özelliðini tek tek yazmamak için ortak olarak klavye fonksiyonunda yazýldý.
        private void klavye(object sender, EventArgs e)
        {
            Button basilanBtn = sender as Button;//basilan harfi tutmak alabilmek için
            basilanBtn.Enabled = false;//Her harf butonuna sadece 1 defa basýlabilsin diye

            if (word.Contains(basilanBtn.Text)==false)//eðer tiklanan harf kelimede yoksa
            {
                hataliTahminSayisi++;//harf yoks hata sayýsý artýrýlýr
                PictureBox1.Load("images/" + hataliTahminSayisi + ".jpg");//hata numarasýna göre resim getirilir
                lblHataSay.Text= (10- hataliTahminSayisi).ToString();//kalan hata sayýsý labele yazdýrýlýr
            }
            else//tiklanan harf kelimede varsa
            {
                string text = lbl2.Text.Replace(" ", "");
                for (int i = 0; i < word.Length; i++)
                    if (word[i].ToString() == basilanBtn.Text)//nasýlan harf kelimenin i inci indisinde varsa
                        text = text.Remove(i, 1).Insert(i, basilanBtn.Text);//textten i inci indisteki _ yi sil yerine baslýna harfi yaz
                string sonuc = "";//text'i lbl2 ye yazdýrabilmek için
                for (int i = 0; i < text.Length; i++)
                    sonuc += text[i].ToString() + "";//text stringini sonuc stringine atar
                lbl2.Text = sonuc;// tahmindeki her deðiþikliði ve son kelimeyi lbl2 de göstermek için
            }
           

            if (lblHataSay.Text=="0")
            {
                MessageBox.Show("Oyunu Kaybettiniz...");
                //KaybettiBitirmesi
            }
          
            if (lbl2.Text.Replace(" ", "")==word)
            {
                MessageBox.Show("Tebrikler Kazandýnýz...");
                //Kazandý Bitirmesi
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();//yeniden oyna butonu oyunu yeniden baþlatmak için
        }
    }
}