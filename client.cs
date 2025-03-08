using System.Net.Sockets;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp40
{
    internal class Program
    {
        static async Task Main()
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    await client.ConnectAsync("127.0.0.1", 5000);
                    Console.WriteLine("Подключение к серверу...");

                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string weatherData = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    Console.WriteLine("Данные о погоде получены:");
                    Console.WriteLine(weatherData.Substring(0, weatherData.Length));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}