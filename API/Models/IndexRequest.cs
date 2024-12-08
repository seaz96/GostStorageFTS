using Core.Entities;

namespace API.Models;

public record IndexRequest(Gost? Document, string? Text);