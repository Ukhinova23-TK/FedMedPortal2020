using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Forms;

namespace MF
{
    public partial class Action : Form
    {
        private Appointment appointment;
        private Article article;
        private CategoryArticle categoryArticle;
        private CategoryMedFac categoryMedFac;
        private City city;
        private Doctor doctor;
        private Speciality speciality;
        private MedicalFacility medicalFacility;

        //Конструкторы

        public Action()
        {
            InitializeComponent();
        }
        public Action(long item, bool view = false)
        {
            InitializeComponent();
            switch (Main.choose)
            {
                case 0:
                    {
                        if (item != -1)
                            InitAppointment(item, view);
                        else
                            InitAppointment();
                        break;
                    }
                case 1:
                    {
                        if (item != -1)
                            InitMedFac(item, view);
                        else
                            InitMedFac();
                        break;
                    }
                case 2:
                    {
                        if (item != -1)
                            InitDoctor(item, view);
                        else
                            InitDoctor();
                        break;
                    }
                case 3:
                    {
                        if (item != -1)
                            InitArticle(item, view);
                        else
                            InitArticle();
                        break;
                    }
                case 4:
                    {
                        if (item != -1)
                            InitCategoryMedFac(item, view);
                        else
                            InitCategoryMedFac();
                        break;
                    }
                case 5:
                    {
                        if (item != -1)
                            InitCategoryArticle(item, view);
                        else
                            InitCategoryArticle();
                        break;
                    }
                case 6:
                    {
                        if (item != -1)
                            InitSpeciality(item, view);
                        else
                            InitSpeciality();
                        break;
                    }
                case 7:
                    {
                        if (item != -1)
                            InitCity(item, view);
                        else
                            InitCity();
                        break;
                    }
            }
        }

        //Заполнение формы

        private void InitAppointment(long s, bool view)
        {
            MedicalFacilityPanel.Visible = false;
            ArticlePanel.Visible = false;
            CategoryPanel.Visible = false;
            DoctorPanel.Visible = false;
            AppointmentPanel.Visible = true;
            appointment = MedFacConnect.context.Appointment.Find(s);
            FamilyVisitorTextBox.Text = appointment.FamilyVisitor;
            NameVisitorTextBox.Text = appointment.NameVisitor;
            PatronymicVisitorTextBox.Text = appointment.PatronymicVisitor;
            BirthdayVisitorDateTimePicker.Value = (DateTime)appointment.DateBirthdayVisitor;
            GenderVisitorCheckBox.Checked = appointment.GenderVisitor;
            CategoryMedFacomboBox.DataSource = (from cat in MedFacConnect.context.CategoryMedFac
                                                select cat.NameCategory).ToList();
            string str = (from cat in MedFacConnect.context.CategoryMedFac
                          join med in MedFacConnect.context.MedicalFacility on cat.IDCategoryMedFac equals med.IDCategoryMedFac
                          where appointment.IDMedFac == med.IDMedFac
                          select cat.NameCategory).FirstOrDefault();
            for(int i = 0; i < CategoryMedFacomboBox.Items.Count; i++)
            {
                if (CategoryMedFacomboBox.Items[i].ToString() == str)
                {
                    CategoryMedFacomboBox.Text = CategoryMedFacomboBox.Items[i].ToString();
                }
            }
            CityMedFacComboBox.DataSource = (from city in MedFacConnect.context.City
                                             join med in MedFacConnect.context.MedicalFacility on city.IDCity equals med.IDCity
                                             where med.IDCategoryMedFac == (from cat in MedFacConnect.context.CategoryMedFac
                                                                            where cat.NameCategory == str
                                                                            select cat.IDCategoryMedFac).FirstOrDefault()
                                             select city.NameCity).ToList();
            string str1 = (from med in MedFacConnect.context.MedicalFacility
                   join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                   where med.IDMedFac == appointment.IDMedFac
                   select city.NameCity).FirstOrDefault();
            for(int i = 0; i < CityMedFacComboBox.Items.Count; i++)
            {
                if (CityMedFacComboBox.Items[i].ToString() == str1)
                {
                    CityMedFacComboBox.Text = CityMedFacComboBox.Items[i].ToString();
                }
            }
            NameMedFacComboBox.DataSource = (from city in MedFacConnect.context.City
                                             join med in MedFacConnect.context.MedicalFacility on city.IDCity equals med.IDCity
                                             where med.IDCategoryMedFac == (from cat in MedFacConnect.context.CategoryMedFac
                                                                            where cat.NameCategory == str
                                                                            select cat.IDCategoryMedFac).FirstOrDefault()
                                             && city.NameCity == str1
                                             select med.NameMedFac).ToList();
            string str2 = (from med in MedFacConnect.context.MedicalFacility
                           where med.IDMedFac == appointment.IDMedFac
                           select med.NameMedFac).FirstOrDefault();
            for(int i = 0; i < NameMedFacComboBox.Items.Count; i++)
            {
                if (NameMedFacComboBox.Items[i].ToString() == str2)
                {
                    NameMedFacComboBox.Text = NameMedFacComboBox.Items[i].ToString();
                }
            }
            SpecialityDoctorComboBox.DataSource = (from spec in MedFacConnect.context.Speciality
                                                   join docspec in MedFacConnect.context.DoctorSpeciality on spec.IDSpeciality equals docspec.IDSpeciality
                                                   join comp in MedFacConnect.context.CompositionOfDoctors on docspec.IDDoctor equals comp.IDDoctor
                                                   where comp.IDMedFac == (from med in MedFacConnect.context.MedicalFacility
                                                                           where med.IDMedFac == appointment.IDMedFac
                                                                           select med.IDMedFac).FirstOrDefault()
                                                   select spec.NameSpeciality).ToList();
            string str3 = (from spec in MedFacConnect.context.Speciality
                          join docspec in MedFacConnect.context.DoctorSpeciality on spec.IDSpeciality equals docspec.IDSpeciality
                          where appointment.IDDoctor == docspec.IDDoctor
                          select spec.NameSpeciality).FirstOrDefault();
            for(int i = 0; i < SpecialityDoctorComboBox.Items.Count; i++)
            {
                if (SpecialityDoctorComboBox.Items[i].ToString() == str3)
                {
                    SpecialityDoctorComboBox.Text = SpecialityDoctorComboBox.Items[i].ToString();
                }
            }
            FIODoctorComboBox.DataSource = (from doc in MedFacConnect.context.Doctor
                                            join comp in MedFacConnect.context.CompositionOfDoctors on doc.IDDoctor equals comp.IDDoctor
                                            join med in MedFacConnect.context.MedicalFacility on comp.IDMedFac equals med.IDMedFac
                                            join docspec in MedFacConnect.context.DoctorSpeciality on doc.IDDoctor equals docspec.IDDoctor
                                            where med.NameMedFac == (from med in MedFacConnect.context.MedicalFacility
                                                                     where med.IDMedFac == appointment.IDMedFac
                                                                     select med.NameMedFac).FirstOrDefault()
                                            && med.IDCity == (from med in MedFacConnect.context.MedicalFacility
                                                              join city in MedFacConnect.context.City on med.IDCity equals city.IDCity
                                                              where med.IDMedFac == appointment.IDMedFac
                                                              select city.IDCity).FirstOrDefault()
                                            && docspec.IDSpeciality == (from spec in MedFacConnect.context.Speciality
                                                                        join docspec in MedFacConnect.context.DoctorSpeciality on spec.IDSpeciality equals docspec.IDSpeciality
                                                                        where appointment.IDDoctor == docspec.IDDoctor
                                                                        select spec.IDSpeciality).FirstOrDefault()
                                            select doc.FamilyDoctor + " " + doc.NameDoctor + " " + doc.PatronymicDoctor).ToList();
            AppointmenrtDataGridView.DataSource = (from apdt in MedFacConnect.context.AppointmentDateVisit
                                                   where apdt.IDAppointment == appointment.IDAppointment
                                                   select new
                                                   {
                                                       ID = apdt.IDAppointmentDateVisit,
                                                       Value = apdt.DateTimeVisit
                                                   }).ToList();
            AppointmenrtDataGridView.Columns[0].Visible = false;
            AppointmenrtDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CategoryMedFacomboBox.TextChanged += CategoryMedFacomboBox_TextChanged;
            CityMedFacComboBox.TextChanged += CityMedFacComboBox_TextChanged;
            NameMedFacComboBox.TextChanged += NameMedFacComboBox_TextChanged;
            SpecialityDoctorComboBox.TextChanged += SpecialityDoctorComboBox_TextChanged;
            FIODoctorComboBox.TextChanged += FIODoctorComboBox_TextChanged;
            if (view)
            {
                this.Text = "Просмотр";
                NewPanel.Visible = false;
                AppointmentPanel.Enabled = false;
                SaveButton.Visible = false;
            }
            else
            {
                this.Text = "Изменение";
            }
        }
        private void InitAppointment()
        {
            MedicalFacilityPanel.Visible = false;
            ArticlePanel.Visible = false;
            CategoryPanel.Visible = false;
            DoctorPanel.Visible = false;
            AppointmentPanel.Visible = true;
            appointment = new Appointment();
            CategoryMedFacomboBox.DataSource = (from cat in MedFacConnect.context.CategoryMedFac
                                                select cat.NameCategory).ToList();
            CityMedFacComboBox.DataSource = (from city in MedFacConnect.context.City
                                             select city.NameCity).ToList();
            NameMedFacComboBox.DataSource = (from med in MedFacConnect.context.MedicalFacility
                                             select med.NameMedFac).ToList();
            SpecialityDoctorComboBox.DataSource = (from spec in MedFacConnect.context.Speciality
                                                   select spec.NameSpeciality).ToList();
            FIODoctorComboBox.DataSource = (from doc in MedFacConnect.context.Doctor
                                            select doc.FamilyDoctor + " " + doc.NameDoctor + " " + doc.PatronymicDoctor).ToList();
            CategoryMedFacomboBox.TextChanged += CategoryMedFacomboBox_TextChanged;
            CityMedFacComboBox.TextChanged += CityMedFacComboBox_TextChanged;
            NameMedFacComboBox.TextChanged += NameMedFacComboBox_TextChanged;
            SpecialityDoctorComboBox.TextChanged += SpecialityDoctorComboBox_TextChanged;
            FIODoctorComboBox.TextChanged += FIODoctorComboBox_TextChanged;
        }

