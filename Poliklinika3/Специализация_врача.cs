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
    
    public partial class Специализация_врача
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Специализация_врача()
        {
            this.Время_приема = new HashSet<Время_приема>();
        }
    
        public int id_записи { get; set; }
        public int id_Врача { get; set; }
        public int id_Специал { get; set; }
        public int id_Отделения { get; set; }
    
        public virtual Врач Врач { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Время_приема> Время_приема { get; set; }
        public virtual Отделение Отделение { get; set; }
        public virtual Специализация Специализация { get; set; }
    }
}
