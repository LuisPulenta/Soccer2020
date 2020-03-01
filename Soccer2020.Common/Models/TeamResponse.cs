namespace Soccer2020.Common.Models
{
    public class TeamResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoPath { get; set; }

        public string LogoFullPath => string.IsNullOrEmpty(LogoPath)
           ? "noimage"//null
           : $"http://keypress.serveftp.net:88/Soccer2020Api{LogoPath.Substring(1)}";
    }
}