        private void InitMedFac(long s, bool view)
        {
            AppointmentPanel.Visible = false;
            ArticlePanel.Visible = false;
            CategoryPanel.Visible = false;
            DoctorPanel.Visible = false;
            MedicalFacilityPanel.Visible = true;
            medicalFacility = MedFacConnect.context.MedicalFacility.Find(s);
            NameMedicalFacilityTextBox.Text = medicalFacility.NameMedFac;
            StreetMedicalFacilityTextBox.Text = medicalFacility.Street;
            BuildingMedicalFacilityTextBox.Text = medicalFacility.Building;
            CategoryMedicalFacilityComboBox.DataSource = (from cat in MedFacConnect.context.CategoryMedFac
                                                          select cat.NameCategory).ToList();
            string str = (from cat in MedFacConnect.context.CategoryMedFac
                          where medicalFacility.IDCategoryMedFac == cat.IDCategoryMedFac
                          select cat.NameCategory).FirstOrDefault();
            for(int i = 0; i < CategoryMedicalFacilityComboBox.Items.Count; i++)
            {
                if(CategoryMedicalFacilityComboBox.Items[i].ToString() == str)
                {
                    CategoryMedicalFacilityComboBox.Text = CategoryMedicalFacilityComboBox.Items[i].ToString();
                }
            }
            CategoryMedicalFacilityComboBox.TextChanged += CategoryMedicalFacilityComboBox_TextChanged;
            CityMedicalFacilityComboBox.DataSource = (from cit in MedFacConnect.context.City
                                                      select cit.NameCity).ToList();
            string str1 = (from city in MedFacConnect.context.City
                           where medicalFacility.IDCity == city.IDCity
                           select city.NameCity).FirstOrDefault();
            for(int i = 0; i < CityMedicalFacilityComboBox.Items.Count; i++)
            {
                if(CityMedicalFacilityComboBox.Items[i].ToString() == str1)
                {
                    CityMedicalFacilityComboBox.Text = CityMedicalFacilityComboBox.Items[i].ToString();
                }
            }
            CityMedicalFacilityComboBox.TextChanged += CityMedicalFacilityComboBox_TextChanged;
            NewPersonalPanel.Visible = false;
            PersonalDataGridView.DataSource = (from composition in MedFacConnect.context.CompositionOfDoctors
                                               join doc in MedFacConnect.context.Doctor on composition.IDDoctor equals doc.IDDoctor
                                               where composition.IDMedFac == medicalFacility.IDMedFac
                                               select new
                                               {
                                                   ID = doc.IDDoctor,
                                                   FIO = doc.FamilyDoctor + " " + doc.NameDoctor + " " + doc.PatronymicDoctor
                                               }).ToList();
            PersonalDataGridView.Columns[0].Visible = false;
            PersonalDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SaveButton.Visible = false;
            if (view)
            {
                this.Text = "Просмотр";
                MedicalFacilityPanel.Enabled = false;
                FiredPresonalButton.Visible = false;
            }
            else
            {
                this.Text = "Изменение";
                SaveButton.Visible = true;
                NewPersonalPanel.Visible = true;
                NewPersonalComboBox.DataSource = (from doc in MedFacConnect.context.Doctor
                                                  select doc.FamilyDoctor + " " + doc.NameDoctor + " " + doc.PatronymicDoctor).ToList();
                NewPersonalComboBox.TextChanged += NewPersonalComboBox_TextChanged;
            }
        }
        private void InitMedFac()
        {
            AppointmentPanel.Visible = false;
            ArticlePanel.Visible = false;
            CategoryPanel.Visible = false;
            DoctorPanel.Visible = false;
            MedicalFacilityPanel.Visible = true;
            medicalFacility = new MedicalFacility();

            CategoryMedicalFacilityComboBox.DataSource = (from cat in MedFacConnect.context.CategoryMedFac
                                                          select cat.NameCategory).ToList();
            CityMedicalFacilityComboBox.DataSource = (from city in MedFacConnect.context.City
                                                      select city.NameCity).ToList();
            PersonalPanel.Visible = false;
            NewPersonalComboBox.DataSource = (from doc in MedFacConnect.context.Doctor
                                              select doc.FamilyDoctor + " " + doc.NameDoctor + " " + doc.PatronymicDoctor).ToList();
            NewPersonalComboBox.TextChanged += NewPersonalComboBox_TextChanged;
        }

