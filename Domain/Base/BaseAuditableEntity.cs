﻿using System;

namespace Domain.Entities.Base;

public class BaseAuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public string CreatedBy { get; set; }
}