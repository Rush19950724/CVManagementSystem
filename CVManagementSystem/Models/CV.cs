﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVManagementSystem.Models
{
    public class CV
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("CVStatusType")]
        public int StatusID { get; set; }

        [ForeignKey("JobSector")]
        public int SectorID { get; set; }
        public string? Title { get; set; }
        public string? Age { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? PrimaryEducation { get; set; }
        public string? SecondaryEducation { get; set; }
        public string? HigherEducation { get; set; }
        public string? EducationQualification { get; set; }
        public int? ExperienceYears { get; set; }
        public int? GCSEPasses { get; set; }
        public string? ProfessionalQualification1 { get; set; }
        public string? ProfessionalQualification2 { get; set; }
        public string? ProfessionalQualification3 { get; set; }
        public string? Experience1 { get; set; }
        public string? Experience2 { get; set; }
        public string? Experience3 { get; set; }
        public string? Skills { get; set; }
        public bool IsRemote { get; set; } = true;
        public string? Photo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