        private void InitDoctor(long s, bool view)
        {
            AppointmentPanel.Visible = false;
            ArticlePanel.Visible = false;
            CategoryPanel.Visible = false;
            DoctorPanel.Visible = true;
            DoctorPanel.Enabled = false;
            MedicalFacilityPanel.Visible = false;
            doctor = MedFacConnect.context.Doctor.Find(s);
            FamilyDoctorTextBox.Text = doctor.FamilyDoctor;
            NameDoctorTextBox.Text = doctor.NameDoctor;
            PatronymicDoctorTextBox.Text = doctor.PatronymicDoctor;
            BirthdayDoctorDateTimePicker.Value = doctor.DateBirthdayDoctor.Value;
            GenderDoctorCheckBox.Checked = doctor.GenderDoctor;
            SpecialistDataGridView.DataSource = (from specdoc in MedFacConnect.context.DoctorSpeciality
                                                 join spec in MedFacConnect.context.Speciality on specdoc.IDSpeciality equals spec.IDSpeciality
                                                 where specdoc.IDDoctor == doctor.IDDoctor
                                                 select new
                                                 {
                                                     ID = spec.IDSpeciality,
                                                     NameSpec = spec.NameSpeciality
                                                 }).ToList();
            SpecialistDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SpecialistDataGridView.Columns[0].Visible = false;
            PlaceWorkDataGridView.DataSource = (from compos in MedFacConnect.context.CompositionOfDoctors
                                                join medfac in MedFacConnect.context.MedicalFacility on compos.IDMedFac equals medfac.IDMedFac
                                                where compos.IDDoctor == doctor.IDDoctor
                                                select new
                                                {
                                                    ID = medfac.IDMedFac,
                                                    NameMedFac = medfac.NameMedFac
                                                }).ToList();
            PlaceWorkDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PlaceWorkDataGridView.Columns[0].Visible = false;
            SaveButton.Visible = false;
            if (view)
            {
                this.Text = "Просмотр";
                NewSpecPanel.Visible = false;
                NewPlaceWorkPanel.Visible = false;
                DelSpecButton.Visible = false;
                DeleteMedFacButton.Visible = false;
            }
            else
            {
                this.Text = "Изменение";
                DoctorPanel.Enabled = true;
                SpecialistNewComboBox.DataSource = (from spec in MedFacConnect.context.Speciality
                                                    select spec.NameSpeciality).ToList();
                SpecialistNewComboBox.TextChanged += SpecialistNewComboBox_TextChanged;
                NewPlaceWorkComboBox.DataSource = (from work in MedFacConnect.context.MedicalFacility
                                                   select work.NameMedFac).ToList();
                NewPlaceWorkComboBox.TextChanged += NewPlaceWorkComboBox_TextChanged;
                SaveButton.Visible = true;
            }
        }
        private void InitDoctor()
        {
            AppointmentPanel.Visible = false;
            ArticlePanel.Visible = false;
            CategoryPanel.Visible = false;
            MedicalFacilityPanel.Visible = false;
            DoctorPanel.Visible = true;
            doctor =  new Doctor();
            PlaceWorkPanel.Visible = false;
            SpecialistNewComboBox.DataSource = (from spec in MedFacConnect.context.Speciality
                                                select spec.NameSpeciality).ToList();
            NewPlaceWorkComboBox.DataSource = (from work in MedFacConnect.context.MedicalFacility
                                               select work.NameMedFac).ToList();
            SpecPanel.Visible = false;
            SpecialistNewComboBox.TextChanged += SpecialistNewComboBox_TextChanged;
            NewPlaceWorkComboBox.TextChanged += NewPlaceWorkComboBox_TextChanged;
        }

