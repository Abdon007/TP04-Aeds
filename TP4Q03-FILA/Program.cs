using System;


class Program
{
    public static void Main(string[] args)
    {
        Jogadores[] time = new Jogadores[30];
        Fila fila = new Fila();
        int n = 0;
        string linha = Console.ReadLine();
        while (linha != "FIM")
        {
            time[n] = new Jogadores();
            time[n].Leitura(linha);
            fila.inserir(time[n]);
            n++;
            linha = Console.ReadLine();
        }
        int num = int.Parse(Console.ReadLine());
        for (int i = 0; i < num; i++)
        {
            string linha2 = Console.ReadLine();
            string[] Formatada = linha2.Split(' ');
            if (Formatada[0] == "I")
            {
                time[n] = new Jogadores();
                time[n].Leitura(linha2);
                fila.inserir(time[n]);
            }
            else if (Formatada[0] == "R")
            {
                fila.remover();
            }
        }
        fila.mostrar();
    }
}

class Celula
{
    public Jogadores elemento; // Elemento inserido na celula.
    public Celula prox; // Aponta a celula prox.

    public Celula()
    {

    }

    public Celula(Jogadores elemento)
    {
        this.elemento = elemento;
        this.prox = null;
    }
}

class Fila : Jogadores
{
    private Celula primeiro;
    private Celula ultimo;

    public Fila()
    {
        primeiro = new Celula();
        ultimo = primeiro;
    }

    public void inserir(Jogadores x)
    {
        ultimo.prox = new Celula(x);
        ultimo = ultimo.prox;
    }

    public Jogadores remover()
    {
        if (primeiro == ultimo)
        {
            throw new Exception("Erro ao remover!");
        }

        Celula tmp = primeiro;
        primeiro = primeiro.prox;
        Jogadores resp = primeiro.elemento;
        tmp.prox = null;
        tmp = null;
        return resp;
    }

    public void mostrar()
    {
        for (Celula i = primeiro.prox; i != null; i = i.prox)
        {
            i.elemento.Imprimir();
        }
    }
}

class Jogadores
{
    string Nome;
    string Foto;
    int Id;
    string Nascimento;
    int[] Times;

    public string GetNome()
    {
        return Nome;
    }
    public string Getfoto()
    {
        return Foto;
    }
    public int GetId()
    {
        return Id;
    }

    public string GetNascimento()
    {
        return Nascimento;
    }

    public static int GetTamanho(string Nome)
    {
        int Total = 0;
        for (int i = 0; i < Nome.Length; i++)
        {
            Total += Convert.ToInt32(Nome[i]);
        }
        return Total;
    }
    public int[] GetTimes()
    {
        return Times;
    }


    public void Leitura(string linha)
    {
        string remove = linha.Replace("[", "");
        string remove1 = remove.Replace("]", "");
        string remove2 = remove1.Replace('"', '@');
        string remove3 = remove2.Replace("@", "");
        linha = remove3;
        int contador = 6;
        string[] Formatada = linha.Split(',');
        Nome = Formatada[1];
        Id = int.Parse(Formatada[5]);
        Foto = Formatada[2];
        Nascimento = Formatada[3];
        if (Formatada.Length <= 7)
        {
            Times = new int[1];
            Times[0] = int.Parse(Formatada[6]);
        }
        else
        {
            Times = new int[Formatada.Length - 6];
            for (int i = 0; i < Times.Length; i++)
            {
                Times[i] = int.Parse(Formatada[contador]);
                contador++;
            }
        }
    }
    public void Imprimir()
    {
        Console.Write(Id + " " + Nome + " " + Nascimento + " " + Foto + " " + "(");
        for (int i = 0; i < Times.Length; i++)
        {
            if (i == Times.Length - 1)
            {
                Console.Write(Times[i]);
                break;
            }
            else
            {
                Console.Write(Times[i] + ", ");
            }
        }
        Console.Write(")");
        Console.WriteLine("");
    }
}