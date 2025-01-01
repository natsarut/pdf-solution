namespace PdfSolution.WinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ShowSingleForm<T>() where T:Form
        {
            var form = this.MdiChildren.FirstOrDefault(x => x is T);

            if (form==null)
            {
                form = (T)Activator.CreateInstance<T>();
                form.MdiParent = this;
            }

            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

        private void DocumentsComparerMenuItem_Click(object sender, EventArgs e)
        {
            ShowSingleForm<DocumentComparerForm>();
        }
    }
}
