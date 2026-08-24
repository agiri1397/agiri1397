namespace SistemaILP.Ruteo.Application.DTOs;

/// <summary>
/// Maps a resource consumed from the sample public REST web service
/// (https://jsonplaceholder.typicode.com). Demonstrates GET/POST over
/// HTTP through the Application -> Infrastructure abstraction.
/// </summary>
public class PostDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;
}
