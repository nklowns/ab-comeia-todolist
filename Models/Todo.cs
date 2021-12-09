using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ab_comeia_todolist.Models
{
    public class Todo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [DisplayName("Código")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Tagname { get; set; } = null!;

        [DisplayName("Descrição")]
        public string Description { get; set; } = null!;

        [DisplayName("Para Data")]
        public DateTime? UpTo { get; set; }
        
        [DisplayName("Concluído")]
        public bool Done { get; set; }
        
        [DisplayName("Criado em")]
        public DateTime CreatedAt { get; set; }
        
        [DisplayName("Última atualização")]
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
        
        [DisplayName("Usuário")]
        public string User { get; set; } = null!;
    }
}