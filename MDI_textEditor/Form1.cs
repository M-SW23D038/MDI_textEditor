namespace MDI_textEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormChild childForm = new FormChild();
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void openToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FormChild childForm = new FormChild();
                childForm.MdiParent = this;

                if (openFileDialog.FileName.EndsWith(".rtf"))
                {
                    childForm.Controls.OfType<RichTextBox>().FirstOrDefault()?.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.RichText);
                }
                else
                {
                    childForm.Controls.OfType<RichTextBox>().FirstOrDefault()?.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                }

                childForm.Text = openFileDialog.FileName; // Цонхны нэр дээр файлын бүтэн замыг гаргана
                childForm.Show();
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Одоо идэвхтэй (дээр нь ажиллаж байгаа) FormChild цонхыг олж авна
            FormChild activeChild = this.ActiveMdiChild as FormChild;

            if (activeChild != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|All Files (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    RichTextBox txtBox = activeChild.Controls.OfType<RichTextBox>().FirstOrDefault();
                    if (txtBox != null)
                    {
                        if (saveFileDialog.FileName.EndsWith(".rtf"))
                        {
                            txtBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                        }
                        else
                        {
                            txtBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                        }
                        activeChild.Text = saveFileDialog.FileName; // Хадгалсан файлын нэрээр цонхны гарчиг солигдоно
                    }
                }
            }
            else
            {
                MessageBox.Show("Хадгалах боломжтой идэвхтэй цонх байхгүй байна.", "Анхааруулга");
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormChild activeChild = this.ActiveMdiChild as FormChild;

            if (activeChild != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|All Files (*.*)|*.*";
                saveFileDialog.Title = "Өөр нэрээр хадгалах (Save As)";

                // шинээр нэр сонгох цонх гаргана
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    RichTextBox txtBox = activeChild.Controls.OfType<RichTextBox>().FirstOrDefault();
                    if (txtBox != null)
                    {
                        if (saveFileDialog.FileName.EndsWith(".rtf"))
                        {
                            txtBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                        }
                        else
                        {
                            txtBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                        }
                        activeChild.Text = saveFileDialog.FileName; 
                    }
                }
            }
            else
            {
                MessageBox.Show("Хадгалах боломжтой идэвхтэй цонх байхгүй байна.", "Анхааруулга");
            }
        }
    }
}
