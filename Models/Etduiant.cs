using System;
using System.Collections.Generic;

#nullable disable

namespace testApi.Models
{
    public partial class Etduiant
    {
        public Etduiant()
        {
            EtudiantsCours = new HashSet<EtudiantsCour>();
            ReleveDeNotes = new HashSet<ReleveDeNote>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public virtual ICollection<EtudiantsCour> EtudiantsCours { get; set; }
        public virtual ICollection<ReleveDeNote> ReleveDeNotes { get; set; }
    }
}
