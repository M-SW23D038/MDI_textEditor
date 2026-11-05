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
    }
}
