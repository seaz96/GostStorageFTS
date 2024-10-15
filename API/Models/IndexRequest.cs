using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace API.Models;

public class IndexRequest
{
    public Gost Document { get; set; }
    public string Text { get; set; }
}