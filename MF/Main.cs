using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Forms;


namespace MF
{
    public partial class Main : Form
    {
        public static int choose;

        //Конструкторы

        public Main()
        {
            InitializeComponent();
            InitMedFac();
        }

        public Main(int c)
        {
            InitializeComponent();
            choose = c;
            switch (choose)
            {
                case 0:
                    {
                        InitAppointment();
                        break;
                    }
                case 1:
                    {
                        InitMedFac();
                        break;
                    }
                case 2:
                    {
                        InitDoctor();
                        break;
                    }
                case 3:
                    {
                        InitArticle();
                        break;
                    }
                case 4:
                    {
                        InitCategoryMedFac();
                        break;
                    }
                case 5:
                    {
                        InitCategoryArticle();
                        break;
                    }
                case 6:
                    {
                        InitSpeciality();
                        break;
                    }
                case 7:
                    {
                        InitCity();
                        break;
                    }
                default:
                    {
                        Application.Exit();
                        break;
                    }
            }
        }

        private void InitAppointment()
        {
            try
            {
                var res = from ap in MedFacConnect.context.Appointment
                          join doc in MedFacConnect.context.Doctor on ap.IDDoctor equals doc.IDDoctor
                          join med in MedFacConnect.context.MedicalFacility on ap.IDMedFac equals med.IDMedFac
                          join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                          join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                          where ap.Finish == false
                          select new
                          {
                              ID = ap.IDAppointment,
                              NameMedFac = med.NameMedFac,
                              NameCity = city.NameCity,
                              CategoryMedFac = cat.NameCategory,
                              Family_Doctor = doc.FamilyDoctor,
                              Name_Doctor = doc.NameDoctor,
                              Patronymic_Doctor = doc.PatronymicDoctor,
                              Family_Visitor = ap.FamilyVisitor,
                              Name_Visitor = ap.NameVisitor,
                              Patronymic_Visitor = ap.PatronymicVisitor,
                          };
                DataGridViewMain.DataSource = null;
                DataGridViewMain.DataSource = res.ToList();
                DataGridViewMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataGridViewMain.Columns[0].Visible = false;
                choose = 0;
                NameMedFacFilterToolStripMenuItem.Visible = true;
                CategoryMedFacFilterToolStripMenuItem.Visible = true;
                CityFilterToolStripMenuItem.Visible = true;
                DoctorFilterToolStripMenuItem.Visible = true;
                SpecialityDoctorFilterToolStripMenuItem.Visible = false;
                VisitorFilterToolStripMenuItem.Visible = true;
                AddressMedFacFilterToolStripMenuItem.Visible = false;
                NameArticleFilterToolStripMenuItem.Visible = false;
                CategoryArticleFilterToolStripMenuItem.Visible = false;
                AuthorAtricleFilterToolStripMenuItem.Visible = false;
                BackButton.Visible = false;
                ViewButton.Visible = true;
                EditButton.Visible = true;
                DeleteButton.Visible = true;
                CreateButton.Visible = true;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void InitMedFac()
        {
            try
            {
                var res = from med in MedFacConnect.context.MedicalFacility
                          join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                          join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                          select new
                          {
                              ID = med.IDMedFac,
                              NameMedFac = med.NameMedFac,
                              CategoryMedFac = cat.NameCategory,
                              NameCity = city.NameCity,
                              Street = med.Street,
                              Builbing = med.Building
                          };
                DataGridViewMain.DataSource = null;
                DataGridViewMain.DataSource = res.ToList();
                DataGridViewMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataGridViewMain.Columns[0].Visible = false;
                choose = 1;
                NameMedFacFilterToolStripMenuItem.Visible = true;
                CategoryMedFacFilterToolStripMenuItem.Visible = true;
                CityFilterToolStripMenuItem.Visible = true;
                DoctorFilterToolStripMenuItem.Visible = false;
                SpecialityDoctorFilterToolStripMenuItem.Visible = false;
                VisitorFilterToolStripMenuItem.Visible = false;
                AddressMedFacFilterToolStripMenuItem.Visible = true;
                NameArticleFilterToolStripMenuItem.Visible = false;
                CategoryArticleFilterToolStripMenuItem.Visible = false;
                AuthorAtricleFilterToolStripMenuItem.Visible = false;
                BackButton.Visible = false;
                ViewButton.Visible = true;
                EditButton.Visible = true;
                DeleteButton.Visible = true;
                CreateButton.Visible = true;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void InitDoctor()
        {
            try
            {
                var res = from doc in MedFacConnect.context.Doctor
                          select new
                          {
                              ID = doc.IDDoctor,
                              Family = doc.FamilyDoctor,
                              Name = doc.NameDoctor,
                              Patronymic = doc.PatronymicDoctor,
                              Gender = doc.GenderDoctor,
                              Birthday = doc.DateBirthdayDoctor
                          };
                DataGridViewMain.DataSource = null;
                DataGridViewMain.DataSource = res.ToList();
                DataGridViewMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataGridViewMain.Columns[0].Visible = false;
                choose = 2;
                NameMedFacFilterToolStripMenuItem.Visible = false;
                CategoryMedFacFilterToolStripMenuItem.Visible = false;
                CityFilterToolStripMenuItem.Visible = false;
                DoctorFilterToolStripMenuItem.Visible = true;
                SpecialityDoctorFilterToolStripMenuItem.Visible = false;
                VisitorFilterToolStripMenuItem.Visible = false;
                AddressMedFacFilterToolStripMenuItem.Visible = false;
                NameArticleFilterToolStripMenuItem.Visible = false;
                CategoryArticleFilterToolStripMenuItem.Visible = false;
                AuthorAtricleFilterToolStripMenuItem.Visible = false;
                BackButton.Visible = false;
                ViewButton.Visible = true;
                EditButton.Visible = true;
                DeleteButton.Visible = true;
                CreateButton.Visible = true;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void InitArticle()
        {
            try
            {
                var res = from art in MedFacConnect.context.Article
                          join cat in MedFacConnect.context.CategoryArticle on art.IDCategoryArticle equals cat.IDCategoryArticle
                          select new
                          {
                              ID = art.IDArticle,
                              Title = art.TitleArticle,
                              Category = cat.NameCategory,
                              Author = art.PsuedonymAuthor
                          };
                DataGridViewMain.DataSource = null;
                DataGridViewMain.DataSource = res.ToList();
                DataGridViewMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataGridViewMain.Columns[0].Visible = false;
                choose = 3;
                NameMedFacFilterToolStripMenuItem.Visible = false;
                CategoryMedFacFilterToolStripMenuItem.Visible = false;
                CityFilterToolStripMenuItem.Visible = false;
                DoctorFilterToolStripMenuItem.Visible = false;
                SpecialityDoctorFilterToolStripMenuItem.Visible = false;
                VisitorFilterToolStripMenuItem.Visible = false;
                AddressMedFacFilterToolStripMenuItem.Visible = false;
                NameArticleFilterToolStripMenuItem.Visible = true;
                CategoryArticleFilterToolStripMenuItem.Visible = true;
                AuthorAtricleFilterToolStripMenuItem.Visible = true;
                BackButton.Visible = false;
                ViewButton.Visible = true;
                EditButton.Visible = true;
                DeleteButton.Visible = true;
                CreateButton.Visible = true;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void InitCategoryMedFac()
        {
            try
            {
                var res = from cat in MedFacConnect.context.CategoryMedFac
                          select new
                          {
                              ID = cat.IDCategoryMedFac,
                              NameCategory = cat.NameCategory
                          };
                DataGridViewMain.DataSource = null;
                DataGridViewMain.DataSource = res.ToList();
                DataGridViewMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataGridViewMain.Columns[0].Visible = false;
                choose = 4;
                NameMedFacFilterToolStripMenuItem.Visible = false;
                CategoryMedFacFilterToolStripMenuItem.Visible = true;
                CityFilterToolStripMenuItem.Visible = false;
                DoctorFilterToolStripMenuItem.Visible = false;
                SpecialityDoctorFilterToolStripMenuItem.Visible = false;
                VisitorFilterToolStripMenuItem.Visible = false;
                AddressMedFacFilterToolStripMenuItem.Visible = false;
                NameArticleFilterToolStripMenuItem.Visible = false;
                CategoryArticleFilterToolStripMenuItem.Visible = false;
                AuthorAtricleFilterToolStripMenuItem.Visible = false;
                BackButton.Visible = false;
                ViewButton.Visible = true;
                EditButton.Visible = true;
                DeleteButton.Visible = true;
                CreateButton.Visible = true;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void InitCategoryArticle()
        {
            try
            {
                var res = from cat in MedFacConnect.context.CategoryArticle
                          select new
                          {
                              ID = cat.IDCategoryArticle,
                              NameCategory = cat.NameCategory
                          };
                DataGridViewMain.DataSource = null;
                DataGridViewMain.DataSource = res.ToList();
                DataGridViewMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataGridViewMain.Columns[0].Visible = false;
                choose = 5;
                NameMedFacFilterToolStripMenuItem.Visible = false;
                CategoryMedFacFilterToolStripMenuItem.Visible = false;
                CityFilterToolStripMenuItem.Visible = false;
                DoctorFilterToolStripMenuItem.Visible = false;
                SpecialityDoctorFilterToolStripMenuItem.Visible = false;
                VisitorFilterToolStripMenuItem.Visible = false;
                AddressMedFacFilterToolStripMenuItem.Visible = false;
                NameArticleFilterToolStripMenuItem.Visible = false;
                CategoryArticleFilterToolStripMenuItem.Visible = true;
                AuthorAtricleFilterToolStripMenuItem.Visible = false;
                BackButton.Visible = false;
                ViewButton.Visible = true;
                EditButton.Visible = true;
                DeleteButton.Visible = true;
                CreateButton.Visible = true;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void InitSpeciality()
        {
            try
            {
                var res = from spec in MedFacConnect.context.Speciality
                          select new
                          {
                              ID = spec.IDSpeciality,
                              NameSpeciality = spec.NameSpeciality
                          };
                DataGridViewMain.DataSource = null;
                DataGridViewMain.DataSource = res.ToList();
                DataGridViewMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataGridViewMain.Columns[0].Visible = false;
                choose = 6;
                NameMedFacFilterToolStripMenuItem.Visible = false;
                CategoryMedFacFilterToolStripMenuItem.Visible = false;
                CityFilterToolStripMenuItem.Visible = false;
                DoctorFilterToolStripMenuItem.Visible = false;
                SpecialityDoctorFilterToolStripMenuItem.Visible = true;
                VisitorFilterToolStripMenuItem.Visible = false;
                AddressMedFacFilterToolStripMenuItem.Visible = false;
                NameArticleFilterToolStripMenuItem.Visible = false;
                CategoryArticleFilterToolStripMenuItem.Visible = false;
                AuthorAtricleFilterToolStripMenuItem.Visible = false;
                BackButton.Visible = false;
                ViewButton.Visible = true;
                EditButton.Visible = true;
                DeleteButton.Visible = true;
                CreateButton.Visible = true;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void InitCity()
        {
            try
            {
                var res = from city in MedFacConnect.context.City
                          select new
                          {
                              ID = city.IDCity,
                              NameCity = city.NameCity
                          };
                DataGridViewMain.DataSource = null;
                DataGridViewMain.DataSource = res.ToList();
                DataGridViewMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataGridViewMain.Columns[0].Visible = false;
                choose = 7;
                NameMedFacFilterToolStripMenuItem.Visible = false;
                CategoryMedFacFilterToolStripMenuItem.Visible = false;
                CityFilterToolStripMenuItem.Visible = true;
                DoctorFilterToolStripMenuItem.Visible = false;
                SpecialityDoctorFilterToolStripMenuItem.Visible = false;
                VisitorFilterToolStripMenuItem.Visible = false;
                AddressMedFacFilterToolStripMenuItem.Visible = false;
                NameArticleFilterToolStripMenuItem.Visible = false;
                CategoryArticleFilterToolStripMenuItem.Visible = false;
                AuthorAtricleFilterToolStripMenuItem.Visible = false;
                BackButton.Visible = false;
                ViewButton.Visible = true;
                EditButton.Visible = true;
                DeleteButton.Visible = true;
                CreateButton.Visible = true;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        //Открытие таблиц

        private void AppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitAppointment();
        }

        private void MedFacToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitMedFac();
        }

        private void CategoryMedFacToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitCategoryMedFac();
        }

        private void DoctorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitDoctor();
        }

        private void CategoryDoctorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitSpeciality();
        }

        private void ArticleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitArticle();
        }

        private void CategoryArticleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitCategoryArticle();
        }

        private void CityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitCity();
        }

        //Профиль

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void сменитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Auth auth = new Auth();
            auth.Show();
            this.Close();
        }

        //Фильтры

        private void NameMedFacFilterToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(choose == 1)
                {
                    var res = from med in MedFacConnect.context.MedicalFacility
                              join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                              join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                              select new
                              {
                                  NameMedFac = med.NameMedFac,
                                  CategoryMedFac = cat.NameCategory,
                                  NameCity = city.NameCity,
                                  Street = med.Street,
                                  Builbing = med.Building
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(NameMedFacFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.NameMedFac.Contains(NameMedFacFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
                else if(choose == 0)
                {
                    var res = from ap in MedFacConnect.context.Appointment
                              join doc in MedFacConnect.context.Doctor on ap.IDDoctor equals doc.IDDoctor
                              join med in MedFacConnect.context.MedicalFacility on ap.IDMedFac equals med.IDMedFac
                              join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                              join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                              where ap.Finish == false
                              select new
                              {
                                  ID = ap.IDAppointment,
                                  NameMedFac = med.NameMedFac,
                                  NameCity = city.NameCity,
                                  CategoryMedFac = cat.NameCategory,
                                  Family_Doctor = doc.FamilyDoctor,
                                  Name_Doctor = doc.NameDoctor,
                                  Patronymic_Doctor = doc.PatronymicDoctor,
                                  Family_Visitor = ap.FamilyVisitor,
                                  Name_Visitor = ap.NameVisitor,
                                  Patronymic_Visitor = ap.PatronymicVisitor,
                                  Finish = ap.Finish
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(NameMedFacFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.NameMedFac.Contains(NameMedFacFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void CategoryMedFacFilterToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (choose == 4)
                {
                    var res = from cat in MedFacConnect.context.CategoryMedFac
                              select new
                              {
                                  NameCategory = cat.NameCategory
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(CategoryMedFacFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.NameCategory.Contains(CategoryMedFacFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
                else if (choose == 0)
                {
                    var res = from ap in MedFacConnect.context.Appointment
                              join doc in MedFacConnect.context.Doctor on ap.IDDoctor equals doc.IDDoctor
                              join med in MedFacConnect.context.MedicalFacility on ap.IDMedFac equals med.IDMedFac
                              join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                              join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                              where ap.Finish == false
                              select new
                              {
                                  ID = ap.IDAppointment,
                                  NameMedFac = med.NameMedFac,
                                  NameCity = city.NameCity,
                                  CategoryMedFac = cat.NameCategory,
                                  Family_Doctor = doc.FamilyDoctor,
                                  Name_Doctor = doc.NameDoctor,
                                  Patronymic_Doctor = doc.PatronymicDoctor,
                                  Family_Visitor = ap.FamilyVisitor,
                                  Name_Visitor = ap.NameVisitor,
                                  Patronymic_Visitor = ap.PatronymicVisitor,
                                  Finish = ap.Finish
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(CategoryMedFacFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.CategoryMedFac.Contains(CategoryMedFacFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
                else if(choose == 1)
                {
                    var res = from med in MedFacConnect.context.MedicalFacility
                              join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                              join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                              select new
                              {
                                  ID = med.IDMedFac,
                                  NameMedFac = med.NameMedFac,
                                  CategoryMedFac = cat.NameCategory,
                                  NameCity = city.NameCity,
                                  Street = med.Street,
                                  Builbing = med.Building
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(CategoryMedFacFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.CategoryMedFac.Contains(CategoryMedFacFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void CityFilterToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (choose == 7)
                {
                    var res = from city in MedFacConnect.context.City
                              select new
                              {
                                  NameCity = city.NameCity
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(CityFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.NameCity.Contains(CityFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
                else if (choose == 0)
                {
                    var res = from ap in MedFacConnect.context.Appointment
                              join doc in MedFacConnect.context.Doctor on ap.IDDoctor equals doc.IDDoctor
                              join med in MedFacConnect.context.MedicalFacility on ap.IDMedFac equals med.IDMedFac
                              join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                              join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                              where ap.Finish == false
                              select new
                              {
                                  ID = ap.IDAppointment,
                                  NameMedFac = med.NameMedFac,
                                  NameCity = city.NameCity,
                                  CategoryMedFac = cat.NameCategory,
                                  Family_Doctor = doc.FamilyDoctor,
                                  Name_Doctor = doc.NameDoctor,
                                  Patronymic_Doctor = doc.PatronymicDoctor,
                                  Family_Visitor = ap.FamilyVisitor,
                                  Name_Visitor = ap.NameVisitor,
                                  Patronymic_Visitor = ap.PatronymicVisitor,
                                  Finish = ap.Finish
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(CityFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.NameCity.Contains(CityFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
                else if (choose == 1)
                {
                    var res = from med in MedFacConnect.context.MedicalFacility
                              join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                              join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                              select new
                              {
                                  ID = med.IDMedFac,
                                  NameMedFac = med.NameMedFac,
                                  CategoryMedFac = cat.NameCategory,
                                  NameCity = city.NameCity,
                                  Street = med.Street,
                                  Builbing = med.Building
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(CityFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.NameCity.Contains(CityFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void FamilyDFilterToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(choose == 0)
                {
                    var res = from ap in MedFacConnect.context.Appointment
                              join doc in MedFacConnect.context.Doctor on ap.IDDoctor equals doc.IDDoctor
                              join med in MedFacConnect.context.MedicalFacility on ap.IDMedFac equals med.IDMedFac
                              join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                              join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                              where ap.Finish == false
                              select new
                              {
                                  ID = ap.IDAppointment,
                                  NameMedFac = med.NameMedFac,
                                  NameCity = city.NameCity,
                                  CategoryMedFac = cat.NameCategory,
                                  Family_Doctor = doc.FamilyDoctor,
                                  Name_Doctor = doc.NameDoctor,
                                  Patronymic_Doctor = doc.PatronymicDoctor,
                                  Family_Visitor = ap.FamilyVisitor,
                                  Name_Visitor = ap.NameVisitor,
                                  Patronymic_Visitor = ap.PatronymicVisitor,
                                  Finish = ap.Finish
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(FamilyDFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.Family_Doctor.Contains(FamilyDFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
                else if(choose == 2)
                {
                    var res = from doc in MedFacConnect.context.Doctor
                              select new
                              {
                                  Family = doc.FamilyDoctor,
                                  Name = doc.NameDoctor,
                                  Patronymic = doc.PatronymicDoctor,
                                  Gender = doc.GenderDoctor,
                                  Birthday = doc.DateBirthdayDoctor
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(FamilyDFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.Family.Contains(FamilyDFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void NameDFilterToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(choose == 0)
                {
                    var res = from ap in MedFacConnect.context.Appointment
                              join doc in MedFacConnect.context.Doctor on ap.IDDoctor equals doc.IDDoctor
                              join med in MedFacConnect.context.MedicalFacility on ap.IDMedFac equals med.IDMedFac
                              join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                              join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                              where ap.Finish == false
                              select new
                              {
                                  ID = ap.IDAppointment,
                                  NameMedFac = med.NameMedFac,
                                  NameCity = city.NameCity,
                                  CategoryMedFac = cat.NameCategory,
                                  Family_Doctor = doc.FamilyDoctor,
                                  Name_Doctor = doc.NameDoctor,
                                  Patronymic_Doctor = doc.PatronymicDoctor,
                                  Family_Visitor = ap.FamilyVisitor,
                                  Name_Visitor = ap.NameVisitor,
                                  Patronymic_Visitor = ap.PatronymicVisitor,
                                  Finish = ap.Finish
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(NameDFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.Name_Doctor.Contains(NameDFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
                else if(choose == 2)
                {
                    var res = from doc in MedFacConnect.context.Doctor
                              select new
                              {
                                  Family = doc.FamilyDoctor,
                                  Name = doc.NameDoctor,
                                  Patronymic = doc.PatronymicDoctor,
                                  Gender = doc.GenderDoctor,
                                  Birthday = doc.DateBirthdayDoctor
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(NameDFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.Name.Contains(NameDFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void PatronymicDFilterToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(choose == 0)
                {
                    var res = from ap in MedFacConnect.context.Appointment
                              join doc in MedFacConnect.context.Doctor on ap.IDDoctor equals doc.IDDoctor
                              join med in MedFacConnect.context.MedicalFacility on ap.IDMedFac equals med.IDMedFac
                              join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                              join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                              where ap.Finish == false
                              select new
                              {
                                  ID = ap.IDAppointment,
                                  NameMedFac = med.NameMedFac,
                                  NameCity = city.NameCity,
                                  CategoryMedFac = cat.NameCategory,
                                  Family_Doctor = doc.FamilyDoctor,
                                  Name_Doctor = doc.NameDoctor,
                                  Patronymic_Doctor = doc.PatronymicDoctor,
                                  Family_Visitor = ap.FamilyVisitor,
                                  Name_Visitor = ap.NameVisitor,
                                  Patronymic_Visitor = ap.PatronymicVisitor,
                                  Finish = ap.Finish
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(PatronymicDFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.Patronymic_Doctor.Contains(PatronymicDFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
                else if(choose == 2)
                {
                    var res = from doc in MedFacConnect.context.Doctor
                              select new
                              {
                                  Family = doc.FamilyDoctor,
                                  Name = doc.NameDoctor,
                                  Patronymic = doc.PatronymicDoctor,
                                  Gender = doc.GenderDoctor,
                                  Birthday = doc.DateBirthdayDoctor
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(PatronymicDFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.Patronymic.Contains(PatronymicDFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void SpecialityDoctorFilterToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (choose == 6)
                {
                    var res = from spec in MedFacConnect.context.Speciality
                              select new
                              {
                                  NameSpeciality = spec.NameSpeciality
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(SpecialityDoctorFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.NameSpeciality.Contains(SpecialityDoctorFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void FamilyVFilterToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            var res = from ap in MedFacConnect.context.Appointment
                      join doc in MedFacConnect.context.Doctor on ap.IDDoctor equals doc.IDDoctor
                      join med in MedFacConnect.context.MedicalFacility on ap.IDMedFac equals med.IDMedFac
                      join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                      join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                      where ap.Finish == false
                      select new
                      {
                          ID = ap.IDAppointment,
                          NameMedFac = med.NameMedFac,
                          NameCity = city.NameCity,
                          CategoryMedFac = cat.NameCategory,
                          Family_Doctor = doc.FamilyDoctor,
                          Name_Doctor = doc.NameDoctor,
                          Patronymic_Doctor = doc.PatronymicDoctor,
                          Family_Visitor = ap.FamilyVisitor,
                          Name_Visitor = ap.NameVisitor,
                          Patronymic_Visitor = ap.PatronymicVisitor,
                          Finish = ap.Finish
                      };
            if (DataGridViewMain.DataSource != null)
            {
                if (!string.IsNullOrWhiteSpace(FamilyVFilterToolStripTextBox.Text))
                {
                    DataGridViewMain.DataSource = res.Where(c => c.Family_Visitor.Contains(FamilyVFilterToolStripTextBox.Text)).ToList();
                }
                else
                {
                    DataGridViewMain.DataSource = res.ToList();
                }
            }
        }

        private void PatronymicVFilterToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            var res = from ap in MedFacConnect.context.Appointment
                      join doc in MedFacConnect.context.Doctor on ap.IDDoctor equals doc.IDDoctor
                      join med in MedFacConnect.context.MedicalFacility on ap.IDMedFac equals med.IDMedFac
                      join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                      join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                      where ap.Finish == false
                      select new
                      {
                          ID = ap.IDAppointment,
                          NameMedFac = med.NameMedFac,
                          NameCity = city.NameCity,
                          CategoryMedFac = cat.NameCategory,
                          Family_Doctor = doc.FamilyDoctor,
                          Name_Doctor = doc.NameDoctor,
                          Patronymic_Doctor = doc.PatronymicDoctor,
                          Family_Visitor = ap.FamilyVisitor,
                          Name_Visitor = ap.NameVisitor,
                          Patronymic_Visitor = ap.PatronymicVisitor,
                          Finish = ap.Finish
                      };
            if (DataGridViewMain.DataSource != null)
            {
                if (!string.IsNullOrWhiteSpace(PatronymicVFilterToolStripTextBox.Text))
                {
                    DataGridViewMain.DataSource = res.Where(c => c.Patronymic_Visitor.Contains(PatronymicVFilterToolStripTextBox.Text)).ToList();
                }
                else
                {
                    DataGridViewMain.DataSource = res.ToList();
                }
            }
        }

        private void NameVFilterToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            var res = from ap in MedFacConnect.context.Appointment
                      join doc in MedFacConnect.context.Doctor on ap.IDDoctor equals doc.IDDoctor
                      join med in MedFacConnect.context.MedicalFacility on ap.IDMedFac equals med.IDMedFac
                      join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                      join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                      where ap.Finish == false
                      select new
                      {
                          ID = ap.IDAppointment,
                          NameMedFac = med.NameMedFac,
                          NameCity = city.NameCity,
                          CategoryMedFac = cat.NameCategory,
                          Family_Doctor = doc.FamilyDoctor,
                          Name_Doctor = doc.NameDoctor,
                          Patronymic_Doctor = doc.PatronymicDoctor,
                          Family_Visitor = ap.FamilyVisitor,
                          Name_Visitor = ap.NameVisitor,
                          Patronymic_Visitor = ap.PatronymicVisitor,
                          Finish = ap.Finish
                      };
            if (DataGridViewMain.DataSource != null)
            {
                if (!string.IsNullOrWhiteSpace(NameVFilterToolStripTextBox.Text))
                {
                    DataGridViewMain.DataSource = res.Where(c => c.Name_Visitor.Contains(NameVFilterToolStripTextBox.Text)).ToList();
                }
                else
                {
                    DataGridViewMain.DataSource = res.ToList();
                }
            }
        }

        private void StreetFilterToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var res = from med in MedFacConnect.context.MedicalFacility
                          join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                          join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                          select new
                          {
                              NameMedFac = med.NameMedFac,
                              CategoryMedFac = cat.NameCategory,
                              NameCity = city.NameCity,
                              Street = med.Street,
                              Builbing = med.Building
                          };
                if (DataGridViewMain.DataSource != null)
                {
                    if (!string.IsNullOrWhiteSpace(StreetFilterToolStripTextBox.Text))
                    {
                        DataGridViewMain.DataSource = res.Where(c => c.Street.Contains(StreetFilterToolStripTextBox.Text)).ToList();
                    }
                    else
                    {
                        DataGridViewMain.DataSource = res.ToList();
                    }
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void BuildingFilterToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var res = from med in MedFacConnect.context.MedicalFacility
                          join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                          join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                          select new
                          {
                              NameMedFac = med.NameMedFac,
                              CategoryMedFac = cat.NameCategory,
                              NameCity = city.NameCity,
                              Street = med.Street,
                              Builbing = med.Building
                          };
                if (DataGridViewMain.DataSource != null)
                {
                    if (!string.IsNullOrWhiteSpace(BuildingFilterToolStripTextBox.Text))
                    {
                        DataGridViewMain.DataSource = res.Where(c => c.Builbing.Contains(BuildingFilterToolStripTextBox.Text)).ToList();
                    }
                    else
                    {
                        DataGridViewMain.DataSource = res.ToList();
                    }
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void NameArticleFilterToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var res = from art in MedFacConnect.context.Article
                          join cat in MedFacConnect.context.CategoryArticle on art.IDCategoryArticle equals cat.IDCategoryArticle
                          select new
                          {
                              Title = art.TitleArticle,
                              Category = cat.NameCategory,
                              Author = art.PsuedonymAuthor
                          };
                if (DataGridViewMain.DataSource != null)
                {
                    if (!string.IsNullOrWhiteSpace(NameArticleFilterToolStripTextBox.Text))
                    {
                        DataGridViewMain.DataSource = res.Where(c => c.Title.Contains(NameArticleFilterToolStripTextBox.Text)).ToList();
                    }
                    else
                    {
                        DataGridViewMain.DataSource = res.ToList();
                    }
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void CategoryArticleFilterToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(choose == 5)
                {
                    var res = from cat in MedFacConnect.context.CategoryArticle
                              select new
                              {
                                  NameCategory = cat.NameCategory
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(CategoryArticleFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.NameCategory.Contains(CategoryArticleFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
                else if(choose == 3)
                {
                    var res = from art in MedFacConnect.context.Article
                              join cat in MedFacConnect.context.CategoryArticle on art.IDCategoryArticle equals cat.IDCategoryArticle
                              select new
                              {
                                  Title = art.TitleArticle,
                                  Category = cat.NameCategory,
                                  Author = art.PsuedonymAuthor
                              };
                    if (DataGridViewMain.DataSource != null)
                    {
                        if (!string.IsNullOrWhiteSpace(CategoryArticleFilterToolStripTextBox.Text))
                        {
                            DataGridViewMain.DataSource = res.Where(c => c.Category.Contains(CategoryArticleFilterToolStripTextBox.Text)).ToList();
                        }
                        else
                        {
                            DataGridViewMain.DataSource = res.ToList();
                        }
                    }
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void AuthorArticleFilterToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var res = from art in MedFacConnect.context.Article
                          join cat in MedFacConnect.context.CategoryArticle on art.IDCategoryArticle equals cat.IDCategoryArticle
                          select new
                          {
                              Title = art.TitleArticle,
                              Category = cat.NameCategory,
                              Author = art.PsuedonymAuthor
                          };
                if (DataGridViewMain.DataSource != null)
                {
                    if (!string.IsNullOrWhiteSpace(AuthorArticleFilterToolStripTextBox.Text))
                    {
                        DataGridViewMain.DataSource = res.Where(c => c.Author.Contains(AuthorArticleFilterToolStripTextBox.Text)).ToList();
                    }
                    else
                    {
                        DataGridViewMain.DataSource = res.ToList();
                    }
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        //Работа с кнопками

        private void ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridViewMain.SelectedRows.Count != 0)
                {
                    long s = Convert.ToInt64(DataGridViewMain.SelectedRows[0].Cells[0].Value);
                    Action act = new Action(s, true);
                    act.FormClosed += Act_FormClosed;
                    act.ShowDialog();
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void Act_FormClosed(object sender, FormClosedEventArgs e)
        {
            switch (choose)
            {
                case 0:
                    {
                        InitAppointment();
                        break;
                    }
                case 1:
                    {
                        InitMedFac();
                        break;
                    }
                case 2:
                    {
                        InitDoctor();
                        break;
                    }
                case 3:
                    {
                        InitArticle();
                        break;
                    }
                case 4:
                    {
                        InitCategoryMedFac();
                        break;
                    }
                case 5:
                    {
                        InitCategoryArticle();
                        break;
                    }
                case 6:
                    {
                        InitSpeciality();
                        break;
                    }
                case 7:
                    {
                        InitCity();
                        break;
                    }
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridViewMain.SelectedRows.Count != 0)
                {
                    long s = Convert.ToInt64(DataGridViewMain.SelectedRows[0].Cells[0].Value);
                    Action act = new Action(s);
                    act.FormClosed += Act_FormClosed;
                    act.ShowDialog();
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить автора? Отменить действие будет невозможно.", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if(result == DialogResult.Yes)
                {
                    if (DataGridViewMain.SelectedRows.Count != 0)
                    {
                        long s = Convert.ToInt64(DataGridViewMain.SelectedRows[0].Cells[0].Value);
                        switch (choose)
                        {
                            case 0:
                                {
                                    MedFacConnect.context.Appointment.Remove(MedFacConnect.context.Appointment.Find(s));
                                    MedFacConnect.context.SaveChanges();
                                    InitAppointment();
                                    break;
                                }
                            case 1:
                                {
                                    MedFacConnect.context.MedicalFacility.Remove(MedFacConnect.context.MedicalFacility.Find(s));
                                    MedFacConnect.context.SaveChanges();
                                    InitMedFac();
                                    break;
                                }
                            case 2:
                                {
                                    MedFacConnect.context.Doctor.Remove(MedFacConnect.context.Doctor.Find(s));
                                    MedFacConnect.context.SaveChanges();
                                    InitDoctor();
                                    break;
                                }
                            case 3:
                                {
                                    MedFacConnect.context.Article.Remove(MedFacConnect.context.Article.Find(s));
                                    MedFacConnect.context.SaveChanges();
                                    InitArticle();
                                    break;
                                }
                            case 4:
                                {
                                    MedFacConnect.context.CategoryMedFac.Remove(MedFacConnect.context.CategoryMedFac.Find(s));
                                    MedFacConnect.context.SaveChanges();
                                    InitCategoryMedFac();
                                    break;
                                }
                            case 5:
                                {
                                    MedFacConnect.context.CategoryArticle.Remove(MedFacConnect.context.CategoryArticle.Find(s));
                                    MedFacConnect.context.SaveChanges();
                                    InitCategoryArticle();
                                    break;
                                }
                            case 6:
                                {
                                    MedFacConnect.context.Speciality.Remove(MedFacConnect.context.Speciality.Find(s));
                                    MedFacConnect.context.SaveChanges();
                                    InitSpeciality();
                                    break;
                                }
                            case 7:
                                {
                                    MedFacConnect.context.City.Remove(MedFacConnect.context.City.Find(s));
                                    MedFacConnect.context.SaveChanges();
                                    InitCity();
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                Action act = new Action(-1);
                act.FormClosed += Act_FormClosed;
                act.ShowDialog();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void DataGridViewMain_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (Main.choose)
            {
                case 0:
                    {
                        long s = Convert.ToInt64(DataGridViewMain.SelectedRows[0].Cells[0].Value);
                        DataGridViewMain.DataSource = (from ap in MedFacConnect.context.Appointment
                                                       join apdt in MedFacConnect.context.AppointmentDateVisit on ap.IDAppointment equals apdt.IDAppointment
                                                       where ap.IDAppointment == s
                                                       select new
                                                       {
                                                           IDAp = apdt.IDAppointment,
                                                           IDDate = apdt.IDAppointmentDateVisit,
                                                           Visit = apdt.DateTimeVisit,
                                                           Approved = apdt.Approved
                                                       } ).ToList();
                        DataGridViewMain.Columns[0].Visible = false;
                        DataGridViewMain.Columns[1].Visible = false;
                        Main.choose = 8;
                        BackButton.Visible = true;
                        ViewButton.Visible = false;
                        EditButton.Visible = false;
                        DeleteButton.Visible = false;
                        CreateButton.Visible = false;
                        break;
                    }
                case 1:
                    {
                        InitMedFac();
                        break;
                    }
                case 2:
                    {
                        InitDoctor();
                        break;
                    }
                case 3:
                    {
                        InitArticle();
                        break;
                    }
                case 4:
                    {
                        InitCategoryMedFac();
                        break;
                    }
                case 5:
                    {
                        InitCategoryArticle();
                        break;
                    }
                case 6:
                    {
                        InitSpeciality();
                        break;
                    }
                case 7:
                    {
                        InitCity();
                        break;
                    }
                default:
                    {
                        long id = (long)DataGridViewMain.SelectedRows[0].Cells[1].Value;
                        AppointmentDateVisit apd = (from apdt in MedFacConnect.context.AppointmentDateVisit
                                                    where apdt.IDAppointmentDateVisit == id
                                                    select apdt).FirstOrDefault();
                        if (apd.Approved == true)
                        {
                            apd.Approved = false;
                        }
                        else
                        {
                            apd.Approved = true;
                        }
                        MedFacConnect.context.SaveChanges();
                        long s = Convert.ToInt64(DataGridViewMain.SelectedRows[0].Cells[0].Value);
                        DataGridViewMain.DataSource = (from ap in MedFacConnect.context.Appointment
                                                       join apdt in MedFacConnect.context.AppointmentDateVisit on ap.IDAppointment equals apdt.IDAppointment
                                                       where ap.IDAppointment == s
                                                       select new
                                                       {
                                                           IDAp = apdt.IDAppointment,
                                                           IDDate = apdt.IDAppointmentDateVisit,
                                                           Visit = apdt.DateTimeVisit,
                                                           Approved = apdt.Approved
                                                       }).ToList();
                        DataGridViewMain.Columns[0].Visible = false;
                        DataGridViewMain.Columns[1].Visible = false;
                        break;
                    }
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Main.choose = 0;
            InitAppointment();
            BackButton.Visible = false;
            ViewButton.Visible = true;
            EditButton.Visible = true;
            DeleteButton.Visible = true;
            CreateButton.Visible = true;
        }
    }
}
