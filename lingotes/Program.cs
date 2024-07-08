int semanas = 260*2; // Simulación de 2 años (52 semanas * 2)
int stockInicial = 1000; // Stock inicial de lingotes
int stockMinimo = 500; // Stock mínimo de seguridad
double tasaReciclaje = 0.3; // Fracción de productos reciclados
Random random = new Random();

List<int> demanda = new List<int>();
List<int> produccion = new List<int>();
List<int> reciclaje = new List<int>();
List<int> lingotesNecesarios = new List<int>();
List<int> comprasLingotes = new List<int>();
List<int> stock = new List<int> { stockInicial };

// Generar demanda aleatoria (por ejemplo entre 100 y 300 marcos por semana)
for (int t = 0; t < semanas; t++)
{
    demanda.Add(random.Next(100, 301));
}

// Simulación
for (int t = 0; t < semanas; t++)
{
    // Producción igual a la demanda
    produccion.Add(demanda[t]);

    // Reciclaje de productos de hace 5 años (260 semanas)
    if (t >= 260)
    {
        reciclaje.Add((int)(tasaReciclaje * produccion[t - 260]));
    }
    else
    {
        reciclaje.Add(0);
    }

    // Lingotes necesarios
    lingotesNecesarios.Add(produccion[t] - reciclaje[t]);

    // Compra de lingotes
    int compra = Math.Max(0, lingotesNecesarios[t] - stock[stock.Count - 1] + stockMinimo);
    comprasLingotes.Add(compra);

    // Actualización de stock (considerando tiempo de entrega de 2 semanas)
    if (t + 2 < semanas)
    {
        int nuevoStock = stock[stock.Count - 1] - lingotesNecesarios[t] + compra;
        stock.Add(nuevoStock);
    }
}

// Imprimir resultados
 Console.WriteLine($"{ "Semana",8} { "Demanda",8} { "Producción",20} { "Reciclaje",20} { "Lingotes Necesarios",25} { "Compras",8} { "Stock",8}");
for (int t = 0; t < semanas; t++)
{
    int stockActual = t + 2 < stock.Count ? stock[t + 2] : stock[stock.Count - 1];
    Console.WriteLine($"{t + 1,6}\t{demanda[t],8}\t{produccion[t],10}\t{reciclaje[t],12}\t{lingotesNecesarios[t],20}\t{comprasLingotes[t],8}\t{stockActual,6}");
}