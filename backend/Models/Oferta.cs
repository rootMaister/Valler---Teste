using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public partial class Oferta
    {
        public Oferta()
        {
            Reserva = new HashSet<Reserva>();
        }

        [Key]
        [Column("id_oferta")]
        public int IdOferta { get; set; }
        [Column("id_produto")]
        public int? IdProduto { get; set; }
        [Required]
        [Column("titulo")]
        [StringLength(255)]
        public string Titulo { get; set; }
        [Required]
        [Column("data_oferta")]
        [StringLength(255)]
        public string DataOferta { get; set; }
        [Required]
        [Column("data_vencimento")]
        [StringLength(255)]
        public string DataVencimento { get; set; }
        [Required]
        [Column("preco")]
        [StringLength(255)]
        public string Preco { get; set; }
        [Required]
        [Column("quantidade")]
        [StringLength(255)]
        public string Quantidade { get; set; }
        [Required]
        [Column("imagem")]
        [StringLength(255)]
        public string Imagem { get; set; }

        [ForeignKey(nameof(IdProduto))]
        [InverseProperty(nameof(Produto.Oferta))]
        public virtual Produto IdProdutoNavigation { get; set; }
        [InverseProperty("IdOfertaNavigation")]
        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
