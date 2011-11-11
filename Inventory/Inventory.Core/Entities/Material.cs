using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Core.Entities
{
    public class Material : EntityBase
    {
        [Required(ErrorMessage = "Part Number is required", AllowEmptyStrings = false)]
        [StringLength(30, ErrorMessage = "Part Number must not exceed 30 characters")]
        public virtual string PartNumber { get; set; }

        [StringLength(50, ErrorMessage = "Description must not exceed 50 characters")]
        public virtual string Description { get; set; }

        public virtual MaterialType Type { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Pieces/Case must be greater than zero")]
        public virtual int PiecesPerCase { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Eaches/Case must be greater than zero")]
        public virtual int EachesPerPiece { get; set; }
    }
}
