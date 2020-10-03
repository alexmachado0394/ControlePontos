namespace ControlePontos.Classes
{
    class Jogo
    {
        public int Id { get; set; }
        public int Placar { get; set; }
        public int PiorResultado { get; set; }
        public int MelhorResultado { get; set; }
        public int MudouPior { get; set; }
        public int MudouMelhor { get; set; }

        public Jogo()
        {

        }

        public Jogo(int id, int placar)
        {
            Id = id;
            Placar = placar;
        }

        public void ResultadoRuim(int anterior)
        {
            if (anterior != 0)
            {
                if (anterior < Placar)
                {
                    PiorResultado = anterior;
                }
                else
                {
                    PiorResultado = Placar;
                }
            }
            else PiorResultado = Placar;
        }

        public void ResultadoBom(int anterior)
        {
            if (anterior != 0)
            {
                if (anterior > Placar)
                {
                    MelhorResultado = anterior;
                }
                else
                {
                    MelhorResultado = Placar;
                }
            }
            else MelhorResultado = Placar;

        }

        public void MudouResultadoRuim(int anterior, int mudou)
        {
            if (Id > 1)
            {
                if (anterior > Placar)
                {
                    MudouPior = mudou + 1;
                }
                else MudouPior = mudou;
            }
        }

        public void MudouResultadoBom(int anterior, int mudou)
        {
            if (Id > 1)
            {
                if (anterior < Placar)
                {
                    MudouMelhor = mudou + 1;
                }
                else MudouMelhor = mudou;
            }
        }
    }
}
