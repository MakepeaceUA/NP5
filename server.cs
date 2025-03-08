using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ConsoleApp39
{
    internal class Program
    {
        static async Task Main()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 5000);
            server.Start();
            Console.WriteLine("Сервер запущен, ожидание подключения клиента...");

            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                Console.WriteLine("Клиент подключился.");

                try
                {
                    string url = "https://openweathermap.org/city/698740"; 
                    using (HttpClient httpClient = new HttpClient())
                    {
                        string pageContent = await httpClient.GetStringAsync(url);
                        byte[] data = Encoding.UTF8.GetBytes(pageContent);

                        NetworkStream stream = client.GetStream();
                        await stream.WriteAsync(data, 0, data.Length);
                        Console.WriteLine("Данные о погоде отправлены клиенту.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                finally
                {
                    client.Close();
                }
            }
        }
    }
}