using TTD_Backend.DTOs;

namespace TTD_Backend.DTOs
{
    public class ProjectResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClientEmail { get; set; }
        public string SlackLink { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal? Completion { get; set; }
        public string ProjectManagerName { get; set; }
        public IEnumerable<TaskResponseDto> Tasks { get; set; }
    }
}
