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
    
    public partial class Врач
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Врач()
        {
            this.Запись_в_медкарте = new HashSet<Запись_в_медкарте>();
            this.Специализация_врача = new HashSet<Специализация_врача>();
        }
    
        public int id_Врача { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }
        public System.DateTime Дата_рождения { get; set; }
        public int id_категории { get; set; }
    
        public virtual Категория_врача Категория_врача { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Запись_в_медкарте> Запись_в_медкарте { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Специализация_врача> Специализация_врача { get; set; }
    }
}
