//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Poliklinika3
{
    using System;
    using System.Collections.Generic;
    
    public partial class Пациенты
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Пациенты()
        {
            this.Время_приема = new HashSet<Время_приема>();
        }
    
        public int id_Пациента { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }
        public System.DateTime Дата_рождения { get; set; }
        public string Серия_Номер_Паспорта { get; set; }
        public string Адрес { get; set; }
        public int id_Пола { get; set; }
        public string Телефон { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Время_приема> Время_приема { get; set; }
        public virtual Мед__карта_пациента Мед__карта_пациента { get; set; }
        public virtual Полис_Пациента Полис_Пациента { get; set; }
        public virtual Пол Пол { get; set; }
    }
}
