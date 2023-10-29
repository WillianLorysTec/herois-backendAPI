using Interfaces.Repositorio;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entidades;
using Models.ViewModel;

namespace Repositorio
{
    public class FluxoHeroiRepositorio : IFluxoHeroiRepositorio
    {
        private readonly AcessoDados _dados;

        public FluxoHeroiRepositorio(AcessoDados dados)
        {
            _dados = dados;
        }
        public Heroi CriarHeroi(HeroiDTO heroi)
        {

            using (var transaction = _dados.Database.BeginTransaction())
            {
                try
                {
                    Heroi heroi_entidade = new()
                    {
                        Id = null,
                        Nome = heroi.Nome,
                        NomeHeroi = heroi.NomeHeroi,
                        Peso = heroi.Peso,
                        Altura = heroi.Altura

                    };

                    _dados.Herois.Add(heroi_entidade);
                    _dados.SaveChanges();

                    transaction.Commit();

                    // não tenho tanto conhecimento em entity, acabei por buscar o registro inserido por nome

                    Heroi? heroiInserido = _dados.Herois.FirstOrDefault(h => h.NomeHeroi == heroi.NomeHeroi);

                    if (heroiInserido != null)
                    {
                        InserirPoderes(heroiInserido, heroi.PoderesAdicionados);

                        return heroiInserido;

                    }
                    else
                    {
                        throw new Exception("Registro não encontrado após a inserção.");
                    }


                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao criar heroi" + ex.Message);
                }
            }

        }



        private void InserirPoderes(Heroi heroiInserido, List<int> poderes)
        {
            using (var transaction = _dados.Database.BeginTransaction())
            {

                try
                {
                    var poderesFiltrados = _dados.Poder
                       .Where(p => poderes.Contains(p.Id))
                       .ToList();

                    heroiInserido.SuperPoderes.AddRange(poderesFiltrados);

                    _dados.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao criar heroi" + ex.Message);
                }
            }
        }
        public bool ExcluirHeroi(int idHeroi)
        {
            using (var transaction = _dados.Database.BeginTransaction())
            {
                try
                {
                    Heroi? heroi = _dados.Herois.FirstOrDefault(h => h.Id == idHeroi);

                    if (heroi != null)
                    {
                        _dados.Herois.Remove(heroi);
                        _dados.SaveChanges();

                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;  // com o fluent result tratar **não encontramos nenhum heroi
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao excluir herói", ex);
                }
            }
        }

        public HeroiViewModel ListarHeroiPorID(int idHeroi)
        {
            try
            {
                HeroiViewModel? heroisAbaAtualizacao = _dados.Herois
                    .AsNoTracking()
                    .Where(id => id.Id == idHeroi)
                    .Select(heroi => new HeroiViewModel
                    {
                        Id = heroi.Id,
                        NomeHeroi = heroi.Nome,
                        SuperPoderes = heroi.SuperPoderes.Select(sp => new SuperpoderViewModel
                        {
                            Id = sp.Id,
                            Nome = sp.Superpoder
                        })
                    })
                    .FirstOrDefault();

                return heroisAbaAtualizacao ?? new HeroiViewModel();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar heróis", ex);
            }

        }

        public List<Heroi> ListarHerois()
        {
            try
            {
                return _dados.Herois
                .AsNoTracking()
                .ToList();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar heróis", ex);
            }
        }

        public List<Superpoderes> ListarPoderes()
        {
            try
            {
                List<Superpoderes>? poderes = _dados.Poder.AsNoTracking().ToList();

                return poderes;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar poderes", ex);
            }
        }
    }
}
