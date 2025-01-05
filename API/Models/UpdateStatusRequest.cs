using Core.Entities;

namespace API.Models;

public record UpdateStatusRequest(int Id, DocumentStatus Status);