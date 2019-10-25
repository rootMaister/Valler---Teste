using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public partial class Reserva
    {
        [Key]
        [Column("id_reserva")]
        public int IdReserva { get; set; }
        [Column("id_oferta")]
        public int? IdOferta { get; set; }
        [Column("id_usuario")]
        public int? IdUsuario { get; set; }
        [Required]
        [Column("quantidade_reserva")]
        [StringLength(255)]
        public string QuantidadeReserva { get; set; }
        [Required]
        [Column("cronometro")]
        [StringLength(255)]
        public string Cronometro { get; set; }
        [Required]
        [Column("status_reserva")]
        [StringLength(255)]
        public string StatusReserva { get; set; }

        [ForeignKey(nameof(IdOferta))]
        [InverseProperty(nameof(Oferta.Reserva))]
        public virtual Oferta IdOfertaNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuario.Reserva))]
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
