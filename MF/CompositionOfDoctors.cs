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
    
    public partial class CompositionOfDoctors
    {
        public long IDMedFac { get; set; }
        public long IDDoctor { get; set; }
        public bool Fired { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual MedicalFacility MedicalFacility { get; set; }
        
        public CompositionOfDoctors()
        {
        }
        public CompositionOfDoctors(long med, long doc, bool fired = false)
        {
            this.IDMedFac = med;
            this.IDDoctor = doc;
            this.Fired = fired;
        }
    }
}