using System.Drawing.Printing;
using System.Windows.Forms;

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
        private string stringToPrint;
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


        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int charactersOnPage = 0;
            int linesPerPage = 0;

            // SizeF гэхийн оронд e.MarginBounds-ийн өргөн, өндрийг шууд дамжуулна
            SizeF marginSize = new SizeF(e.MarginBounds.Width, e.MarginBounds.Height);

            e.Graphics.MeasureString(stringToPrint, this.Font, marginSize,
                StringFormat.GenericTypographic, out charactersOnPage, out linesPerPage);

            // Текстийг цаасан дээр зурах
            e.Graphics.DrawString(stringToPrint, this.Font, System.Drawing.Brushes.Black,
                e.MarginBounds, StringFormat.GenericTypographic);

            // Хэрэв текст нэг хуудаснаас урт байвал дараагийн хуудас руу шилжүүлэх
            stringToPrint = stringToPrint.Substring(charactersOnPage);
            e.HasMorePages = (stringToPrint.Length > 0);
        }

        private void printPreviewToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormChild activeChild = this.ActiveMdiChild as FormChild;
            if (activeChild != null)
            {
                RichTextBox txtBox = activeChild.Controls.OfType<RichTextBox>().FirstOrDefault();
                if (txtBox != null)
                {
                    stringToPrint = txtBox.Text;
                    printPreviewDialog1.Document = printDocument1;
                    printPreviewDialog1.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Хэвлэх боломжтой идэвхтэй цонх байхгүй байна.", "Анхааруулга");
            }
        }

        private void printToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormChild activeChild = this.ActiveMdiChild as FormChild;
            if (activeChild != null)
            {
                RichTextBox txtBox = activeChild.Controls.OfType<RichTextBox>().FirstOrDefault();
                if (txtBox != null)
                {
                    stringToPrint = txtBox.Text;
                    printDialog1.Document = printDocument1;

                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.Print();
                    }
                }
            }
            else
            {
                MessageBox.Show("Хэвлэх боломжтой идэвхтэй цонх байхгүй байна.", "Анхааруулга");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Та програмаас гарахдаа итгэлтэй байна уу?", "Гарах", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
