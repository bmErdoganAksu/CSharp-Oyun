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
        string word = "";//yukar�daki kategorilerden herhangi birinden rastgele se�ilen kelime
        List<string> secilenKelimeler = new List<string>();//se�ilen kategorideki t�m kelimeleri bir listede tutabilemek i�in
        int hataliTahminSayisi = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            PictureBox1.Load("images/" + hataliTahminSayisi + ".jpg");//Her hatada resmi de�i�tirmek i�in
            lbl1.Text = words[random.Next(words.Length)];//se�ilen kelimeyi g�rebilmek i�in
            //kelimeler klas�r�nde rastgele se�ilen ba�l�ktaki txt dosyas�n� okuma modunda a�mak i�in
            FileStream fs = new FileStream("kelimeler/"+lbl1.Text+".txt", FileMode.Open, FileAccess.Read);
            StreamReader sr= new StreamReader(fs);
            string kelime = sr.ReadLine();//txt dosyas�ndaki kelimeyi okumak i�in

            while (kelime!=null)//kelimelerin t�m�n� okuyana dek
            {
                secilenKelimeler.Add(kelime.ToUpper());//txt dosyas�ndaki kelimeleri secilenKelimeler listesine eklemek i�in
                kelime = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
            word = secilenKelimeler[random.Next(secilenKelimeler.Count)];//se�ilen kelimeler listesinden random bir �ekilde bir kelime se�ilip word(tahmin edilecek kelime) olarak belirlenir.
            lblGoster.Text = word;//tahmn edilen kelimeyi g�rebilmek i�in
            
            lbl2.Text = "";//lbl2 bo� olarak belirlenir.
            for (int i = 0; i < word.Length; i++)
            {
                lbl2.Text+= "_ ";//tahmin edilecek kelimeyi �nce _ 'ler �eklinde g�stermek i�in
            }
           
        }
        // harflerin click �zelli�ini tek tek yazmamak i�in ortak olarak klavye fonksiyonunda yaz�ld�.
        private void klavye(object sender, EventArgs e)
        {
            Button basilanBtn = sender as Button;//basilan harfi tutmak alabilmek i�in
            basilanBtn.Enabled = false;//Her harf butonuna sadece 1 defa bas�labilsin diye

            if (word.Contains(basilanBtn.Text)==false)//e�er tiklanan harf kelimede yoksa
            {
                hataliTahminSayisi++;//harf yoks hata say�s� art�r�l�r
                PictureBox1.Load("images/" + hataliTahminSayisi + ".jpg");//hata numaras�na g�re resim getirilir
                lblHataSay.Text= (10- hataliTahminSayisi).ToString();//kalan hata say�s� labele yazd�r�l�r
            }
            else//tiklanan harf kelimede varsa
            {
                string text = lbl2.Text.Replace(" ", "");
                for (int i = 0; i < word.Length; i++)
                    if (word[i].ToString() == basilanBtn.Text)//nas�lan harf kelimenin i inci indisinde varsa
                        text = text.Remove(i, 1).Insert(i, basilanBtn.Text);//textten i inci indisteki _ yi sil yerine basl�na harfi yaz
                string sonuc = "";//text'i lbl2 ye yazd�rabilmek i�in
                for (int i = 0; i < text.Length; i++)
                    sonuc += text[i].ToString() + "";//text stringini sonuc stringine atar
                lbl2.Text = sonuc;// tahmindeki her de�i�ikli�i ve son kelimeyi lbl2 de g�stermek i�in
            }
           

            if (lblHataSay.Text=="0")
            {
                MessageBox.Show("Oyunu Kaybettiniz...");
                //KaybettiBitirmesi
            }
          
            if (lbl2.Text.Replace(" ", "")==word)
            {
                MessageBox.Show("Tebrikler Kazand�n�z...");
                //Kazand� Bitirmesi
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();//yeniden oyna butonu oyunu yeniden ba�latmak i�in
        }
    }
}