        private void InitArticle(long s, bool view)
        {
            AppointmentPanel.Visible = false;
            CategoryPanel.Visible = false;
            DoctorPanel.Visible = false;
            MedicalFacilityPanel.Visible = false;
            ArticlePanel.Visible = true;
            article = MedFacConnect.context.Article.Find(s);
            TitleArticleTextBox.Text = article.TitleArticle;
            DescriptionArticleRichTextBox.Text = article.DescriptionArticle;
            AuthorArticleTextBox.Text = article.PsuedonymAuthor;
            CategoryArticleComboBox.DataSource = (from cat in MedFacConnect.context.CategoryArticle
                                                  select cat.NameCategory).ToList();
            string str = (from cat in MedFacConnect.context.CategoryArticle
                          where article.IDCategoryArticle == cat.IDCategoryArticle
                          select cat.NameCategory).FirstOrDefault();
            for(int i = 0; i < CategoryArticleComboBox.Items.Count; i++)
            {
                if(CategoryArticleComboBox.Items[i].ToString() == str)
                {
                    CategoryArticleComboBox.Text = CategoryArticleComboBox.Items[i].ToString();
                }
            }
            CategoryArticleComboBox.TextChanged += CategoryArticleComboBox_TextChanged;
            if (view)
            {
                this.Text = "Просмотр";
                SaveButton.Visible = false;
                ArticlePanel.Enabled = false;
            }
            else
            {
                this.Text = "Изменение";

            }
        }
        private void InitArticle()
        {
            AppointmentPanel.Visible = false;
            CategoryPanel.Visible = false;
            DoctorPanel.Visible = false;
            MedicalFacilityPanel.Visible = false;
            ArticlePanel.Visible = true;
            article = new Article();
            CategoryArticleComboBox.DataSource = (from cat in MedFacConnect.context.CategoryArticle
                                                  select cat.NameCategory).ToList();
        }

        private void InitCategoryMedFac(long s, bool view)
        {
            AppointmentPanel.Visible = false;
            ArticlePanel.Visible = false;
            DoctorPanel.Visible = false;
            MedicalFacilityPanel.Visible = false;
            CategoryPanel.Visible = true;
            categoryMedFac = MedFacConnect.context.CategoryMedFac.Find(s);
            NameTextBox.Text = categoryMedFac.NameCategory;
            if (view)
            {
                this.Text = "Просмотр";
                CategoryPanel.Enabled = false;
                SaveButton.Visible = false;

            }
            else
            {
                this.Text = "Изменение";
            }
        }
        private void InitCategoryMedFac()
        {
            AppointmentPanel.Visible = false;
            ArticlePanel.Visible = false;
            DoctorPanel.Visible = false;
            MedicalFacilityPanel.Visible = false;
            CategoryPanel.Visible = true;
            categoryMedFac = new CategoryMedFac();
        }

        private void InitCategoryArticle(long s, bool view)
        {
            AppointmentPanel.Visible = false;
            ArticlePanel.Visible = false;
            DoctorPanel.Visible = false;
            MedicalFacilityPanel.Visible = false;
            CategoryPanel.Visible = true;
            categoryArticle = MedFacConnect.context.CategoryArticle.Find(s);
            NameTextBox.Text = categoryArticle.NameCategory;
            if (view)
            {
                this.Text = "Просмотр";
                CategoryPanel.Enabled = false;
                SaveButton.Visible = false;
            }
            else
            {
                this.Text = "Изменение";
            }
        }
        private void InitCategoryArticle()
        {
            AppointmentPanel.Visible = false;
            ArticlePanel.Visible = false;
            DoctorPanel.Visible = false;
            MedicalFacilityPanel.Visible = false;
            CategoryPanel.Visible = true;
            categoryArticle = new CategoryArticle();
        }

        private void InitSpeciality(long s, bool view)
        {
            AppointmentPanel.Visible = false;
            ArticlePanel.Visible = false;
            DoctorPanel.Visible = false;
            MedicalFacilityPanel.Visible = false;
            CategoryPanel.Visible = true;
            speciality = MedFacConnect.context.Speciality.Find(s);
            NameTextBox.Text = speciality.NameSpeciality;
            if (view)
            {
                this.Text = "Просмотр";
                CategoryPanel.Enabled = false;
                SaveButton.Visible = false;
            }
            else
            {
                this.Text = "Изменение";
            }
        }
        private void InitSpeciality()
        {
            AppointmentPanel.Visible = false;
            ArticlePanel.Visible = false;
            DoctorPanel.Visible = false;
            MedicalFacilityPanel.Visible = false;
            CategoryPanel.Visible = true;
            speciality = new Speciality();
        }

        private void InitCity(long s, bool view)
        {
            AppointmentPanel.Visible = false;
            ArticlePanel.Visible = false;
            DoctorPanel.Visible = false;
            MedicalFacilityPanel.Visible = false;
            CategoryPanel.Visible = true;
            city = MedFacConnect.context.City.Find(s);
            NameTextBox.Text = city.NameCity;
            if (view)
            {
                this.Text = "Просмотр";
                CategoryPanel.Enabled = false;
                SaveButton.Visible = false;
            }
            else
            {
                this.Text = "Изменение";
            }
        }
        private void InitCity()
        {
            AppointmentPanel.Visible = false;
            ArticlePanel.Visible = false;
            DoctorPanel.Visible = false;
            CategoryPanel.Visible = true;
            MedicalFacilityPanel.Visible = false;
            city = new City();
        }

        //Проверка заполнения обязательных полей

