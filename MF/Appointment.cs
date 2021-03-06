//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Appointment()
        {
            this.AppointmentDateVisit = new HashSet<AppointmentDateVisit>();
        }

        public Appointment(long id, long med, long doc, string fam, string nam, string pat, Nullable<System.DateTime> dat, bool gen, bool fin)
        {
            this.IDAppointment = id;
            this.IDMedFac = med;
            this.IDDoctor = doc;
            this.FamilyVisitor = fam;
            this.NameVisitor = nam;
            this.PatronymicVisitor = pat;
            this.DateBirthdayVisitor = dat;
            this.GenderVisitor = gen;
            this.Finish = fin;
        }

        public Appointment(long med, long doc, string fam, string nam, string pat, Nullable<System.DateTime> dat, bool gen, bool fin = false)
        {
            this.IDMedFac = med;
            this.IDDoctor = doc;
            this.FamilyVisitor = fam;
            this.NameVisitor = nam;
            this.PatronymicVisitor = pat;
            this.DateBirthdayVisitor = dat;
            this.GenderVisitor = gen;
            this.Finish = fin;
        }

        public long IDAppointment { get; set; }
        public long IDMedFac { get; set; }
        public long IDDoctor { get; set; }
        public string FamilyVisitor { get; set; }
        public string NameVisitor { get; set; }
        public string PatronymicVisitor { get; set; }
        public Nullable<System.DateTime> DateBirthdayVisitor { get; set; }
        public bool GenderVisitor { get; set; }
        public bool Finish { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentDateVisit> AppointmentDateVisit { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual MedicalFacility MedicalFacility { get; set; }
    }
}
