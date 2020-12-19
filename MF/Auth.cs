using System;
using System.Windows.Forms;

namespace MF
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            try
            {
                MedFacSystemEntities context = new MedFacSystemEntities($"metadata=res://*/ModelMedFac.csdl|res://*/ModelMedFac.ssdl|res://*/ModelMedFac.msl;provider=System.Data.SqlClient;provider connection string=\"data source={ServerTextBox.Text};initial catalog={DBTextBox.Text};integrated security=False;user ID={LoginTextBox.Text};password={PasswordTextBox.Text};MultipleActiveResultSets=True;App=EntityFramework\"");
                context.Database.Connection.Open();
                context.Database.Connection.Close();
                MedFacConnect.Konst($"metadata=res://*/ModelMedFac.csdl|res://*/ModelMedFac.ssdl|res://*/ModelMedFac.msl;provider=System.Data.SqlClient;provider connection string=\"data source={ServerTextBox.Text};initial catalog={DBTextBox.Text};integrated security=False;user ID={LoginTextBox.Text};password={PasswordTextBox.Text};MultipleActiveResultSets=True;App=EntityFramework\"");
                Main mainform = new Main();
                mainform.Show();
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка соединения");
            }
        }
    }
}