        private bool CheckAppointment()
        {
            if(!(string.IsNullOrEmpty(appointment.FamilyVisitor)) && !(string.IsNullOrEmpty(appointment.NameVisitor))
                && !(string.IsNullOrEmpty(appointment.PatronymicVisitor)) && (appointment.IDMedFac != 0)
                && (appointment.IDDoctor != 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckMedicalFacility()
        {
            if (!(string.IsNullOrEmpty(medicalFacility.NameMedFac)) && !(string.IsNullOrEmpty(medicalFacility.Street))
                && !(string.IsNullOrEmpty(medicalFacility.Building)) && (medicalFacility.IDCategoryMedFac != 0)
                && (medicalFacility.IDCity != 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckDoctor()
        {
            if (!(string.IsNullOrEmpty(doctor.FamilyDoctor)) && !(string.IsNullOrEmpty(doctor.NameDoctor))
                && !(string.IsNullOrEmpty(doctor.PatronymicDoctor)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckArticle()
        {
            if (!(string.IsNullOrEmpty(article.TitleArticle)) && !(string.IsNullOrEmpty(article.DescriptionArticle))
                && !(string.IsNullOrEmpty(article.PsuedonymAuthor)) && (article.IDCategoryArticle != 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckCategoryMedFac()
        {
            if (!(string.IsNullOrEmpty(categoryMedFac.NameCategory)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckCategoryArticle()
        {
            if (!(string.IsNullOrEmpty(categoryArticle.NameCategory)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckSpeciality()
        {
            if (!(string.IsNullOrEmpty(speciality.NameSpeciality)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckCity()
        {
            if (!(string.IsNullOrEmpty(city.NameCity)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //События нажатия на кнопки

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (Main.choose == 0)
            {
                if(this.Text == "Изменение")
                {
                    string medic = NameMedFacComboBox.Text;
                    appointment.IDMedFac = (from med in MedFacConnect.context.MedicalFacility
                                            where med.NameMedFac == medic
                                            select med.IDMedFac).FirstOrDefault();
                    string[] fio = FIODoctorComboBox.Text.Split(' ');
                    string f = fio[0];
                    string n = fio[1];
                    string p = fio[2];
                    appointment.IDDoctor = (from doc in MedFacConnect.context.Doctor
                                            where doc.FamilyDoctor == f
                                            && doc.NameDoctor == n
                                            && doc.PatronymicDoctor == p
                                            select doc.IDDoctor).FirstOrDefault();
                    appointment.FamilyVisitor = FamilyVisitorTextBox.Text;
                    appointment.NameVisitor = NameVisitorTextBox.Text;
                    appointment.PatronymicVisitor = PatronymicVisitorTextBox.Text;
                    appointment.DateBirthdayVisitor = BirthdayVisitorDateTimePicker.Value;
                    appointment.GenderVisitor = GenderVisitorCheckBox.Checked;
                    appointment.Finish = FinishCheckBox.Checked;
                    if (CheckAppointment())
                    {
                        for (int i = 0; i < AppointmentNewDataGridView.Rows.Count; i++)
                        {
                            DateTime dt = (DateTime)AppointmentNewDataGridView.Rows[i].Cells[0].Value;
                            MedFacConnect.context.AppointmentDateVisit.Add(new AppointmentDateVisit(appointment.IDAppointment, dt));
                        }
                        MedFacConnect.context.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(text: "Не все поля заполнены", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (CheckAppointment())
                    {
                        MedFacConnect.context.Appointment.Add(new Appointment(appointment.IDMedFac, appointment.IDDoctor, 
                            appointment.FamilyVisitor, appointment.NameVisitor, appointment.PatronymicVisitor, 
                            appointment.DateBirthdayVisitor, appointment.GenderVisitor, appointment.Finish));
                        for (int i = 0; i < AppointmentNewDataGridView.Rows.Count; i++)
                        {
                            DateTime dt = (DateTime)AppointmentNewDataGridView.Rows[i].Cells[0].Value;
                            MedFacConnect.context.AppointmentDateVisit.Add(new AppointmentDateVisit(appointment.IDAppointment, dt));
                        }
                        MedFacConnect.context.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(text: "Не все поля заполнены", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
            }
            else if (Main.choose == 1)
            {
                if(this.Text == "Изменение")
                {
                    medicalFacility.NameMedFac = NameMedicalFacilityTextBox.Text;
                    medicalFacility.IDCategoryMedFac = (from cat in MedFacConnect.context.CategoryMedFac
                                                        where cat.NameCategory == CategoryMedicalFacilityComboBox.Text
                                                        select cat.IDCategoryMedFac).ToList().ElementAtOrDefault(0);
                    medicalFacility.IDCity = (from city in MedFacConnect.context.City
                                              where city.NameCity == CityMedicalFacilityComboBox.Text
                                              select city.IDCity).ToList().ElementAtOrDefault(0);
                    medicalFacility.Street = StreetMedicalFacilityTextBox.Text;
                    medicalFacility.Building = BuildingMedicalFacilityTextBox.Text;
                    if (CheckMedicalFacility())
                    {
                        for (int i = 0; i < NewPersonalDataGridView.Rows.Count; i++)
                        {
                            string[] d = NewPersonalDataGridView.Rows[i].Cells[0].Value.ToString().Split(' ');
                            string fam = d[0];
                            string nam = d[1];
                            string pat = d[2];
                            MedFacConnect.context.CompositionOfDoctors.Add(new CompositionOfDoctors(medicalFacility.IDMedFac,
                                (from doc in MedFacConnect.context.Doctor
                                 where doc.FamilyDoctor == fam
                                 && doc.NameDoctor == nam
                                 && doc.PatronymicDoctor == pat
                                 select doc.IDDoctor).FirstOrDefault()));
                        }
                        MedFacConnect.context.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(text: "Не все поля заполнены", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (CheckMedicalFacility())
                    {
                        MedFacConnect.context.MedicalFacility.Add(new MedicalFacility(medicalFacility.NameMedFac, medicalFacility.Street,
                        medicalFacility.Building, medicalFacility.IDCategoryMedFac, medicalFacility.IDCity));
                        long id = (from med in MedFacConnect.context.MedicalFacility
                                   where medicalFacility.NameMedFac == med.NameMedFac
                                   && medicalFacility.IDCity == med.IDCity
                                   select med.IDMedFac).FirstOrDefault();
                        for (int i = 0; i < NewPersonalDataGridView.Rows.Count; i++)
                        {
                            string[] d = NewPersonalDataGridView.Rows[i].Cells[0].Value.ToString().Split(' ');
                            string fam = d[0];
                            string nam = d[1];
                            string pat = d[2];
                            MedFacConnect.context.CompositionOfDoctors.Add(new CompositionOfDoctors(id,
                                (from doc in MedFacConnect.context.Doctor
                                 where doc.FamilyDoctor == fam
                                 && doc.NameDoctor == nam
                                 && doc.PatronymicDoctor == pat
                                 select doc.IDDoctor).FirstOrDefault()));
                        }
                        MedFacConnect.context.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(text: "Не все поля заполнены", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
            }
            else if (Main.choose == 2)
            {
                if (this.Text == "Изменение")
                {
                    doctor.FamilyDoctor = FamilyDoctorTextBox.Text;
                    doctor.NameDoctor = NameDoctorTextBox.Text;
                    doctor.PatronymicDoctor = PatronymicDoctorTextBox.Text;
                    doctor.DateBirthdayDoctor = BirthdayDoctorDateTimePicker.Value;
                    doctor.GenderDoctor = GenderDoctorCheckBox.Checked;
                    if (CheckDoctor())
                    {
                        for (int i = 0; i < SpecialistNewDataGridView.Rows.Count; i++)
                        {
                            string a = SpecialistNewDataGridView.Rows[i].Cells[0].Value.ToString();
                            MedFacConnect.context.DoctorSpeciality.Add(new DoctorSpeciality(doctor.IDDoctor,
                                (from spec in MedFacConnect.context.Speciality
                                 where spec.NameSpeciality == a
                                 select spec.IDSpeciality).FirstOrDefault()));
                        }
                        for (int i = 0; i < NewPlaceWorkDataGridView.Rows.Count; i++)
                        {
                            string a = NewPlaceWorkDataGridView.Rows[i].Cells[0].Value.ToString();
                            MedFacConnect.context.CompositionOfDoctors.Add(new CompositionOfDoctors(
                                (from med in MedFacConnect.context.MedicalFacility
                                 where med.NameMedFac == a
                                 select med.IDMedFac).FirstOrDefault(), doctor.IDDoctor));
                        }
                        MedFacConnect.context.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(text: "Не все поля заполнены", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (CheckDoctor())
                    {
                        MedFacConnect.context.Doctor.Add(new Doctor(doctor.FamilyDoctor, doctor.NameDoctor, doctor.PatronymicDoctor,
                        doctor.DateBirthdayDoctor, doctor.GenderDoctor));
                        long id = (from doc in MedFacConnect.context.Doctor
                                   where doc.FamilyDoctor == doctor.FamilyDoctor
                                   && doc.NameDoctor == doctor.NameDoctor
                                   && doc.PatronymicDoctor == doctor.PatronymicDoctor
                                   select doc.IDDoctor).FirstOrDefault();
                        for (int i = 0; i < SpecialistNewDataGridView.Rows.Count; i++)
                        {
                            MedFacConnect.context.DoctorSpeciality.Add(new DoctorSpeciality(id,
                                (from spec in MedFacConnect.context.Speciality
                                 where spec.NameSpeciality == SpecialistNewDataGridView.Rows[i].Cells[0].Value.ToString()
                                 select spec.IDSpeciality).FirstOrDefault()));
                        }
                        for (int i = 0; i < NewPlaceWorkDataGridView.Rows.Count; i++)
                        {
                            MedFacConnect.context.CompositionOfDoctors.Add(new CompositionOfDoctors(
                                (from med in MedFacConnect.context.MedicalFacility
                                 where med.NameMedFac == NewPlaceWorkDataGridView.Rows[i].Cells[0].Value.ToString()
                                 select med.IDMedFac).FirstOrDefault(), id));
                        }
                        MedFacConnect.context.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(text: "Не все поля заполнены", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
            }
            else if (Main.choose == 3)
            {
                if (this.Text == "Изменение")
                {
                    article.TitleArticle = TitleArticleTextBox.Text;
                    article.DescriptionArticle = DescriptionArticleRichTextBox.Text;
                    article.PsuedonymAuthor = AuthorArticleTextBox.Text;
                    article.IDCategoryArticle = (from cat in MedFacConnect.context.CategoryArticle
                                               where cat.NameCategory == CategoryArticleComboBox.Text
                                               select cat.IDCategoryArticle).ToList().ElementAtOrDefault(0);
                    if (CheckArticle())
                    {
                        MedFacConnect.context.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(text: "Не все поля заполнены", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (CheckArticle())
                    {
                        MedFacConnect.context.Article.Add(new Article(article.TitleArticle,
                        article.DescriptionArticle, article.PsuedonymAuthor, article.IDCategoryArticle));
                        MedFacConnect.context.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(text: "Не все поля заполнены", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
            }
            else if (Main.choose == 4)
            {
                if(this.Text == "Изменение")
                {
                    if (CheckCategoryMedFac())
                    {
                        categoryMedFac.NameCategory = NameTextBox.Text;
                        MedFacConnect.context.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(text: "Не все поля заполнены", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (CheckCategoryMedFac())
                    {
                        MedFacConnect.context.CategoryMedFac.Add(new CategoryMedFac(categoryMedFac.NameCategory));
                        MedFacConnect.context.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(text: "Не все поля заполнены", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
            }
            else if (Main.choose == 5)
            {
                if (this.Text == "Изменение")
                {
                    if (CheckCategoryArticle())
                    {
                        categoryArticle.NameCategory = NameTextBox.Text;
                        MedFacConnect.context.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(text: "Не все поля заполнены", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (CheckCategoryArticle())
                    {
                        MedFacConnect.context.CategoryArticle.Add(new CategoryArticle(categoryArticle.NameCategory));
                        MedFacConnect.context.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(text: "Не все поля заполнены", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
            }
            else if (Main.choose == 6)
            {
                if (this.Text == "Изменение")
                {
                    if (CheckSpeciality())
                    {
                        speciality.NameSpeciality = NameTextBox.Text;
                        MedFacConnect.context.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(text: "Не все поля заполнены", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (CheckSpeciality())
                    {
                        MedFacConnect.context.Speciality.Add(new Speciality(speciality.NameSpeciality));
                        MedFacConnect.context.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(text: "Не все поля заполнены", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
            }
            else if (Main.choose == 7)
            {
                if (this.Text == "Изменение")
                {
                    if (CheckCity())
                    {
                        city.NameCity = NameTextBox.Text;
                        MedFacConnect.context.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(text: "Не все поля заполнены", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (CheckCity())
                    {
                        MedFacConnect.context.City.Add(new City(city.NameCity));
                        MedFacConnect.context.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(text: "Не все поля заполнены", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void DelSpecButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить автора? Отменить действие будет невозможно.", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (result == DialogResult.Yes)
                {
                    if (SpecialistDataGridView.SelectedRows.Count != 0)
                    {
                        long i = Convert.ToInt64(SpecialistDataGridView.SelectedRows[0].Cells[0].Value);
                        MedFacConnect.context.DoctorSpeciality.Remove((from docspec in MedFacConnect.context.DoctorSpeciality
                                                                           where i == docspec.IDSpeciality
                                                                           && doctor.IDDoctor == docspec.IDDoctor
                                                                           select docspec).FirstOrDefault());
                        MedFacConnect.context.SaveChanges();
                        SpecialistDataGridView.DataSource = (from specdoc in MedFacConnect.context.DoctorSpeciality
                                                             join spec in MedFacConnect.context.Speciality on specdoc.IDSpeciality equals spec.IDSpeciality
                                                             where specdoc.IDDoctor == doctor.IDDoctor
                                                             select new
                                                             {
                                                                 ID = spec.IDSpeciality,
                                                                 NameSpec = spec.NameSpeciality
                                                             }).ToList();
                        SpecialistDataGridView.Columns[0].Visible = false;
                        SpecialistDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void DeleteMedFacButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить автора? Отменить действие будет невозможно.", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (result == DialogResult.Yes)
                {
                    if (PlaceWorkDataGridView.SelectedRows.Count != 0)
                    {
                        long i = Convert.ToInt64(PlaceWorkDataGridView.SelectedRows[0].Cells[0].Value);
                        MedFacConnect.context.CompositionOfDoctors.Remove((from comp in MedFacConnect.context.CompositionOfDoctors
                                                                       where i == comp.IDMedFac
                                                                       && doctor.IDDoctor == comp.IDDoctor
                                                                       select comp).FirstOrDefault());
                        MedFacConnect.context.SaveChanges();
                        PlaceWorkDataGridView.DataSource = (from compos in MedFacConnect.context.CompositionOfDoctors
                                                            join medfac in MedFacConnect.context.MedicalFacility on compos.IDMedFac equals medfac.IDMedFac
                                                            where compos.IDDoctor == doctor.IDDoctor
                                                            select new
                                                            {
                                                                ID = medfac.IDMedFac,
                                                                NameMedFac = medfac.NameMedFac
                                                            }).ToList();
                        PlaceWorkDataGridView.Columns[0].Visible = false;
                        PlaceWorkDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            DateTime d = DateDateTimePicker.Value;
            DateTime t = TimeDateTimePicker.Value;
            AppointmentNewDataGridView.Rows.Add();
            DateTime q = new DateTime(DateDateTimePicker.Value.Year, DateDateTimePicker.Value.Month, DateDateTimePicker.Value.Day, 
                TimeDateTimePicker.Value.Hour, TimeDateTimePicker.Value.Minute, TimeDateTimePicker.Value.Second);
            AppointmentNewDataGridView.Rows[AppointmentNewDataGridView.Rows.Count - 1].Cells[0].Value = q;
            AppointmentNewDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void DeleteDteTimeButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить автора? Отменить действие будет невозможно.", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (result == DialogResult.Yes)
                {
                    if (AppointmenrtDataGridView.SelectedRows.Count != 0)
                    {
                        long i = Convert.ToInt64(AppointmenrtDataGridView.SelectedRows[0].Cells[0].Value);
                        MedFacConnect.context.AppointmentDateVisit.Remove((from apdt in MedFacConnect.context.AppointmentDateVisit
                                                                           where i == apdt.IDAppointmentDateVisit
                                                                           && apdt.IDAppointment == appointment.IDAppointment
                                                                           select apdt).FirstOrDefault());
                        MedFacConnect.context.SaveChanges();
                        AppointmenrtDataGridView.DataSource = (from apdt in MedFacConnect.context.AppointmentDateVisit
                                                               where apdt.IDAppointment == appointment.IDAppointment
                                                               select new
                                                               {
                                                                   ID = apdt.IDAppointmentDateVisit,
                                                                   Value = apdt.DateTimeVisit
                                                               }).ToList();
                        AppointmenrtDataGridView.Columns[0].Visible = false;
                        AppointmenrtDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        //События смены значений полей

        private void CategoryArticleComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                article.IDCategoryArticle = (from cat in MedFacConnect.context.CategoryArticle
                                             where cat.NameCategory == CategoryArticleComboBox.Text
                                             select cat.IDCategoryArticle).ToList().ElementAtOrDefault(0);
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void SpecialistNewComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (SpecialistNewComboBox.Text != null)
                {
                    SpecialistNewDataGridView.Rows.Add();
                    SpecialistNewDataGridView.Rows[SpecialistNewDataGridView.Rows.Count - 1].Cells[0].Value = SpecialistNewComboBox.Text;
                    SpecialistNewDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void NewPlaceWorkComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (NewPlaceWorkComboBox.Text != null)
                {
                    NewPlaceWorkDataGridView.Rows.Add();
                    NewPlaceWorkDataGridView.Rows[NewPlaceWorkDataGridView.Rows.Count - 1].Cells[0].Value = NewPlaceWorkComboBox.Text;
                    NewPlaceWorkDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void CityMedicalFacilityComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                medicalFacility.IDCity = (from city in MedFacConnect.context.City
                                          where city.NameCity == CityMedicalFacilityComboBox.Text
                                          select city.IDCity).ToList().ElementAtOrDefault(0);
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void CategoryMedicalFacilityComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                medicalFacility.IDCategoryMedFac = (from cat in MedFacConnect.context.CategoryMedFac
                                                    where cat.NameCategory == CategoryMedicalFacilityComboBox.Text
                                                    select cat.IDCategoryMedFac).ToList().ElementAtOrDefault(0);
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void FIODoctorComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string[] str = FIODoctorComboBox.Text.Split(' ');
                string s = str[0];
                string t = str[1];
                string r = str[2];
                appointment.IDDoctor = (from doc in MedFacConnect.context.Doctor
                                        where doc.FamilyDoctor == s
                                        && doc.NameDoctor == t
                                        && doc.PatronymicDoctor == r
                                        select doc.IDDoctor).FirstOrDefault();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void SpecialityDoctorComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FIODoctorComboBox.Enabled = true;
                string c = CityMedFacComboBox.Text;
                long citId = (from cit in MedFacConnect.context.City
                              where cit.NameCity == c
                              select cit.IDCity).FirstOrDefault();
                string sp = SpecialityDoctorComboBox.Text;
                long spId = (from spec in MedFacConnect.context.Speciality
                             where spec.NameSpeciality == sp
                             select spec.IDSpeciality).FirstOrDefault();
                string medic = NameMedFacComboBox.Text;
                long medId = (from med in MedFacConnect.context.MedicalFacility
                              where med.NameMedFac == medic
                              select med.IDMedFac).FirstOrDefault();
                FIODoctorComboBox.DataSource = (from med in MedFacConnect.context.MedicalFacility
                                                join com in MedFacConnect.context.CompositionOfDoctors on med.IDMedFac equals com.IDMedFac
                                                join doc in MedFacConnect.context.Doctor on com.IDDoctor equals doc.IDDoctor
                                                join specdoc in MedFacConnect.context.DoctorSpeciality on doc.IDDoctor equals specdoc.IDDoctor
                                                where med.IDMedFac == medId
                                                && med.IDCity == citId
                                                && specdoc.IDSpeciality == spId
                                                select doc.FamilyDoctor + " " + doc.NameDoctor + " " + doc.PatronymicDoctor).Distinct().ToList();
                FIODoctorComboBox.TextChanged += FIODoctorComboBox_TextChanged;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void NameMedFacComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SpecialityDoctorComboBox.Enabled = true;
                string str2 = NameMedFacComboBox.Text;
                SpecialityDoctorComboBox.DataSource = (from spec in MedFacConnect.context.Speciality
                                                       join docspec in MedFacConnect.context.DoctorSpeciality on spec.IDSpeciality equals docspec.IDSpeciality
                                                       join comp in MedFacConnect.context.CompositionOfDoctors on docspec.IDDoctor equals comp.IDDoctor
                                                       join med in MedFacConnect.context.MedicalFacility on comp.IDMedFac equals med.IDMedFac
                                                       where med.NameMedFac == str2
                                                       select spec.NameSpeciality).Distinct().ToList();
                appointment.IDMedFac = (from med in MedFacConnect.context.MedicalFacility
                                        where med.NameMedFac == str2
                                        select med.IDMedFac).FirstOrDefault();
                SpecialityDoctorComboBox.TextChanged += SpecialityDoctorComboBox_TextChanged;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void CityMedFacComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NameMedFacComboBox.Enabled = true;
                string str = CityMedFacComboBox.Text;
                NameMedFacComboBox.DataSource = (from city in MedFacConnect.context.City
                                                 join med in MedFacConnect.context.MedicalFacility on city.IDCity equals med.IDCity
                                                 where city.NameCity == str
                                                 select med.NameMedFac).Distinct().ToList();
                NameMedFacComboBox.TextChanged += NameMedFacComboBox_TextChanged;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void CategoryMedFacomboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CityMedFacComboBox.Enabled = true;
                string str = CategoryMedFacomboBox.Text;
                CityMedFacComboBox.DataSource = (from city in MedFacConnect.context.City
                                                 join med in MedFacConnect.context.MedicalFacility on city.IDCity equals med.IDCity
                                                 join cat in MedFacConnect.context.CategoryMedFac on med.IDCategoryMedFac equals cat.IDCategoryMedFac
                                                 where cat.NameCategory == str
                                                 select city.NameCity).Distinct().ToList();
                CityMedFacComboBox.TextChanged += CityMedFacComboBox_TextChanged;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void NameMedicalFacilityTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                medicalFacility.NameMedFac = NameMedicalFacilityTextBox.Text;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void StreetMedicalFacilityTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                medicalFacility.Street = StreetMedicalFacilityTextBox.Text;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void BuildingMedicalFacilityTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                medicalFacility.Building = BuildingMedicalFacilityTextBox.Text;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void NewPersonalComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (NewPersonalComboBox.Text != null)
                {
                    string s = NewPersonalComboBox.Text;
                    NewPersonalDataGridView.Rows.Add();
                    NewPersonalDataGridView.Rows[NewPersonalDataGridView.Rows.Count - 1].Cells[0].Value = NewPersonalComboBox.Text;
                    NewPersonalDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(Main.choose == 4)
                {
                    categoryMedFac.NameCategory = NameTextBox.Text;
                }
                else if(Main.choose == 5)
                {
                    categoryArticle.NameCategory = NameTextBox.Text;
                }
                else if (Main.choose == 6)
                {
                    speciality.NameSpeciality = NameTextBox.Text;
                }
                else if (Main.choose == 7)
                {
                    city.NameCity = NameTextBox.Text;
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void TitleArticleTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                article.TitleArticle = TitleArticleTextBox.Text;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void DescriptionArticleRichTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                article.DescriptionArticle = DescriptionArticleRichTextBox.Text;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void AuthorArticleTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                article.PsuedonymAuthor = AuthorArticleTextBox.Text;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void FiredPresonalButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить автора? Отменить действие будет невозможно.", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (result == DialogResult.Yes)
                {
                    if (PersonalDataGridView.SelectedRows.Count != 0)
                    {
                        long i = Convert.ToInt64(PersonalDataGridView.SelectedRows[0].Cells[0].Value);
                        MedFacConnect.context.CompositionOfDoctors.Remove((from com in MedFacConnect.context.CompositionOfDoctors
                                                                           where i == com.IDDoctor
                                                                           && medicalFacility.IDMedFac == com.IDMedFac
                                                                           select com).FirstOrDefault());
                        MedFacConnect.context.SaveChanges();
                        PersonalDataGridView.DataSource = (from composition in MedFacConnect.context.CompositionOfDoctors
                                                           join doc in MedFacConnect.context.Doctor on composition.IDDoctor equals doc.IDDoctor
                                                           where composition.IDMedFac == medicalFacility.IDMedFac
                                                           select new
                                                           {
                                                               ID = doc.IDDoctor,
                                                               FIO = doc.FamilyDoctor + " " + doc.NameDoctor + " " + doc.PatronymicDoctor
                                                           }).ToList();
                        PersonalDataGridView.Columns[0].Visible = false;
                        PersonalDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void FamilyDoctorTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                doctor.FamilyDoctor = FamilyDoctorTextBox.Text;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void NameDoctorTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                doctor.NameDoctor = NameDoctorTextBox.Text;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void PatronymicDoctorTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                doctor.PatronymicDoctor = PatronymicDoctorTextBox.Text;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void BirthdayDoctorDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                doctor.DateBirthdayDoctor = BirthdayDoctorDateTimePicker.Value;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void GenderDoctorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                doctor.GenderDoctor = GenderDoctorCheckBox.Checked;
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(text: "У вас нет прав на это действие.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }
        private void FamilyVisitorTextBox_TextChanged(object sender, EventArgs e)
        {
            appointment.FamilyVisitor = FamilyVisitorTextBox.Text;
        }
        private void NameVisitorTextBox_TextChanged(object sender, EventArgs e)
        {
            appointment.NameVisitor = NameVisitorTextBox.Text;
        }
        private void PatronymicVisitorTextBox_TextChanged(object sender, EventArgs e)
        {
            appointment.PatronymicVisitor = PatronymicVisitorTextBox.Text;
        }
        private void BirthdayVisitorDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            appointment.DateBirthdayVisitor = BirthdayVisitorDateTimePicker.Value;
        }
        private void GenderVisitorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            appointment.GenderVisitor = GenderVisitorCheckBox.Checked;
        }

        private void FinishCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            appointment.Finish = FinishCheckBox.Checked;
        }
    }
}