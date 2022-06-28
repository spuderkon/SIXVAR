using System.Linq;
using System.IO;
using System.Diagnostics;

StreamWriter sw1 = new StreamWriter("list_one.txt");
StreamWriter sw2 = new StreamWriter("list_two.txt");
string[] countries = new string[10]
{
    "Россия",
    "Чехия",
    "США",
    "Казахстан",
    "Украина",
    "Польша",
    "Франция",
    "Канада",
    "Великобритания",
    "Китай"
};
string[] characters = new string[10]
{
    "Три_царя 3 3000",
    "Fernando 3 5000",
    "Pendosia 1 50000",
    "Рахатлукум 300 500",
    "HahaLand 2 9000",
    "HohLandDva 4 7000",
    "Frogtel 1 10000",
    "Snowland 4 5000",
    "Breakfest 1 99000",
    "LittleEye 7000 10"
};
for (int i = 0; i < countries.Length; i++)
{
    sw1.Write(i + " ");
    sw1.WriteLine(countries[i]);
    sw2.Write(i + " ");
    sw2.WriteLine(characters[i]);
}
sw1.Close();
sw2.Close();
StreamReader sr1 = new StreamReader("list_one.txt");
StreamReader sr2 = new StreamReader("list_two.txt");
List<Countries> from_sr1 = new List<Countries>();
List<Hotels> from_sr2 = new List<Hotels>();
List<Connection> connection = new List<Connection>();
string[] temp = new string[6];
while (!sr1.EndOfStream)
{
    temp = sr1.ReadLine().Split(" ");
    from_sr1.Add(new Countries { id = temp[0], name = temp[1] });
}
sr1.Close();
while (!sr2.EndOfStream)
{
    temp = sr2.ReadLine().Split(" ");
    from_sr2.Add(new Hotels { id = temp[0], name = temp[1], count = temp[2], cost = temp[3] });
}
sr2.Close();
int sort = 0;
NewConnection();
OutputList("id");
WriteInfo();
void AddCountry()
{
    Console.Clear();
    Console.WriteLine("Добавление страны");
    string temp = String.Empty;
    StreamWriter streamW = new StreamWriter("list_one.txt", true);
    Console.WriteLine("Введите название страны:");
    temp = Console.ReadLine();
    streamW.Write($"{int.Parse(from_sr1.Last().id) + 1} {temp}");
    from_sr1.Add(new Countries { id = (int.Parse(from_sr1.Last().id) + 1).ToString(), name = temp });
    streamW.Close();
    Console.Clear();
    NewConnection();
    OutputList("id");
    WriteInfo();
}
void AddHotel()
{
    Console.Clear();
    Console.WriteLine("Добавление отеля");
    string id = String.Empty;
    string name = String.Empty;
    string count = String.Empty;
    string cost = String.Empty;
    StreamWriter streamW = new StreamWriter("list_two.txt", true);
    do
    {
        foreach (var item in from_sr1)
        {
            Console.WriteLine($"{item.id} - {item.name}");
        }
        Console.WriteLine("\nНапишите идентификатор страны:");
        id = Console.ReadLine();
    }
    while ("01234567890".IndexOf(id) < 0 || int.Parse(id) > from_sr1.Count || int.Parse(id) < 0);
    Console.WriteLine("Введите название отеля:");
    name = Console.ReadLine();
    do
    {

        Console.WriteLine("Введите количество человек:");
        count = Console.ReadLine();
    }
    while ("01234567890".IndexOf(count) < 0);
    do
    {
        Console.WriteLine("Введите цену:");
        cost = Console.ReadLine();
    }
    while (int.TryParse(cost, out int z) == false);
    streamW.WriteLine($"{id} {name} {count} {cost}");
    from_sr2.Add(new Hotels { id = id, name = name, count = count, cost = cost });
    streamW.Close();
    Console.Clear();
    NewConnection();
    OutputList("id");
    WriteInfo();
}
void WriteInfo()
{
    Console.WriteLine(
        "\n//////////////////////////////////////////////\n" +
        "                   Управление:" +
        "\n     Чтобы вывести список, нажмите 'O'" +
        "\n     Чтобы добавить страну нажмите 'C'" +
        "\n     Чтобы добавить отель нажмите 'H'" +
        "\n     Чтобы выйти из программы нажмите 'Escape'" +
        "\n     Сортировка по ID 'I'" +
        "\n     Сортировка по Названию страны 'Z'" +
        "\n     Сортировка по Названию отеля 'X'" +
        "\n     Сортировка по Количеству 'K'" +
        "\n     Сортировка по Цене 'P'" +
        "\n//////////////////////////////////////////////\n"
        );
    var temp = Console.ReadKey().Key;
    if (temp == ConsoleKey.Escape)
        Process.GetCurrentProcess().Kill();
    else if (temp == ConsoleKey.O)
        OutputList("id");
    else if (temp == ConsoleKey.C)
        AddCountry();
    else if (temp == ConsoleKey.H)
        AddHotel();
    else if (temp == ConsoleKey.H)
        AddHotel();
    else if (temp == ConsoleKey.I)
        OutputList("id");
    else if (temp == ConsoleKey.Z)
        OutputList("country");
    else if (temp == ConsoleKey.X)
        OutputList("hotel");
    else if (temp == ConsoleKey.K)
        OutputList("count");
    else if (temp == ConsoleKey.P)
        OutputList("cost");
}
void OutputList(string param)
{
    List<Connection> sort_connections = new List<Connection>();
    Console.Clear();
    Console.WriteLine("ID Название отеля Страна Количество Цена");
    if (param == "id")
    {
        if (sort % 2 == 0)
            sort_connections = connection.OrderBy(x => int.Parse(x.id)).ToList();
        else
            sort_connections = connection.OrderByDescending(x => int.Parse(x.id)).ToList();
        sort++;
    }
    else if (param == "country")
    {
        if (sort % 2 == 0)
            sort_connections = connection.OrderBy(x => x.country_name).ToList();
        else
            sort_connections = connection.OrderByDescending(x => x.country_name).ToList();
        sort++;
    }
    else if (param == "hotel")
    {
        if (sort % 2 == 0)
            sort_connections = connection.OrderBy(x => x.hotel_name).ToList();
        else
            sort_connections = connection.OrderByDescending(x => x.hotel_name).ToList();
        sort++;
    }
    else if (param == "count")
    {
        if (sort % 2 == 0)
            sort_connections = connection.OrderBy(x => int.Parse(x.count)).ToList();
        else
            sort_connections = connection.OrderByDescending(x => int.Parse(x.count)).ToList();
        sort++;
    }
    else if (param == "cost")
    {
        if (sort % 2 == 0)
            sort_connections = connection.OrderBy(x => int.Parse(x.cost)).ToList();
        else
            sort_connections = connection.OrderByDescending(x => int.Parse(x.cost)).ToList();
        sort++;
    }
    foreach (var item in sort_connections)
    {
        Console.WriteLine($"{item.id} {item.hotel_name} {item.country_name} {item.count} {item.count}*{item.cost}");
    }
    WriteInfo();
}
void NewConnection()
{
    foreach (var item in from_sr1)
    {
        foreach (var item2 in from_sr2)
        {
            if (!connection.Select(x => x.hotel_name).Contains(item2.name))
            {
                if (item.id == item2.id)
                {
                    connection.Add(new Connection { id = item.id, country_name = item.name, hotel_name = item2.name, cost = item2.cost, count = item2.count });
                }
            }
        }
    }
}

public class Countries
{
    public string id;
    public string name;
}
class Hotels
{
    public string id;
    public string name;
    public string count;
    public string cost;
}
class Connection
{
    public string id;
    public string hotel_name;
    public string country_name;
    public string count;
    public string cost;
}