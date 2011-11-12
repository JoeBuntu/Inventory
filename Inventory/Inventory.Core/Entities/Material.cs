using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Core.Entities
{
    public class Material : EntityBase
    {
        private string m_PartNumber;
        private string m_Description;

        [Required(ErrorMessage = "Part Number is required", AllowEmptyStrings = false)]
        [StringLength(30, ErrorMessage = "Part Number must not exceed 30 characters")]
        [RegularExpression("(?i)^[A-Z0-9](.+)?", ErrorMessage = "Part Number must start with alpha-numeric character")]
        public virtual string PartNumber
        {
            get { return m_PartNumber; }
            set { m_PartNumber = value.Trim().ToUpper(); }
        }

        [StringLength(50, ErrorMessage = "Description must not exceed 50 characters")]
        public virtual string Description
        {
            get { return m_Description; }
            set { m_Description = value.Trim().ToUpper(); }
        }
        
        public virtual MaterialType Type { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Pieces/Case must be greater than zero")]
        public virtual int PiecesPerCase { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Eaches/Piece must be greater than zero")]
        public virtual int EachesPerPiece { get; set; }
    }
}
