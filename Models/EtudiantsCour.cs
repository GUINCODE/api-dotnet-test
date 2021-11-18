using System;
using System.Collections.Generic;

#nullable disable

namespace testApi.Models
{
    public partial class EtudiantsCour
    {
        public int Id { get; set; }
        public int IdEtudiant { get; set; }
        public int IdCour { get; set; }

        public virtual Cour IdCourNavigation { get; set; }
        public virtual Etduiant IdEtudiantNavigation { get; set; }
    }
}
