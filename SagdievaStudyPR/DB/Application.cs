//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SagdievaStudyPR.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Application
    {
        public int ID { get; set; }
        public string ID_Speciality { get; set; }
        public Nullable<int> ID_Discipline { get; set; }
    
        public virtual Discipline Discipline { get; set; }
        public virtual Speciality Speciality { get; set; }
    }
}