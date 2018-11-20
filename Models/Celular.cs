using System.ComponentModel.DataAnnotations;

namespace CrudComAngularJsWebApi.Models
{
    public class Celular
    {
        
        public virtual int Id { get; set; }

        public virtual string Marca { get; set; }

        public virtual string Modelo { get; set; }

        public virtual string Cor { get; set; }

        public virtual string TipoChip { get; set; }

        public virtual string MemoriaInterna { get; set; }

    }
}