using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullTextSearch.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // NormalizedName: İsimdeki Türkçe karakterlerin İngilizce karşılıklarıyla değiştirilmiş halidir Full-Text Search için
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed)]
        public string NormalizedName { get; set; }
        public string Email { get; set; }

    }   
}