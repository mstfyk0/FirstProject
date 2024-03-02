﻿
namespace Domain.Common
{
    //general properties for any entity
    public abstract record AuditableEntity
    {
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set;}
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }


    }
}