namespace IpInfo.Domain.Interfaces
{
    public interface IAuditable
    {
        //Данный интерфейс нужен в основном для работы интерцепторов
        //Энтити реализуют его, а затем в интерцепторе можно использовать данный момент
        public DateTime RequestTime { get; set; } 
    }
}
