﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataGriedViewDB.Classes
{
    [Table("tblDepartment")]
    public class MedizinDepartment
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        public int CabinetNumber { get; set; }
    }
}
