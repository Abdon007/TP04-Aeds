using System;


class Program
{
    public static void Main(string[] args)
    {
        Jogadores[] time = new Jogadores[30];
        Lista list = new Lista();
        int n = 0;
        string linha = Console.ReadLine();
        while (linha != "FIM")
        {
            time[n] = new Jogadores();
            time[n].Leitura(linha);
            list.InserirFim(time[n]);
            n++;
            linha = Console.ReadLine();
        }
        int num = int.Parse(Console.ReadLine());
        for (int i = 0; i < num; i++)
        {
            string linha2 = Console.ReadLine();
            string[] Formatada = linha2.Split(' ');
            if (Formatada[0] == "II")
            {
                time[n] = new Jogadores();
                time[n].Leitura(linha2);
                list.inserirInicio(time[n]);
            }
            else if (Formatada[0] == "I*")
            {
                time[n] = new Jogadores();
                time[n].Leitura(linha2);
                int posição = int.Parse(Formatada[1]);
                list.Inserir(time[n], posição);
            }
            else if (Formatada[0] == "R*")
            {
                int posição = int.Parse(Formatada[1]);
                list.remover(posição);
            }
            else if (Formatada[0] == "IF")
            {
                time[n] = new Jogadores();
                time[n].Leitura(linha2);
                list.InserirFim(time[n]);
            }
            else if (Formatada[0] == "RF")
            {
                list.removerFim();
            }
            else if (Formatada[0] == "RI")
            {
                list.removerInicio();
            }
        }
        list.ImprimiList();
    }
}

class Celula {
	public Jogadores elemento; // Elemento inserido na celula.
	public Celula prox; // Aponta a celula prox.
    
	public Celula() {
		
	}

	public Celula(Jogadores elemento) {
      this.elemento = elemento;
      this.prox = null;
	}
}

class Lista:Jogadores
{
    private Celula primeiro;
    private Celula ultimo;

    public Lista()
    {
        primeiro = new Celula(null);
        ultimo = primeiro;
    }

    public void InserirFim(Jogadores x)
    {
        ultimo.prox = new Celula(x);
        ultimo = ultimo.prox;
    }


    public void inserirInicio(Jogadores x)
    {
        Celula tmp = new Celula(x);
        tmp.prox = primeiro.prox;
        primeiro.prox = tmp;
        if (primeiro == ultimo)
        {
            ultimo = tmp;
        }
        tmp = null;
    }
    public void Inserir(Jogadores x, int pos)
    {
        int tamanho = Tamanho();

        if (pos < 0 || pos > tamanho)
        {
            throw new Exception("Erro ao inserir posição (" + pos + " / Tamanho = " + tamanho + ") inválida!");
        }
        else if (pos == tamanho)
        {
            InserirFim(x);
        }
        else
        {
            Celula i = primeiro;
            for (int j = 0; j < pos; j++)
            {
                i = i.prox;
            }

            Celula tmp = new Celula(x);
            tmp.prox = i.prox;
            i.prox = tmp;
        }
    }

    public Jogadores remover(int pos)
    {
        Jogadores resp;
        int tamanho = Tamanho();

        if (primeiro == ultimo)
        {
            throw new Exception("Erro ao remover (vazia)!");

        }
        else if (pos < 0 || pos >= tamanho)
        {
            throw new Exception("Erro ao remover (posicao " + pos + " / " + tamanho + " invalida!");
        }
        else if (pos == 0)
        {
            resp = removerInicio();
        }
        else if (pos == tamanho - 1)
        {
            resp = removerFim();
        }
        else
        {
            // Caminhar ate a posicao anterior a insercao
            Celula i = primeiro;
            for (int j = 0; j < pos; j++, i = i.prox) ;

            Celula tmp = i.prox;
            resp = tmp.elemento;
            i.prox = tmp.prox;
            tmp.prox = null;
            i = tmp = null;
        }

        return resp;
    }

    public Jogadores removerInicio()
    {
        if (primeiro == ultimo)
        {
            throw new Exception("Erro ao remover (vazia)!");
        }

        Celula tmp = primeiro;
        primeiro = primeiro.prox;
        Jogadores resp = primeiro.elemento;
        tmp.prox = null;
        tmp = null;
        return resp;
    }


    public Jogadores removerFim()
    {
        if (primeiro == ultimo)
        {
            throw new Exception("Erro ao remover (vazia)!");
        }

        // Caminhar ate a penultima celula:
        Celula i;
        for (i = primeiro; i.prox != ultimo; i = i.prox) ;

        Jogadores resp = ultimo.elemento;
        ultimo = i;
        i = ultimo.prox = null;

        return resp;
    }



    public void ImprimiList(){
        for (Celula i = primeiro.prox; i != null; i = i.prox)
        {
            i.elemento.Imprimir();
        }
    }

    public int Tamanho()
    {
        int tamanho = 0;
        Celula i = primeiro.prox;

        while (i != null)
        {
            tamanho++;
            i = i.prox;
        }

        return tamanho;
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
    public string Getfoto(){
        return Foto;
    }
    public int GetId(){
        return Id;
    }

    public string GetNascimento(){
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
    public int[] GetTimes(){
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