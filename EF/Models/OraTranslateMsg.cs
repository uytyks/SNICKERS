using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SNICKERS.EF.Models
{
    [Table("ORA_TRANSLATE_MSG")]
    public partial class OraTranslateMsg
    {
        [Key]
        [Column("ORA_TRANSLATE_MSG_ID")]
        [StringLength(38)]
        [Unicode(false)]
        public string OraTranslateMsgId { get; set; } = null!;
        [Column("ORA_CONSTRAINT_NAME")]
        [StringLength(128)]
        [Unicode(false)]
        public string OraConstraintName { get; set; } = null!;
        [Column("ORA_ERROR_MESSAGE")]
        [StringLength(200)]
        [Unicode(false)]
        public string OraErrorMessage { get; set; } = null!;
        [Column("CREATED_BY")]
        [StringLength(38)]
        [Unicode(false)]
        public string CreatedBy { get; set; } = null!;
        [Column("CREATED_DATE", TypeName = "DATE")]
        public DateTime CreatedDate { get; set; }
        [Column("MODIFIED_BY")]
        [StringLength(38)]
        [Unicode(false)]
        public string ModifiedBy { get; set; } = null!;
        [Column("MODIFIED_DATE", TypeName = "DATE")]
        public DateTime ModifiedDate { get; set; }
    }
}
